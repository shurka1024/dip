using EleWise.ELMA.Common.Managers;
using EleWise.ELMA.ComponentModel;
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

namespace EleWise.ELMA.MailSniffer.Services
{
    [Service]
    public class IncidentService : IIncidentService
    {
        public UserManager UserManager { get; set; }

        public AttachmentManager AttachmentManager { get; set; }

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
    }
}
