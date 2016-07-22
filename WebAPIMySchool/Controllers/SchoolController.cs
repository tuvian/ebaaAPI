using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIMySchool.Models;

namespace WebAPIMySchool.Controllers
{
    public class SchoolController : ApiController
    {
        public APIStatus getConfigGoogleNotification(string scid, string senderid, string appid)
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

                if (school_id == 0 || senderid == null || appid == null)
                    updateMessage = "Please pass valid from school ID/ sender ID/ appid";
                else
                {
                    sqlQuery = "UPDATE school SET google_sender_id = '" + senderid + "', google_app_id = '" + appid + "' WHERE id = " + school_id;
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
