using ElmaIntegration.Extensions;
using ElmaIntegration.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ElmaIntegration
{
    public class Integration
    {
        /// <summary>
        /// Адрес сервера
        /// </summary>
        private string ElmaServer { get; set; }

        /// <summary>
        /// Токен авторизации
        /// </summary>
        private string AuthToken { get; set; }

        /// <summary>
        /// Токен сессии
        /// </summary>
        private string SessionToken { get; set; }

        /// <summary>
        /// Токен приложения
        /// </summary>
        private string ApplicationToken = "AF2E8183CC0A5291DA695C084A3FF18192EFE4B83C4183CCE26D364470D18034D965829BC4F4FF5682E5A6E9B5BB62980A6612ACD483E31784333D4EC87C841E";

        /// <summary>
        /// Настройки JsonSerializer
        /// </summary>
        private JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            DateParseHandling = DateParseHandling.DateTime,
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        };

        Integration()
        { }

        public Integration(string elmaServerUrl)
        {
            ElmaServer = elmaServerUrl;
        }

        public ServerStatus GetServerStatus()
        {
            return GetServerStatus(ElmaServer);
        }

        public ServerStatus GetServerStatus(string elmaServerUrl)
        {
            //Отправляем запрос на сервер для получения статуса
            var httpWReq = (HttpWebRequest)WebRequest.Create(string.Format("{0}StartInfoHandler.ashx?type=StartInfo", ElmaServer));
            var response = httpWReq.GetResponse();
            var result = new ServerStatus();
            string stringResponse;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                stringResponse = sr.ReadToEnd();
            }
            if (!string.IsNullOrWhiteSpace(stringResponse))
                result.Status = stringResponse.Substring(0, 1);
            //while (result == "0")
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    result = GetServerStatus();
            //    //debugView.AppendText(Environment.NewLine);
            //    //debugView.AppendText("Сервер запускается", Color.Green);
            //    //debugView.AppendText(Environment.NewLine);
            //}
            if (result.Status == "3")
            {
                var index = stringResponse.LastIndexOf(";");
                result.Reason = stringResponse.Substring(index, stringResponse.Length - index);
            }

            return result;
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Auth Authorization(string login, string password)
        {
            var urlAuth = string.Format("{0}/API/REST/Authorization/LoginWith?username={1}", ElmaServer, login);
            var auth = RestExecute<Auth>(urlAuth, password, WebRequestMethods.Http.Post);
            if (auth != null)
            {
                AuthToken = auth.AuthToken;
                SessionToken = auth.SessionToken;
            }
            return auth;
        }

        public SnifferSettings GetMailSnifferSettings()
        {
            var url = string.Format("{0}/PublicAPI/REST/EleWise.ELMA.MailSniffer/MailSniffer/GetSettings", ElmaServer);
            var res = RestExecute<SnifferSettings>(url, "", WebRequestMethods.Http.Get);
            return res;
        }

        public Guid UploadFile(string fullFileName)
        {
            var file = new FileStream(fullFileName, FileMode.Open);
            var lastIndexOfSlash = file.Name.LastIndexOf('\\');
            var filename = file.Name.Substring(lastIndexOfSlash + 1, file.Name.Length - lastIndexOfSlash - 1);
            var guidFile = UploadFile(file, filename);
            file.Close();
            return guidFile;
        }

        public Guid UploadFile(Stream fileStream, string fileName)
        {
            var uploadFileReq = string.Format("{0}API/REST/Files/Upload", ElmaServer);
            var headers = new Dictionary<string, string>();
            headers["FileName"] = HttpUtility.UrlEncode(fileName);
            return RestExecute<Guid>(uploadFileReq, fileStream, WebRequestMethods.Http.Post, headers);
        }

        /// <summary>
        /// Создать запись об инциденте а Elma
        /// </summary>
        /// <param name="logFullFileName"></param>
        /// <param name="streamIsBlocked"></param>
        /// <param name="userIp"></param>
        /// <returns></returns>
        public long? CreateIncident(string logFullFileName, bool streamIsBlocked, string userIp)
        {
            var guidFile = UploadFile(logFullFileName);
            //var guidFile = UploadFile(@"D:\Учеба (Саша)\Диплом\32-37.docx");
            var lastIndexOfSlash = logFullFileName.LastIndexOf('\\');
            var shotLogFileName = logFullFileName.Substring(lastIndexOfSlash + 1, logFullFileName.Length - lastIndexOfSlash - 1);

            return CreateIncident(guidFile, shotLogFileName, streamIsBlocked, userIp);
        }

        /// <summary>
        /// Создать запись об инциденте а Elma
        /// </summary>
        /// <param name="guidFile"></param>
        /// <param name="streamIsBlocked"></param>
        /// <param name="userIp"></param>
        /// <returns></returns>
        public long? CreateIncident(Guid guidFile, string fileName, bool streamIsBlocked, string userIp)
        {
            var url = string.Format("{0}/PublicAPI/REST/EleWise.ELMA.MailSniffer/MailSniffer/CreateIncident?guidFile={1}&streamIsBlocked={2}&userIp={3}&fileName={4}", ElmaServer, guidFile, streamIsBlocked, userIp, fileName);
            var incidentIp = RestExecute<long?>(url, "", WebRequestMethods.Http.Post);
            return incidentIp;
        }

        /// <summary>
        /// Выполнить запрос
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="url">URL</param>
        /// <param name="requestData">Сериализованные данные</param>
        /// <param name="httpMethod">Метод запроса (post, get)</param>
        /// <returns>Десериализованный ответ с сервера</returns>
        private T RestExecute<T>(string url, object requestData, string httpMethod, Dictionary<string, string> addHeaders = null)
        {
            RestData g;
            if (requestData is Stream)
            {
                g = new RestData(url, requestData as Stream, httpMethod);
            }
            else
            {
                var serializedObject = JsonConvert.SerializeObject(requestData);
                g = new RestData(url, serializedObject, httpMethod);
            }

            HttpWebResponse httpWResp = null;
            try
            {
                httpWResp = DoRestExecute(g, addHeaders);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Response);
            }

            var result = "";
            if (httpWResp != null && httpWResp.StatusCode == HttpStatusCode.OK)
            {
                using (var sr = new StreamReader(httpWResp.GetResponseStream()))
                {
                    result = sr.ReadToEnd();

                    //debugView.AppendText(Environment.NewLine);
                    //debugView.AppendText(string.Format("Ответ сервера: {0}", result), Color.Black);
                    //debugView.AppendText(Environment.NewLine);
                }
            }
            else
            {
                if (httpWResp != null)
                    Console.WriteLine("ERROR STATUS CODE:{0}, MESSAGE: {1}", httpWResp.StatusCode,
                        httpWResp.StatusDescription);
                else
                {
                    //debugView.AppendText(Environment.NewLine);
                    //debugView.AppendText("Ошибка при выполнении запроса", Color.Red);
                    //debugView.AppendText(Environment.NewLine);
                    //throw new WebException("httpWResp == null");
                }
            }
            return JsonConvert.DeserializeObject<T>(result, JsonSettings);
        }

        /// <summary>
        /// Выполнить запрос на основании данных
        /// </summary>
        /// <param name="restData">Данные</param>
        /// <returns>Ответ сервера</returns>
        private HttpWebResponse DoRestExecute(RestData restData, Dictionary<string, string> headers = null)
        {
            //Создаем экземпляр запроса по URL
            var HttpWReq = (HttpWebRequest)WebRequest.Create(restData.Url);

            //Указываем метод запроса
            HttpWReq.Method = restData.HTTPMethod;
            HttpWReq.Credentials = CredentialCache.DefaultCredentials;
            //Добавляем токен приложения
            HttpWReq.Headers.Add("ApplicationToken", ApplicationToken);
            //Добавляем токен авторизации, если есть
            if (!string.IsNullOrWhiteSpace(AuthToken))
            {
                HttpWReq.Headers.Add("AuthToken", AuthToken);
            }
            //Добавляем токен сессии, если есть
            if (!string.IsNullOrEmpty(SessionToken))
            {
                HttpWReq.Headers.Add("SessionToken", SessionToken);
            }
            HttpWReq.Accept = "application/json; charset=utf-8";
            HttpWReq.Headers.Add("WebData-Version", "2.0");
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    HttpWReq.Headers.Add(header.Key, header.Value);
                }
            }
            var encoding = new UTF8Encoding();

            //Если метод POST, тогда добавляем тип контента и вставляем данные
            if (restData.HTTPMethod == WebRequestMethods.Http.Post)
            {
                if (!string.IsNullOrWhiteSpace(restData.Data))
                {
                    var byteData = encoding.GetBytes(restData.Data);
                    HttpWReq.ContentType = "application/json; charset=utf-8";

                    HttpWReq.ContentLength = byteData.Length;
                    using (Stream requestStream = HttpWReq.GetRequestStream())
                    {
                        requestStream.Write(byteData, 0, byteData.Length);
                    }
                }

                if (restData.Stream != null)
                {
                    HttpWReq.ContentType = "application/json; charset=utf-8";
                    HttpWReq.ContentLength = restData.Stream.Length;

                    using (Stream requestStream = HttpWReq.GetRequestStream())
                    {
                        byte[] buffer = new byte[restData.Stream.Length];
                        restData.Stream.Read(buffer, 0, (int)restData.Stream.Length);
                        requestStream.Write(buffer, 0, (int)restData.Stream.Length);
                    }
                }
            }
            return (HttpWebResponse)HttpWReq.GetResponse();
        }
    }
}
