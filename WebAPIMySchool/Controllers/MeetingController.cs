using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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
                sqlQuery += " OR (tmembers like '%," + fid + "' or tmembers like '%," + fid + ",%' OR tmembers like '" + fid + ",%' OR tmembers like '%" + fid + "%') " +
                    " OR (tclass like '%," + class_id + "' or tclass like '%," + class_id + ",%' OR tclass like '" + class_id + ",%' OR tclass like '%" + class_id + "%')";
            else if (ftype == "4")
                sqlQuery += " OR (smembers like '%," + fid + "' or smembers like '%," + fid + ",%' OR smembers like '" + fid + ",%' OR smembers like '%" + fid + "%') " +
                    " OR (sclass like '%," + class_id + "' or sclass like '%," + class_id + ",%' OR sclass like '" + class_id + ",%' OR sclass like '%" + class_id + "%')";

            dt = objDAL.ExecuteDataTable(sqlQuery);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List<Teacher> tmembers = new List<Teacher>();
                List<Student> smembers = new List<Student>();
                //get teachers
                tmemberstring = dt.Rows[i]["tmembers"].ToString() == "" ? "0" : dt.Rows[i]["tmembers"].ToString();
                tclassstring = dt.Rows[i]["tclass"].ToString() == "" ? "0" : dt.Rows[i]["tclass"].ToString();

                DataTable dt1 = new DataTable();
                string sqlQuery1 = "SELECT t.id,t.code,t.name,t.mobile,t.email,t.present_address,t.permenant_address,t.status,t.qualification, t.image_path, " +
                        "dep.name as department, des.id as designation_id,des.name as designation, dep.id as department_id,t.nationality, t.school_id, " +
                        "cls.std as class_std, cls.division as class_division, cls.id as class_id " +
                        "FROM teacher t INNER JOIN department dep ON t.department_id = dep.id INNER JOIN designation des ON des.id = t.designation_id " +
                        "INNER JOIN class cls ON cls.id = t.class_id WHERE t.id in (" + tmemberstring + ") OR class_id in (" + tclassstring + ")";

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
                        " FROM student t INNER JOIN class cls ON t.class_id = cls.ID WHERE t.id in (" + smemberstring + ") OR class_id in (" + sclassstring + ")";
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
                else
                {
                    sqlQuery = "INSERT INTO meeting(school_id, from_id, from_type, fromdate, todate, subject, message, status, smembers, tmembers, sclass, tclass) VALUES(" + school_id +
                            "," + from_id + ",'" + ftype + "','" + fdate + "','" + tdate + "','" + sub + "','" + msg + "','REQUEST','" + smembers + "','" + tmembers + "','" + sclass + "','" + tclass + "' )";
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
                    updateMessage = "Please pass valid from meeting ID/ School ID/ fromid";
                else
                {
                    sqlQuery = "INSERT INTO meeting_trn(meeting_id, school_id, from_id, from_type, status, comments, date) VALUES(" + meetid +
                            "," + school_id + "," + from_id + ",'" + ftype + "','" + status + "','" + comments + "',now() )";
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

        //public MeetingHeader getMymeeting(string scid, string fid, string ftype)
        //{

        //}


        //api/meetingUpdate?school_id=1&meetingid=1&comment=testcomment&toIDs={All ids participating this meeting}&Status={APPROVE,DELETE,DENEY}&fromID=1
    }
}
