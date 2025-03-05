using Microsoft.Data.SqlClient;
using Property_API.Model;
using System.Data;

namespace Property_API.Data
{
    public class VisitBookRepo
    {
        public string ConnectionString { get; set; }
        public VisitBookRepo(IConfiguration _configuration)
        {
            ConnectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<Visit_Booking_Model> GetAllBooking()
        {
            var visit_Bookings = new List<Visit_Booking_Model>();

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAll_VisitBookings";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                visit_Bookings.Add(new Visit_Booking_Model()
                {
                    BookingID = Convert.ToInt32(reader["BookingID"]),
                    BuyerID = Convert.ToInt32(reader["BuyerID"]),
                    Visite_Address = Convert.ToString(reader["Visit_Address"]),
                    Visite_Date = Convert.ToDateTime(reader["VisitDate"]),
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }
            reader.Close();
            conn.Close();
            return visit_Bookings;


        }

        public List<Visit_Booking_Model> GetBookingByID(int BookingID)
        {
            var visit_Bookings = new List<Visit_Booking_Model>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetByID_VisitBooking";
            cmd.Parameters.Add("@BookingID", SqlDbType.Int).Value = BookingID;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                visit_Bookings.Add(new Visit_Booking_Model()
                {
                    BookingID = Convert.ToInt32(reader["BookingID"]),
                    BuyerID = Convert.ToInt32(reader["BuyerID"]),
                    Visite_Address = Convert.ToString(reader["Visit_Address"]),
                    Visite_Date = Convert.ToDateTime(reader["VisitDate"]),
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return visit_Bookings;

        }

        public bool DeleteBooking(int BookingID)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteVisitBooking";
            cmd.Parameters.AddWithValue("@BookingID", BookingID);
            int countEdit = cmd.ExecuteNonQuery();
            conn.Close();
            return countEdit > 0;
        }

         public bool InsertBooking(Visit_Booking_Model visit_Booking_Model)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Insert_VisitBooking";

            cmd.Parameters.AddWithValue("@BuyerID", visit_Booking_Model.BuyerID);
            cmd.Parameters.AddWithValue("@PropertyID", visit_Booking_Model.PropertyID);
            cmd.Parameters.AddWithValue("@Visit_Address", visit_Booking_Model.Visite_Address);
            cmd.Parameters.AddWithValue("@VisitDate", visit_Booking_Model.Visite_Date);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }

        public bool UpdateBooking(Visit_Booking_Model visit_Booking_Model)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Update_VisitBooking";
            cmd.Parameters.AddWithValue("@BookingID", visit_Booking_Model.BookingID);
            cmd.Parameters.AddWithValue("@BuyerID", visit_Booking_Model.BuyerID);
            cmd.Parameters.AddWithValue("@PropertyID", visit_Booking_Model.PropertyID);
            cmd.Parameters.AddWithValue("@Visit_Address", visit_Booking_Model.Visite_Address);
            cmd.Parameters.AddWithValue("@VisitDate", visit_Booking_Model.Visite_Date);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }
    }
}
