﻿using ElmaIntegration;
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
            log.Text += string.Format("FilterList: {0}", string.Join(", ", snifferSettings.FilterList) + Environment.NewLine);
            log.Text += string.Format("BlockFilterList: {0}", string.Join(", ", snifferSettings.BlockFilterList) + Environment.NewLine);
            log.Text += string.Format("IpUsersOnProbation: {0}", string.Join(", ", snifferSettings.IpUsersOnProbation) + Environment.NewLine);
            log.Text += string.Format("IpUsersOnDismissal: {0}", string.Join(", ", snifferSettings.IpUsersOnDismissal) + Environment.NewLine);
            log.Text += string.Format("IpExceptionUsers: {0}", string.Join(", ", snifferSettings.IpExceptionUsers) + Environment.NewLine);
            
            // Путь до сертификата для mail.yandex.ru
            // TODO: Сделать автогенерируемым
            AppSettings.CertificateFileName = @"D:\Учеба (Саша)\Диплом\Разработка\Результат\dip\MailProxyApp\MailProxyApp\Certificates\cer-der(fd-ya).cer";
            //AppSettings.CertificateFileName = @"D:\Учеба (Саша)\Диплом\Разработка\Результат\dip\MailProxyApp\MailProxyApp\Certificates\server.crt";
            
            AppSettings.SnifferSettings = snifferSettings;

            //Proxy = new YandexMailProxy();

            //int port = 443;
            string targetHost = "mail.yandex.ru";
            string ip = "213.180.204.125";

            YandexMailProxy.StartListen(AppSettings, targetHost, ip, Integration);
            //Proxy.StartListen(settings, 465, "smtp.yandex.com", "213.180.193.38");

            appStatus.Text = string.Format("Прокси запущен. Порт {0}. Хост: {1}. IP: {2}", YandexMailProxy.Port, targetHost, ip);
            currentUserInfo.Text = string.Format("Текущий пользователь: {0}", "");
        }        
    }
}
