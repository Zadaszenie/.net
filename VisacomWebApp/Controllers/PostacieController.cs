using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using VisacomWepApp.Models;
namespace VisacomWepApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostacieController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PostacieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
                      select * from dbo.Postacie";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VisacomWebApp1");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }
        [HttpPost]
        public JsonResult Post(Postacie postac)
        {
            string query = @"
                      insert into dbo.Postacie (Imie) values
                       (
                        '" + postac.Imie + @"'    
                        )";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VisacomWebApp1");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("added successfully");

        }
        [HttpPut]
        public JsonResult Put(Postacie postac)
        {
            string query = @"
                      update dbo.Postacie set 
                      Imie = '" + postac.Imie + @"'
                      where Id = '" + postac.Id + @"'
                      ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VisacomWebApp1");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("UPDATED successfully");

        }

        [HttpDelete("{id}")]
        public JsonResult Put(int id)
        {
            string query = @"
                      delete from dbo.Postacie
                      where Id = '" + id + @"'
                      ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VisacomWebApp1");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted successfully");

        }
    }
}
