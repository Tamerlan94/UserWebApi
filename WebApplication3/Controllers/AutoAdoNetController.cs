using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoAdoNetController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=simple;Trusted_Connection=True;";
            List<Auto> autos = new List<Auto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM auto";
                command.Connection = connection;

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            object id = reader["Id"];
                            object mark = reader["Mark"];
                            object color = reader["Color"];
                            object releaseDate = reader["ReleaseDate"];
                            object cost = reader["Cost"];
                            object bodyType = reader["BodyType"];
                            object engineVolume = reader["EngineVolume"];
                            object clearedByCustomer = reader["ClearedByCustomer"];
                            object comment = reader["Comment"];

                            autos.Add(new Auto
                            {
                                Id = (int)id,
                                Mark = (string)mark,
                                Color = (string)color,
                                ReleaseDate = (DateTime)releaseDate,
                                Cost = (int)cost,
                                BodyType = (string)bodyType,
                                ClearedByCustomer = (bool)clearedByCustomer,
                                EngineVolume = (int)engineVolume,
                                Comment = (string)comment
                            });
                        }
                    }
                return Ok(autos);
            }
        }

        [HttpPost]
        public async Task<object> Post([FromBody] Auto auto)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=simple;Trusted_Connection=True;";
            string sqlExpression = $"INSERT INTO Auto (Id, Mark, Color, ReleaseDate, Cost, BodyType, ClearedByCustomer, EngineVolume, Comment) VALUES ({auto.Id}, {auto.Mark}, {auto.BodyType}, {auto.Cost}, {auto.Comment}, {auto.ClearedByCustomer}, {auto.Color}, {auto.EngineVolume})";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = await command.ExecuteNonQueryAsync();
                if (number == 0)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok();
                }
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=simple;Trusted_Connection=True;";
            string sqlExpression = $"DELETE FROM Auto WHERE Id={id}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = await command.ExecuteNonQueryAsync();
                if (number == 0)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok();
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Auto auto)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=simple;Trusted_Connection=True;";
            string sqlExpression = $"UPDATE Auto SET Mark={auto.Mark}, Color={auto.Color}, ReleaseDate={auto.ReleaseDate}, Cost={auto.Cost}, BodyType={auto.BodyType}, ClearedByCustomer={auto.ClearedByCustomer}, EngineVolume={auto.EngineVolume}, Comment={auto.Comment} WHERE Id={auto.Id}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = await command.ExecuteNonQueryAsync();
                if (number == 0)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok();
                }
            }
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int id)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=simple;Trusted_Connection=True;";
            string sqlExpression = $"SELECT FROM Auto WHERE Id={id}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    object autoId = reader["Id"];
                    object mark = reader["Mark"];
                    object color = reader["Color"];
                    object releaseDate = reader["ReleaseDate"];
                    object cost = reader["Cost"];
                    object bodyType = reader["BodyType"];
                    object engineVolume = reader["EngineVolume"];
                    object clearedByCustomer = reader["ClearedByCustomer"];
                    object comment = reader["Comment"];

                    return Ok(new Auto
                    {
                        Id = (int)autoId,
                        Mark = (string)mark,
                        Color = (string)color,
                        ReleaseDate = (DateTime)releaseDate,
                        Cost = (int)cost,
                        BodyType = (string)bodyType,
                        ClearedByCustomer = (bool)clearedByCustomer,
                        EngineVolume = (int)engineVolume,
                        Comment = (string)comment
                    }));
                }

            }
        }
    }
}
