using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailProxyApp.Src.Models
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
