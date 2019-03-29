using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;
using ADO_NETCRUD_MVC.Models;

namespace ADO_NETCRUD_MVC.Controllers
{
    public class ProductController : Controller
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
      
        public ActionResult Index()
        {
            DataTable dtoTable = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {               
               sqlcon.Open();
                string query = "select * from tblProduct";
                SqlDataAdapter sqldata=new SqlDataAdapter(query,sqlcon);
                sqldata.Fill(dtoTable);
            }

            return View(dtoTable);
        }
        //
        // GET: /Product/Create
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        //
        // POST: /Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                sqlcon.Open();
                string query = "INSERT INTO tblProduct Values (@ProductName,@Price,@Count)";
                SqlCommand cmd=new SqlCommand(query,sqlcon);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@Price", productModel.Price);
                cmd.Parameters.AddWithValue("@Count", productModel.Count);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Create");
        }

        //
        // GET: /Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel productModel=new ProductModel();
            DataTable dtoTable=new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                sqlcon.Open();
                string query = "Select * from tblProduct where ProductId=@ProductId";
                SqlDataAdapter sqldata=new SqlDataAdapter(query,sqlcon);
                sqldata.SelectCommand.Parameters.AddWithValue("@ProductId", id);
                sqldata.Fill(dtoTable);
            }

            if (dtoTable.Rows.Count == 1)
            {
                productModel.ProductId = Convert.ToInt32(dtoTable.Rows[0][0].ToString());
                productModel.ProductName = dtoTable.Rows[0][1].ToString();
                productModel.Price = Convert.ToInt32(dtoTable.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dtoTable.Rows[0][3].ToString());
                return View(productModel);
            }

            else
                return RedirectToAction("Create");


        }

        //
        // POST: /Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                sqlcon.Open();
                string query = "UPDATE tblProduct SET ProductName=@ProductName,Price=@Price,Count=@Count Where ProductId=@ProductId";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@ProductId", productModel.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@Price", productModel.Price);
                cmd.Parameters.AddWithValue("@Count", productModel.Count);
                cmd.ExecuteNonQuery();

            }
            return RedirectToAction("Create");
        }

        //
        // GET: /Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                sqlcon.Open();
                string query = "DELETE from tblProduct Where ProductId=@ProductId";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@ProductId", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Create");
        }

       
    }
}
