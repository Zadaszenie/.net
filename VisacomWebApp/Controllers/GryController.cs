using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using VisacomWepApp.Models;
using VisacomWepApp.Infrastructure.Repositories;

namespace VisacomWepApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GryController : ControllerBase
    {
        private readonly IGryRepository _gamesRepository;
        private readonly IConfiguration _configuration;

        public GryController(IGryRepository gamesRepository, IConfiguration configuration)
        {
            _gamesRepository = gamesRepository;
            _configuration = configuration;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            //return Ok(new GamePresentation[]
            //{
            //    new GamePresentation
            //    {
            //        Id = 3,
            //        Nazwa = "czxxczxzczxc",
            //        DataWydania = "2020-10-10"
            //    }
            //});

            var games = await _gamesRepository.GetGamesAsync();
            return Ok(games);         
        }

        [HttpPost]
        public JsonResult Post(Gry gra)
        {
            string query = @"
                      insert into dbo.Gry values
                       (
                        '" +gra.Nazwa+ @"',
                        '" + gra.DataWydania + @"'
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
        public JsonResult Put(Gry gra)
        {
            string query = @"
                      update dbo.Gry set 
                      Nazwa = '" + gra.Nazwa + @"',
                      DataWydania = '" + gra.DataWydania + @"'
                      where Id = '" + gra.Id + @"'
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

           
            return new JsonResult("edytowano successfully");

        }

        [HttpDelete("{id}")]
        public JsonResult Put(int id)
        {
            string query = @"
                      delete from dbo.Gry
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
