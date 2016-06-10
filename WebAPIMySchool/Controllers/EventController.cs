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
    public class EventController : ApiController
    {
        List<Events> events = new List<Events>();
        EventHeader eventlist = new EventHeader();
        
        public EventHeader GetAllEvents()
        {
            GetEvents();
            eventlist.eventdetails = events;  
            return eventlist;
        }


        public EventHeader GetAllEvents(string school_id)
        {
            if (events.Count() > 0)
            {
                events = events.Where(p => p.school_id == school_id).ToList<Events>();
                eventlist.eventdetails = events; 
                return eventlist;
            }
            else
            {
                GetEvents();
                events = events.Where(p => p.school_id == school_id).ToList<Events>();
                eventlist.eventdetails = events;          
                return eventlist;
            }
        }

        private void GetEvents()
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            string sqlQuery = "SELECT e.id,e.title,e.school_id,e.start_date,e.end_date,e.description,e.status,e.created_date, e.created_by, " +
                    "s.code as school_code " +
                    "FROM events e INNER JOIN school s ON e.school_id = s.id  ";
            dt = objDAL.ExecuteDataTable(sqlQuery);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                events.Add(new Events
                {
                    ID = dt.Rows[i]["ID"].ToString(),
                    title = dt.Rows[i]["title"].ToString(),
                    school_id = dt.Rows[i]["school_id"].ToString(),
                    description = dt.Rows[i]["description"].ToString(),
                    start_date = Convert.ToDateTime(dt.Rows[i]["start_date"]).ToString("yyyy-MM-dd"),
                    end_date = Convert.ToDateTime(dt.Rows[i]["end_date"]).ToString("yyyy-MM-dd")
                });
            }        
        }
    }
}
