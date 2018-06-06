using System.Text.RegularExpressions;
using EleWise.ELMA.Common.Managers;
using EleWise.ELMA.ComponentModel;
using EleWise.ELMA.Documents.Managers;
using EleWise.ELMA.Extensions;
using EleWise.ELMA.Files;
using EleWise.ELMA.MailSniffer.Models;
using EleWise.ELMA.Messages.Models;
using EleWise.ELMA.Model.Managers;
using EleWise.ELMA.Model.Services;
using EleWise.ELMA.Security;
using EleWise.ELMA.Security.Managers;
using EleWise.ELMA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using EleWise.ELMA.Runtime.Managers;
using EleWise.ELMA.Common.Models;

namespace EleWise.ELMA.MailSniffer.Services
{
    [Service]
    public class IncidentService : IIncidentService
    {
        public UserManager UserManager { get; set; }

        public AttachmentManager AttachmentManager { get; set; }

        private const string AttachmentContentName = "Content-Disposition: form-data; name=\"attachment\";";

        public void CreateMessage(long incidentId)
        {
            var incident = EntityManager<IIncident>.Instance.LoadOrNull(incidentId);
            if (incident != null)
            {
                var message = InterfaceActivator.Create<IChannelMessage>();

                // Получение списка получателей из настроек модуля
                var settings = Locator.GetService<MailSnifferSettingsModule>().Settings;
                message.Recipients.AddAll(settings.NotifyUsers);

                message.Subject = SR.T("Зафиксирован инцидент со статусом {0}", incident.Status);
                message.MessageType = ChannelMessageType.Post;
                message.CreationAuthor = UserManager.Load(SecurityConstants.SystemUserUid);
                message.CreationDate = DateTime.Now;
                message.FullMessage = SR.T("{0} {1} был зафиксирован инцидент со статусом {2}. Ознакомьтесь с содержимым во вложении."
                    , incident.CreationDate.Value.ToShortDateString()
                    , incident.CreationDate.Value.ToShortTimeString()
                    , incident.Status);
                //message.Attachments.Add(AttachmentManager.Instance.Create(incident.ThreadFile));

                // Создание статуса сообщения (новое или прочитанное)
                foreach (var recipient in message.Recipients)
                {
                    var status = InterfaceActivator.Create<IRecipientMessageStatus>();
                    status.Message = message;
                    status.Recipient = recipient;
                    status.Status = MessageStatus.New;
                    status.Save();
                    message.Statuses.Add(status);
                }
                message.Save();
            }
        }

        public ICollection<string> CheckFileOnWarningFilter(BinaryFile file)
        {
            var settings = Locator.GetService<MailSnifferSettingsModule>().Settings;
            var list = CheckFileOnFilter(file, settings.FilterList);
            return list;
        }

        public ICollection<string> CheckFileOnStopFilter(BinaryFile file)
        {
            var settings = Locator.GetService<MailSnifferSettingsModule>().Settings;
            var list = CheckFileOnFilter(file, settings.BlockFilterList);
            return list;
        }

        private ICollection<string> CheckFileOnFilter(BinaryFile file, string filterString)
        {
            var filterList = !string.IsNullOrWhiteSpace(filterString) ?
                    filterString.Trim().Split('\n').ToList() :
                    new List<string>();

            var list = new List<string>();
            var content = WebDocumentManager.Instance.GetContentFromFile(file);
            if (!content.IsNullOrWhiteSpace() && filterList.Count > 0)
            {
                list.AddRange(
                    from filter in filterList let amount = new Regex(filter.ToLower()).Matches(content.ToLower()).Count 
                    where amount > 0 
                    select SR.T("\"{0}\" : {1} раз;", filter, amount));
            }
            return list;
        }

        public ICollection<BinaryFile> GetAttachments(BinaryFile file)
        {
            var files = new List<BinaryFile>();

            var content = WebDocumentManager.Instance.GetContentFromFile(file);
            if (content.Contains(AttachmentContentName))
            {
                content = content.Remove(0, content.IndexOf(AttachmentContentName));
                var index = content.IndexOf("\r\n\r\n");
                var contentInfo = content.Substring(0, index);
                var match = new Regex("filename=\"(?<encodedFileName>[^\"]+)\"").Match(contentInfo);
                var fileName = "";
                if (match.Groups["encodedFileName"].Success)
                {
                    var encodedFileName = match.Groups["encodedFileName"].Value;
                    fileName = HttpUtility.UrlDecode(encodedFileName);
                }
                else
                {
                    fileName = string.Format("MailAttachment_{0}_{1}.txt", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                }
                
                content = content.Remove(0, index + 4);
                index = content.IndexOf("------WebKitFormBoundary");
                var attachmentContent = content.Remove(index);

                var fullFileName = @"C:\TEMP\" + fileName;
                var fileInfo = new FileInfo(fullFileName);
                if (!fileInfo.Exists)
                    fileInfo.Create().Dispose();

                using (var fileStream = fileInfo.OpenWrite())
                {
                    var encoding = Encoding.GetEncoding(1251);
                    fileStream.Write(encoding.GetBytes(attachmentContent), 0, attachmentContent.Length);
                }
                var attachmentFile = InterfaceActivator.Create<BinaryFile>();
                attachmentFile.Name = fileName;
                attachmentFile.CreateDate = DateTime.Now;
                attachmentFile.InitializeContentFilePath();
                File.Copy(fullFileName, attachmentFile.ContentFilePath);
                Locator.GetServiceNotNull<IFileManager>().SaveFile(attachmentFile);
                files.Add(attachmentFile);
            }

            return files;
        }

        public void GenerateAttachmentsFromFile(long incidentId)
        {
            var incident = EntityManager<IIncident>.Instance.LoadOrNull(incidentId);
            if(incident != null)
            {
                GenerateAttachmentsFromFile(incident);
            }
        }

        public void GenerateAttachmentsFromFile(IIncident incident)
        {
            var files = new List<BinaryFile>();

            if(incident != null && incident.AttachmentList.Any())
            {
                foreach(var attachment in incident.AttachmentList)
                {
                    var fileList = GetAttachments(attachment.File);
                    if (fileList.Any())
                    {
                        files.AddRange(fileList);
                    }
                }
            }

            foreach(var file in files)
            {
                var attachment = InterfaceActivator.Create<IAttachment>();
                attachment.File = file;
                attachment.CreationDate = DateTime.Now;
                attachment.CreationAuthor = UserManager.Instance.GetCurrentUser();
                attachment.Save();

                incident.MailAttachments.Add(attachment);
            }

            incident.Save();
        }
    }
}
