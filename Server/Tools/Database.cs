using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Server.Tools;

public class DataBase
    {
        private SqlConnection Connection { get; set; }

        public DataBase(string connectionString)
        {
            Connection = new SqlConnection()
            {
                ConnectionString = connectionString
            };
        }

        public bool CheckConnection()
        {
            try
            {
                Connection.Open();
            }
            catch
            {
                return false;
            }
            Connection.Close();
            return true;
        }
        public void CommandExecuter(string command)
        {
            OpenConnection();
            SqlCommand sqlCommand = new SqlCommand(command, Connection);
            sqlCommand.ExecuteNonQuery();
            CloseConnection();
        }
        public List<object[]> DataReader(string commandText)
        {
            List<object[]> result = new List<object[]>();
            OpenConnection();

            SqlCommand sqlCommand = new SqlCommand(commandText, Connection);
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    object[] row = new object[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                        row[i] = reader.GetValue(i);
                    result.Add(row);
                }
            }
            CloseConnection();
            return result;
        }
        private void OpenConnection()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
        private void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                SqlConnection.ClearPool(Connection);
            }
        }
    }
