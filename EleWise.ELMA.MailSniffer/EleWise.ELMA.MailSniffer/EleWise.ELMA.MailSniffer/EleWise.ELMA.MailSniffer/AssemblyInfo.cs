[assembly: System.Runtime.InteropServices.Guid("342f9625-e30c-4d13-a8c9-53b5d7c25cac")]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: System.Reflection.AssemblyTitle("Модуль для перехвата почтового трафика")]
[assembly: EleWise.ELMA.ComponentModel.ComponentAssembly()]
[assembly: EleWise.ELMA.Model.Attributes.ModelAssembly()]

namespace EleWise.ELMA.MailSniffer
{
    using System;
    
    
    [global::EleWise.ELMA.Model.Attributes.MetadataType(typeof(global::EleWise.ELMA.Model.Metadata.AssemblyInfoMetadata))]
    [global::EleWise.ELMA.Model.Attributes.Uid("342f9625-e30c-4d13-a8c9-53b5d7c25cac")]
    internal class @__AssemblyInfo
    {
        
        /// <summary>
        /// UID of this class
        /// </summary>
        public const string UID_S = "342f9625-e30c-4d13-a8c9-53b5d7c25cac";
        
        private static global::System.Guid _UID = new global::System.Guid(UID_S);
        
        /// <summary>
        /// UID of this class
        /// </summary>
        public static global::System.Guid UID
        {
            get
            {
                return _UID;
            }
        }
    }
    
    internal class @__Resources__AssemblyInfo
    {
        
        public static string DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Модуль для перехвата почтового трафика");
            }
        }
    }
}
