using EleWise.ELMA.MailSniffer.Models;
using EleWise.ELMA.Model.Common;
using EleWise.ELMA.Model.Managers;
using EleWise.ELMA.Security.Models;
using EleWise.ELMA.Security.Managers;
using EleWise.ELMA.Services;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace EleWise.ELMA.MailSniffer.Managers
{
    public class UserManagerExt : EntityExtensionManager<IUser, IUserExt, long, UserManager>
    {
        public new static UserManagerExt Instance
        {
            get
            {
                return Locator.GetServiceNotNull<UserManagerExt>();
            }
        }

        public ICollection<IUser> GetUsersOnDismissal()
        {
            return new List<IUser>();
        }

        public ICollection<IUser> GetUsersOnTrialPeriod()
        {
            var userList = new List<IUser>();
            var settingsModule = Locator.GetService<MailSnifferSettingsModule>();
            if (settingsModule != null && settingsModule.Settings != null)
            {
                var trialDaysDuration = settingsModule.Settings.TrialDuration;
                var date = DateTime.Now.Date - trialDaysDuration;

                var criteria = Session.CreateCriteria<IUser>();
                criteria.Add(Restrictions.IsNotNull("EmployDate"));
                criteria.Add(Restrictions.Ge("EmployDate", date));
                userList.AddRange(criteria.List<IUser>());
            }
            return userList;
        }

        public ICollection<string> GetUsersIpAddresses(ICollection<IUser> list)
        {
            if (list == null || !list.Any())
            {
                return new List<string>();
            }
            var ipList = list.Where(u => (u as IUserExt).IPAdress != null)
                .Select(u => (u as IUserExt).IPAdress).ToList();
            return ipList;
        }
    }
}
