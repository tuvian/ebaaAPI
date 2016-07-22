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
    public class LoginController : ApiController
    {
        Login login = new Login();
        Teacher teacher = new Teacher();
        List<Teacher> teachers = new List<Teacher>();
        School schooldetails = new School();
        Student student = new Student();
        Driver driver = new Driver();
        LoginHeader loginHeader = new LoginHeader();

        //public LoginHeader GetLogin()
        //{
        //    loginHeader = new LoginHeader();
        //    return loginHeader;
        //}

        public LoginHeader GetLogin(string uname, string pwd, string sid, string did)
        {
            userLogin(uname, pwd, sid, did);
            loginHeader.teacherdetails = teachers;
            loginHeader.logindetails = login;
            loginHeader.schooldetails = schooldetails;
            loginHeader.studentDetails = student;
            loginHeader.driverDetails = driver;
            return loginHeader;
        }

        private void userLogin(string username, string password, string schoolid, string deviceid)
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();

            string sqlQuery = "SELECT * FROM login WHERE login.userName = '" + username + "'" +
                    " AND login.password = '" + password + "' and school_id = " + schoolid;

            dt = objDAL.ExecuteDataTable(sqlQuery);
            if (dt.Rows.Count >= 1)
            {
                int i = 0;
                if (dt.Rows[i]["type"].ToString() == "4" && dt.Rows[i]["device_id"].ToString() != deviceid ) //type 4 - student
                {
                    login.loginMessage = "Invalid device ID";
                }
                else
                {
                    login.ID = dt.Rows[i]["ID"].ToString();
                    login.username = dt.Rows[i]["username"].ToString();
                    login.password = dt.Rows[i]["password"].ToString();
                    login.status = dt.Rows[i]["status"].ToString();
                    login.type = dt.Rows[i]["type"].ToString();
                    login.user_id = dt.Rows[i]["user_id"].ToString();
                    login.created_date = dt.Rows[i]["created_date"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["created_date"]).ToString("yyyy-MM-dd");
                    login.modified_date = dt.Rows[i]["modified_date"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["modified_date"]).ToString("yyyy-MM-dd");  
                    login.school_id = dt.Rows[i]["school_id"].ToString();
                    login.deviceid = dt.Rows[i]["device_id"].ToString();
                    login.loginMessage = "Success";

                    if (login.type == "3") //type 3 - teacher
                        getTeacherDetails(login.user_id, "id");
                    if (login.school_id != "0")
                        getSchoolDetails(login.school_id);
                    if (login.type == "4") // type 4 - student
                    {
                        getStudentDetails(login.user_id);
                        getTeacherDetails(student.class_id, "class_id");
                        if (student.driver_id != "")
                            getDriverDetails(student.driver_id);
                    }
                    if (login.type == "5") // type 5 - driver
                    {
                        getDriverDetails(login.user_id);
                    }
                }
            }
            else
            {
                //Check new Student try to login
                DataTable dtStudent = new DataTable();
                string sqlQuery1 = "SELECT s.id as user_id,s.student_id,l.username,l.id as login_id " +
                                    " FROM student s LEFT JOIN login l ON s.id = l.user_id and l.type = 4 WHERE l.id is NULL AND s.student_id = '" + username + "'";;
                dtStudent = objDAL.ExecuteDataTable(sqlQuery1);
                if(dtStudent.Rows.Count >= 1)
                {
                    int student_id = Convert.ToInt32(dtStudent.Rows[0]["user_id"].ToString());
                    createNewStudentUser(username, password, schoolid, deviceid, student_id);
                    deactivatePassword(password, schoolid);
                }
                else
                    login.loginMessage = "Invalid username or pasword";
            }
        }

        private void getDriverDetails(string driver_id)
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            
            string sqlQuery = "SELECT d.id,d.name,d.school_id,d.mobile,d.address,l.id as login_id,l.username,l.password,b.id as bus_id, b.bus_number, b.rout " +
                "FROM driver d INNER JOIN school s ON d.school_id = s.id LEFT JOIN login l ON d.id = l.user_id AND l.type = 5 " +
                "LEFT JOIN bus b ON b.driver_id = d.id WHERE d.id = " + driver_id;
            dt = objDAL.ExecuteDataTable(sqlQuery);

            int i = 0;
            driver.ID = dt.Rows[i]["ID"].ToString();
            driver.name = dt.Rows[i]["name"].ToString();
            driver.mobile = dt.Rows[i]["mobile"].ToString();
            driver.address = dt.Rows[i]["address"].ToString();
            driver.bus_id = dt.Rows[i]["bus_id"].ToString();
            driver.bus_number = dt.Rows[i]["bus_number"].ToString();
            driver.bus_rout = dt.Rows[i]["rout"].ToString();
        }

        private void deactivatePassword(string password, string schoolid)
        {
            DAL objDAL = new DAL();
            string sqlQuery = "UPDATE password SET is_taken = 1 WHERE school_id = " + schoolid + " AND password = '" + password + "'";
            objDAL.ExecuteNonQuery(sqlQuery);
        }

        private void createNewStudentUser(string username, string password, string schoolid, string deviceid, int student_id)
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            string type = "4";
            string sqlQuery = "SELECT p.id, p.password, p.school_id, p.is_taken FROM password p WHERE p.is_taken = 0 AND p.school_id = " + schoolid + " AND password ='" + password + "'";
            dt = objDAL.ExecuteDataTable(sqlQuery);
            if (dt.Rows.Count >= 1)
            {
                string sqlQuery3 = "INSERT INTO login (username,password,type,user_id,created_date,school_id,device_id) VALUES(" +
                            "'" + username + "','" + password + "','" + type  + "'," + student_id + ",now()," +
                            "" + schoolid + ",'" + deviceid + "')";
                objDAL.ExecuteNonQuery(sqlQuery3);
                userLogin(username, password, schoolid, deviceid);
            }
            else
            {
                login.loginMessage = "The provided password is not active. Please contact school Administrator";
            }

        }

        private void getStudentDetails(string studentid)
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();

            string sqlQuery = "SELECT t.id,t.school_id,t.student_id,t.first_name,t.mobile,t.email,t.present_address,t.permenant_address,t.father_name, t.mother_name, " +
                    "t.contact_mobile, t.contact_email,t.gender, t.wilayath,t.waynumber,t.nationality,t.middle_name,t.family_name,t.gender,t.image_path,t.status, " +
                    "cls.id as class_id, cls.std as class_std, cls.division as class_division, b.id as bus_id, b.bus_number, b.driver_id " + 
                    "FROM student t INNER JOIN class cls ON t.class_id = cls.ID LEFT JOIN bus b ON t.bus_id = b.id WHERE t.id = '" + studentid + "'";
            dt = objDAL.ExecuteDataTable(sqlQuery);

            ////for (int i = 0; i < dt.Rows.Count; i++)
            ////{
            ////    student.Add(new Student
            ////    {
            int i = 0;
            student.ID = dt.Rows[i]["ID"].ToString();
            student.studentID = dt.Rows[i]["student_id"].ToString();
            student.mobile = dt.Rows[i]["mobile"].ToString();
            student.email = dt.Rows[i]["email"].ToString();
            student.present_address = dt.Rows[i]["present_address"].ToString();
            student.permenant_address = dt.Rows[i]["permenant_address"].ToString();
            student.status = dt.Rows[i]["status"].ToString();
            student.fathername = dt.Rows[i]["father_name"].ToString();
            student.image_path = dt.Rows[i]["image_path"].ToString();
            student.mothername = dt.Rows[i]["mother_name"].ToString();
            student.contactmobile = dt.Rows[i]["contact_mobile"].ToString();
            student.nationality = dt.Rows[i]["nationality"].ToString();
            student.contactemail = dt.Rows[i]["contact_email"].ToString();
            student.gender = dt.Rows[i]["gender"].ToString();
            student.wilayath = dt.Rows[i]["wilayath"].ToString();
            student.waynumber = dt.Rows[i]["waynumber"].ToString();
            student.familyname = dt.Rows[i]["family_name"].ToString();
            student.middlename = dt.Rows[i]["middle_name"].ToString();
            student.class_id = dt.Rows[i]["class_id"].ToString();
            student.class_division = dt.Rows[i]["class_division"].ToString();
            student.class_std = dt.Rows[i]["class_std"].ToString();
            student.schoolId = dt.Rows[i]["school_id"].ToString();
            student.bus_id = dt.Rows[i]["bus_id"].ToString();
            student.bus_number = dt.Rows[i]["bus_number"].ToString();
            student.driver_id = dt.Rows[i]["driver_id"].ToString();
            //});
            //}
        }

        private void getSchoolDetails(string p)
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            string sqlQuery = "SELECT s.id,s.code,s.name,s.site_url,s.address,s.contact_person,s.mobile,s.email,s.phone,s.contact_address, " +
                "s.nationality, s.package_id, s.register_date, s.expire_date, s.logo, s.status, s.notes, p.name as package, s.wilayath, s.waynumber " +
                "FROM school s INNER JOIN package p ON s.package_id = p.id ";
            dt = objDAL.ExecuteDataTable(sqlQuery);

            int i = 0;
            schooldetails.ID = dt.Rows[i]["ID"].ToString();
            schooldetails.name = dt.Rows[i]["name"].ToString();
            schooldetails.mobile = dt.Rows[i]["mobile"].ToString();
            schooldetails.email = dt.Rows[i]["email"].ToString();
            schooldetails.code = dt.Rows[i]["code"].ToString();
            schooldetails.siteurl = dt.Rows[i]["site_url"].ToString();
            schooldetails.status = dt.Rows[i]["status"].ToString();
            schooldetails.contact_person = dt.Rows[i]["contact_person"].ToString();
            schooldetails.phone = dt.Rows[i]["phone"].ToString();
            schooldetails.contact_address = dt.Rows[i]["contact_address"].ToString();
            schooldetails.nationality = dt.Rows[i]["nationality"].ToString();
            schooldetails.package_id = dt.Rows[i]["package_id"].ToString();
            schooldetails.register_date = Convert.ToDateTime(dt.Rows[i]["register_date"]).ToString("yyyy-MM-dd");
            schooldetails.expire_date = Convert.ToDateTime(dt.Rows[i]["expire_date"]).ToString("yyyy-MM-dd");
            schooldetails.logo = dt.Rows[i]["logo"].ToString();
            schooldetails.notes = dt.Rows[i]["notes"].ToString();
            schooldetails.package = dt.Rows[i]["package"].ToString();
            schooldetails.wilayath = dt.Rows[i]["wilayath"].ToString();
            schooldetails.waynumber = dt.Rows[i]["waynumber"].ToString();
        }
        
        private void getTeacherDetails(string filtervalue, string filtertype)
        {
            getTeacherDetails();
            if(filtertype == "id")
                teachers = teachers.Where(p => p.ID == filtervalue).ToList<Teacher>();            
            if(filtertype == "class_id")
                teachers = teachers.Where(p => p.class_id == filtervalue).ToList<Teacher>();            
        }
        
        private void getTeacherDetails()
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            string sqlQuery = "SELECT t.id,t.code,t.name,t.mobile,t.email,t.present_address,t.permenant_address,t.status,t.qualification, t.image_path, " +
                    "dep.name as department, des.id as designation_id,des.name as designation, dep.id as department_id,t.nationality, t.school_id, " +
                    "cls.std as class_std, cls.division as class_division, cls.id as class_id " +
                    "FROM teacher t INNER JOIN department dep ON t.department_id = dep.id INNER JOIN designation des ON des.id = t.designation_id " +
                    "INNER JOIN class cls ON cls.id = t.class_id ";
            dt = objDAL.ExecuteDataTable(sqlQuery);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
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
                    class_id = dt.Rows[i]["class_id"].ToString(),
                    class_division = dt.Rows[i]["class_division"].ToString(),
                    class_std = dt.Rows[i]["class_std"].ToString(),

                });
            }
        }

        public APIStatus getGoogleRegID(string loginid, string regid)
        {
            APIStatus api_status = new APIStatus();
            try
            {
                DAL objDAL = new DAL();
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;

                string updateMessage = "Success";

                int login_id = 0;
                int.TryParse(loginid, out login_id);

                if (login_id == 0 || regid == null )
                    updateMessage = "Please pass valid from login ID/ regID";
                else
                {
                    sqlQuery = "UPDATE login SET google_regid = '" + regid + "' WHERE id = " + login_id;
                    objDAL.ExecuteNonQuery(sqlQuery);
                }
                api_status.api_status = updateMessage;
                return api_status;
            }
            catch (Exception ex)
            {
                api_status.api_status = ex.Message;
                return api_status;
            }
        }

    }
}
