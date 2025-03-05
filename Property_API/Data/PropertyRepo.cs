using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using Property_API.Model;
using PropertyModel = Property_API.Model.PropertyModel;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Property_API.Data
{
    public class PropertyRepo
    {
        public string ConnectionString { get; set; }
        private readonly IWebHostEnvironment _environment;
        public readonly PropertyImgRepo _imgRepo;
        public PropertyRepo(IConfiguration _configuration, IWebHostEnvironment environment, PropertyImgRepo propertyImgRepo)
        {
            ConnectionString = _configuration.GetConnectionString("ConnectionString");
            _environment = environment;
            _imgRepo = propertyImgRepo;

        }

        public List<Object> PropertyCountByDate()
        {
            var prop = new List<Object>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Groupby_PropertyType";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                prop.Add(new
                {
                    uv = Convert.ToInt32(reader["PropertyCount"]),
                    name = Convert.ToString(reader["Listing_Date"]).Substring(0, 10),
                    pv = Convert.ToDateTime(reader["Listing_Date"]).Day + "-" + Convert.ToDateTime(reader["Listing_Date"]).Month,
                });
            }

            reader.Close();
            conn.Close();
            return prop;


        }

        public List<Object> GetAllProperty()
        {
            var property = new List<Object>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetAll_Properties";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                property.Add(new
                {
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    PropertyName = Convert.ToString(reader["PropertyName"]),
                    Address = Convert.ToString(reader["Address"]),
                    Description = Convert.ToString(reader["Description"]),
                    Price = Convert.ToString(reader["Price"]),
                    Title = Convert.ToString(reader["Title"]),
                    CarpetArea = Convert.ToInt32(reader["CarpetArea"]),
                    PropertyType = Convert.ToString(reader["PropertyType"]),
                    Price_per_sqft = Convert.ToString(reader["Price_per_sqft"]),
                    Latitude = Convert.ToDecimal(reader["Latitude"]),
                    Longitude = Convert.ToDecimal(reader["Longitude"]),
                    BadroomNo = Convert.ToInt32(reader["BadroomNo"]),
                    CityID = Convert.ToInt32(reader["CityID"]),
                    Listing_Date = Convert.ToDateTime(reader["Listing_Date"]),
                    SellerID = Convert.ToInt32(reader["SellerID"]),
                    Category = Convert.ToString(reader["Category"]),
                    Status = Convert.ToString(reader["Status"]),
                    ImagePath = Convert.ToString(reader["ImagePath"])
                });
            }

            reader.Close();
            conn.Close();
            return property;
        }

        public List<Object> GetAllPropertyByUser(int userID)
        {
            var property = new List<Object>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetAllUser_Properties";
            cmd.Parameters.AddWithValue("@SellerID", userID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                property.Add(new
                {
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    PropertyName = Convert.ToString(reader["PropertyName"]),
                    Address = Convert.ToString(reader["Address"]),
                    Description = Convert.ToString(reader["Description"]),
                    Price = Convert.ToString(reader["Price"]),
                    Title = Convert.ToString(reader["Title"]),
                    CarpetArea = Convert.ToInt32(reader["CarpetArea"]),
                    PropertyType = Convert.ToString(reader["PropertyType"]),
                    Price_per_sqft = Convert.ToString(reader["Price_per_sqft"]),
                    Latitude = Convert.ToDecimal(reader["Latitude"]),
                    Longitude = Convert.ToDecimal(reader["Longitude"]),
                    BadroomNo = Convert.ToInt32(reader["BadroomNo"]),
                    CityID = Convert.ToInt32(reader["CityID"]),
                    Listing_Date = Convert.ToDateTime(reader["Listing_Date"]),
                    SellerID = Convert.ToInt32(reader["SellerID"]),
                    Category = Convert.ToString(reader["Category"]),
                    Status = Convert.ToString(reader["Status"]),
                    ImagePath = Convert.ToString(reader["ImagePath"])
                });
            }

            reader.Close();
            conn.Close();
            return property;
        }

        public PropertyModel GetPropertyByID(int PropertyID)
        {

            PropertyModel property = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetByID_Property";
            cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = PropertyID;
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                property = new PropertyModel()
                {
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    PropertyName = Convert.ToString(reader["PropertyName"]),
                    Address = Convert.ToString(reader["Address"]),
                    Description = Convert.ToString(reader["Description"]),
                    Price = Convert.ToString(reader["Price"]),
                    Title = Convert.ToString(reader["Title"]),
                    CarpetArea = Convert.ToInt32(reader["CarpetArea"]),
                    PropertyType = Convert.ToString(reader["PropertyType"]),
                    Price_per_sqft = Convert.ToString(reader["Price_per_sqft"]),
                    Latitude = Convert.ToDecimal(reader["Latitude"]),
                    Longitude = Convert.ToDecimal(reader["Longitude"]),
                    BadroomNo = Convert.ToInt32(reader["BadroomNo"]),
                    CityID = Convert.ToInt32(reader["CityID"]),
                    CityName = Convert.ToString(reader["CityName"]),
                    Listing_Date = Convert.ToDateTime(reader["Listing_Date"]),
                    Category = Convert.ToString(reader["Category"]),
                    SellerID = Convert.ToInt32(reader["SellerID"]),
                    SellerName = Convert.ToString(reader["SellerName"]),
                    Status = Convert.ToString(reader["Status"]),
                    images = new List<Object>()
                };
            }
            reader.Close();
            var images = _imgRepo.GetPropertyImageByID(PropertyID);
            property.images.AddRange(images);
            conn.Close();
            return property;

        }
        
        public List<Object> GetTopTenProperty(int? UserID)
        {
            var property = new List<Object>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (UserID > 0)
            {

                cmd.CommandText = "LOC_GetTopTen_Properties";
                cmd.Parameters.AddWithValue("@UserID", UserID);
            }
            else {
                cmd.CommandText = "LOC_GetTopTenAdmin_Properties";
            }
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                property.Add(new
                {
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    PropertyName = Convert.ToString(reader["PropertyName"]),
                    Price = Convert.ToString(reader["Price"]),
                    PropertyType = Convert.ToString(reader["PropertyType"]),
                    Listing_Date = Convert.ToDateTime(reader["Listing_Date"]),
                    Category = Convert.ToString(reader["Category"]),
                    Status = Convert.ToString(reader["Status"]),
                    ImagePath = Convert.ToString(reader["ImagePath"]),
                    VisitCount = Convert.ToString(reader["VisitCount"]),
                    RequestCount = Convert.ToString(reader["RequestCount"])
                });
            }

            reader.Close();
            conn.Close();
            return property;
        }



        public bool DeleteProperty(int PropertyID)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Delete_Property";
            cmd.Parameters.AddWithValue("@PropertyID", PropertyID);
            int countEdit = cmd.ExecuteNonQuery();
            conn.Close();
            return countEdit > 0;
        }

        public bool InsertProperty(PropertyModel propertyModel)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Insert_Property";

            cmd.Parameters.AddWithValue("@PropertyName", propertyModel.PropertyName);
            cmd.Parameters.AddWithValue("@Description", propertyModel.Description);
            cmd.Parameters.AddWithValue("@Title", propertyModel.Title);
            cmd.Parameters.AddWithValue("@PropertyType", propertyModel.PropertyType);
            cmd.Parameters.AddWithValue("@Price", propertyModel.Price);
            cmd.Parameters.AddWithValue("@CarperArea", propertyModel.CarpetArea);
            cmd.Parameters.AddWithValue("@Price_per_sqft", propertyModel.Price_per_sqft);
            cmd.Parameters.AddWithValue("@Address", propertyModel.Address);
            cmd.Parameters.AddWithValue("@Latitude", propertyModel.Latitude);
            cmd.Parameters.AddWithValue("@Longitude", propertyModel.Longitude);
            cmd.Parameters.AddWithValue("@SellerID", propertyModel.SellerID);
            cmd.Parameters.AddWithValue("@Status", propertyModel.Status);
            cmd.Parameters.AddWithValue("@BadroomNo", propertyModel.BadroomNo);
            cmd.Parameters.AddWithValue("@Category", propertyModel.Category);
            cmd.Parameters.AddWithValue("@CityID", propertyModel.CityID);
            cmd.Parameters.AddWithValue("@Listing_Date", propertyModel.Listing_Date);

            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }

        //public bool UpdateProperty(PropertyModel propertyModel)
        //{
        //    SqlConnection conn = new SqlConnection(ConnectionString);
        //    conn.Open();
        //    SqlCommand cmd = conn.CreateCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "LOC_Update_Property";

        //    cmd.Parameters.AddWithValue("@PropertyID", propertyModel.PropertyID);
        //    cmd.Parameters.AddWithValue("@PropertyName", propertyModel.PropertyName);
        //    cmd.Parameters.AddWithValue("@Description", propertyModel.Description);
        //    cmd.Parameters.AddWithValue("@Title", propertyModel.Title);
        //    cmd.Parameters.AddWithValue("@PropertyType", propertyModel.PropertyType);
        //    cmd.Parameters.AddWithValue("@Price", propertyModel.Price);
        //    cmd.Parameters.AddWithValue("@CarpetArea", propertyModel.CarpetArea);
        //    cmd.Parameters.AddWithValue("@Price_per_sqft", propertyModel.Price_per_sqft);
        //    cmd.Parameters.AddWithValue("@Address", propertyModel.Address);
        //    cmd.Parameters.AddWithValue("@Latitude", propertyModel.Latitude);
        //    cmd.Parameters.AddWithValue("@Longitude", propertyModel.Longitude);
        //    cmd.Parameters.AddWithValue("@SellerID", propertyModel.SellerID);
        //    cmd.Parameters.AddWithValue("@Status", propertyModel.Status);
        //    cmd.Parameters.AddWithValue("@BadroomNo", propertyModel.BadroomNo);
        //    cmd.Parameters.AddWithValue("@Category", propertyModel.Category);
        //    cmd.Parameters.AddWithValue("@CityID", propertyModel.CityID);
        //    cmd.Parameters.AddWithValue("@Listing_Date", propertyModel.Listing_Date);

        //    int countEdit = cmd.ExecuteNonQuery();

        //    conn.Close();
        //    return countEdit > 0;
        //}

        public async Task UpdateResourceWithImage(PropertyUploadModel resource)
        {
            try
            {
                var imagePaths = new List<string>();
                if (resource.Images != null && resource.Images.Count > 0) // Check if images exist
                {
                    var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    foreach (var image in resource.Images)
                    {
                        image.OpenReadStream().Seek(0, SeekOrigin.Begin);
                        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            image.CopyTo(fileStream);
                        }

                        imagePaths.Add(Path.Combine("Images", uniqueFileName).Replace("\\", "/"));
                    }
                }

                var imagePathsJson = imagePaths.Count > 0 ? JsonConvert.SerializeObject(imagePaths) : null;


                // Ensure JSON format is correct
                //var imagePathsJson = JsonConvert.SerializeObject(imagePaths, Formatting.None);
                //var imagePathsJson = imagePaths.Any() ? JsonConvert.SerializeObject(imagePaths) : null;

                using (var connection = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand("PR_Property_UpdateResourceWithImages", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@PropertyID", resource.PropertyID);
                    cmd.Parameters.AddWithValue("@PropertyName", resource.PropertyName);
                    cmd.Parameters.AddWithValue("@Description", resource.Description);
                    cmd.Parameters.AddWithValue("@Title", resource.Title);
                    cmd.Parameters.AddWithValue("@PropertyType", resource.PropertyType);
                    cmd.Parameters.AddWithValue("@Price", resource.Price);
                    cmd.Parameters.AddWithValue("@CarperArea", resource.CarpetArea);
                    cmd.Parameters.AddWithValue("@Price_per_sqft", resource.Price_per_sqft);
                    cmd.Parameters.AddWithValue("@Address", resource.Address);
                    cmd.Parameters.AddWithValue("@Latitude", resource.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", resource.Longitude);
                    cmd.Parameters.AddWithValue("@SellerID", resource.SellerID);
                    cmd.Parameters.AddWithValue("@Status", resource.Status);
                    cmd.Parameters.AddWithValue("@BadroomNo", resource.BadroomNo);
                    cmd.Parameters.AddWithValue("@CityID", resource.CityID);
                    cmd.Parameters.AddWithValue("@Category", resource.Category);
                    cmd.Parameters.AddWithValue("@Images", imagePathsJson);

                    await connection.OpenAsync();
                    Console.WriteLine(resource.PropertyID);
                    var rowsAffected = await cmd.ExecuteNonQueryAsync();
                    Console.WriteLine($"Rows Affected: {rowsAffected}");

                }

                Console.WriteLine("Resource updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating resource: {ex.Message}");
            }
        }

        public async Task UploadPropertyWithImage(PropertyUploadModel model)
        {

            try
            {
                var imagePaths = new List<string>();
                var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var image in model.Images)
                {
                    image.OpenReadStream().Seek(0, SeekOrigin.Begin);
                    var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

                    if (string.IsNullOrEmpty(uniqueFileName))
                    {
                        throw new ArgumentNullException("Path components cannot be null.");
                    }
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    Console.WriteLine(filePath);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    imagePaths.Add(Path.Combine("Images", uniqueFileName).Replace("\\", "/"));
                }

                var imagePathsJson = JsonConvert.SerializeObject(imagePaths);
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var command = new SqlCommand("AddPropertyWithImages", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@PropertyName", model.PropertyName);
                    command.Parameters.AddWithValue("@Description", model.Description);
                    command.Parameters.AddWithValue("@Title", model.Title);
                    command.Parameters.AddWithValue("@PropertyType", model.PropertyType);
                    command.Parameters.AddWithValue("@Price", model.Price);
                    command.Parameters.AddWithValue("@CarperArea", model.CarpetArea);
                    command.Parameters.AddWithValue("@Price_per_sqft", model.Price_per_sqft);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Latitude", model.Latitude);
                    command.Parameters.AddWithValue("@Longitude", model.Longitude);
                    command.Parameters.AddWithValue("@SellerID", model.SellerID);
                    command.Parameters.AddWithValue("@Status", model.Status);
                    command.Parameters.AddWithValue("@BadroomNo", model.BadroomNo);
                    command.Parameters.AddWithValue("@CityID", model.CityID);
                    command.Parameters.AddWithValue("@Category", model.Category);
                    command.Parameters.AddWithValue("@Images", imagePathsJson);

                    await connection.OpenAsync();
                    var propertyId = await command.ExecuteScalarAsync();

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        //[HttpPost("save-property")]
        //public async Task<IActionResult> SaveProperty([FromForm] PropertyUploadModel model)
        //{
        //    var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Images");
        //    if (!Directory.Exists(uploadsFolder))
        //    {
        //        Directory.CreateDirectory(uploadsFolder);
        //    }

        //    List<string> imagePaths = new List<string>();

        //    if (model.PropertyID > 0)  // 🟢 UPDATE Existing Property
        //    {
        //        //var property = await _context.Properties.FindAsync(model.PropertyID);
        //        //if (property == null) return NotFound("Property not found.");

        //        // **Remove Deleted Images**
        //        var images = _imgRepo.GetPropertyImageByID(model.PropertyID).Select(x => x.ImagePath).ToList();
        //        var existingImages = JsonConvert.DeserializeObject<List<string>>(images) ?? new List<string>();
        //        var imagesToDelete = existingImages.Except(model.Images).ToList();
        //        foreach (var img in imagesToDelete)
        //        {
        //            var filePath = Path.Combine(uploadsFolder, Path.GetFileName(img));
        //            if (System.IO.File.Exists(filePath))
        //                System.IO.File.Delete(filePath);
        //        }

        //        // **Handle New Images**
        //        imagePaths = model.ExistingImages.ToList();
        //    }

        //    // **Handle Image Upload (for both Insert & Update)**
        //    if (model.NewImages != null)
        //    {
        //        foreach (var image in model.NewImages)
        //        {
        //            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        //            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await image.CopyToAsync(stream);
        //            }

        //            imagePaths.Add(Path.Combine("Images", uniqueFileName).Replace("\\", "/"));
        //        }
        //    }

        //    if (model.PropertyId == 0)  // 🔵 INSERT New Property
        //    {
        //        var newProperty = new Property
        //        {
        //            PropertyName = model.PropertyName,
        //            Title = model.Title,
        //            Description = model.Description,
        //            PropertyType = model.PropertyType,
        //            BadroomNo = model.BadroomNo,
        //            Status = model.Status,
        //            Price = model.Price,
        //            Price_per_sqft = model.Price_per_sqft,
        //            CarpetArea = model.CarpetArea,
        //            CityID = model.CityID,
        //            Address = model.Address,
        //            Map_details = model.Map_details,
        //            SellerID = model.SellerID,
        //            Location = model.Location,
        //            Listing_Date = model.Listing_Date,
        //            Images = JsonConvert.SerializeObject(imagePaths)
        //        };

        //        _context.Properties.Add(newProperty);
        //        await _context.SaveChangesAsync();
        //        return Ok(new { message = "Property inserted successfully.", propertyId = newProperty.PropertyID });
        //    }
        //    else  // 🟢 UPDATE Existing Property
        //    {
        //        var property = await _context.Properties.FindAsync(model.PropertyId);
        //        if (property == null) return NotFound("Property not found.");

        //        property.PropertyName = model.PropertyName;
        //        property.Title = model.Title;
        //        property.Description = model.Description;
        //        property.PropertyType = model.PropertyType;
        //        property.BadroomNo = model.BadroomNo;
        //        property.Status = model.Status;
        //        property.Price = model.Price;
        //        property.Price_per_sqft = model.Price_per_sqft;
        //        property.CarpetArea = model.CarpetArea;
        //        property.CityID = model.CityID;
        //        property.Address = model.Address;
        //        property.Map_details = model.Map_details;
        //        property.SellerID = model.SellerID;
        //        property.Location = model.Location;
        //        property.Listing_Date = model.Listing_Date;
        //        property.Images = JsonConvert.SerializeObject(imagePaths);

        //        _context.Properties.Update(property);
        //        await _context.SaveChangesAsync();
        //        return Ok(new { message = "Property updated successfully.", propertyId = property.PropertyID });
        //    }
        //}


        //    public async void UpdatePropertyWithImage(PropertyUploadModel model) {
        //        using (var connection = new SqlConnection(ConnectionString))
        //        {
        //            await connection.OpenAsync();
        //            using (var command = new SqlCommand("SaveProperty", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.Parameters.AddWithValue("@PropertyID", model.PropertyID);
        //                command.Parameters.AddWithValue("@PropertyName", model.PropertyName);
        //                command.Parameters.AddWithValue("@Title", model.Title);
        //                command.Parameters.AddWithValue("@Description", model.Description);
        //                command.Parameters.AddWithValue("@PropertyType", model.PropertyType);
        //                command.Parameters.AddWithValue("@BadroomNo", model.BadroomNo);
        //                command.Parameters.AddWithValue("@Status", model.Status);
        //                command.Parameters.AddWithValue("@Price", model.Price);
        //                command.Parameters.AddWithValue("@Price_per_sqft", model.Price_per_sqft);
        //                command.Parameters.AddWithValue("@CarpetArea", model.CarpetArea);
        //                command.Parameters.AddWithValue("@CityID", model.CityID);
        //                command.Parameters.AddWithValue("@Address", model.Address);
        //                command.Parameters.AddWithValue("@Latitude", model.Latitude);
        //                command.Parameters.AddWithValue("@SellerID", model.SellerID);
        //                command.Parameters.AddWithValue("@Longitude", model.Longitude);
        //                command.Parameters.AddWithValue("@Listing_Date", model.Listing_Date);
        //                command.Parameters.AddWithValue("@ImagePaths", JsonConvert.SerializeObject(imagePaths));

        //                var result = await command.ExecuteReaderAsync();
        //                if (await result.ReadAsync())
        //                {
        //                    return Ok(new
        //                    {
        //                        message = result["Message"].ToString(),
        //                        propertyId = Convert.ToInt32(result["PropertyID"])
        //                    });
        //                }
        //            }
        //        }

        //    }
        //}
    }
}
