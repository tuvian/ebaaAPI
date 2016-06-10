using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebAPIMySchool.Models;

namespace WebAPIMySchool.Controllers
{
    public class StudentController : ApiController
    {
        List<Student> students = new List<Student>();
        StudentHeader studentlist = new StudentHeader();

        public StudentHeader GetAllStudents(string school_id)
        {
            if (students.Count() > 0)
            {
                students = students.Where(p => p.schoolId == school_id).ToList<Student>();
                studentlist.studentdetails = students;
                return studentlist;
            }
            else
            {
                GetStudents();
                students = students.Where(p => p.schoolId == school_id).ToList<Student>();
                studentlist.studentdetails = students;
                return studentlist;
            }
        }

        public StudentHeader GetAllStudents(string school_id, string class_id)
        
        {
            if (students.Count() > 0)
            {
                students = students.Where(p => p.schoolId == school_id).ToList<Student>();
                students = students.Where(p => p.class_id == class_id).ToList<Student>();
                studentlist.studentdetails = students;
                return studentlist;
            }
            else
            {
                GetStudents();
                students = students.Where(p => p.schoolId == school_id).ToList<Student>();
                students = students.Where(p => p.class_id == class_id).ToList<Student>();
                studentlist.studentdetails = students;
                return studentlist;
            }
        }

        public APIStatus GetStudentLocation(string scid,string stid, string lat, string lon, string acrcy)
        {
            DAL objDAL = new DAL();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            APIStatus apiStatus = new APIStatus();

            string updateMessage = "Success";

            int school_id = 0;
            int.TryParse(scid, out school_id);
            int student_id = 0;
            int.TryParse(stid, out student_id);

            if (school_id == 0 || student_id == 0)
                updateMessage = "Please pass valid student ID/ School ID";
            else
            {
                sqlQuery = "UPDATE student SET latitude = '" + lat + "', longitude = '" + lon + "', accuracy = '" + acrcy + "' WHERE " +
                "id = '" + student_id + "' AND school_id = '" + school_id + "'";
                objDAL.ExecuteNonQuery(sqlQuery);
            }
            apiStatus.api_status = updateMessage;
            return apiStatus;
        }

        private void GetStudents()
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();

            string sqlQuery = "SELECT t.id,t.school_id,t.student_id,t.first_name,t.mobile,t.email,t.present_address,t.permenant_address,t.father_name, t.mother_name, " +
                    "t.contact_mobile, t.contact_email,t.gender, t.wilayath,t.waynumber,t.nationality,t.middle_name,t.family_name,t.gender,t.image_path,t.status, " +
                    "cls.id as class_id, cls.std as class_std, cls.division as class_division,t.latitude, t.longitude, t.accuracy " + 
                    " FROM  student t INNER JOIN class cls ON t.class_id = cls.ID " ;
            dt = objDAL.ExecuteDataTable(sqlQuery);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                students.Add(new Student
                {
                    ID = dt.Rows[i]["ID"].ToString(),
                    studentID = dt.Rows[i]["student_id"].ToString(),
                    mobile = dt.Rows[i]["mobile"].ToString(),
                    email = dt.Rows[i]["email"].ToString(),
                    present_address = dt.Rows[i]["present_address"].ToString(),
                    permenant_address = dt.Rows[i]["permenant_address"].ToString(),
                    status = dt.Rows[i]["status"].ToString(),
                    fathername = dt.Rows[i]["father_name"].ToString(),
                    image_path = dt.Rows[i]["image_path"].ToString(),
                    mothername = dt.Rows[i]["mother_name"].ToString(),
                    contactmobile = dt.Rows[i]["contact_mobile"].ToString(),
                    nationality = dt.Rows[i]["nationality"].ToString(),
                    contactemail = dt.Rows[i]["contact_email"].ToString(),
                    gender = dt.Rows[i]["gender"].ToString(),
                    wilayath = dt.Rows[i]["wilayath"].ToString(),
                    waynumber = dt.Rows[i]["waynumber"].ToString(),
                    familyname = dt.Rows[i]["family_name"].ToString(),
                    middlename = dt.Rows[i]["middle_name"].ToString(),
                    class_id = dt.Rows[i]["class_id"].ToString(),
                    class_division = dt.Rows[i]["class_division"].ToString(),
                    class_std = dt.Rows[i]["class_std"].ToString(),
                    schoolId = dt.Rows[i]["school_id"].ToString(),
                    firstname = dt.Rows[i]["first_name"].ToString(),
                    latitude = dt.Rows[i]["latitude"].ToString(),
                    longitude = dt.Rows[i]["longitude"].ToString(),
                    accuracy = dt.Rows[i]["accuracy"].ToString(),
                });
            }
        }
    }
}
