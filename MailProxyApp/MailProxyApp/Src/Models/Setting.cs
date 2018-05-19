using ElmaIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MailProxyApp.Src.Models
{
    public class Setting
    {
        public Setting()
        {
            if (SnifferSettings == null)
                SnifferSettings = new SnifferSettings();
        }

        /// <summary>
        /// Путь до файла сертификата
        /// </summary>
        public string CertificateFileName { get; set; }

        private X509Certificate2 _certificate { get; set; }

        /// <summary>
        /// Сертификат
        /// </summary>
        public X509Certificate2 Certificate
        {
            get
            {
                if (_certificate != null)
                {
                    return _certificate;
                }

                try
                {
                    _certificate = new X509Certificate2(CertificateFileName);
                    return _certificate;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                _certificate = value;
            }
        }

        /// <summary>
        /// Папка, куда будут складываться отчеты
        /// </summary>
        public static string ReportDirectory
        {
            get
            {
                return @"D:\Учеба (Саша)\Диплом\Разработка\Результат\MailProxyApp\MailProxyApp\Reports\";
            }
        }

        /// <summary>
        /// Настройки для отслеживания почтового потока
        /// </summary>
        public SnifferSettings SnifferSettings { get; set; }
    }
}
