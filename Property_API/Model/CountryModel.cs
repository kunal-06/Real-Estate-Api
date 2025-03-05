using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Property_API.Model
{
    public class CountryModel
    {
        [Key]
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public DateTime Created { get; set; }
    }
}
