using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class Events
    {
        public string ID { get; set; }
        public string school_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string modified_date { get; set; }
    }

    public class EventHeader
    {
        public IList<Events> eventdetails { get; set; }
    }
}