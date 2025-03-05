using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Property_API.Model
{
    public class Property_Img_Model
    {
        [Key]
        public int ImageID { get; set; }
        public string ImagePath { get; set; }
        public int PropertyID { get; set; }
        public DateTime? Created { get; set; }

    }

    public class ImgModel
    {
        [Key]
        public int ImageID { get; set; }
        public string? ImagePath { get; set; }
        public int PropertyID { get; set; }
        public List<IFormFile> Images { get; set; }
        public DateTime? Created { get; set; }

    }
}
