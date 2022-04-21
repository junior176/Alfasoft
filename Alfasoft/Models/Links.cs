using System;
using System.Collections.Generic;
using System.Text;

namespace Alfasoft.Models
{
    public class Links
    {
        public LinksChild self { get; set; }
        public LinksChild html { get; set; }
        public LinksChild avatar { get; set; }
        public LinksChild followers { get; set; }
        public LinksChild following { get; set; }
        public LinksChild repositories { get; set; }
    }
}
