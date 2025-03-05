
using System.ComponentModel.DataAnnotations;

namespace Property_API.Model
{
    public class PropertyModel
    {
        [Key]
        public int PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PropertyType { get; set; }
        public int BadroomNo { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string Price_per_sqft { get; set; }
        public int CarpetArea { get; set; }
        public int CityID { get; set; }
        public string? CityName { get; set; }

        public string? Category { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int SellerID { get; set; }
        public string? SellerName { get; set; }

        public List<Object>? images { get; set; }

        public DateTime? Listing_Date { get; set; }

    }

    public class PropertyUploadModel
    {
        [Key]
        public int PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PropertyType { get; set; }
        public int BadroomNo { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string Price_per_sqft { get; set; }
        public int CarpetArea { get; set; }
        public int CityID { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int SellerID { get; set; }

        public DateTime? Listing_Date { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
