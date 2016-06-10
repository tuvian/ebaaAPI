using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class School
    {
        public string ID { get; set; }
        public string package_id { get; set; }
        public string package { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string siteurl { get; set; }
        public string school_address { get; set; }
        public string contact_person { get; set; }
        public string mobile { get; set; }
        public string status { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string contact_address { get; set; }
        public string nationality { get; set; }
        public string register_date { get; set; }
        public string expire_date { get; set; }
        public string logo { get; set; }
        public string notes { get; set; }
        public string search_operator { get; set; }
        public string wilayath { get; set; }
        public string waynumber { get; set; }

    }
}