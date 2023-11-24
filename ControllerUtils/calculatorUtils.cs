using CalculadoraNET_JuanCastro.config;
using CalculadoraNET_JuanCastro.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CalculadoraNET_JuanCastro.ControllerUtils
{
    public class calculatorUtils
    {
        public async Task<List<calculator>> getOperationsResults()
        {
            var List = new List<calculator>();
            SQLserverDBConnection conn = new SQLserverDBConnection();

            using (var sql = new SqlConnection(conn.dbConnection()))
            {
                await sql.OpenAsync();

                using (var query = new SqlCommand("SELECT*FROM calculator", sql))
                {
                    await query.ExecuteNonQueryAsync();

                    using (var read = await query.ExecuteReaderAsync())
                    {

                        while (read.Read())
                        {
                            calculator cal = new calculator();
                            cal.ID = (int)read["ID"];
                            cal.USER_IDS = (int)read["USER_IDS"];
                            cal.FIRST_DIGITED_NUMBER = (double)read["FIRST_DIGITED_NUMBER"];
                            cal.SECOND_DIGITED_NUMBER = (double)read["SECOND_DIGITED_NUMBER"];
                            cal.OPERATION_SIMBOL = (string)read["OPERATION_SIMBOL"];
                            cal.RESULT = (double)read["RESULT"];
                            cal.DATES = (DateTime)read["DATES"];
                            List.Add(cal);
                        }
                    }
                }
            }

            return List;

        }


        public async Task operations(calculator cal)
        {

            SQLserverDBConnection conn = new SQLserverDBConnection();

            using (var sql = new SqlConnection(conn.dbConnection()))
            {
                await sql.OpenAsync();

                using (var query = new SqlCommand("INSERT INTO calculator(USER_IDS,FIRST_DIGITED_NUMBER,SECOND_DIGITED_NUMBER,OPERATION_SIMBOL,RESULT,DATES) VALUES(@user_id,@first_number,@second_number,@simbol,@result,@dates)", sql))
                {
                    query.Parameters.AddWithValue("@user_id", cal.USER_IDS);
                    query.Parameters.AddWithValue("@first_number",cal.FIRST_DIGITED_NUMBER);
                    query.Parameters.AddWithValue("@second_number", cal.SECOND_DIGITED_NUMBER);
                    query.Parameters.AddWithValue("@simbol", cal.OPERATION_SIMBOL);
                    query.Parameters.AddWithValue("@result", cal.RESULT);
                    query.Parameters.AddWithValue("@dates", cal.DATES);
                    await query.ExecuteNonQueryAsync();
                }
            }
        }

        public void numberOperations(double operation, calculator cal)
        {
            if (cal.OPERATION_SIMBOL == "+")
            {
                operation = cal.FIRST_DIGITED_NUMBER + cal.SECOND_DIGITED_NUMBER;
                cal.RESULT = operation;

            }
            else if (cal.OPERATION_SIMBOL == "-")
            {
                operation = cal.FIRST_DIGITED_NUMBER - cal.SECOND_DIGITED_NUMBER;
                cal.RESULT = operation;
            }
            else if (cal.OPERATION_SIMBOL == "*")
            {
                operation = cal.FIRST_DIGITED_NUMBER * cal.SECOND_DIGITED_NUMBER;
                cal.RESULT = operation;
            }
            else if (cal.OPERATION_SIMBOL == "/")
            {
                operation = cal.FIRST_DIGITED_NUMBER / cal.SECOND_DIGITED_NUMBER;
                cal.RESULT = operation;
            }


        }



        public async Task updateOperations(calculator cals)
        {

            SQLserverDBConnection conn = new SQLserverDBConnection();

            using(var sql = new SqlConnection(conn.dbConnection()))
            {
                await sql.OpenAsync();

                using(var query = new SqlCommand("UPDATE calculator SET FIRST_DIGITED_NUMBER=@number1 , SECOND_DIGITED_NUMBER=@number2, OPERATION_SIMBOL=@simbol, RESULT=@result, DATES=@dates WHERE ID=@id", sql))
                {
                    query.Parameters.AddWithValue("@number1", cals.FIRST_DIGITED_NUMBER);
                    query.Parameters.AddWithValue("@number2", cals.SECOND_DIGITED_NUMBER);
                    query.Parameters.AddWithValue("@simbol", cals.OPERATION_SIMBOL);
                    query.Parameters.AddWithValue("@result", cals.RESULT);
                    query.Parameters.AddWithValue("@id", cals.ID);
                    query.Parameters.AddWithValue("@dates", cals.DATES);
                    await query.ExecuteNonQueryAsync();

                }

            }
        }


        public async Task deleteOperations(int id)
        {

            SQLserverDBConnection conn = new SQLserverDBConnection();

            using(var sql = new SqlConnection(conn.dbConnection()))
            {
                await sql.OpenAsync();

                using(var query= new SqlCommand("DELETE FROM calculator WHERE ID=@id", sql))
                {

                    query.Parameters.AddWithValue("@id", id);
                    await query.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task<List<calculator>> getUsersOps(int id)
        {

            var list = new List<calculator>();

            SQLserverDBConnection conn = new SQLserverDBConnection();

            using (var sql = new SqlConnection(conn.dbConnection()))
            {

                await sql.OpenAsync();

                using (var query = new SqlCommand("SELECT*FROM calculator WHERE USER_IDS=@id", sql))
                {
                    query.Parameters.AddWithValue("@id", id);
                    await query.ExecuteNonQueryAsync();

                    using (var read = await query.ExecuteReaderAsync())
                    {
                        while (read.Read())
                        {
                            calculator cal = new calculator();
                            cal.ID = (int)read["ID"];
                            cal.USER_IDS = (int)read["USER_IDS"];
                            cal.FIRST_DIGITED_NUMBER = (double)read["FIRST_DIGITED_NUMBER"];
                            cal.SECOND_DIGITED_NUMBER = (double)read["SECOND_DIGITED_NUMBER"];
                            cal.OPERATION_SIMBOL = (string)read["OPERATION_SIMBOL"];
                            cal.RESULT = (double)read["RESULT"];
                            cal.DATES = (DateTime)read["DATES"];
                            list.Add(cal);
                        }
                    }
                }
            }
            return list;
        }


    }
}
