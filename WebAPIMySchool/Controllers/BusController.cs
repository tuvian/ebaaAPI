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
    public class BusController : ApiController
    {
        List<Bus> bus = new List<Bus>();
        BusHeader busList = new BusHeader();

        public BusHeader GetBus(string school_id)
        {
            if (bus.Count() > 0)
            {
                bus = bus.Where(p => p.school_id == school_id).ToList<Bus>();
                busList.busdetails = bus;
                return busList;
            }
            else
            {
                GetBuses();
                bus = bus.Where(p => p.school_id == school_id).ToList<Bus>();
                busList.busdetails = bus;
                return busList;
            }
        }

        public BusHeader GetBusbyID(string bus_id)
        {
            if (bus.Count() > 0)
            {
                bus = bus.Where(p => p.ID == bus_id).ToList<Bus>();
                busList.busdetails = bus;
                return busList;
            }
            else
            {
                GetBuses();
                bus = bus.Where(p => p.ID == bus_id).ToList<Bus>();
                busList.busdetails = bus;
                return busList;
            }
        }

        public APIStatus GetBus(string scid, string bid, string fdate, string acrcy, string lat, string lon)
        {
            APIStatus apiStatus = new APIStatus();
            apiStatus.api_status = saveBusLocation(scid, bid, fdate, acrcy, lat, lon);
            return apiStatus;
        }

        private void GetBuses()
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            string sqlQuery = "SELECT b.id, b.school_id, b.bus_number, b.rout, b.driver, b.driver_mobile, b.latitude, b.longitude, b.accuracy from bus b ;";
            dt = objDAL.ExecuteDataTable(sqlQuery);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bus.Add(new Bus
                {
                    ID = dt.Rows[i]["ID"].ToString(),
                    bus_number = dt.Rows[i]["bus_number"].ToString(),
                    school_id = dt.Rows[i]["school_id"].ToString(),
                    latitude = dt.Rows[i]["latitude"].ToString(),
                    longitude = dt.Rows[i]["longitude"].ToString(),
                    accuracy = dt.Rows[i]["accuracy"].ToString()
                });
            }   
        }

        public string saveBusLocation(string scid, string bid, string fdate, string acrcy, string lat, string lon)
        {
            DAL objDAL = new DAL();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;

            string updateMessage = "Success";

            int school_id = 0;
            int.TryParse(scid, out school_id);
            int bus_id = 0;
            int.TryParse(bid, out bus_id);

            if (school_id == 0 || bus_id == 0)
                updateMessage = "Please pass valid bus ID/ School ID";
            else
            {
                sqlQuery = "UPDATE bus SET latitude = '"+ lat +"', longitude = '"+ lon +"', accuracy = '"+ acrcy +"',last_updated_on = '"+ fdate +"' WHERE " +
                "id = '" + bus_id + "' AND school_id = '"+ school_id +"'";
                objDAL.ExecuteNonQuery(sqlQuery);
            }
            return updateMessage;
        }
    }

}
