using ElmaIntegration;
using ElmaIntegration.Models;
using MailProxyApp.Src;
using MailProxyApp.Src.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailProxyApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(Integration integration)
        {
            InitializeComponent();

            Integration = integration;
            StartListen();
        }

        private YandexMailProxy Proxy { get; set; }

        private Integration Integration { get; set; }

        private Setting AppSettings = new Setting();

        private void StartListen()
        {
            var snifferSettings = Integration.GetMailSnifferSettings();

            // Отображение полученных настроек
            textBox1.Text = string.Join(", ", snifferSettings.IpExceptionUsers);

            radioButton5.Checked = snifferSettings.MonitorMailsWithAttachment;
            radioButton6.Checked = !snifferSettings.MonitorMailsWithAttachment;

            textBox4.Text = string.Join(", ", snifferSettings.FilterList);
            textBox5.Text = string.Join(", ", snifferSettings.BlockFilterList);

            var usersOnProbationCount = snifferSettings.IpUsersOnProbation.Count;
            radioButton1.Checked = usersOnProbationCount > 0;
            radioButton2.Checked = usersOnProbationCount == 0;
            textBox2.Text = string.Join(", ", snifferSettings.IpUsersOnProbation);

            var usersOnDismissalCount = snifferSettings.IpUsersOnDismissal.Count;
            radioButton3.Checked = usersOnDismissalCount > 0;
            radioButton4.Checked = usersOnDismissalCount == 0;
            textBox3.Text = string.Join(", ", snifferSettings.IpUsersOnDismissal);

            log.AppendText("Загружены настройки:" + Environment.NewLine);

            // Путь до сертификата для mail.yandex.ru
            // TODO: Сделать автогенерируемым
            AppSettings.CertificateFileName = @"D:\Учеба (Саша)\Диплом\Разработка\Результат\dip\MailProxyApp\MailProxyApp\Certificates\cer-der(fd-ya).cer";
            //AppSettings.CertificateFileName = @"D:\Учеба (Саша)\Диплом\Разработка\Результат\dip\MailProxyApp\MailProxyApp\Certificates\server.crt";
            
            AppSettings.SnifferSettings = snifferSettings;
            
            string targetHost = "mail.yandex.ru";
            string ip = "213.180.204.125";

            YandexMailProxy.StartListen(AppSettings, targetHost, ip, Integration, log);

            log.AppendText(string.Format("Прокси запущен. Порт {0}. Хост: {1}. IP: {2}" + Environment.NewLine, YandexMailProxy.Port, targetHost, ip));
        }        
    }
}
