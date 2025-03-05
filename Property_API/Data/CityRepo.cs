using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Property_API.Model;
using System.Data;

namespace Property_API.Data
{
    public class CityRepo : Controller
    {
        public string ConnectionString { get; set; }
        public CityRepo(IConfiguration _configuration)
        {
            ConnectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<CityModel> GetAllCity()
        {
            var city = new List<CityModel>();
          
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                Console.WriteLine("Connection successful!");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "LOC_GetAll_City";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    city.Add(new CityModel()
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        CityName = Convert.ToString(reader["CityName"]),
                        StateName = Convert.ToString(reader["StateName"]),
                        Created = Convert.ToDateTime(reader["Created"]),
                        StateID = Convert.ToInt32(reader["StateID"])
                    });
                }

                reader.Close();
                conn.Close();
                return city;
            

        }


        public List<CityModel> GetCityByID(int CityID)
        {
            var city = new List<CityModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetByID_City";
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                city.Add(new CityModel()
                {
                    CityID = Convert.ToInt32(reader["CityID"]),
                    CityName = Convert.ToString(reader["CityName"]),
                    StateName = Convert.ToString(reader["StateName"]),
                    StateID = Convert.ToInt32(reader["StateID"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return city;

        }

        public List<CityModel> GetCityByStateID(int StateID)
        {
            var city = new List<CityModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CityBy_StateID";
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                city.Add(new CityModel()
                {
                    CityID = Convert.ToInt32(reader["CityID"]),
                    CityName = Convert.ToString(reader["CityName"]),
                    StateName = Convert.ToString(reader["StateName"]),
                    StateID = Convert.ToInt32(reader["StateID"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return city;

        }
        public bool DeleteCity(int CityID)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Delete_City";
            cmd.Parameters.AddWithValue("@CityID", CityID);
            int countEdit = cmd.ExecuteNonQuery();
            conn.Close();
            return countEdit > 0;
        }

        public bool InsertCity(CityModel cityModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Insert_City";


            cmd.Parameters.AddWithValue("@CityName", cityModel.CityName);
            cmd.Parameters.AddWithValue("@StateID", cityModel.StateID);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }

        public bool UpdateCity(CityModel cityModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Update_City";
            cmd.Parameters.AddWithValue("@CityID", cityModel.CityID);
            cmd.Parameters.AddWithValue("@CityName", cityModel.CityName);
            cmd.Parameters.AddWithValue("@StateID", cityModel.StateID);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }
    }
}
