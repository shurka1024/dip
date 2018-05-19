using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EleWise.ELMA.ComponentModel;
using EleWise.ELMA.Runtime.Settings;

namespace EleWise.ELMA.MailSniffer
{
    [Component]
    public class MailSnifferSettingsModule : GlobalSettingsModuleBase<MailSnifferSettings>
    {
        public static Guid UID = new Guid("{BBF28990-C620-47E8-ABBC-9B1AD53851AA}");

        public override Guid ModuleGuid
        {
            get { return UID; }
        }

        public override string ModuleName
        {
            get { return SR.T("Настройки мониторинга почтового трафика"); }
        }
    }
}
