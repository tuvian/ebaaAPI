using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class Bus
    {
        public string ID { get; set; }
        public string school_id { get; set; }
        public string bus_number { get; set; }
        public string rout { get; set; }
        public string driver { get; set; }
        public string mobile { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string accuracy { get; set; }
        public string modified_date { get; set; }
    }

    public class BusHeader
    {
        public IList<Bus> busdetails { get; set; }
    }
}