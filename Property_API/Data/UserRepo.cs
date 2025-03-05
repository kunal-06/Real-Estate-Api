using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Property_API.Model;
using System.Data;

namespace Property_API.Data
{
    public class UserRepo : Controller
    {
        public string ConnectionString { get; set; }
        public UserRepo(IConfiguration _configuration)
        {
            ConnectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<UserModel> GetAllUser()
        {
            var user = new List<UserModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAll_User";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Add(new UserModel()
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = Convert.ToString(reader["UserName"]),
                    Email = Convert.ToString(reader["Email"]),
                    Password = Convert.ToString(reader["Password"]),
                    Phone = Convert.ToString(reader["Phone"]),
                    Role = Convert.ToString(reader["Role"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return user;
        }

        public List<UserModel> GetUserByID(int UserID) {
            var user = new List<UserModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Get_User_ByID";
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Add(new UserModel()
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = Convert.ToString(reader["UserName"]),
                    Email = Convert.ToString(reader["Email"]),
                    Password = Convert.ToString(reader["Password"]),
                    Phone = Convert.ToString(reader["Phone"]),
                    Role = Convert.ToString(reader["Role"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }
            reader.Close();
            conn.Close();
            return user;
        }

        public bool DeleteUser(int UserID)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Delete_User";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            int countEdit = cmd.ExecuteNonQuery();
            conn.Close();
            return countEdit > 0;
        }

        public bool InsertUser(UserModel userModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Insert_User";


            cmd.Parameters.AddWithValue("@UserName", userModel.UserName);
            cmd.Parameters.AddWithValue("@Email", userModel.Email);
            cmd.Parameters.AddWithValue("@Password", userModel.Password);
            cmd.Parameters.AddWithValue("@Phone", userModel.Phone);
            cmd.Parameters.AddWithValue("@Role", userModel.Role);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }

        public bool UpdateUser(UserModel userModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Update_User";
            cmd.Parameters.AddWithValue("@UserID", userModel.UserID);
            cmd.Parameters.AddWithValue("@UserName", userModel.UserName);
            cmd.Parameters.AddWithValue("@Email", userModel.Email);
            cmd.Parameters.AddWithValue("@Password", userModel.Password);
            cmd.Parameters.AddWithValue("@Phone", userModel.Phone);
            cmd.Parameters.AddWithValue("@Role", userModel.Role);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }

        public UserModel AuthenticateUser(UserLogin userLogin) {
            var user = new UserModel();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_IS_Login";
            cmd.Parameters.AddWithValue("@Email", userLogin.Email);
            cmd.Parameters.AddWithValue("@Password", userLogin.Password);

            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                user = new UserModel()
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = Convert.ToString(reader["UserName"]),
                    Password = "",
                    Created = DateTime.Now,
                    Phone = Convert.ToString(reader["Phone"]),
                    Email = Convert.ToString(reader["Email"]),
                    Role = Convert.ToString(reader["Role"]),
                };
            }

            conn.Close();
            return user;
        }
    }
}
