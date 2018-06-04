using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleWise.ELMA.MailSniffer.API.Models
{
    /// <summary>
    /// Статус результат проверки
    /// </summary>
    public enum CheckStatus
    {
        Default = 0,
        Ok = 1,
        Warning = 2,
        Stop = 3
    }
}
