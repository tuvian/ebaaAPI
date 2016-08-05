using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class Notification
    {
        public class pushnotification
        {
            public string school_id { get; set; }
            public string fid { get; set; }
            public string fname { get; set; }
            public string ftype { get; set; }
            public string sub { get; set; }
            public string msg { get; set; }
            public string smembers { get; set; }
            public string tmembers { get; set; }
            public string sclass { get; set; }
            public string tclass { get; set; }
            public string imagestring { get; set; }
            public string filename { get; set; }
        }
    }
}