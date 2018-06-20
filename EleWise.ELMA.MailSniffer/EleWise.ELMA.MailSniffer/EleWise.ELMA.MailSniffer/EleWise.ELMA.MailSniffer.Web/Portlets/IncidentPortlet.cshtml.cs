using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace EleWise.ELMA.MailSniffer.Portlets
{

    /// <summary>
    /// Portlet personalization model (custom options in the portlet settings)
    /// </summary>
    [Serializable]
    public class IncidentPortletPersonalization : EleWise.ELMA.Web.Mvc.Portlets.PortletPersonalization
    {
        /// <summary>
        /// "My option" - new option in the portlet settings
        /// Each user is allowed to change this option
        /// </summary>
        [EleWise.ELMA.Web.Mvc.Portlets.Personalization(EleWise.ELMA.Web.Mvc.Portlets.PersonalizationScope.User)]
        [System.ComponentModel.Category("My category")]
        [EleWise.ELMA.Model.Attributes.DisplayName(typeof(IncidentPortletPersonalization_SR), "P_Text")]
        public string Text { get; set; }

    }

    /// <summary>
    /// Class with string resources for portlet localization
    /// </summary>
    internal static class IncidentPortletPersonalization_SR
    {
        public static string P_Text
        {
            get { return EleWise.ELMA.SR.T("My option"); }
        }

        /// <summary>
        /// This property is required for localization mechanism
        /// </summary>
        public static string P_CategoryAppearanceName
        {
            get { return EleWise.ELMA.SR.T("My category"); }
        }
    }

    /// <summary>
    /// This component determines how the portlet works
    /// </summary>
    [EleWise.ELMA.ComponentModel.Component]
    public class IncidentPortlet : EleWise.ELMA.Web.Mvc.Portlets.Portlet<IncidentPortletPersonalization>
    {

        /// <summary>
        /// Builder. It is called once for an application instance
        /// </summary>
        public IncidentPortlet()
        {
            _profile = base.Profile as EleWise.ELMA.Web.Mvc.Portlets.PortletProfile ?? EleWise.ELMA.Web.Mvc.Portlets.PortletProfile.Default;

            //Override additional settings of portlet's profile
            _profile.ImageUrl = "#x16/Unk16.gif";
            _profile.Customizable = true;
        }

        private readonly EleWise.ELMA.Web.Mvc.Portlets.PortletProfile _profile;

        /// <summary>
        /// Additional portlet settings
        /// </summary>
        public override EleWise.ELMA.Web.Mvc.Portlets.IPortletProfile Profile
        {
            get { return _profile; }
        }

        /// <summary>
        /// Return portlet's content
        /// </summary>
        /// <param name="html">Html helper</param><param name="data"/>
        /// <returns/>
        public override MvcHtmlString Content(HtmlHelper html, IncidentPortletPersonalization data)
        {
            return html.Partial("/Modules/EleWise.ELMA.MailSniffer.Web/Portlets/IncidentPortlet.cshtml", data);
        }

        /// <summary>
        /// Portlet description
        /// </summary>
        public override string Description
        {
            get { return "IncidentPortlet"; }
        }

        /// <summary>
        /// Portlet name
        /// </summary>
        public override string Name
        {
            get { return "IncidentPortlet"; }
        }

        /// <summary>
        /// Permission to be able to see the portlet. If no permission, then null
        /// </summary>
        /// <returns/>
        protected override EleWise.ELMA.Security.Permission PortletPermission()
        {
            return null;
        }

        /// <summary>
        /// Portlet identifier
        /// </summary>
        public override Guid Uid
        {
            get { return new Guid("716e95c4-6e5e-4b7a-a2c5-8ef22a8d2ce0"); }
        }

    }

}
