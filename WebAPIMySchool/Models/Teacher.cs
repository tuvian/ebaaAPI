using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class Teacher
    {
        public string ID { get; set; }
        public string school_id { get; set; }
        public string department_id { get; set; }
        public string designation_id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string qualification { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string permenant_address { get; set; }
        public string status { get; set; }
        public string nationality { get; set; }
        public string present_address { get; set; }
        public string image_path { get; set; }
        public string notes { get; set; }
        public string search_operator { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string class_id { get; set; }
        public string class_std { get; set; }
        public string class_division { get; set; }

    }

    public class TeacherHeader
    {
        public IList<Teacher> teacherdetails { get; set; }
    }
}