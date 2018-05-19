using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EleWise.ELMA.MailSniffer.API.Models
{
    /// <summary>
    /// Настройки MailSniffer WCF
    /// </summary>
    [DataContract(Name = "Settings")]
    [EleWise.ELMA.Model.Attributes.Description(typeof(__SettingsResponse_SR), "Settings")]
    [Serializable]
    public class SettingsResponse
    {
        [DataMember]
        [EleWise.ELMA.Model.Attributes.Description(typeof(__SettingsResponse_SR), "FilterList")]
        public string FilterList { get; set; }

        [DataMember]
        [EleWise.ELMA.Model.Attributes.Description(typeof(__SettingsResponse_SR), "FilterList")]
        public string BlockFilterList { get; set; }
    }

    internal class __SettingsResponse_SR
    {
        public static string Settings { get { return SR.T("Текщие настройки модуля"); } }
        public static string FilterList { get { return SR.T("Фильтры, при выполнении которых будут отправлены уведомления ответственному"); } }
        public static string BlockFilterList { get { return SR.T("Фильтры, при выполнении которых почтовый поток будет обрываться"); } }
    }
}
