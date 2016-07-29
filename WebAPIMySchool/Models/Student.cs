using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class Student
    {
        public string ID { get; set; }
        public string studentID { get; set; }
        public string firstname { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string dob { get; set; }
        public string fathername { get; set; }
        public string mothername { get; set; }
        public string contactemail { get; set; }
        public string contactmobile { get; set; }
        public string nationality { get; set; }
        public string permenant_address { get; set; }
        public string present_address { get; set; }
        public string studentclass { get; set; }
        public string wilayath { get; set; }
        public string waynumber { get; set; }
        public string middlename { get; set; }
        public string familyname { get; set; }
        public string gender { get; set; }
        public string schoolId { get; set; }
        public string search_operator { get; set; }
        public string status { get; set; }
        public string image_path { get; set; }
        public string class_id { get; set; }
        public string class_std { get; set; }
        public string class_division { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string accuracy { get; set; }
        public string bus_id { get; set; }
        public string bus_number { get; set; }
        public string driver_id { get; set; }
        public string login_id { get; set; }
    }

    public class StudentProfile
    {
        public string id { get; set; }
        public string school_id { get; set; }
        public string imagestring { get; set; }
        public string filename { get; set; }
    }

    public class StudentHeader
    {
        public IList<Student> studentdetails { get; set; }
    }
}