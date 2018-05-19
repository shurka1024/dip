namespace EleWise.ELMA.MailSniffer.Models
{
    using System;
    
    
    /// <summary>
    /// SniffState
    /// </summary>
    [global::EleWise.ELMA.Model.Attributes.MetadataType(typeof(global::EleWise.ELMA.Model.Metadata.EnumMetadata))]
    [global::EleWise.ELMA.Model.Attributes.Uid("b3dc98b7-07a4-4a4a-9d51-912c59d2f52d")]
    [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_SniffState), "DisplayName")]
    [global::EleWise.ELMA.Model.Attributes.DefaultValueUid("c3daf856-b165-4ebd-b39a-5328f09eceef")]
    public enum SniffState
    {
        
        /// <summary>
        /// По умолчанию
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("c3daf856-b165-4ebd-b39a-5328f09eceef")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_SniffState), "P_Default_DisplayName")]
        Default = 0,
        
        /// <summary>
        /// Ок
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("a0923e7e-8d98-476b-92a7-2230889e74e4")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_SniffState), "P_Ok_DisplayName")]
        Ok = 1,
        
        /// <summary>
        /// Предупреждение
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("ceffd833-477a-410c-bfe8-7cc5adceee78")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_SniffState), "P_Warning_DisplayName")]
        Warning = 2,
        
        /// <summary>
        /// Опасно
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("5348e5bf-a597-49e8-b896-33669fcef290")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_SniffState), "P_Stop_DisplayName")]
        Stop = 3,
    }
    
    internal class @__Resources_SniffState
    {
        
        public static string DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("SniffState");
            }
        }
        
        public static string P_Default_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("По умолчанию");
            }
        }
        
        public static string P_Ok_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Ок");
            }
        }
        
        public static string P_Warning_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Предупреждение");
            }
        }
        
        public static string P_Stop_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Опасно");
            }
        }
    }
}
