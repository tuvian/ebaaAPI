using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPIMySchool.Models;

namespace WebAPIMySchool.Controllers
{
    public class NotificationController : ApiController
    {
        public APIStatus getNotiifcation(string scid, string fname, string fid, string ftype, string msg, string tid, string ttype)
        {
            APIStatus api_status = new APIStatus();
            try
            {
                DAL objDAL = new DAL();
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;

                string updateMessage = "Success";

                int to_id = 0;
                int.TryParse(tid, out to_id);
                int to_type = 0;
                int.TryParse(ttype, out to_type);
                int school_id = 0;
                int.TryParse(scid, out school_id);

                if (to_id == 0 || to_type == 0 || school_id == 0)
                    updateMessage = "Please pass valid to ID/ School ID/ to type";
                else
                {
                    DataTable resultTable = new DataTable();
                    DataTable APItable = new DataTable();

                    if (to_type == 3)
                        sqlQuery = "SELECT t.email,l.google_regid FROM teacher t INNER JOIN login l ON l.user_id = t.id AND l.type = 3 WHERE t.id = " + to_id;
                    else if (to_type == 4)
                        sqlQuery = "SELECT s.email,l.google_regid from student s INNER JOIN login l ON l.user_id = s.id AND l.type = 4 WHERE s.id = " + to_id;
                    else if (to_type == 1 || to_type == 2)
                        sqlQuery = "SELECT l.google_regid from login l WHERE l.type = " + to_type + " AND l.id = " + to_id;
                    resultTable = objDAL.ExecuteDataTable(sqlQuery);

                    string googleSenderID = "";
                    string googleAppID = "";

                    string sqlQueryAPIKey = "SELECT google_sender_id, google_app_id FROM school WHERE id = " + school_id;
                    APItable = objDAL.ExecuteDataTable(sqlQueryAPIKey);
                    if (APItable.Rows.Count > 0)
                    {
                        googleSenderID = APItable.Rows[0]["google_sender_id"].ToString();
                        googleAppID = APItable.Rows[0]["google_app_id"].ToString();
                    }

                    string notStatus = "success";
                    for (int i = 0; i < resultTable.Rows.Count; i++)
                    {
                        string regid = resultTable.Rows[i]["google_regid"].ToString();
                        if (regid != null && regid != "" && googleSenderID != "" && googleAppID != "")
                            notStatus = sendPushNot(fname, fid, ftype, msg, regid, googleSenderID, googleAppID);
                    }
                    //return "success";

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

        private string sendPushNot(string fname, string fid, string ftype, string msg, string regid, string googleSenderID, string googleAppID)
        {
            try
            {
                string GoogleAppID = googleAppID;
                var SENDER_ID = googleSenderID;
                string devider = ":RBAIJSDUR:";
                var value = msg + devider + System.DateTime.Now.ToString("yyyy-MM-dd / H:mm:ss");
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
                return sResponseFromServer;
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                //        "Error :" + ex.Message), true);
                return "Error" + ex.Message;
            }
        }

        public APIStatus getPushNotificationFromMobile(string scid, string fid, string ftype, string sub, string msg, string smembers, string tmembers, string sclass, string tclass)
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

                    sqlQuery = "";
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
                    if (smembers != "0")
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
                            sendPushNotForNotifcationModule(fname, "testfile",  sub, msg, regid, googleSenderID, googleAppID);
                    }
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

        private void sendPushNotForNotifcationModule(string fname, string filename, string sub, string msg, string regid, string senderID, string googleAPPID)
        {
            try
            {
                string GoogleAppID = googleAPPID;
                var SENDER_ID = senderID;
                string devider = ":RBAIJSDUR:";
                var value = "NT" + devider + System.DateTime.Now.ToString("yyyy-MM-dd / H:mm:ss") + devider + fname + devider + sub + devider + "testfile" + devider + msg;
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

        [AcceptVerbs("GET", "POST")]
        [ActionName("sendPushNotWithFileForNotifcationModule")]
        [System.Web.Http.HttpPost]
        public APIStatus sendPushNotWithFileForNotifcationModule(WebAPIMySchool.Models.Notification.pushnotification pn)
        {
            APIStatus ap = new APIStatus();
            ap.api_status = "";
            FileStream str = null;

            string root = System.Web.HttpContext.Current.Server.MapPath("~");
            string parent = Path.GetDirectoryName(root);
            string grandParent = Path.GetDirectoryName(parent);
            string path = grandParent + @"\dev.ebaa.co\notification_file\";
            string filename = path + pn.filename;

            int school_id = 0;
            int.TryParse(pn.school_id, out school_id);
            int from_id = 0;
            int.TryParse(pn.fid, out from_id);
            if (pn.ftype != "1" || pn.ftype != "2" || pn.ftype != "3" || pn.ftype != "4")
            {
                ap.api_status = "Please pass valid ftype, ftype : " + pn.ftype;
                return ap;
            }
            if (school_id == 0 || from_id == 0)
            {
                ap.api_status = "Please pass valid student ID/ From ID";
                return ap;
            }

            try
            {
                byte[] byteArrayIn = Convert.FromBase64String(pn.imagestring);
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
                    configPushNotification(pn);
                }

                //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                //ms.Write(imageBytes, 0, imageBytes.Length);
                //System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            }
            catch (Exception ex)
            {
                ap.api_status = "Faliure : " + ex.Message + " : ftype" + pn.ftype + " fid" + pn.fid;
            }

            //ap.api_status = "ID = " + sp.id + " schoolid = " + sp.school_id + " Image Status = " + base64Status;
            return ap;
        }

        private APIStatus configPushNotification(Notification.pushnotification pn)
        {
            APIStatus api_status = new APIStatus();
            try
            {
                DAL objDAL = new DAL();
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;
                string updateMessage = "Success";

                string smembers = pn.smembers;
                string tmembers = pn.tmembers;
                string sclass = pn.sclass;
                string tclass = pn.tclass;

                int school_id = 0;
                int.TryParse(pn.school_id, out school_id);
                int from_id = 0;
                int.TryParse(pn.fid, out from_id);
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

                    sqlQuery = "";
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
                    if (smembers != "0")
                        sqlQuery += " WHERE s.id IN (" + smembers + ") ";

                    resultTable = objDAL.ExecuteDataTable(sqlQuery);

                    string googleSenderID = "";
                    string googleAppID = "";

                    string sqlQueryAPIKey = "SELECT google_sender_id, google_app_id FROM school WHERE id = " + school_id;
                    APItable = objDAL.ExecuteDataTable(sqlQueryAPIKey);
                    if (APItable.Rows.Count > 0)
                    {
                        googleSenderID = APItable.Rows[0]["google_sender_id"].ToString();
                        googleAppID = APItable.Rows[0]["google_app_id"].ToString();
                    }

                    string sqlQueryFname;
                    string fname = "";
                    if (pn.ftype == "3")
                    {
                        sqlQueryFname = "SELECT t.name FROM teacher t WHERE id = " + pn.fid;
                        fnameTable = objDAL.ExecuteDataTable(sqlQueryFname);
                        if (fnameTable.Rows.Count > 0)
                            fname = fnameTable.Rows[0]["name"].ToString();
                    }
                    else if (pn.ftype == "4")
                    {
                        sqlQueryFname = "SELECT CONCAT(first_name, ' ' , middle_name, ' ' , family_name) as name  FROM student WHERE id = " + pn.fid;
                        fnameTable = objDAL.ExecuteDataTable(sqlQueryFname);
                        if (fnameTable.Rows.Count > 0)
                            fname = fnameTable.Rows[0]["name"].ToString();
                    }
                    else if (pn.ftype == "2")
                        fname = "School Admin";
                    else if (pn.ftype == "1")
                        fname = "Super Admin";
                    else
                        fname = "";

                    for (int i = 0; i < resultTable.Rows.Count; i++)
                    {
                        string regid = resultTable.Rows[i]["google_regid"].ToString();
                        if (regid != null && regid != "" && googleSenderID != "" && googleAppID != "")
                            sendPushNotForNotifcationModule(fname, pn.filename, pn.sub, pn.msg, regid, googleSenderID, googleAppID);
                    }
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

        //////public Task<HttpResponseMessage> PostFile()
        //////{
        //////    HttpRequestMessage request = this.Request;
        //////    if (!request.Content.IsMimeMultipartContent())
        //////    {
        //////        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //////    }

        //////    string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads");
        //////    var provider = new MultipartFormDataStreamProvider(root);

        //////    var task = request.Content.ReadAsMultipartAsync(provider).
        //////        ContinueWith<HttpResponseMessage>(o =>
        //////        {

        //////            string file1 = provider.BodyPartFileNames.First().Value;
        //////            // this is the file name on the server where the file was saved 

        //////            return new HttpResponseMessage()
        //////            {
        //////                Content = new StringContent("File uploaded.")
        //////            };
        //////        }
        //////    );
        //////    return task;
        //////} 
    }
}
