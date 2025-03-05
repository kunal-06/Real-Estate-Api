using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Property_API.Model;
using System.Data;

namespace Property_API.Data
{
    public class StateRepo : Controller
    {
        public string ConnectionString { get; set; }
        public StateRepo(IConfiguration _configuration)
        {
            ConnectionString = _configuration.GetConnectionString("ConnectionString");
        }
        
        public List<StateModel> GetAllState()
        {
            var state = new List<StateModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetAll_State";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                state.Add(new StateModel()
                {
                    StateID = Convert.ToInt32(reader["StateID"]),
                    StateName = Convert.ToString(reader["StateName"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    CountryName = Convert.ToString(reader["CountryName"]),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                });
            }

            reader.Close();
            conn.Close();
            return state;

        }

        public List<StateModel> GetStateByID(int StateID)
        {
            var state = new List<StateModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetByID_State";
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                state.Add(new StateModel()
                {
                    StateID = Convert.ToInt32(reader["StateID"]),
                    StateName = Convert.ToString(reader["StateName"]),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = Convert.ToString(reader["CountryName"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return state;

        }

        public List<StateModel> GetStateByCountry(int CountryID)
        {
            var state = new List<StateModel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_StateBy_CountryID";
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                state.Add(new StateModel()
                {
                    StateID = Convert.ToInt32(reader["StateID"]),
                    StateName = Convert.ToString(reader["StateName"]),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = Convert.ToString(reader["CountryName"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return state;

        }

        public bool DeleteState(int StateID)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Delete_State";
            cmd.Parameters.AddWithValue("@StateID", StateID);
            int countEdit = cmd.ExecuteNonQuery();
            conn.Close();
            return countEdit > 0;
        }

        public bool InsertState(StateModel stateModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Insert_State";
            cmd.Parameters.AddWithValue("@StateName", stateModel.StateName);
            cmd.Parameters.AddWithValue("@CountryID", stateModel.CountryID);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }

        public bool UpdateState(StateModel stateModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Update_State";
            cmd.Parameters.AddWithValue("@StateID", stateModel.StateID);
            cmd.Parameters.AddWithValue("@StateName", stateModel.StateName);
            cmd.Parameters.AddWithValue("@CountryID", stateModel.CountryID);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }
    }
}
