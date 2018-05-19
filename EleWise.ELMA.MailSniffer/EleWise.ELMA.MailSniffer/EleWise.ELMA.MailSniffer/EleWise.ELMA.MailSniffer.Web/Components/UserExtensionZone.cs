using EleWise.ELMA.BPM.Web.Security.Models;
using EleWise.ELMA.ComponentModel;
using EleWise.ELMA.Web.Mvc.ExtensionPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace EleWise.ELMA.MailSniffer.Web.Components
{
    [Component(Order = -749)]
    public class UserExtensionZone : IExtensionZone
    {
        public const string ZoneId = "EleWise.ELMA.BPM.Web.Security.ProfileEditor.Table";

        public bool CanRenderInZone(string zoneId, HtmlHelper html)
        {
            return zoneId == ZoneId;
        }

        public void RenderZone(string zoneId, HtmlHelper html)
        {
            if (ZoneId == zoneId)
            {
                html.RenderPartial("User/UserProfileInfo");
            }
        }
    }
}