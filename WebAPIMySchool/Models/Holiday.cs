using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class Holiday
    {
        public string ID { get; set; }
        public string school_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string modified_date { get; set; }
        public string modified_by { get; set; }
    }

    public class HolidayHeader
    {
        public IList<Holiday> holidaydetails { get; set; }
    }
}