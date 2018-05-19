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

            log.Text += "Загружены настройки:" + Environment.NewLine;
            log.Text += string.Format("FilterList: {0}", snifferSettings.FilterList + Environment.NewLine);
            log.Text += string.Format("BlockFilterList: {0}", snifferSettings.BlockFilterList + Environment.NewLine);

            // Путь до сертификата для mail.yandex.ru
            // TODO: Сделать автогенерируемым
            AppSettings.CertificateFileName = @"D:\Учеба (Саша)\Диплом\Разработка\Результат\MailProxyApp\MailProxyApp\Certificates\cer-der(fd-ya).cer";
            AppSettings.SnifferSettings = snifferSettings;

            Proxy = new YandexMailProxy();

            int port = 443;
            string targetHost = "mail.yandex.ru";
            string ip = "213.180.204.125";

            Proxy.StartListen(AppSettings, port, targetHost, ip, Integration);
            //Proxy.StartListen(settings, 465, "smtp.yandex.com", "213.180.193.38");

            appStatus.Text = string.Format("Прокси запущен. Порт {0}. Хост: {1}. IP: {2}", port, targetHost, ip);
            currentUserInfo.Text = string.Format("Текущий пользователь: {0}", "");
        }        
    }
}
