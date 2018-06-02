using EleWise.ELMA.BPM.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EleWise.ELMA.MailSniffer.Web.Controllers
{
    public class MailSnifferSettingController : BPMController
    {
        public MailSnifferSettingsModule SettingsModule { get; set; }

        public MailSnifferSettings Settings
        {
            get
            {
                return SettingsModule.Settings;
            }
        }

        [HttpGet]
        public ActionResult RenderDisplay()
        {
            return PartialView(Settings);
        }

        [HttpGet]
        public ActionResult RenderEdit()
        {
            return PartialView(Settings);
        }
    }
}