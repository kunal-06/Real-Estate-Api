using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Property_API.Model
{
    public class CityModel
    {
        [Key]
        public int CityID { get; set; } 
        public string CityName { get; set; }
        public int StateID { get; set; }
        public string? StateName { get; set; }
        public DateTime Created { get; set; }
    }
}
