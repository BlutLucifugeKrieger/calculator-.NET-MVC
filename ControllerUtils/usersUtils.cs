using CalculadoraNET_JuanCastro.config;
using CalculadoraNET_JuanCastro.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace CalculadoraNET_JuanCastro.ControllerUtils
{
    public class usersUtils
    {
        public async Task <List<users>> getAllUsers()
        {
            var List = new List<users>();
            SQLserverDBConnection conn = new SQLserverDBConnection();

            using(var sql = new SqlConnection(conn.dbConnection()))
            {
                await sql.OpenAsync();

                using(var query = new SqlCommand("SELECT*FROM users",sql))
                {
                  await query.ExecuteNonQueryAsync();

                    using(var read = await query.ExecuteReaderAsync())
                    {

                        while (read.Read())
                        {
                            users user = new users();
                            user.USERS_ID = (int)read["USERS_ID"];
                            user.USERNAME = (string)read["USERNAME"];
                            user.FIRSTNAME = (string)read["FIRSTNAME"];
                            user.LASTNAME = (string)read["LASTNAME"];
                            user.USERS_PASS = (string)read["USERS_PASS"];
                            List.Add(user);
                        }
                    }
                }
            }

            return List;

        }


        public async Task createUser(users user)
        {

            SQLserverDBConnection conn = new SQLserverDBConnection();

            using(var sql = new SqlConnection(conn.dbConnection()))
            {
                await sql.OpenAsync();

                using(var query = new SqlCommand("INSERT INTO users(USERNAME,FIRSTNAME,LASTNAME,USERS_PASS) VALUES(@username,@firstname,@lastname,@users_pass)",sql))
                {
                    query.Parameters.AddWithValue("@username", user.USERNAME);
                    query.Parameters.AddWithValue("@firstname", user.FIRSTNAME);
                    query.Parameters.AddWithValue("@lastname", user.LASTNAME);
                    query.Parameters.AddWithValue("@users_pass", user.USERS_PASS);
                    await query.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task updateUser(users user)
        {
            SQLserverDBConnection conn = new SQLserverDBConnection();
            using(var sql = new SqlConnection(conn.dbConnection()))
            {
                await sql.OpenAsync();

                using(var query = new SqlCommand("UPDATE users SET USERNAME=@username,FIRSTNAME=@firstname,LASTNAME=@lastname,USERS_PASS=@users_pass WHERE USERS_ID=@id", sql))
                {
                    query.Parameters.AddWithValue("@username", user.USERNAME);
                    query.Parameters.AddWithValue("@firstname", user.FIRSTNAME);
                    query.Parameters.AddWithValue("@lastname", user.LASTNAME);
                    query.Parameters.AddWithValue("@users_pass", user.USERS_PASS);
                    query.Parameters.AddWithValue("@id", user.USERS_ID);
                    await query.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteUser(int id)
        {
            SQLserverDBConnection conn = new SQLserverDBConnection();
            using (var sql = new SqlConnection(conn.dbConnection()))
            {
                await sql.OpenAsync();

                using(var query = new SqlCommand("DELETE FROM users WHERE USERS_ID=@id", sql))
                {
                    query.Parameters.AddWithValue("@id", id);
                    await query.ExecuteNonQueryAsync();
                }
            }

        }



        public async Task<List<users>> userlogging(users u)
        {
            var List = new List<users>();
            
            SQLserverDBConnection conn = new SQLserverDBConnection();
            using(var sql = new SqlConnection(conn.dbConnection()))
            {
                await sql.OpenAsync();

                using(var query = new SqlCommand("SELECT*FROM users WHERE USERNAME=@username AND USERS_PASS=@pass", sql))
                {
                    query.Parameters.AddWithValue("@username", u.USERNAME);
                    query.Parameters.AddWithValue("@pass", u.USERS_PASS);
                   

                    using(var read = await query.ExecuteReaderAsync())
                    {   
                        while(read.Read())
                        {

                            u.USERS_ID = (int)read["USERS_ID"];
                            u.USERNAME = (string)read["USERNAME"];
                            u.FIRSTNAME = (string)read["FIRSTNAME"];
                            u.LASTNAME = (string)read["LASTNAME"];
                            u.USERS_PASS = (string)read["USERS_PASS"];
                            List.Add(u);
                        }
                            
                            
                        
                    }
                }
            }

            return List;

        }


        public async Task<List<users>> getAnUser(int id)
        {
            var list = new List<users>();
            SQLserverDBConnection conn = new SQLserverDBConnection();

            using(var sql = new SqlConnection(conn.dbConnection()))
            {
                await sql.OpenAsync();

                using(var query = new SqlCommand("SELECT*FROM users WHERE USERS_ID=@id",sql))
                {
                    query.Parameters.AddWithValue("@id", id);

                    using (var read = await query.ExecuteReaderAsync())
                    {

                        while (read.Read())
                        {
                            users u = new users();
                            u.USERS_ID = (int)read["USERS_ID"];
                            u.USERNAME = (string)read["USERNAME"];
                            u.FIRSTNAME = (string)read["FIRSTNAME"];
                            u.LASTNAME = (string)read["LASTNAME"];
                            u.USERS_PASS = (string)read["USERS_PASS"];
                            list.Add(u);
                        }
                    }
                }
            }

            return list;
        }




    }
}
