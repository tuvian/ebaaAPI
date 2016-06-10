using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class Driver
    {
        public string ID { get; set; }
        public string school_id { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string bus_id { get; set; }
        public string bus_number { get; set; }
        public string bus_rout { get; set; }
    }

    
}