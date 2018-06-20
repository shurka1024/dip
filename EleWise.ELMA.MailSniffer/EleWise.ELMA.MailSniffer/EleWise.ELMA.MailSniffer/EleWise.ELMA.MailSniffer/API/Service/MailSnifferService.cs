using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using EleWise.ELMA.ComponentModel;
using EleWise.ELMA.Extensions;
using EleWise.ELMA.Model.Attributes;
using EleWise.ELMA.Services;
using EleWise.ELMA.Services.Public;
using EleWise.ELMA.Web.Service;
using EleWise.ELMA.MailSniffer.API.Models;
using EleWise.ELMA.Runtime.Managers;
using EleWise.ELMA.Model.Managers;
using EleWise.ELMA.MailSniffer.Models;
using EleWise.ELMA.Model.Services;
using EleWise.ELMA.Security.Managers;
using EleWise.ELMA.MailSniffer.Services;
using EleWise.ELMA.MailSniffer.Managers;
using EleWise.ELMA.Extensions;
using EleWise.ELMA.Common.Models;
using EleWise.ELMA.Model.Types;

namespace EleWise.ELMA.MailSniffer.API.Service
{
    [ServiceContract(Namespace = APIRouteProvider.ApiServiceNamespaceRoot)]
    [Description("Сервис для работы с MailSniffer")]
    [WsdlDocumentation("Сервис для работы с MailSniffer")]
    public interface IMailSnifferService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetSettings")]
        [AuthorizeOperationBehavior]
        [FaultContract(typeof(PublicServiceException))]
        [Description("Загрузить настройки MailSniffer")]
        [WsdlDocumentation("Загрузить настройки MailSniffer")]
        SettingsResponse GetSettings();

        [OperationContract]
        //[WebGet(UriTemplate = "/CreateIncident?url={url}")]
        [WebInvoke(Method = "POST", UriTemplate = "/CreateIncident?guidFile={guidFile}&streamIsBlocked={streamIsBlocked}&userIp={userIp}&fileName={fileName}")]
        [AuthorizeOperationBehavior]
        [FaultContract(typeof(PublicServiceException))]
        [Description("Создать инцидент")]
        [WsdlDocumentation("Создать инцидент")]
        long CreateIncident(Guid guidFile, bool streamIsBlocked, string userIp, string fileName);

        [OperationContract]
        [WebGet(UriTemplate = "/CheckLoadedFile?guidFile={guidFile}")]
        [AuthorizeOperationBehavior]
        [FaultContract(typeof(PublicServiceException))]
        [Description("Проверить файл")]
        [WsdlDocumentation("Проверить файл")]
        CheckStatus CheckLoadedFile(Guid guidFile);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, MaxItemsInObjectGraph = int.MaxValue)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceKnownType("GetEntityKnownTypes", typeof(ServiceKnownTypeHelper))]
    [Component]
    [Uid(GuidS)]
    public class MailSnifferService : IMailSnifferService, IPublicAPIWebService
    {
        public const string GuidS = "2D9D027E-6A28-4071-B3BB-E4B1F54F0BA3";
        public SettingsResponse GetSettings()
        {
            var settings = Locator.GetService<MailSnifferSettingsModule>().Settings;
            var manager = Locator.GetService<UserManagerExt>();

            var responseSettings = new SettingsResponse
            {
                FilterString = settings.FilterList,
                BlockFilterString = settings.BlockFilterList,
                IpExceptionUsers = manager.GetUsersIpAddresses(settings.ExceptionUsers).ToList(),
                MonitorMailsWithAttachment = settings.MonitorMailsWithAttachment
            };
            if (settings.MonitorEmployeesOnProbation)
            {
                responseSettings.IpUsersOnProbation = 
                    manager.GetUsersIpAddresses(settings.EmployeesOnProbation).ToList();
            }

            if (settings.MonitorEmployeesOnDismissal)
            {
                responseSettings.IpUsersOnDismissal =
                    manager.GetUsersIpAddresses(settings.EmployeesOnDismissal).ToList();
            }

            return responseSettings;
        }

        public long CreateIncident(Guid guidFile, bool streamIsBlocked, string userIp, string fileName)
        {            
            var incident = IncidentManager.Instance.FindNearOrCreateIncident(userIp);

            if(incident.Id == 0)
            {
                incident.Save();
                var user = UserManager.Instance.Find(u => (u as IUserExt) != null && (u as IUserExt).IPAdress == userIp).FirstOrDefault();
                incident.IPAdress = userIp;
                incident.User = user;
                incident.CreationDate = DateTime.Now;
                incident.Name = SR.T("Инцидент от {0} {1}", incident.CreationDate.Value.ToShortDateString(), incident.CreationDate.Value.ToLongTimeString());
            }

            incident.LastIncidentDate = DateTime.Now;
            
            var file = BinaryFileDescriptor.Download(guidFile);

            var attachment = InterfaceActivator.Create<IAttachment>();
            attachment.File = file;
            attachment.CreationDate = DateTime.Now;
            attachment.CreationAuthor = UserManager.Instance.GetCurrentUser();

            incident.AttachmentList.Add(attachment);

            var service = Locator.GetService<IncidentService>();
            var descriptionStringBuilder = new StringBuilder();
            descriptionStringBuilder.AppendLine(SR.T("Проверка почтового потока:"));

            descriptionStringBuilder.AppendLine(SR.T("Совпадения по предупреждающему фильтру:"));
            var warningResult = service.CheckFileOnWarningFilter(file);
            descriptionStringBuilder.AppendLine(warningResult.Any() ? string.Join(Environment.NewLine, warningResult.Select(w => " - " + w)) : SR.T("Не найдено"));

            descriptionStringBuilder.AppendLine(SR.T("Совпадения по стоп фильтру:"));
            var stopResult = service.CheckFileOnStopFilter(file);
            descriptionStringBuilder.AppendLine(stopResult.Any() ? string.Join(Environment.NewLine, stopResult.Select(w => " - " + w)) : SR.T("Не найдено"));

            bool mailAttachmentIsWarning = false, mailAttachmentIsBlocked = false;
            var mailAttachments = service.GetAttachments(file);
            foreach (var mailAttachmentFile in mailAttachments)
            {
                var mailAttachment = InterfaceActivator.Create<IAttachment>();
                mailAttachment.File = mailAttachmentFile;
                mailAttachment.CreationDate = DateTime.Now;
                mailAttachment.CreationAuthor = UserManager.Instance.GetCurrentUser();
                mailAttachment.Save();

                incident.MailAttachments.Add(mailAttachment);
                descriptionStringBuilder.AppendLine(SR.T("Проверка вложений:"));

                descriptionStringBuilder.AppendLine(SR.T("Совпадения по предупреждающему фильтру:"));
                var mailAttachmentWarningResult = service.CheckFileOnWarningFilter(mailAttachmentFile);
                descriptionStringBuilder.AppendLine(mailAttachmentWarningResult.Any() ? string.Join(Environment.NewLine, mailAttachmentWarningResult.Select(w => " - " + w)) : SR.T("Не найдено"));
                mailAttachmentIsWarning |= mailAttachmentWarningResult.Any();

                descriptionStringBuilder.AppendLine(SR.T("Совпадения по стоп фильтру:"));
                var mailAttachmentStopResult = service.CheckFileOnStopFilter(mailAttachmentFile);
                descriptionStringBuilder.AppendLine(mailAttachmentStopResult.Any() ? string.Join(Environment.NewLine, mailAttachmentStopResult.Select(w => " - " + w)) : SR.T("Не найдено"));
                mailAttachmentIsBlocked |= mailAttachmentStopResult.Any();
            }

            if (incident.Status != SniffState.Stop)
            {
                if (stopResult.Any() || mailAttachmentIsBlocked)
                {
                    incident.Status = SniffState.Stop;
                }
                else
                {
                    if(incident.Status != SniffState.Warning)
                    {
                        incident.Status = (warningResult.Any() || mailAttachmentIsWarning) ? SniffState.Warning : SniffState.Ok;                        
                    }
                    incident.Status = streamIsBlocked ? SniffState.Stop : SniffState.Warning;
                }
                
            }

            incident.Description = descriptionStringBuilder.ToString();
            incident.Save();

            //service.CreateMessage(incident.Id);
            if (!service.CheckStartedProcesses(incident))
            {
                service.StartProcess(incident);
            }
            return incident.Id;
        }

        public CheckStatus CheckLoadedFile(Guid guidFile)
        {
            var cacheFilesService = Locator.GetService<ICacheFilesService>();
            var file = cacheFilesService.GetBinaryFile(guidFile);
            var service = Locator.GetService<IncidentService>();

            var stopCheck = service.CheckFileOnStopFilter(file);
            if (stopCheck.Count > 0)
            {
                return CheckStatus.Stop;
            }

            var warningCheck = service.CheckFileOnWarningFilter(file);
            if (warningCheck.Count > 0)
            {
                return CheckStatus.Warning;
            }

            return CheckStatus.Ok;
        }
    }
}
