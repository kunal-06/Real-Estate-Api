using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Property_API.Model
{
    public class StateModel
    {
        [Key]
        public int StateID { get; set; }
        public string StateName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public DateTime Created { get; set; }

    }
}
