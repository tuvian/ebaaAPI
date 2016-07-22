using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        ////public APIStatus GetStudentLocation(string scid,string stid, string lat, string lon, string acrcy)
        ////{
        ////    DAL objDAL = new DAL();
        ////    string returnValue = string.Empty;
        ////    string sqlQuery = string.Empty;
        ////    APIStatus apiStatus = new APIStatus();

        ////    string updateMessage = "Success";

        ////    int school_id = 0;
        ////    int.TryParse(scid, out school_id);
        ////    int student_id = 0;
        ////    int.TryParse(stid, out student_id);

        ////    if (school_id == 0 || student_id == 0)
        ////        updateMessage = "Please pass valid student ID/ School ID";
        ////    else
        ////    {
        ////        sqlQuery = "UPDATE student SET latitude = '" + lat + "', longitude = '" + lon + "', accuracy = '" + acrcy + "' WHERE " +
        ////        "id = '" + student_id + "' AND school_id = '" + school_id + "'";
        ////        objDAL.ExecuteNonQuery(sqlQuery);
        ////    }
        ////    apiStatus.api_status = updateMessage;
        ////    return apiStatus;
        ////}

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public APIStatus SaveStudentLocation(string scid, string stid, string lat, string lon, string acrcy)
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
                    " FROM  student t INNER JOIN class cls ON t.class_id = cls.ID ";
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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public APIStatus SaveStudentProfile(string scid, string stid, string fname, string mname, string fmname, string cmob, string cemail, string peradd, string preadd, string fatname, string motname, string wilayat, string wayno, string gender)
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
            else if (fname == "" || mname == "" || fmname == "" || cmob == "" || cemail == "" || peradd == "" || preadd == "" || fatname == "" || motname == "" || wilayat == "" || wayno == "" || gender == "")
                updateMessage = "All fields are mandatory.";
            else if (fname == null || mname == null || fmname == null || cmob == null || cemail == null || peradd == null || preadd == null || fatname == null || motname == null || wilayat == null || wayno == null || gender == null)
                updateMessage = "All fields are mandatory.";
            else
            {
                sqlQuery = "UPDATE student SET first_name = '" + fname + "', middle_name = '" + mname + "', family_name = '" + fmname + "', " +
                    "contact_mobile = '" + cmob + "', contact_email = '" + cemail + "', permenant_address = '" + peradd + "', " +
                    "present_address = '" + preadd + "', father_name = '" + fatname + "', mother_name = '" + motname + "', " +
                    "wilayath = '" + wilayat + "', waynumber = '" + wayno + "', gender = '" + gender + "' WHERE " +
                    "id = '" + stid + "' AND school_id = '" + scid + "'";
                objDAL.ExecuteNonQuery(sqlQuery);
            }
            apiStatus.api_status = updateMessage;
            return apiStatus;
        }

        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[System.Web.Http.HttpPost]
        //public APIStatus UpdateStudentProfilePhoto(string scid, string stid, string imgstr)
        //{
        //    DAL objDAL = new DAL();
        //    string returnValue = string.Empty;
        //    string sqlQuery = string.Empty;
        //    APIStatus apiStatus = new APIStatus();

        //    string updateMessage = "Success";

        //    int school_id = 0;
        //    int.TryParse(scid, out school_id);
        //    int student_id = 0;
        //    int.TryParse(stid, out student_id);

        //    if (school_id == 0 || student_id == 0)
        //        updateMessage = "Please pass valid student ID/ School ID";
        //    else
        //    {
        //        byte[] imageBytes = Convert.FromBase64String(imgstr);
        //        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        //        ms.Write(imageBytes, 0, imageBytes.Length);
        //        System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        //        image.Save("http://ebaa.co/dev.ebaa.co/student_image/Hello.jpg");
        //        image.Save("ftp://ebaauser@ebaa.co/dev.ebaa.co/student_image/DP3.png");

        //        //sqlQuery = "UPDATE student SET latitude = '" + lat + "', longitude = '" + lon + "', accuracy = '" + acrcy + "' WHERE " +
        //        //"id = '" + student_id + "' AND school_id = '" + school_id + "'";
        //        //objDAL.ExecuteNonQuery(sqlQuery);
        //    }
        //    apiStatus.api_status = updateMessage;
        //    return apiStatus;
        //}

        //[AcceptVerbs("GET", "POST")]
        //[ActionName("ProfilePhotoTest")]
        //[System.Web.Http.HttpPost]
        //public APIStatus ProfilePhotoTest(StudentProfile sp)
        //{
        //    APIStatus ap = new APIStatus();
        //    string base64Status = "Valid Image";
        //    try
        //    {
        //        byte[] imageBytes = Convert.FromBase64String(sp.imagestring);
        //        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        //        ms.Write(imageBytes, 0, imageBytes.Length);
        //        System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        //    }
        //    catch
        //    {
        //        base64Status = "Invalid Image";
        //    }

        //    ap.api_status = "ID = " + sp.id + " schoolid = " + sp.school_id + " Image Status = " + base64Status;
        //    return ap;
        //}

        //[AcceptVerbs("GET", "POST")]
        //[ActionName("byteArrayToImage")]
        //[System.Web.Http.HttpPost]
        //public string byteArrayToImage(string photos, string filenames)
        //{
        //    string success = "false";
        //    string photo = "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";
        //    //string filename = System.Web.HttpContext.Current.Server.MapPath("../../../dev.ebaa.co/student_image/") + filenames;
        //    //string filename = "http://dev.ebaa.co/student_image/" + filenames;
                        
        //    FileStream str = null;
        //    //System.Drawing.Image newImage;

        //    string root = System.Web.HttpContext.Current.Server.MapPath("~");
        //    string parent = Path.GetDirectoryName(root);
        //    string grandParent = Path.GetDirectoryName(parent);
        //    string path = grandParent + @"\dev.ebaa.co\student_image\";
        //    //return "ROOT : " + root + " PARENT : " + parent + " grantparent : " + grandParent + " FPATH: " + path;
        //    string filename = path + filenames;
        //    try
        //    {
        //        byte[] byteArrayIn = Convert.FromBase64String(photo);

        //        if (byteArrayIn != null)
        //        {

        //            //using (MemoryStream stream = new MemoryStream(byteArrayIn))
        //            //{
        //            //    newImage = System.Drawing.Image.FromStream(stream);
        //            //    newImage.Save(filename);
        //            //}
        //            if (File.Exists(filename))
        //            {
        //                using (str = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
        //                {
        //                    //WriteFast(str, byteArrayIn, 0, byteArrayIn.Length);
        //                    str.Write(byteArrayIn, 0, Convert.ToInt32(byteArrayIn.Length));
        //                    success = "true";

        //                }
        //            }
        //            else
        //            {
        //                using (str = new FileStream(filename, FileMode.Create))
        //                {
        //                    // WriteFast(str, byteArrayIn, 0, byteArrayIn.Length);
        //                    str.Write(byteArrayIn, 0, Convert.ToInt32(byteArrayIn.Length));
        //                    success = "true";

        //                }
        //            }
        //        }
        //        else
        //        {
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Error(ex);
        //        success = "false " + ex.Message;
        //    }
        //    finally
        //    {
        //        //str.Close();
        //        //byteArrayIn = null;
        //    }
        //    return success + " path : " + filename;
        //}

        ////[AcceptVerbs("GET", "POST")]
        ////[ActionName("TestProfilePhotoTest2")]
        ////[System.Web.Http.HttpPost]
        ////public APIStatus TestProfilePhotoTest2(StudentProfile sp)
        ////{
        ////    APIStatus ap = new APIStatus();
        ////    string base64Status = "2nd Fn : Valid Image";
        ////    try
        ////    {
        ////        byte[] imageBytes = Convert.FromBase64String(sp.imagestring);
        ////        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        ////        ms.Write(imageBytes, 0, imageBytes.Length);
        ////        System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        ////    }
        ////    catch
        ////    {
        ////        base64Status = "I2nd Fn : nvalid Image";
        ////    }

        ////    ap.api_status = "2nd Fn : ID = " + sp.id + " schoolid = " + sp.school_id + " Image Status = " + base64Status;
        ////    return ap;
        ////}

        [AcceptVerbs("GET", "POST")]
        [ActionName("UpdateStudentPhoto")]
        [System.Web.Http.HttpPost]
        public APIStatus UpdateStudentPhoto(StudentProfile sp)
        {
            APIStatus ap = new APIStatus();
            ap.api_status = "";
            FileStream str = null;

            string root = System.Web.HttpContext.Current.Server.MapPath("~");
            string parent = Path.GetDirectoryName(root);
            string grandParent = Path.GetDirectoryName(parent);
            string path = grandParent + @"\dev.ebaa.co\student_image\";
            string filename = path + sp.filename;

            int school_id = 0;
            int.TryParse(sp.school_id, out school_id);
            int student_id = 0;
            int.TryParse(sp.id, out student_id);
            if (school_id == 0 || student_id == 0)
            {
                ap.api_status = "Please pass valid student ID/ School ID";
                return ap;
            }

            try
            {
                byte[] byteArrayIn = Convert.FromBase64String(sp.imagestring);
                if (byteArrayIn != null)
                {
                    if (File.Exists(filename))
                    {
                        using (str = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
                        {
                            //WriteFast(str, byteArrayIn, 0, byteArrayIn.Length);
                            str.Write(byteArrayIn, 0, Convert.ToInt32(byteArrayIn.Length));
                            ap.api_status = "Success";
                        }
                    }
                    else
                    {
                        using (str = new FileStream(filename, FileMode.Create))
                        {
                            // WriteFast(str, byteArrayIn, 0, byteArrayIn.Length);
                            str.Write(byteArrayIn, 0, Convert.ToInt32(byteArrayIn.Length));
                            ap.api_status = "Success";

                        }
                    }
                }
                else
                {
                    ap.api_status = "Failure :" + " Please send valid Base64 string";
                }

                if (ap.api_status == "Success")
                {
                    DAL objDAL = new DAL();
                    string sqlQuery = "UPDATE student SET image_path = '" + sp.filename + "' WHERE " +
                        "id = '" + student_id + "' AND school_id = '" + school_id + "'";
                    objDAL.ExecuteNonQuery(sqlQuery);
                }

                //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                //ms.Write(imageBytes, 0, imageBytes.Length);
                //System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            }
            catch (Exception ex)
            {
                ap.api_status = "Faliure : " + ex.Message;
            }

            //ap.api_status = "ID = " + sp.id + " schoolid = " + sp.school_id + " Image Status = " + base64Status;
            return ap;
        }
    }
}
