using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace VisacomWepApp.Infrastructure.Repositories
{
    public class GryRepository : IGryRepository
    {
        private IConfiguration _configuration;

        public GryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<GamePresentation>> GetGamesAsync()
        {
            var query = @"
                      select Id, Nazwa, DataWydania from dbo.Gry";

            var sqlDataSource = _configuration.GetConnectionString("VisacomWebApp1");

            var games = new List<GamePresentation>();

            using (var connection = new SqlConnection(sqlDataSource))
            {
                await connection.OpenAsync();

                using var myCommand = new SqlCommand(query, connection);
                using var reader = await myCommand.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        games.Add(new GamePresentation
                        {
                            Id = reader.GetInt32(0),
                            Nazwa = reader.GetString(1),
                            DataWydania = reader.GetString(2)
                        });
                    }
                }

                reader.Close();
            }

            return games;
        }
    }
}
