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
        [EleWise.ELMA.Model.Attributes.Description(typeof(__SettingsResponse_SR), "FilterString")]
        public string FilterString { get; set; }

        [DataMember]
        [EleWise.ELMA.Model.Attributes.Description(typeof(__SettingsResponse_SR), "BlockFilterString")]
        public string BlockFilterString { get; set; }

        [DataMember]
        [EleWise.ELMA.Model.Attributes.Description(typeof(__SettingsResponse_SR), "Property_SR")]
        public List<string> IpUsersOnProbation { get; set; }

        [DataMember]
        [EleWise.ELMA.Model.Attributes.Description(typeof(__SettingsResponse_SR), "Property_SR")]
        public List<string> IpUsersOnDismissal { get; set; }

        [DataMember]
        [EleWise.ELMA.Model.Attributes.Description(typeof(__SettingsResponse_SR), "Property_SR")]
        public List<string> IpExceptionUsers { get; set; }

        [DataMember]
        [EleWise.ELMA.Model.Attributes.Description(typeof(__SettingsResponse_SR), "Property_SR")]
        public bool MonitorMailsWithAttachment { get; set; }
    }

    internal class __SettingsResponse_SR
    {
        public static string Settings { get { return SR.T("Текщие настройки модуля"); } }
        public static string FilterString { get { return SR.T("Фильтры, при выполнении которых будут отправлены уведомления ответственному"); } }
        public static string BlockFilterString { get { return SR.T("Фильтры, при выполнении которых почтовый поток будет обрываться"); } }
        public static string Property_SR { get { return SR.T("Свойство SettingsResponse"); } }
    }
}
