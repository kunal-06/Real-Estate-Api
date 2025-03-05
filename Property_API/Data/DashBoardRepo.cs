using Microsoft.Data.SqlClient;
using Property_API.Model;
using System.Data;

namespace Property_API.Data
{
    public class DashBoardRepo
    {
        public string ConnectionString { get; set; }
        public DashBoardRepo(IConfiguration _configuration)
        {
            ConnectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public Object GetDashBoardStatestics(int? userID)
        {
            var user = new Object();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (userID > 0)
            {
                cmd.CommandText = "SP_Dashboard_stetestics";
                cmd.Parameters.AddWithValue("@UserID", userID);
            }
            else {
                cmd.CommandText = "SP_DashboardAdmin_stetestics";
            }
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                user = new
                {
                    PropertyCount = Convert.ToString(reader["PropertyCount"]),
                    RequestCount = Convert.ToString(reader["RequestCount"]),
                    SiteCount = Convert.ToString(reader["SiteCount"])
                };
            }

            reader.Close();
            conn.Close();
            return user;
        }

        public List<Object> VisitBookStatestic(int UserID)
        {
            var user = new List<Object>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetMonthlySiteVisitCount";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Add(new
                {
                    Month = Convert.ToString(reader["Month"]),
                    VisitCount = Convert.ToString(reader["VisitCount"]),
                });
            }

            reader.Close();
            conn.Close();
            return user;
        }

        public List<Object> RentStetestic(int UserID) { 
                var user = new List<Object>();
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                Console.WriteLine("Connection successful!");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GETPropertyRents";
                cmd.Parameters.AddWithValue("@UserID",UserID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.Add(new
                    {
                        Month = Convert.ToString(reader["Month"]),
                        TotalRent = Convert.ToString(reader["TotalRent"]),
                    });
                }

                reader.Close();
                conn.Close();
                return user;
        }
        public List<Object> PropertyStetestic()
        {
            var user = new List<Object>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_MonthWisePropertyAdded";
            
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Add(new
                {
                    Month = Convert.ToString(reader["Month"]),
                    TotalRent = Convert.ToString(reader["TotalRent"]),
                });
            }

            reader.Close();
            conn.Close();
            return user;
        }

        public List<Object> PropertyTypeStetestic(int? UserID)
        {
            var user = new List<Object>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (UserID > 0)
            {
                cmd.CommandText = "GetPropertyTypeCounts";
                cmd.Parameters.AddWithValue("@UserID", UserID);
            }
            else {
                cmd.CommandText = "GetAllPropertyTypeCounts";
            }
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Add(new
                {
                    name = Convert.ToString(reader["PropertyType"]),
                    value = Convert.ToInt32(reader["Counts"]),
                });
            }

            reader.Close();
            conn.Close();
            return user;
        }
        public List<string> MonthlyVisite(int UserID)
        {
            var user = new List<string>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_MonthWise_Visit";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(Convert.ToString(reader["VisitDate"]));
                user.Add(Convert.ToString(reader["VisitDate"]));
            }

            reader.Close();
            conn.Close();
            return user;
        }
    }
}
