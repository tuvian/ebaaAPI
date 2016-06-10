using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class Login
    {
        public string ID { get; set; }
        public string school_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string type { get; set; }
        public string user_id { get; set; }
        public string status { get; set; }
        public string created_date { get; set; }
        public string modified_date { get; set; }        
        public string loginMessage { get; set; }
        public string deviceid { get; set; }
    }

    public class LoginHeader
    {
        //public Teacher teacherdetails { get; set; }
        public IList<Teacher> teacherdetails { get; set; }
        public Login logindetails { get; set; }
        public School schooldetails { get; set; }
        public Student studentDetails { get; set; }
        public Driver driverDetails { get; set; }        
    }

}