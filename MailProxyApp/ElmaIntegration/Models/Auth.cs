using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmaIntegration.Models
{
    public class Auth
    {
        public string AuthToken { get; set; }
        public string CurrentUserId { get; set; }
        public string SessionToken { get; set; }
    }
}
