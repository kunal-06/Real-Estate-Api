using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Property_API.Model
{
    public class RequestModel
    {
        [Key]
        public int RequestID { get; set; }
        public string Description { get; set; }
        public int BuyerID { get; set; }
        public string? BuyerName {  get; set; }
        public int SellerID { get; set; }
        public string? SellerName { get; set; }
        public int PropertyID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Status { get; set; }
        public DateTime Created { get; set; }
    }
}
