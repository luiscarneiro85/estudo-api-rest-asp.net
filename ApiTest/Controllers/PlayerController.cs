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
    //Inserir player POST
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

        [HttpPost]
        public void InsertPlayer([FromBody] Player player)
        {

        }
    }
}