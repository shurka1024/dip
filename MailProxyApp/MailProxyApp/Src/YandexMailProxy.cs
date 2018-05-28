﻿using ElmaIntegration;
using MailProxyApp.Src.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MailProxyApp.Src
{
    public class YandexMailProxy
    {
        /*
         Для mail.yandex.ru
         port - 443
         targetHost - mail.yandex.ru
         hostName - 213.180.204.125
             */

        public const int Port = 443;
        private static string TargetHost { get; set; }
        private static string HostName { get; set; }

        static readonly TcpListener Listener = new TcpListener(IPAddress.Any, Port);

        const int BufferSize = 4096;

        private static Setting ProxySettings { get; set; }

        private static Integration IntegrationService { get; set; }

        public static void StartListen(Setting settings, string targetHost, string hostName, Integration integration)
        {
            //Port = port;
            TargetHost = targetHost;
            HostName = hostName;

            //Listener = new TcpListener(IPAddress.Any, Port);
            ProxySettings = settings;
            IntegrationService = integration;

            Listener.Start();
            new Task(() =>
            {
                while (true)
                {
                    var client = Listener.AcceptTcpClient();
                    new Task(() => AcceptConnection(client)).Start();
                }
            }).Start();
        }

        public void StopListen()
        {
            Listener.Stop();
        }

        private static void AcceptConnection(TcpClient client)
        {
            try
            {
                var certificate = ProxySettings.Certificate;
                var clientStream = new SslStream(client.GetStream(), false);
                clientStream.AuthenticateAsServer(certificate, false, System.Security.Authentication.SslProtocols.Default, false);

                var server = new TcpClient(HostName, Port);
                var serverSslStream = new SslStream(server.GetStream(), false, SslValidationCallback, null);
                serverSslStream.AuthenticateAsClient(TargetHost);

                new Task(() => ReadFromClient(client, clientStream, serverSslStream)).Start();
                new Task(() => ReadFromServer(serverSslStream, clientStream)).Start();
            }
            catch(Exception ex)
            {
                throw;
            }            
        }

        private static bool SslValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
        {
            return true;
        }

        private static void ReadFromServer(Stream serverStream, Stream clientStream)
        {
            var message = new byte[BufferSize];
            while (true)
            {
                int serverBytes;
                try
                {
                    serverBytes = serverStream.Read(message, 0, BufferSize);
                    clientStream.Write(message, 0, serverBytes);
                }
                catch
                {
                    break;
                }
                if (serverBytes == 0)
                {
                    break;
                }
            }
        }

        private static void ReadFromClient(TcpClient client, Stream clientStream, Stream serverStream)
        {
            var message = new byte[BufferSize];
            var userIpAddress = (client.Client.RemoteEndPoint as IPEndPoint).Address;
            var fileName = GetReportFileName(userIpAddress);

            var fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists)
                fileInfo.Create().Dispose();

            var currentStatus = CheckStatus.Default;
            //var previousStatus = CheckStatus.Default;

            var streamIsWarning = false;
            var streamIsBlocked = false;

            using (var fileStream = fileInfo.OpenWrite())
            {
                while (true)
                {
                    int clientBytes;
                    try
                    {
                        clientBytes = clientStream.Read(message, 0, BufferSize);
                    }
                    catch
                    {
                        break;
                    }
                    if (clientBytes == 0)
                    {
                        break;
                    }

                    // Проверка содержимого буфера
                    // Если один раз уже было возвращено "Прервать поток", больше не проверяем
                    if (!streamIsBlocked)
                    {
                        currentStatus = CheckBuffer(message);
                        streamIsBlocked |= currentStatus == CheckStatus.Stop;
                        streamIsWarning |= currentStatus == CheckStatus.Warning;
                    }
                    
                    if(!streamIsBlocked)
                    {
                        serverStream.Write(message, 0, clientBytes);
                    }
                    else
                    {
                        serverStream.Write(new byte[BufferSize], 0, clientBytes);
                    }

                    //previousStatus = currentStatus;

                    //var str = Encoding.GetEncoding(1252).GetString(message);
                    //var str2 = HttpUtility.HtmlDecode(str);
                    fileStream.Write(message, 0, clientBytes);
                }
                client.Close();                
            }

            if (streamIsBlocked || streamIsWarning)
            {
                new Task(() => CreateIncident(fileInfo.FullName, streamIsBlocked, userIpAddress.ToString())).Start();
            }
            //if(fileInfo.Length == 0)
            //{
            //    fileInfo.Delete();
            //}
        }

        private static void CreateIncident(string logFullFileName, bool streamIsBlocked, string userIp)
        {
            long? incidentId = IntegrationService.CreateIncident(logFullFileName, streamIsBlocked, userIp);
        }

        /// <summary>
        /// Проверка буффера
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static CheckStatus CheckBuffer(byte[] message)
        {
            var warningList = ProxySettings.SnifferSettings.FilterList.Replace(", ", ",").Split(',');
            var stopList = ProxySettings.SnifferSettings.BlockFilterList.Replace(", ", ",").Split(',');

            var mesString = HttpUtility.HtmlDecode(Encoding.UTF8.GetString(message)).ToLower();  // TODO: НЕ РАБОТАЕТЬ!!!!

            var res1 = stopList.Where(l => mesString.Contains(l.ToLower())).ToList();
            if(res1.Count > 0)
            {
                Debug.WriteLine(string.Format("Почтовый поток заблокирован. Слово: \"{0}\"", res1.First()));
                return CheckStatus.Stop;
            }

            var res2 = warningList.Where(l => mesString.Contains(l.ToLower())).ToList();
            if (res2.Count > 0)
            {
                Debug.WriteLine(string.Format("Найдено слово: \"{0}\"", res2.First()));
                return CheckStatus.Warning;
            }

            return CheckStatus.Ok;
        }

        private static string GetReportFileName(IPAddress address)
        {
            var now = DateTime.Now;
            var dirName = Setting.ReportDirectory;
            var fileName = string.Format("Report_{0}{1}{2}_{3}{4}{5}_{6}_{7}"
                , now.Day
                , now.Month
                , now.Year
                , now.Hour
                , now.Minute
                , now.Second
                , address
                , Guid.NewGuid());

            return dirName + fileName;
        }
    }
}
