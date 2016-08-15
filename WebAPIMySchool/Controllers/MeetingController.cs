using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using WebAPIMySchool.Models;

namespace WebAPIMySchool.Controllers
{
    public class MeetingController : ApiController
    {
        List<Meeting> meetings = new List<Meeting>();
        MeetingHeader meetinglist = new MeetingHeader();

        List<MeetingTrn> meetingstatus = new List<MeetingTrn>();
        MeetingTrnHeader meetingstatuslist = new MeetingTrnHeader();

        //public MeetingHeader GetAllHoliday(string school_id, string student_id)
        //{
        //    if (meetings.Count() > 0)
        //    {
        //        meetings = meetings.Where(p => p.school_id == school_id).ToList<Meeting>();
        //        meetings = meetings.Where(p => p.student_id == student_id).ToList<Meeting>();
        //        meetinglist.meetingdetails = meetings;
        //        return meetinglist;
        //    }
        //    else
        //    {
        //        GetMeetings();
        //        meetings = meetings.Where(p => p.school_id == school_id).ToList<Meeting>();
        //        meetings = meetings.Where(p => p.student_id == student_id).ToList<Meeting>();
        //        meetinglist.meetingdetails = meetings;
        //        return meetinglist;
        //    }
        //}

        //public MeetingHeader GetMeetingByParticipat(string school_id, string partcpnt_id)
        //{
        //    DAL objDAL = new DAL();
        //    DataTable dt = new DataTable();
        //    string sqlQuery = "Select m.id, m.fromdate, m.todate, m.status, m.school_id, m.student_id, " +
        //            "m.participants, m.message, m.subject, m.comments from meeting m " +
        //            "INNER JOIN school s ON s.id = m.school_id " +
        //            " WHERE participants like '%," + partcpnt_id + "' or participants like '%," + partcpnt_id + ",%' OR participants like '" + partcpnt_id + ",%' OR participants like '%" + partcpnt_id + "%'";
        //    dt = objDAL.ExecuteDataTable(sqlQuery);

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        List<Teacher> participantList = new List<Teacher>();
        //        if (dt.Rows[i]["participants"].ToString() != "")
        //        {
        //            DataTable dt1 = new DataTable();
        //            string sqlQuery1 = "SELECT t.id,t.code,t.name,t.mobile,t.email,t.present_address,t.permenant_address,t.status,t.qualification, t.image_path, " +
        //                    "dep.name as department, des.id as designation_id,des.name as designation, dep.id as department_id,t.nationality, t.school_id, " +
        //                    "cls.std as class_std, cls.division as class_division, cls.id as class_id " +
        //                    "FROM teacher t INNER JOIN department dep ON t.department_id = dep.id INNER JOIN designation des ON des.id = t.designation_id " +
        //                    "INNER JOIN class cls ON cls.id = t.class_id WHERE t.id in (" + dt.Rows[i]["participants"].ToString() + ") ";

        //            dt1 = objDAL.ExecuteDataTable(sqlQuery1);

        //            for (int j = 0; j < dt1.Rows.Count; j++)
        //            {
        //                participantList.Add(new Teacher
        //                {
        //                    ID = dt1.Rows[j]["ID"].ToString(),
        //                    name = dt1.Rows[j]["name"].ToString(),
        //                    mobile = dt1.Rows[j]["mobile"].ToString(),
        //                    email = dt1.Rows[j]["email"].ToString(),
        //                    present_address = dt1.Rows[j]["present_address"].ToString(),
        //                    permenant_address = dt1.Rows[j]["permenant_address"].ToString(),
        //                    status = dt1.Rows[j]["status"].ToString(),
        //                    qualification = dt1.Rows[j]["qualification"].ToString(),
        //                    image_path = dt1.Rows[j]["image_path"].ToString(),
        //                    department_id = dt1.Rows[j]["department_id"].ToString(),
        //                    designation_id = dt1.Rows[j]["designation_id"].ToString(),
        //                    nationality = dt1.Rows[j]["nationality"].ToString(),
        //                    school_id = dt1.Rows[j]["school_id"].ToString(),
        //                    department = dt1.Rows[j]["department"].ToString(),
        //                    designation = dt1.Rows[j]["designation"].ToString(),
        //                    class_id = dt1.Rows[j]["class_id"].ToString(),
        //                    class_division = dt1.Rows[j]["class_division"].ToString(),
        //                    class_std = dt1.Rows[j]["class_std"].ToString(),

        //                });
        //            }
        //        }
        //        meetings.Add(new Meeting
        //        {
        //            ID = dt.Rows[i]["ID"].ToString(),
        //            student_id = dt.Rows[i]["student_id"].ToString(),
        //            subject = dt.Rows[i]["subject"].ToString(),
        //            message = dt.Rows[i]["message"].ToString(),
        //            fromdate = dt.Rows[i]["fromdate"].ToString(),
        //            todate = dt.Rows[i]["todate"].ToString(),
        //            status = dt.Rows[i]["status"].ToString(),
        //            school_id = dt.Rows[i]["school_id"].ToString(),
        //            participants = dt.Rows[i]["participants"].ToString(),
        //            comments = dt.Rows[i]["participants"].ToString(),
        //            participantList = participantList
        //        });
        //    }
        //    meetinglist.meetingdetails = meetings;
        //    return meetinglist;
        //}

        //public APIStatus GetAllHoliday(string scid, string stid, string fdate, string tdate, string sub, string msg, string empids)
        //{
        //    DAL objDAL = new DAL();
        //    string returnValue = string.Empty;
        //    string sqlQuery = string.Empty;
        //    APIStatus api_status = new APIStatus();

        //    string updateMessage = "Success";

        //    int school_id = 0;
        //    int.TryParse(scid, out school_id);
        //    int student_id = 0;
        //    int.TryParse(stid, out student_id);

        //    if (school_id == 0 || student_id == 0)
        //        updateMessage = "Please pass valid student ID/ School ID";
        //    else
        //    {
        //        string status = "REQUESTED";
        //        sqlQuery = "INSERT INTO meeting (school_id, student_id, fromdate, todate, status, subject, message, participants ) VALUES(" +
        //                "" + scid + "," + stid + ",'" + fdate + "', " +
        //                "'" + tdate + "','" + status + "','" + sub + "','" + msg + "','" + empids + "')";
        //        objDAL.ExecuteNonQuery(sqlQuery);
        //    }
        //    api_status.api_status = updateMessage;
        //    return api_status;
        //}

        //private void GetMeetings()
        //{
        //    DAL objDAL = new DAL();
        //    DataTable dt = new DataTable();
        //    string sqlQuery = "Select m.id, m.fromdate, m.todate, m.status, m.school_id, m.student_id, " +
        //            "m.participants, m.message, m.subject, m.comments from meeting m " +
        //            "INNER JOIN school s ON s.id = m.school_id ";
        //    dt = objDAL.ExecuteDataTable(sqlQuery);

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        List<Teacher> participantList = new List<Teacher>();
        //        if (dt.Rows[i]["participants"].ToString() != "")
        //        {                    
        //            DataTable dt1 = new DataTable();
        //            string sqlQuery1 = "SELECT t.id,t.code,t.name,t.mobile,t.email,t.present_address,t.permenant_address,t.status,t.qualification, t.image_path, " +
        //                    "dep.name as department, des.id as designation_id,des.name as designation, dep.id as department_id,t.nationality, t.school_id, " +
        //                    "cls.std as class_std, cls.division as class_division, cls.id as class_id " +
        //                    "FROM teacher t INNER JOIN department dep ON t.department_id = dep.id INNER JOIN designation des ON des.id = t.designation_id " +
        //                    "INNER JOIN class cls ON cls.id = t.class_id WHERE t.id in (" + dt.Rows[i]["participants"].ToString() + ") ";

        //            dt1 = objDAL.ExecuteDataTable(sqlQuery1);

        //            for (int j = 0; j < dt1.Rows.Count; j++)
        //            {
        //                participantList.Add(new Teacher
        //                {
        //                    ID = dt1.Rows[j]["ID"].ToString(),
        //                    name = dt1.Rows[j]["name"].ToString(),
        //                    mobile = dt1.Rows[j]["mobile"].ToString(),
        //                    email = dt1.Rows[j]["email"].ToString(),
        //                    present_address = dt1.Rows[j]["present_address"].ToString(),
        //                    permenant_address = dt1.Rows[j]["permenant_address"].ToString(),
        //                    status = dt1.Rows[j]["status"].ToString(),
        //                    qualification = dt1.Rows[j]["qualification"].ToString(),
        //                    image_path = dt1.Rows[j]["image_path"].ToString(),
        //                    department_id = dt1.Rows[j]["department_id"].ToString(),
        //                    designation_id = dt1.Rows[j]["designation_id"].ToString(),
        //                    nationality = dt1.Rows[j]["nationality"].ToString(),
        //                    school_id = dt1.Rows[j]["school_id"].ToString(),
        //                    department = dt1.Rows[j]["department"].ToString(),
        //                    designation = dt1.Rows[j]["designation"].ToString(),
        //                    class_id = dt1.Rows[j]["class_id"].ToString(),
        //                    class_division = dt1.Rows[j]["class_division"].ToString(),
        //                    class_std = dt1.Rows[j]["class_std"].ToString(),

        //                });
        //            }
        //        }
        //        meetings.Add(new Meeting
        //        {
        //            ID = dt.Rows[i]["ID"].ToString(),
        //            student_id = dt.Rows[i]["student_id"].ToString(),
        //            subject = dt.Rows[i]["subject"].ToString(),
        //            message = dt.Rows[i]["message"].ToString(),
        //            fromdate = dt.Rows[i]["fromdate"].ToString(),
        //            todate = dt.Rows[i]["todate"].ToString(),
        //            status = dt.Rows[i]["status"].ToString(),
        //            school_id = dt.Rows[i]["school_id"].ToString(),
        //            participants = dt.Rows[i]["participants"].ToString(),
        //            comments = dt.Rows[i]["participants"].ToString(),
        //            participantList = participantList
        //        });
        //    }
        //}

        //public APIStatus getMeetingStatus(string school_id, string meetingid, string comment, string toIDs, string status, string fromID, string fromtype)
        //{
        //    DAL objDAL = new DAL();
        //    string returnValue = string.Empty;
        //    string sqlQuery = string.Empty;
        //    APIStatus api_status = new APIStatus();

        //    string updateMessage = "Success";

        //    int schoolID = 0;
        //    int.TryParse(school_id, out schoolID);
        //    int meetingID = 0;
        //    int.TryParse(meetingid, out meetingID);

        //    if (schoolID == 0 || meetingID == 0)
        //        updateMessage = "Please pass valid Meeting ID/ School ID";
        //    else
        //    {
        //        sqlQuery = "INSERT INTO meeting_trn (meeting_id, school_id, from_id, status, comments, from_type) VALUES(" + meetingID + 
        //                "," + school_id + "," + fromID + ",'" + status + "'," +
        //                "'" + comment + "','" + fromtype + "')";
        //        objDAL.ExecuteNonQuery(sqlQuery);
        //    }
        //    api_status.api_status = updateMessage;
        //    return api_status;
        //}

        public MeetingHeader getmymeeting(string scid, string fid, string ftype)
        {
            string tmemberstring, smemberstring, tclassstring, sclassstring = "";
            DAL objDAL = new DAL();
            string employeeTable = "";
            if (ftype == "3")
                employeeTable = "teacher";
            else if (ftype == "4")
                employeeTable = "student";
            else
            {
                meetinglist.APIStatus = "Failure : Please send valid ftype";
                return meetinglist;
            }

            DataTable dtclass = new DataTable();
            string sql = "SELECT class_id FROM " + employeeTable + " WHERE id =" + fid;
            dtclass = objDAL.ExecuteDataTable(sql);
            string class_id = "0";
            if (dtclass.Rows.Count > 0)
                class_id = dtclass.Rows[0]["class_id"] == null ? "0" : dtclass.Rows[0]["class_id"].ToString();
            else
            {
                meetinglist.APIStatus = "Failure : No entry with given ftype and fid";
                return meetinglist;
            }


            DataTable dt = new DataTable();
            string sqlQuery = "Select m.id, m.school_id, m.from_id, m.from_type, m.fromdate, m.todate, m.status, " +
                    "m.smembers,m.tmembers, m.message, m.subject, m.sclass, m.tclass ";
            //" CASE m.from_type WHEN '3' THEN te.name WHEN '4' THEN CONCAT(st.first_name,' ',st.middle_name, ' ',st.family_name) ELSE ' ' END as CreatedByName ";
            if (ftype == "3")
                sqlQuery += ",te.name as CreatedByName";
            if (ftype == "4")
                sqlQuery += ", CONCAT(te.first_name,' ',te.middle_name, ' ',te.family_name) as CreatedByName";

            sqlQuery += " FROM meeting m INNER JOIN school s ON s.id = m.school_id ";

            if (ftype == "3")
                sqlQuery += " INNER JOIN teacher te ON te.id = m.from_id ";
            if (ftype == "4")
                sqlQuery += " INNER JOIN student te ON te.id = m.from_id ";

            sqlQuery += "WHERE (from_id = " + fid + " AND from_type = '" + ftype + "') ";

            if (ftype == "3")
                sqlQuery += " OR (tmembers = '0' OR tmembers like '%," + fid + "' or tmembers like '%," + fid + ",%' OR tmembers like '" + fid + ",%' OR tmembers like '%" + fid + "%') " +
                    " OR (tclass = '0' OR tclass like '%," + class_id + "' or tclass like '%," + class_id + ",%' OR tclass like '" + class_id + ",%' OR tclass like '%" + class_id + "%')";
            else if (ftype == "4")
                sqlQuery += " OR (smembers = '0' OR smembers like '%," + fid + "' or smembers like '%," + fid + ",%' OR smembers like '" + fid + ",%' OR smembers like '%" + fid + "%') " +
                    " OR (sclass = '0' OR sclass like '%," + class_id + "' or sclass like '%," + class_id + ",%' OR sclass like '" + class_id + ",%' OR sclass like '%" + class_id + "%')";

            dt = objDAL.ExecuteDataTable(sqlQuery);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List<Teacher> tmembers = new List<Teacher>();
                List<Student> smembers = new List<Student>();
                //get teachers
                tmemberstring = dt.Rows[i]["tmembers"].ToString() == "" ? "-1" : dt.Rows[i]["tmembers"].ToString();
                tclassstring = dt.Rows[i]["tclass"].ToString() == "" ? "-1" : dt.Rows[i]["tclass"].ToString();

                DataTable dt1 = new DataTable();
                string sqlQuery1 = "SELECT t.id,t.code,t.name,t.mobile,t.email,t.present_address,t.permenant_address,t.status,t.qualification, t.image_path, " +
                        "dep.name as department, des.id as designation_id,des.name as designation, dep.id as department_id,t.nationality, t.school_id, " +
                        "cls.std as class_std, cls.division as class_division, cls.id as class_id " +
                        "FROM teacher t INNER JOIN department dep ON t.department_id = dep.id INNER JOIN designation des ON des.id = t.designation_id " +
                        "INNER JOIN class cls ON cls.id = t.class_id ";
                if(tmemberstring != "0"  && tclassstring != "0")
                    sqlQuery1 +=  " WHERE t.id in (" + tmemberstring + ") OR class_id in (" + tclassstring + ")";
                else if (tmemberstring != "0" && tclassstring == "0")
                    sqlQuery1 += " WHERE t.id in (" + tmemberstring + ") ";
                else if (tmemberstring == "0" && tclassstring != "0")
                    sqlQuery1 += " WHERE class_id in (" + tclassstring + ") ";
                
                dt1 = objDAL.ExecuteDataTable(sqlQuery1);

                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    tmembers.Add(new Teacher
                    {
                        ID = dt1.Rows[j]["ID"].ToString(),
                        name = dt1.Rows[j]["name"].ToString(),
                        mobile = dt1.Rows[j]["mobile"].ToString(),
                        email = dt1.Rows[j]["email"].ToString(),
                        present_address = dt1.Rows[j]["present_address"].ToString(),
                        permenant_address = dt1.Rows[j]["permenant_address"].ToString(),
                        status = dt1.Rows[j]["status"].ToString(),
                        qualification = dt1.Rows[j]["qualification"].ToString(),
                        image_path = dt1.Rows[j]["image_path"].ToString(),
                        department_id = dt1.Rows[j]["department_id"].ToString(),
                        designation_id = dt1.Rows[j]["designation_id"].ToString(),
                        nationality = dt1.Rows[j]["nationality"].ToString(),
                        school_id = dt1.Rows[j]["school_id"].ToString(),
                        department = dt1.Rows[j]["department"].ToString(),
                        designation = dt1.Rows[j]["designation"].ToString(),
                        class_id = dt1.Rows[j]["class_id"].ToString(),
                        class_division = dt1.Rows[j]["class_division"].ToString(),
                        class_std = dt1.Rows[j]["class_std"].ToString(),
                    });
                }

                //get students
                smemberstring = dt.Rows[i]["smembers"].ToString() == "" ? "0" : dt.Rows[i]["smembers"].ToString();
                sclassstring = dt.Rows[i]["sclass"].ToString() == "" ? "0" : dt.Rows[i]["sclass"].ToString();

                DataTable dtStudent = new DataTable();
                string sqlQueryStd = "SELECT t.id,t.school_id,t.student_id,t.first_name,t.mobile,t.email,t.present_address,t.permenant_address,t.father_name, t.mother_name, " +
                        "t.contact_mobile, t.contact_email,t.gender, t.wilayath,t.waynumber,t.nationality,t.middle_name,t.family_name,t.gender,t.image_path,t.status, " +
                        "cls.id as class_id, cls.std as class_std, cls.division as class_division,t.latitude, t.longitude, t.accuracy " +
                        " FROM student t INNER JOIN class cls ON t.class_id = cls.ID ";
                if (smemberstring != "0" && sclassstring != "0")
                    sqlQueryStd += " WHERE t.id in (" + smemberstring + ") OR class_id in (" + sclassstring + ")";
                else if (smemberstring != "0" && sclassstring == "0")
                    sqlQueryStd += " WHERE t.id in (" + smemberstring + ") ";
                else if (smemberstring == "0" && sclassstring != "0")
                    sqlQueryStd += " WHERE class_id in (" + sclassstring + ")";


                dtStudent = objDAL.ExecuteDataTable(sqlQueryStd);

                for (int k = 0; k < dtStudent.Rows.Count; k++)
                {
                    smembers.Add(new Student
                    {
                        ID = dtStudent.Rows[k]["ID"].ToString(),
                        studentID = dtStudent.Rows[k]["student_id"].ToString(),
                        mobile = dtStudent.Rows[k]["mobile"].ToString(),
                        email = dtStudent.Rows[k]["email"].ToString(),
                        present_address = dtStudent.Rows[k]["present_address"].ToString(),
                        permenant_address = dtStudent.Rows[k]["permenant_address"].ToString(),
                        status = dtStudent.Rows[k]["status"].ToString(),
                        fathername = dtStudent.Rows[k]["father_name"].ToString(),
                        image_path = dtStudent.Rows[k]["image_path"].ToString(),
                        mothername = dtStudent.Rows[k]["mother_name"].ToString(),
                        contactmobile = dtStudent.Rows[k]["contact_mobile"].ToString(),
                        nationality = dtStudent.Rows[k]["nationality"].ToString(),
                        contactemail = dtStudent.Rows[k]["contact_email"].ToString(),
                        gender = dtStudent.Rows[k]["gender"].ToString(),
                        wilayath = dtStudent.Rows[k]["wilayath"].ToString(),
                        waynumber = dtStudent.Rows[k]["waynumber"].ToString(),
                        familyname = dtStudent.Rows[k]["family_name"].ToString(),
                        middlename = dtStudent.Rows[k]["middle_name"].ToString(),
                        class_id = dtStudent.Rows[k]["class_id"].ToString(),
                        class_division = dtStudent.Rows[k]["class_division"].ToString(),
                        class_std = dtStudent.Rows[k]["class_std"].ToString(),
                        schoolId = dtStudent.Rows[k]["school_id"].ToString(),
                        firstname = dtStudent.Rows[k]["first_name"].ToString(),
                        latitude = dtStudent.Rows[k]["latitude"].ToString(),
                        longitude = dtStudent.Rows[k]["longitude"].ToString(),
                        accuracy = dtStudent.Rows[k]["accuracy"].ToString(),
                    });
                }

                meetings.Add(new Meeting
                {
                    ID = dt.Rows[i]["ID"].ToString(),
                    from_id = dt.Rows[i]["from_id"].ToString(),
                    from_type = dt.Rows[i]["from_type"].ToString(),
                    subject = dt.Rows[i]["subject"].ToString(),
                    message = dt.Rows[i]["message"].ToString(),
                    fromdate = dt.Rows[i]["fromdate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["fromdate"]).ToString("yyyy-MM-dd / H:mm:ss"),
                    todate = dt.Rows[i]["todate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["todate"]).ToString("yyyy-MM-dd / H:mm:ss"),
                    status = dt.Rows[i]["status"].ToString(),
                    school_id = dt.Rows[i]["school_id"].ToString(),
                    createdbyname = dt.Rows[i]["CreatedByName"].ToString(),
                    tmembers = tmembers,
                    smembers = smembers
                });
            }
            meetinglist.meetingdetails = meetings;
            return meetinglist;
        }

        public MeetingTrnHeader getmymeetingStatus(string scid, string meetid, string fid, string ftype)
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            string sqlQuery = "SELECT m.id, m.school_id, m.from_id as CreatedBy, m.from_type, m.fromdate, m.todate, m.subject, m.message, " +
                    " mt.status as updated_status, mt.comments, mt.from_id as updated_by, mt.from_type as updated_type , date as updated_date, " +
                    " CASE mt.from_type WHEN '3' THEN te.name WHEN '4' THEN CONCAT(st.first_name,' ',st.middle_name, ' ',st.family_name) ELSE ' ' END as UpdateByName " +
                    " FROM meeting m INNER JOIN meeting_trn mt ON m.id = mt.meeting_id " +
                    " LEFT JOIN student st ON mt.from_id = st.id AND mt.from_type = 4 " +
                    " LEFT JOIN teacher te ON mt.from_id = te.id AND mt.from_type = 3 " +
                    " WHERE mt.meeting_id = " + meetid + " AND mt.school_id =" + scid + " ";
            dt = objDAL.ExecuteDataTable(sqlQuery);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                meetingstatus.Add(new MeetingTrn
                {
                    ID = dt.Rows[i]["ID"].ToString(),
                    created_by = dt.Rows[i]["CreatedBy"].ToString(),
                    created_type = dt.Rows[i]["from_type"].ToString(),
                    subject = dt.Rows[i]["subject"].ToString(),
                    message = dt.Rows[i]["message"].ToString(),
                    fromdate = dt.Rows[i]["fromdate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["fromdate"]).ToString("yyyy-MM-dd / H:mm:ss"),
                    todate = dt.Rows[i]["todate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["todate"]).ToString("yyyy-MM-dd / H:mm:ss"),
                    updated_status = dt.Rows[i]["updated_status"].ToString(),
                    school_id = dt.Rows[i]["school_id"].ToString(),
                    updated_comments = dt.Rows[i]["comments"].ToString(),
                    updated_by = dt.Rows[i]["updated_by"].ToString(),
                    updated_by_name = dt.Rows[i]["UpdateByName"].ToString(),
                    updated_type = dt.Rows[i]["updated_type"].ToString(),
                    updated_date = dt.Rows[i]["updated_date"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["updated_date"]).ToString("yyyy-MM-dd / H:mm:ss zzz")
                });
            }
            meetingstatuslist.meetingstatusdetails = meetingstatus;
            return meetingstatuslist;
        }

        public APIStatus getMeetingSave(string scid, string fid, string ftype, string fdate, string tdate, string sub, string msg, string smembers, string tmembers, string sclass, string tclass)
        {
            APIStatus api_status = new APIStatus();
            try
            {
                DAL objDAL = new DAL();
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;
                string updateMessage = "Success";

                int school_id = 0;
                int.TryParse(scid, out school_id);
                int from_id = 0;
                int.TryParse(fid, out from_id);
                if (smembers == null)
                    smembers = "";
                if (tmembers == null)
                    tmembers = "";
                if (sclass == null)
                    sclass = "";
                if (tclass == null)
                    tclass = "";

                List<string> tmemberArray = tmembers.Split(',').Distinct().ToList();
                tmembers = string.Join(",", tmemberArray);

                List<string> smemberArray = smembers.Split(',').Distinct().ToList();
                smembers = string.Join(",", smemberArray);

                List<string> sclassArray = sclass.Split(',').Distinct().ToList();
                sclass = string.Join(",", sclassArray);

                List<string> tclassArray = tclass.Split(',').Distinct().ToList();
                tclass = string.Join(",", tclassArray);

                if (school_id == 0 || from_id == 0)
                    updateMessage = "Please pass valid from ID/ School ID";
                else if (tmembers != "" && !Regex.IsMatch(tmembers, @"^[0-9]+(,[0-9]+)*$"))
                    updateMessage = "Please pass valid tmembers (Eg: 1,2,3) ";
                else if (smembers != "" && !Regex.IsMatch(smembers, @"^[0-9]+(,[0-9]+)*$"))
                    updateMessage = "Please pass valid smembers (Eg : 1,3,4)";
                else if (sclass != "" && !Regex.IsMatch(sclass, @"^[0-9]+(,[0-9]+)*$"))
                    updateMessage = "Please pass valid sclass (Eg : 1,3,4)";
                else if (tclass != "" && !Regex.IsMatch(tclass, @"^[0-9]+(,[0-9]+)*$"))
                    updateMessage = "Please pass valid tclass (Eg : 1,3,4)";
                else if(fdate == "" || tdate == "")
                    updateMessage = "Please pass valid fdate and tdate";
                else
                {
                    sqlQuery = "INSERT INTO meeting(school_id, from_id, from_type, fromdate, todate, subject, message, status, smembers, tmembers, sclass, tclass) VALUES(" + school_id +
                            "," + from_id + ",'" + ftype + "','" + fdate + "','" + tdate + "','" + sub + "','" + msg + "','REQUEST','" + smembers + "','" + tmembers + "','" + sclass + "','" + tclass + "' )";
                    objDAL.ExecuteNonQuery(sqlQuery);
                }
                api_status.api_status = updateMessage;

                if (api_status.api_status == "Success")
                {
                    sendMeetingNotification(scid, fid, ftype, fdate, tdate, sub, msg, smembers, tmembers, sclass, tclass);
                }

                return api_status;
            }
            catch (Exception ex)
            {
                api_status.api_status = ex.Message;
                return api_status;
            }
        }

        private void sendMeetingNotification(string scid, string fid, string ftype, string fdate, string tdate, string sub, string msg, string smembers, string tmembers, string sclass, string tclass)
        {
            DAL objDAL = new DAL();
            DataTable resultTable = new DataTable();
            DataTable APItable = new DataTable();
            DataTable fnameTable = new DataTable();

            smembers = smembers == "" ? "-1" : smembers;
            tmembers = tmembers == "" ? "-1" : tmembers;
            sclass = sclass == "" ? "-1" : sclass;
            tclass = tclass == "" ? "-1" : tclass;

            //string sqlQuery = "SELECT s.email,s.class_id,l.google_regid from student s INNER JOIN class c ON s.class_id = c.id INNER JOIN login l ON l.user_id = s.id AND l.type = 4 WHERE s.class_id IN (" + sclass + ") UNION  " +
            //    " SELECT t.email,t.class_id,l.google_regid FROM teacher t INNER JOIN class c ON t.class_id = c.id INNER JOIN login l ON l.user_id = t.id AND l.type = 3 WHERE t.class_id IN (" + tclass + ") UNION " +
            //    " SELECT t.email,t.class_id,l.google_regid FROM teacher t INNER JOIN login l ON l.user_id = t.id AND l.type = 3 WHERE t.id IN (" + tmembers + ") UNION " +
            //    " SELECT s.email,s.class_id,l.google_regid from student s INNER JOIN login l ON l.user_id = s.id AND l.type = 4 WHERE s.id IN (" + smembers + ") ";

            string sqlQuery = "";
            sqlQuery = "SELECT s.email,s.class_id,l.google_regid from student s INNER JOIN class c ON s.class_id = c.id INNER JOIN login l ON l.user_id = s.id AND l.type = 4 ";
            if (sclass != "0")
                sqlQuery += " WHERE s.class_id IN (" + sclass + ") ";
            sqlQuery += " UNION SELECT t.email,t.class_id,l.google_regid FROM teacher t INNER JOIN class c ON t.class_id = c.id INNER JOIN login l ON l.user_id = t.id AND l.type = 3 ";
            if (tclass != "0")
                sqlQuery += "WHERE t.class_id IN (" + tclass + ") ";
            sqlQuery += " UNION SELECT t.email,t.class_id,l.google_regid FROM teacher t INNER JOIN login l ON l.user_id = t.id AND l.type = 3 ";
            if (tmembers != "0")
                sqlQuery += " WHERE t.id IN (" + tmembers + ") ";
            sqlQuery += " UNION  SELECT s.email,s.class_id,l.google_regid from student s INNER JOIN login l ON l.user_id = s.id AND l.type = 4 ";
            if(smembers != "0")
                sqlQuery += " WHERE s.id IN (" + smembers + ") ";

            resultTable = objDAL.ExecuteDataTable(sqlQuery);

            string googleSenderID = "";
            string googleAppID = "";

            string sqlQueryAPIKey = "SELECT google_sender_id, google_app_id FROM school WHERE id = " + scid;
            APItable = objDAL.ExecuteDataTable(sqlQueryAPIKey);
            if (APItable.Rows.Count > 0)
            {
                googleSenderID = APItable.Rows[0]["google_sender_id"].ToString();
                googleAppID = APItable.Rows[0]["google_app_id"].ToString();
            }

            string sqlQueryFname;
            string fname = "";
            if (ftype == "3")
            {
                sqlQueryFname = "SELECT t.name FROM teacher t WHERE id = " + fid;
                fnameTable = objDAL.ExecuteDataTable(sqlQueryFname);
                if (fnameTable.Rows.Count > 0)
                    fname = fnameTable.Rows[0]["name"].ToString();
            }
            else if (ftype == "4")
            {
                sqlQueryFname = "SELECT CONCAT(first_name, ' ' , middle_name, ' ' , family_name) as name  FROM student WHERE id = " + fid;
                fnameTable = objDAL.ExecuteDataTable(sqlQueryFname);
                if (fnameTable.Rows.Count > 0)
                    fname = fnameTable.Rows[0]["name"].ToString();
            }
            else if (ftype == "2")
                fname = "School Admin";
            else if (ftype == "1")
                fname = "Super Admin";
            else
                fname = "";

            for (int i = 0; i < resultTable.Rows.Count; i++)
            {
                string regid = resultTable.Rows[i]["google_regid"].ToString();
                if (regid != null && regid != "" && googleSenderID != "" && googleAppID != "")
                    sendPushNot(fname, fdate, tdate, sub, msg, regid, googleSenderID, googleAppID);
            }
        }

        private void sendPushNot(string fname, string fdate, string tdate, string sub, string msg, string regid, string senderID, string googleAPPID)
        {
            try
            {
                string GoogleAppID = googleAPPID;
                var SENDER_ID = senderID;
                string devider = ":RBAIJSDUR:";
                var value = "MT" + devider + System.DateTime.Now.ToString("yyyy-MM-dd / H:mm:ss") + devider + fname + devider + sub + devider + fdate + devider + tdate + devider + msg;
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + HttpUtility.UrlEncode(value) + "&data.time=" +
                System.DateTime.Now.ToString("yyyy-MM-dd / H:mm:ss") + "&registration_id=" + regid + "";
                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                dataStream.Close();
                tResponse.Close();
                //return sResponseFromServer;
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                //        "Error :" + ex.Message), true);
                //return "Error" + ex.Message;
            }
        }

        public APIStatus getMeetingStatusSave(string scid, string meetid, string fid, string ftype, string status, string comments)
        {
            APIStatus api_status = new APIStatus();
            try
            {
                DAL objDAL = new DAL();
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;

                string updateMessage = "Success";

                int meeting_id = 0;
                int.TryParse(meetid, out meeting_id);
                int from_id = 0;
                int.TryParse(fid, out from_id);
                int school_id = 0;
                int.TryParse(scid, out school_id);

                if (meeting_id == 0 || from_id == 0 || school_id == 0)
                    updateMessage = "Please pass valid meeting ID/ School ID/ fromid";
                else
                {
                    sqlQuery = "INSERT INTO meeting_trn(meeting_id, school_id, from_id, from_type, status, comments, date) VALUES(" + meetid +
                            "," + school_id + "," + from_id + ",'" + ftype + "','" + status + "','" + comments + "',now() )";
                    objDAL.ExecuteNonQuery(sqlQuery);
                }
                api_status.api_status = updateMessage;

                if (updateMessage == "Success")
                    sendMeetingUpdateNotification(scid, meetid, fid, ftype, status, comments);
                return api_status;
            }
            catch (Exception ex)
            {
                api_status.api_status = ex.Message;
                return api_status;
            }
        }

        private void sendMeetingUpdateNotification(string scid, string meetid, string fid, string ftype, string status, string comments)
        {
            DAL objDAL = new DAL();
            DataTable resultTable = new DataTable();
            DataTable APItable = new DataTable();
            DataTable fnameTable = new DataTable();
            string sqlQuery = "";
            if (ftype == "3")
                sqlQuery = "SELECT t.email,t.name,l.google_regid,m.subject FROM teacher t INNER JOIN meeting m ON t.id = m.from_id AND m.from_type = 3 INNER JOIN login l ON l.user_id = t.id AND l.type = 3 WHERE m.id = " + meetid;
            else if (ftype == "4")
                sqlQuery = "SELECT s.email,CONCAT(s.first_name, ' ' , s.middle_name, ' ' , s.family_name) as name,l.google_regid,m.subject FROM student s INNER JOIN meeting m ON s.id = m.from_id AND m.from_type = 4 INNER JOIN login l ON l.user_id = t.id AND l.type = 4 WHERE m.id = " + meetid;

            resultTable = objDAL.ExecuteDataTable(sqlQuery);

            string googleSenderID = "";
            string googleAppID = "";

            string sqlQueryAPIKey = "SELECT google_sender_id, google_app_id FROM school WHERE id = " + scid;
            APItable = objDAL.ExecuteDataTable(sqlQueryAPIKey);
            if (APItable.Rows.Count > 0)
            {
                googleSenderID = APItable.Rows[0]["google_sender_id"].ToString();
                googleAppID = APItable.Rows[0]["google_app_id"].ToString();
            }


            string sqlQueryFname;
            string fname = "";
            if (ftype == "3")
            {
                sqlQueryFname = "SELECT t.name FROM teacher t WHERE id = " + fid;
                fnameTable = objDAL.ExecuteDataTable(sqlQueryFname);
                if (fnameTable.Rows.Count > 0)
                    fname = fnameTable.Rows[0]["name"].ToString();
            }
            else if (ftype == "4")
            {
                sqlQueryFname = "SELECT CONCAT(first_name, ' ' , middle_name, ' ' , family_name) as name  FROM student WHERE id = " + fid;
                fnameTable = objDAL.ExecuteDataTable(sqlQueryFname);
                if (fnameTable.Rows.Count > 0)
                    fname = fnameTable.Rows[0]["name"].ToString();
            }
            else if (ftype == "2")
                fname = "School Admin";
            else if (ftype == "1")
                fname = "Super Admin";
            else
                fname = "";

            for (int i = 0; i < resultTable.Rows.Count; i++)
            {
                string regid = resultTable.Rows[i]["google_regid"].ToString();
                string sub = resultTable.Rows[i]["subject"].ToString();
                if (regid != null && regid != "" && googleSenderID != "" && googleAppID != "")
                    sendPushNotForMU(fname, sub, status, comments, regid, googleSenderID, googleAppID);
            }
        }

        private void sendPushNotForMU(string fromName, string sub, string status, string comments, string regid, string googleSenderID, string googleAppID)
        {
            try
            {
                string GoogleAppID = googleAppID;
                var SENDER_ID = googleSenderID;
                string devider = ":RBAIJSDUR:";
                var value = "MT" + devider + System.DateTime.Now.ToString("yyyy-MM-dd / H:mm:ss") + devider + fromName + devider + sub + devider + status + devider + comments;
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + HttpUtility.UrlEncode(value) + "&data.time=" +
                System.DateTime.Now.ToString("yyyy-MM-dd / H:mm:ss") + "&registration_id=" + regid + "";
                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                dataStream.Close();
                tResponse.Close();
                //return sResponseFromServer;
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                //        "Error :" + ex.Message), true);
                //return "Error" + ex.Message;
            }
        }

    }
}
