using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Property_API.Model
{
    public class Visit_Booking_Model
    {
        [Key]
        public int BookingID { get; set; }
        public int BuyerID { get; set; }
        public int PropertyID { get; set; }
        public string Visite_Address { get; set; }
        public DateTime Visite_Date { get; set; }
        public DateTime Created { get; set; }
    }
}
