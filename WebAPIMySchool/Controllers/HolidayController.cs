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
    public class HolidayController : ApiController
    {
        List<Holiday> holidays = new List<Holiday>();
        HolidayHeader holidaylist = new HolidayHeader();


        //public HolidayHeader GetAllHoliday()
        //{
        //    GetHolidays();
        //    holidaylist.holidaydetails = holidays;  
        //    return holidaylist;
        //}


        public HolidayHeader GetAllHoliday(string school_id)
        {
            if (holidays.Count() > 0)
            {
                holidays = holidays.Where(p => p.school_id == school_id).ToList<Holiday>();
                holidaylist.holidaydetails = holidays;
                return holidaylist;
            }
            else
            {
                GetHolidays();
                holidays = holidays.Where(p => p.school_id == school_id).ToList<Holiday>();
                holidaylist.holidaydetails = holidays;
                return holidaylist;
            }
        }

        private void GetHolidays()
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            string sqlQuery = "Select h.id, h.name, h.description, h.fromdate, h.todate, h.type, h.status, h.schoolid, h.createdby, " +
                    "h.createddate, h.updatedby, h.updatedDate from holiday h " +
                    "INNER JOIN school s ON s.id = h.schoolid ";
            dt = objDAL.ExecuteDataTable(sqlQuery);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                holidays.Add(new Holiday
                {
                    ID = dt.Rows[i]["ID"].ToString(),
                    name = dt.Rows[i]["name"].ToString(),
                    description = dt.Rows[i]["description"].ToString(),
                    fromdate = Convert.ToDateTime(dt.Rows[i]["fromdate"]).ToString("yyyy-MM-dd"),
                    todate = Convert.ToDateTime(dt.Rows[i]["todate"]).ToString("yyyy-MM-dd"),
                    status = dt.Rows[i]["status"].ToString(),
                    school_id = dt.Rows[i]["schoolid"].ToString(),
                    created_by =  dt.Rows[i]["createdby"].ToString(),
                    created_date = Convert.ToDateTime(dt.Rows[i]["createddate"]).ToString("yyyy-MM-dd"),                    
                    modified_date = Convert.ToDateTime(dt.Rows[i]["updatedDate"]).ToString("yyyy-MM-dd"),
                    modified_by = dt.Rows[i]["updatedby"].ToString(),
                });
            }
        }
    }
}
