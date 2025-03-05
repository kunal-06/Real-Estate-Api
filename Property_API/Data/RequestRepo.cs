using Microsoft.Data.SqlClient;
using Property_API.Model;
using System.Data;

namespace Property_API.Data
{
    public class RequestRepo
    {
        public string ConnectionString { get; set; }
        public RequestRepo(IConfiguration _configuration)
        {
            ConnectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<RequestModel> GetAllRequests()
        {
            var requests = new List<RequestModel>();

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAll_Requests";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                requests.Add(new RequestModel()
                {
                    RequestID = Convert.ToInt32(reader["RequestID"]),
                    Description = Convert.ToString(reader["Description"]),
                    BuyerID = Convert.ToInt32(reader["BuyerID"]),
                    BuyerName = Convert.ToString(reader["BuyerName"]),
                    SellerName = Convert.ToString(reader["SellerName"]),
                    SellerID = Convert.ToInt32(reader["SellerID"]),
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    Email = Convert.ToString(reader["Email"]),
                    Phone = Convert.ToString(reader["Phone"]),
                    Status = Convert.ToString(reader["Status"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }
            reader.Close();
            conn.Close();
            return requests;


        }
        public List<RequestModel> GetRequestsByID(int RequestID)
        {
            var requests = new List<RequestModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetByID_Requests";
            cmd.Parameters.Add("@RequestID", SqlDbType.Int).Value = RequestID;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                requests.Add(new RequestModel()
                {
                    RequestID = Convert.ToInt32(reader["RequestID"]),
                    Description = Convert.ToString(reader["Description"]),
                    BuyerID = Convert.ToInt32(reader["BuyerID"]),
                    SellerID = Convert.ToInt32(reader["SellerID"]),
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    BuyerName = Convert.ToString(reader["BuyerName"]),
                    SellerName = Convert.ToString(reader["SellerName"]),
                    Email = Convert.ToString(reader["Email"]),
                    Phone = Convert.ToString(reader["Phone"]),
                    Status = Convert.ToString(reader["Status"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return requests;

        }

        public List<RequestModel> GetRequestsByUserID(int UserID)
        {
            var requests = new List<RequestModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[SP_GetAllProperty_Requests]";
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                requests.Add(new RequestModel()
                {
                    RequestID = Convert.ToInt32(reader["RequestID"]),
                    Description = Convert.ToString(reader["Description"]),
                    BuyerID = Convert.ToInt32(reader["BuyerID"]),
                    SellerID = Convert.ToInt32(reader["SellerID"]),
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    BuyerName = Convert.ToString(reader["BuyerName"]),
                    SellerName = Convert.ToString(reader["SellerName"]),
                    Email = Convert.ToString(reader["Email"]),
                    Phone = Convert.ToString(reader["Phone"]),
                    Status = Convert.ToString(reader["Status"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return requests;

        }


        public bool DeleteRequests(int RequestID)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteRequest";
            cmd.Parameters.AddWithValue("@RequestID", RequestID);
            int countEdit = cmd.ExecuteNonQuery();
            conn.Close();
            return countEdit > 0;   
        }

        public bool InsertRequests(RequestModel requestModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Insert_Request";

            cmd.Parameters.AddWithValue("@PropertyID", requestModel.PropertyID);
            cmd.Parameters.AddWithValue("@SellerID", requestModel.SellerID);
            cmd.Parameters.AddWithValue("@Description", requestModel.Description);
            cmd.Parameters.AddWithValue("@Email", requestModel.Email);
            cmd.Parameters.AddWithValue("@Phone", requestModel.Phone);
            cmd.Parameters.AddWithValue("@BuyerID", requestModel.BuyerID);
            cmd.Parameters.AddWithValue("@Created", DateTime.Now);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }

        public bool UpdateRequests(RequestModel requestModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Update_Request";

            cmd.Parameters.AddWithValue("@RequestID", requestModel.RequestID);
            cmd.Parameters.AddWithValue("@PropertyID", requestModel.PropertyID);
            cmd.Parameters.AddWithValue("@SellerID", requestModel.SellerID);
            cmd.Parameters.AddWithValue("@Description", requestModel.Description);
            cmd.Parameters.AddWithValue("@Email", requestModel.Email);
            cmd.Parameters.AddWithValue("@Phone", requestModel.Phone);
            cmd.Parameters.AddWithValue("@Status", requestModel.Status);
            cmd.Parameters.AddWithValue("@BuyerID", requestModel.BuyerID);
            
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }

        public bool UpdateRequestStatus(int RequestID,String Status)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_UpdateRequests_Status";

            cmd.Parameters.AddWithValue("@RequestID", RequestID);
            cmd.Parameters.AddWithValue("@Status", Status);

            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }
    }
}
