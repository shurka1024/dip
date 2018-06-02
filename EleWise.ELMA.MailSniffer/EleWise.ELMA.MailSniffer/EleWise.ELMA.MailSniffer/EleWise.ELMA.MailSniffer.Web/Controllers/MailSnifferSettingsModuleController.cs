using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EleWise.ELMA.ComponentModel;
using EleWise.ELMA.Web.Mvc.Models.Settings;
using System.Web.Mvc.Html;

namespace EleWise.ELMA.MailSniffer.Web.Controllers
{
    [Component(Order = 261)]
    public class MailSnifferSettingsModuleController : GlobalSettingsModuleControllerBase<MailSnifferSettings, MailSnifferSettingsModule>
    {
        public MailSnifferSettingsModuleController(MailSnifferSettingsModule module) 
            : base(module)
        {
        }

        public override MvcHtmlString RenderDisplay(HtmlHelper html)
        {
            return html.Action("RenderDisplay", "MailSnifferSetting", new { area = RouteProvider.AreaName });
        }

        public override MvcHtmlString RenderEdit(HtmlHelper html)
        {
            return html.Action("RenderEdit", "MailSnifferSetting", new { area = RouteProvider.AreaName });
        }
    }
}
