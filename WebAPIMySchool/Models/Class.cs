using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class Class
    {
        public string ID { get; set; }
        public string school_id { get; set; }
        public string std { get; set; }
        public string division { get; set; }
        public string status { get; set; }
    }

    public class ClassHeader
    {
        public IList<Class> classdetails { get; set; }
    }
}