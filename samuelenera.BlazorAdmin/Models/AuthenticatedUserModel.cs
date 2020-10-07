using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samuelenera.BlazorAdmin.Models
{
    public class AuthenticatedUserModel
    {
        public string token { get; set; }
        public string user_email { get; set; }
        public string user_nicename { get; set; }
        public string user_display_name { get; set; }
    }
}
