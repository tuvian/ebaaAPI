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
    public class ClassController : ApiController
    {
        List<Class> classes = new List<Class>();
        ClassHeader classList = new ClassHeader();

        public ClassHeader GetClass(string school_id)
        {
            if (classes.Count() > 0)
            {
                classes = classes.Where(p => p.school_id == school_id).ToList<Class>();
                classList.classdetails = classes;
                return classList;
            }
            else
            {
                GetClasses();
                classes = classes.Where(p => p.school_id == school_id).ToList<Class>();
                classList.classdetails = classes;
                return classList;
            }
        }

        private void GetClasses()
        {
            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            string sqlQuery = "SELECT c.id, c.school_id, c.std, c.division, c.status from class c ;";
            dt = objDAL.ExecuteDataTable(sqlQuery);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                classes.Add(new Class
                {
                    ID = dt.Rows[i]["ID"].ToString(),
                    std = dt.Rows[i]["std"].ToString(),
                    school_id = dt.Rows[i]["school_id"].ToString(),
                    division = dt.Rows[i]["division"].ToString(),
                    status = dt.Rows[i]["status"].ToString()
                });
            }   
        }

        public ClassHeader GetClassesByID(string class_id)
        {
            if (classes.Count() > 0)
            {
                classes = classes.Where(p => p.ID == class_id).ToList<Class>();
                classList.classdetails = classes;
                return classList;
            }
            else
            {
                GetClasses();
                classes = classes.Where(p => p.ID == class_id).ToList<Class>();
                classList.classdetails = classes;
                return classList;
            }
        }
    }
}
