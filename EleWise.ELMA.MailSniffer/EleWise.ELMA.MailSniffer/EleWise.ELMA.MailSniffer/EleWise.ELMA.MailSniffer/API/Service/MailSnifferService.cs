using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using EleWise.ELMA.ComponentModel;
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
                IpExceptionUsers = manager.GetUsersIpAddresses(settings.ExceptionUsers).ToList()
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
            if(incident.Status != SniffState.Stop)
            {
                incident.Status = streamIsBlocked ? SniffState.Stop : SniffState.Warning;
            }

            var cacheFilesService = Locator.GetService<ICacheFilesService>();
            var file = cacheFilesService.GetFilePath(guidFile);

            incident.ThreadFile = null;     // !!!
            //incident.FileName = fileName;
            incident.Save();

            Locator.GetService<IncidentService>().CreateMessage(incident.Id);
            return incident.Id;
        }
    }
}
