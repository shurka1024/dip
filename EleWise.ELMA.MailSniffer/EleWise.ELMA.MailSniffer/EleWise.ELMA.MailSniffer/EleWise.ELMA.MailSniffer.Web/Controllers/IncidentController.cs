using EleWise.ELMA.BPM.Mvc.Controllers;
using EleWise.ELMA.Common.Managers;
using EleWise.ELMA.MailSniffer.Services;
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
using System.Web;
using System.Web.Mvc;

namespace EleWise.ELMA.MailSniffer.Web.Controllers
{
    public class IncidentController : BPMController
    {
        public IncidentService IncidentService { get; set; }

        /// <summary>
        /// Отправить сообщение об инциденте ответственным
        /// </summary>
        /// <param name="incidentId"></param>
        /// <returns></returns>
        public bool SendMessage(long incidentId)
        {
            IncidentService.CreateMessage(incidentId);
            return true;
        }

    }
}
