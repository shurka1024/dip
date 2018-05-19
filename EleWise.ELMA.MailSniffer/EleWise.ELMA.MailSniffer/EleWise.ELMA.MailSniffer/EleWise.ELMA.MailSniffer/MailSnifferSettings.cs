using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EleWise.ELMA.Extensions;
using EleWise.ELMA.Model.Attributes;
using EleWise.ELMA.Runtime.Settings;
using EleWise.ELMA.Security.Managers;
using EleWise.ELMA.Security.Models;
using System.Runtime.Serialization;

namespace EleWise.ELMA.MailSniffer
{
    /// <summary>
    /// Глобальные настройки
    /// </summary>
    public class MailSnifferSettings : GlobalSettingsBase
    {
        public MailSnifferSettings()
        {
            UpdateSettingsInterval = 24;
        }

        /// <summary>
        /// Идентификаторы оповещаемых пользователей
        /// </summary>
        public string NotifyUsersId { get; set; }       // TODO: Позже скрыть это поле

        /// <summary>
        /// Оповещаемые пользователи
        /// </summary>
        [DisplayName(typeof(MailSniffer_SR), "P_NotifyUsers")]
        [Description(typeof(MailSniffer_SR), "P_NotifyUsers_Description")]
        [IgnoreDataMember]
        public List<IUser> NotifyUsers
        {
            get
            {
                var users = new List<IUser>();
                if (!String.IsNullOrEmpty(NotifyUsersId))
                {
                    var usersId = NotifyUsersId.Split(',');
                    foreach (var us in usersId)
                    {
                        long id;
                        if (Int64.TryParse(us, out id))
                        {
                            users.Add(UserManager.Instance.LoadOrNull(id));
                        }
                    }
                }
                return users;
            }
            set
            {
                string res = null;
                if (value != null)
                {
                    List<long> ids = new List<long>();
                    foreach (var us in value)
                    {
                        ids.Add(us.Id);

                    }
                    res = ids.ToSeparatedString(",");
                }
                NotifyUsersId = res;
            }
        }

        /// <summary>
        /// Интервал обновления настроек, часы
        /// </summary> 
        [DisplayName(typeof(MailSniffer_SR), "P_UpdateSettingsInterval")]
        public long UpdateSettingsInterval { get; set; }

        /// <summary>
        /// Полный монитор почтового трафика сотрудников на испытательном сроке
        /// </summary> 
        [DisplayName(typeof(MailSniffer_SR), "P_MonitorEmployeesOnProbation")]
        public bool MonitorEmployeesOnProbation { get; set; }

        /// <summary>
        /// Полный монитор почтового трафика сотрудников, подавших заявление на увольнение
        /// </summary> 
        [DisplayName(typeof(MailSniffer_SR), "P_MonitorEmployeesOnDismissal")]
        public bool MonitorEmployeesOnDismissal { get; set; }

        /// <summary>
        /// Фильтры, при выполнении которых будут отправлены уведомления ответственному
        /// </summary> 
        [DisplayName(typeof(MailSniffer_SR), "P_FilterList")]
        [Description(typeof(MailSniffer_SR), "P_FilterListDescription")]
        public string FilterList { get; set; }

        /// <summary>
        /// Фильтры, при выполнении которых почтовый поток будет обрываться
        /// </summary> 
        [DisplayName(typeof(MailSniffer_SR), "P_BlockFilterList")]
        [Description(typeof(MailSniffer_SR), "P_FilterListDescription")]
        public string BlockFilterList { get; set; }

        /// <summary>
        /// Стоп слова
        /// </summary> 
        [DisplayName(typeof(MailSniffer_SR), "P_StopWordList")]
        public string StopWordList { get; set; }
    }

    internal class MailSniffer_SR
    {
        public static string P_NotifyUsers { get { return SR.T("Оповещаемые пользователи"); } }
        public static string P_NotifyUsers_Description { get { return SR.T("Пользователи, которым придет оповещение о блокировке почтового потока"); } }
        public static string P_UpdateSettingsInterval { get { return SR.T("Интервал обновления настроек, часы"); } }
        public static string P_MonitorEmployeesOnProbation { get { return SR.T("Полный монитор почтового трафика сотрудников на испытательном сроке"); } }
        public static string P_MonitorEmployeesOnDismissal { get { return SR.T("Полный монитор почтового трафика сотрудников, подавших заявление на увольнение"); } }
        public static string P_FilterList { get { return SR.T("Фильтры, при выполнении которых будут отправлены уведомления ответственному"); } }
        public static string P_BlockFilterList { get { return SR.T("Фильтры, при выполнении которых почтовый поток будет обрываться"); } }
        public static string P_FilterListDescription { get { return SR.T("Фильтр необходимо задавать в следующем виде: \"*@competitor.com\" или \"job@*\""); } }
        public static string P_StopWordList { get { return SR.T("Стоп слова"); } }
    }
}
