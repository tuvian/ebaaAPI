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
    public class ProductController : ApiController
    {

        List<Product> products = new List<Product>();
        List<Events> events = new List<Events>();

        private IEnumerable<Product> GetAllProducts()
        {
            GetProducts();
            return products;
        }

        [ActionName("Events")]
        public IEnumerable<Events> GetAllEvents()
        {
            GetEvents();
            return events;
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
                    start_date = dt.Rows[i]["start_date"].ToString(),
                    end_date = dt.Rows[i]["end_date"].ToString()
                });
            }

                //eMallDA objDA = new eMallDA();
                //DataTable resultTable = new DataTable();
                //objEvents.search_operator = objEvents.search_operator == null ? " OR " : objEvents.search_operator;
                //string sqlQuery = "SELECT e.id,e.title,e.school_id,e.start_date,e.end_date,e.description,e.status,e.created_date, e.created_by, " +
                //    "s.code as school_code " +
                //    "FROM events e INNER JOIN school s ON e.school_id = s.id  " +
                //    "WHERE ((0 = " + objEvents.ID + ") OR (e.id = " + objEvents.ID + ")) AND " +
                //    "((0 = " + objEvents.status + ") OR (e.status = " + objEvents.status + ")) AND " +
                //    "((0 = " + objEvents.school_id + ") OR (e.school_id = " + objEvents.school_id + ")) AND " +
                //    "((('' = '" + objEvents.title + "') OR (e.title like '%" + objEvents.title + "%' )) " + objEvents.search_operator +
                //    "(('' = '" + objEvents.description + "') OR (e.description like '%" + objEvents.description + "%' ))) ORDER BY e.start_date;";
                //resultTable = objDA.ExecuteDataTable(sqlQuery);
                //return resultTable;
        }

        private void GetProducts()
        {
            products.Add(new Product { Id = 1, Name = "Television", Category = "Electronic", Price = 82000 });
            products.Add(new Product { Id = 2, Name = "Refrigerator", Category = "Electronic", Price = 23000 });
            products.Add(new Product { Id = 3, Name = "Mobiles", Category = "Electronic", Price = 20000 });
            products.Add(new Product { Id = 4, Name = "Laptops", Category = "Electronic", Price = 45000 });
            products.Add(new Product { Id = 5, Name = "iPads", Category = "Electronic", Price = 67000 });
            products.Add(new Product { Id = 6, Name = "Toys", Category = "Gift Items", Price = 15000 });
        }

        public IEnumerable<Product> GetProducts(int selectedId)
        {
            if (products.Count() > 0)
            {
                return products.Where(p => p.Id == selectedId);
            }
            else
            {
                GetProducts();
                return products.Where(p => p.Id == selectedId);
            }
        }
    }
}
