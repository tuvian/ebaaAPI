using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIMySchool.Models;

namespace WebAPIMySchool.Controllers
{
    public class TeacherController : ApiController
    {
        List<Teacher> teachers = new List<Teacher>();
        TeacherHeader teacherlist = new TeacherHeader();

        public TeacherHeader GetAllTeachers(string school_id)
        {
            if (teachers.Count() > 0)
            {
                teachers = teachers.Where(p => p.school_id == school_id).ToList<Teacher>();
                teacherlist.teacherdetails = teachers;
                return teacherlist;
            }
            else
            {
                GetTeachers();
                teachers = teachers.Where(p => p.school_id == school_id).ToList<Teacher>();
                teacherlist.teacherdetails = teachers;
                return teacherlist;
            }
        }

        public TeacherHeader GetAllTeachers(string school_id, string class_id)
        {
            TeacherHeader teacherlist = new TeacherHeader();
            teacherlist.teacherdetails = GetTeachersByClassID(school_id, class_id);
            return teacherlist;
            //if (teachers.Count() > 0)
            //{
            //    teachers = teachers.Where(p => p.school_id == school_id).ToList<Teacher>();
            //    teachers = teachers.Where(p => p.class_id == class_id).ToList<Teacher>();                
            //    teacherlist.teacherdetails = teachers;
            //    return teacherlist;
            //}
            //else
            //{
            //    GetTeachers();
            //    teachers = teachers.Where(p => p.school_id == school_id).ToList<Teacher>();
            //    teachers = teachers.Where(p => p.assignedClasses == class_id).ToList<Teacher>();
            //    teacherlist.teacherdetails = teachers;
            //    return teacherlist;
            //}

        }

        private void GetTeachers()
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            string sqlQuery = "SELECT t.id,t.code,t.name,t.mobile,t.email,t.present_address,t.permenant_address,t.status,t.qualification, t.image_path, " +
                    "dep.name as department, des.id as designation_id,des.name as designation, dep.id as department_id,t.nationality, t.school_id, " +
                    "cls.std as class_std, cls.division as class_division, cls.id as class_id, l.id as login_id " +
                    "FROM teacher t INNER JOIN department dep ON t.department_id = dep.id INNER JOIN designation des ON des.id = t.designation_id " +
                //"INNER JOIN class cls ON cls.id = t.class_id " 
                    " LEFT JOIN login l ON t.id = l.user_id AND l.type = 3 " +
                    " LEFT JOIN teacher_class tc ON tc.teacher_id = t.id  LEFT JOIN class cls ON cls.id = tc.class_id ORDER BY id;";

            dt = objDAL.ExecuteDataTable(sqlQuery);
            string teacherID = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                teacherID = dt.Rows[i]["ID"].ToString();
                DataRow[] assignedClasses;
                assignedClasses = dt.Select("id=" + teacherID);
                IList<Class> a_classes = new List<Class>();
                for (int j = 0; j < assignedClasses.Length; j++)
                {
                    a_classes.Add(new Class
                    {
                        ID = assignedClasses[j]["class_id"].ToString(),
                        division = assignedClasses[j]["class_division"].ToString(),
                        std = assignedClasses[j]["class_std"].ToString(),
                        school_id = assignedClasses[j]["school_id"].ToString(),
                        status = assignedClasses[j]["status"].ToString()
                    });
                }
                teachers.Add(new Teacher
                {
                    ID = dt.Rows[i]["ID"].ToString(),
                    name = dt.Rows[i]["name"].ToString(),
                    mobile = dt.Rows[i]["mobile"].ToString(),
                    email = dt.Rows[i]["email"].ToString(),
                    present_address = dt.Rows[i]["present_address"].ToString(),
                    permenant_address = dt.Rows[i]["permenant_address"].ToString(),
                    status = dt.Rows[i]["status"].ToString(),
                    qualification = dt.Rows[i]["qualification"].ToString(),
                    image_path = dt.Rows[i]["image_path"].ToString(),
                    department_id = dt.Rows[i]["department_id"].ToString(),
                    designation_id = dt.Rows[i]["designation_id"].ToString(),
                    nationality = dt.Rows[i]["nationality"].ToString(),
                    school_id = dt.Rows[i]["school_id"].ToString(),
                    department = dt.Rows[i]["department"].ToString(),
                    designation = dt.Rows[i]["designation"].ToString(),
                    //class_id = dt.Rows[i]["class_id"].ToString(),
                    //class_division = dt.Rows[i]["class_division"].ToString(),
                    //class_std = dt.Rows[i]["class_std"].ToString(),
                    assignedClasses = a_classes,
                    login_id = dt.Rows[i]["login_id"].ToString()
                });
            }
        }

        private List<Teacher> GetTeachersByClassID(string school_id, string class_id)
        {
            List<Teacher> teachers = new List<Teacher>();
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            string sqlQuery = "SELECT t.id,t.code,t.name,t.mobile,t.email,t.present_address,t.permenant_address,t.status,t.qualification, t.image_path, " +
                    "dep.name as department, des.id as designation_id,des.name as designation, dep.id as department_id,t.nationality, t.school_id, " +
                    "cls.std as class_std, cls.division as class_division, cls.id as class_id, l.id as login_id " +
                    "FROM teacher t INNER JOIN department dep ON t.department_id = dep.id INNER JOIN designation des ON des.id = t.designation_id " +
                //"INNER JOIN class cls ON cls.id = t.class_id " 
                    " LEFT JOIN login l ON t.id = l.user_id AND l.type = 3 " +
                    " LEFT JOIN teacher_class tc ON tc.teacher_id = t.id  LEFT JOIN class cls ON cls.id = tc.class_id " +
                    " WHERE t.school_id ='" + school_id + "' AND tc.class_id = '" + class_id + "' ORDER BY id ";

            dt = objDAL.ExecuteDataTable(sqlQuery);
            string teacherID = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                teacherID = dt.Rows[i]["ID"].ToString();
                DataRow[] assignedClasses;
                assignedClasses = dt.Select("id=" + teacherID);
                IList<Class> a_classes = new List<Class>();
                for (int j = 0; j < assignedClasses.Length; j++)
                {
                    a_classes.Add(new Class
                    {
                        ID = assignedClasses[j]["class_id"].ToString(),
                        division = assignedClasses[j]["class_division"].ToString(),
                        std = assignedClasses[j]["class_std"].ToString(),
                        school_id = assignedClasses[j]["school_id"].ToString(),
                        status = assignedClasses[j]["status"].ToString()
                    });
                }
                teachers.Add(new Teacher
                {
                    ID = dt.Rows[i]["ID"].ToString(),
                    name = dt.Rows[i]["name"].ToString(),
                    mobile = dt.Rows[i]["mobile"].ToString(),
                    email = dt.Rows[i]["email"].ToString(),
                    present_address = dt.Rows[i]["present_address"].ToString(),
                    permenant_address = dt.Rows[i]["permenant_address"].ToString(),
                    status = dt.Rows[i]["status"].ToString(),
                    qualification = dt.Rows[i]["qualification"].ToString(),
                    image_path = dt.Rows[i]["image_path"].ToString(),
                    department_id = dt.Rows[i]["department_id"].ToString(),
                    designation_id = dt.Rows[i]["designation_id"].ToString(),
                    nationality = dt.Rows[i]["nationality"].ToString(),
                    school_id = dt.Rows[i]["school_id"].ToString(),
                    department = dt.Rows[i]["department"].ToString(),
                    designation = dt.Rows[i]["designation"].ToString(),
                    //class_id = dt.Rows[i]["class_id"].ToString(),
                    //class_division = dt.Rows[i]["class_division"].ToString(),
                    //class_std = dt.Rows[i]["class_std"].ToString(),
                    assignedClasses = a_classes,
                    login_id = dt.Rows[i]["login_id"].ToString()
                });
            }
            return teachers;
        }
    }
}
