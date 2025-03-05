using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Property_API.Model;
using System.Data;

namespace Property_API.Data
{
    public class CountryRepo : Controller
    {
        public string ConnectionString { get; set; }
        public CountryRepo(IConfiguration _configuration)
        {
            ConnectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<CountryModel> GetAllCountry()
        {
            var country = new List<CountryModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetAll_Country";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                country.Add(new CountryModel()
                {
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = Convert.ToString(reader["CountryName"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return country;
        }

        public List<CountryModel> GetCountryByID(int CountryID)
        {
            var country = new List<CountryModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetByID_Country";
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                country.Add(new CountryModel()
                {
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = Convert.ToString(reader["CountryName"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return country;

        }

        public bool DeleteCountry(int CountryID)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Delete_Country";
            cmd.Parameters.AddWithValue("@CountryID", CountryID);
            int countEdit = cmd.ExecuteNonQuery();
            conn.Close();
            return countEdit > 0;
        }

        public bool InsertCountry(CountryModel countryModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Insert_Country";

            cmd.Parameters.AddWithValue("@CountryName", countryModel.CountryName);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }

        public bool UpdateCountry(CountryModel countryModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Update_Country";
            cmd.Parameters.AddWithValue("@CountryID", countryModel.CountryID);
            cmd.Parameters.AddWithValue("@CountryName", countryModel.CountryName);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }
    }
}
