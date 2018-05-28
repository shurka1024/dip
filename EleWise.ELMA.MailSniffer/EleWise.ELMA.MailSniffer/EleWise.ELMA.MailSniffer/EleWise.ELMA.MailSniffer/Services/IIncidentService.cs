using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleWise.ELMA.MailSniffer.Services
{
    public interface IIncidentService
    {
        void CreateMessage(long incidentId);
    }
}
