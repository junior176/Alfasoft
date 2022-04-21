using Alfasoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alfasoft.Model
{
    public class User
    {
        public Links links { get; set; }
        public string display_name { get; set; }
        public string account_id { get; set; }
        public string nickname { get; set; }
        public string created_on { get; set; }
        public string is_staff { get; set; }
        public string location { get; set; }
        public string account_status { get; set; }
        public string type { get; set; }
        public string uuid { get; set; }
        public string has_2fa_enabled { get; set; }

    }
}
