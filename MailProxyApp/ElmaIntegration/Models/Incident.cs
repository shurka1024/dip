using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmaIntegration.Models
{
    public class Incident
    {
        public Guid GuidFile { get; set; }

        public bool StreamIsBlocked { get; set; }
    }
}
