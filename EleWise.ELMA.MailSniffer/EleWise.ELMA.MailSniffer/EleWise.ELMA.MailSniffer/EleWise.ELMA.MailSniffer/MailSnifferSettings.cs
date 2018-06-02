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
using EleWise.ELMA.MailSniffer.Managers;

namespace EleWise.ELMA.MailSniffer
{
    /// <summary>
    /// Глобальные настройки
    /// </summary>
    public class MailSnifferSettings : GlobalSettingsBase
    {
        public MailSnifferSettings()
        {
        }

        public UserManager UserManager { get; set; }

        /// <summary>
        /// Идентификаторы оповещаемых пользователей
        /// </summary>
        public string NotifyUsersId { get; set; }

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
        /// Длительность испытательного срока, мес
        /// </summary> 
        [DisplayName(typeof(MailSniffer_SR), "P_TrialDuration")]
        public TimeSpan TrialDuration { get; set; }

        /// <summary>
        /// Пользователи на испытательном сроке
        /// </summary>
        [DisplayName(typeof(MailSniffer_SR), "P_EmployeesOnProbation")]
        public ICollection<IUser> EmployeesOnProbation
        {
            get
            {
                var users = UserManagerExt.Instance.GetUsersOnTrialPeriod();
                return users;
            }
        }

        /// <summary>
        /// Пользователи, подавшие заявление на увольнение
        /// </summary>
        [DisplayName(typeof(MailSniffer_SR), "P_EmployeesOnDismissal")]
        public ICollection<IUser> EmployeesOnDismissal
        {
            get
            {
                var users = UserManagerExt.Instance.GetUsersOnDismissal();
                return users;
            }
        }

        /// <summary>
        /// Идентификаторы пользователей, для которых фильтры применяться не будут
        /// </summary>
        public string ExceptionUsersId { get; set; }

        /// <summary>
        /// Пользователи, для которых фильтры применяться не будут
        /// </summary>
        [DisplayName(typeof(MailSniffer_SR), "P_ExceptionUsers")]
        public ICollection<IUser> ExceptionUsers
        {
            get
            {
                var users = new List<IUser>();
                if (!String.IsNullOrEmpty(ExceptionUsersId))
                {
                    var usersId = ExceptionUsersId.Split(',');
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
                ExceptionUsersId = res;
            }
        }
    }

    internal class MailSniffer_SR
    {
        public static string P_NotifyUsers { get { return SR.T("Оповещаемые пользователи"); } }
        public static string P_NotifyUsers_Description { get { return SR.T("Пользователи, которым придет оповещение о блокировке почтового потока"); } }
        public static string P_MonitorEmployeesOnProbation { get { return SR.T("Полный монитор почтового трафика сотрудников на испытательном сроке"); } }
        public static string P_MonitorEmployeesOnDismissal { get { return SR.T("Полный монитор почтового трафика сотрудников, подавших заявление на увольнение"); } }
        public static string P_FilterList { get { return SR.T("Фильтры, при выполнении которых будут отправлены уведомления ответственному"); } }
        public static string P_BlockFilterList { get { return SR.T("Фильтры, при выполнении которых почтовый поток будет обрываться"); } }
        public static string P_FilterListDescription { get { return SR.T("Фильтр необходимо задавать в следующем виде: \"*@competitor.com\" или \"job@*\""); } }
        public static string P_TrialDuration { get { return SR.T("Длительность испытательного срока"); } }

        public static string P_EmployeesOnProbation { get { return SR.T("Пользователи на испытательном сроке"); } }
        public static string P_EmployeesOnDismissal { get { return SR.T("Пользователи, подавшие заявление на увольнение"); } }
        public static string P_ExceptionUsers { get { return SR.T("Пользователи, для которых фильтры применяться не будут"); } }
    }
}
