using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
                    else if(to_type == 4)
                        sqlQuery = "SELECT s.email,l.google_regid from student s INNER JOIN login l ON l.user_id = s.id AND l.type = 4 WHERE s.id = " + to_id;
                    else if(to_type == 1 || to_type == 2)
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

        private string sendPushNot(string fname,string fid, string ftype, string msg, string regid, string googleSenderID, string googleAppID)
        {
            try
            {

                string GoogleAppID = googleAppID;
                var SENDER_ID = googleSenderID;
                string devider = ":RBAIJSDUR:";
                var value = msg + devider + System.DateTime.Now;
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + HttpUtility.UrlEncode(value) + "&data.time=" +
                System.DateTime.Now.ToString() + "&registration_id=" + regid + "";
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
