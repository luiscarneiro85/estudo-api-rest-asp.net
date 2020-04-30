using ApiTest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace ApiTest.Controllers
{
    //TODO
    //Metodo para abrir conexao
    //Metodo para fechar conexao
    //Classe PlayerService


    [RoutePrefix("api/player")]
    public class PlayerController : ApiController
    {
        string connectionString = "Server=localhost;Port=3306;Database=fifa_test;Uid=root;Pwd=;";

        /// <summary>
        /// Get all players in database
        /// </summary>
        /// <returns>List of Player</returns>
        [HttpGet]
        public List<Player> Get()
        {
            List<Player> _result = new List<Player>();
            //using (SqlConnection connection = new SqlConnection(connectionString))
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                //using (SqlCommand command = new SqlCommand())
                using(MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM player;";

                    try
                    {
                        connection.Open();
                        //using (SqlDataReader reader = command.ExecuteReader())
                        using(MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _result.Add(new Player()
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Name = reader["name"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Psn = reader["psn"].ToString()
                                });
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

                connection.Close();
            }

            return _result;
        }

        /// <summary>
        /// Return a Player by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Player</returns>
        [HttpGet]
        [Route ("{id}")]
        public Player GetPlayer([FromUri] int id)
        {
            Player _result = null;

            //using (SqlConnection connection = new SqlConnection(connectionString))
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                //using (SqlCommand command = new SqlCommand())
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM player WHERE id = @ID;";

                    command.Parameters.AddWithValue("ID", id);

                    try
                    {
                        connection.Open();
                        //using (SqlDataReader reader = command.ExecuteReader())
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _result = new Player()
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Name = reader["name"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Psn = reader["psn"].ToString()
                                };
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

                connection.Close();
            }

            return _result;
        }

        /// <summary>
        /// Insert a new player
        /// </summary>
        /// <param name="player"></param>
        [HttpPost]
        public void Post([FromBody] Player player)
        {
            if (player == null) return;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO player(name, email, psn) " +
                        "values(@NAME, @EMAIL, @PSN);";

                    command.Parameters.AddWithValue("NAME", player.Name);
                    command.Parameters.AddWithValue("EMAIL", player.Email);
                    command.Parameters.AddWithValue("PSN", player.Psn);

                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

                connection.Close();
            }
        }
    }
}