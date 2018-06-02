using EleWise.ELMA.MailSniffer.Models;
using EleWise.ELMA.Model.Managers;
using EleWise.ELMA.Model.Services;
using EleWise.ELMA.Services;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleWise.ELMA.MailSniffer.Managers
{
    public class IncidentManager : EntityManager<IIncident, long>
    {
        public new static IncidentManager Instance
        {
            get
            {
                return Locator.GetServiceNotNull<IncidentManager>();
            }
        }

        public IIncident FindNearOrCreateIncident(string ip)
        {
            var beginDate = DateTime.Now.Subtract(new TimeSpan(0, 0, 10));

            var criteria = Session.CreateCriteria<IIncident>();
            criteria.Add(Restrictions.Eq("IPAdress", ip));
            criteria.Add(Restrictions.Ge("CreationDate", beginDate));
            var incident = criteria.UniqueResult<IIncident>();

            if (incident != null)
            {
                return incident;
            }
            else
            {
                return InterfaceActivator.Create<IIncident>();
            }
        }
    }
}
