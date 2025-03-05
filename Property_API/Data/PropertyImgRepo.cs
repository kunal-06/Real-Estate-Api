using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Property_API.Model;
using System;
using System.Data;
using System.Security.Cryptography;

namespace Property_API.Data
{
    public class PropertyImgRepo
    {
        public string ConnectionString { get; set; }
        private readonly IWebHostEnvironment _environment;
        public PropertyImgRepo(IConfiguration _configuration, IWebHostEnvironment environment)
        {
            ConnectionString = _configuration.GetConnectionString("ConnectionString");
            _environment = environment;
        }

       
        public List<Property_Img_Model> GetAllPropertyImage()
        {
            var city = new List<Property_Img_Model>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetAll_Property_Images";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                city.Add(new Property_Img_Model()
                {
                    ImageID = Convert.ToInt32(reader["ImageID"]),
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    ImagePath = reader["ImagePath"].ToString(),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return city;

        }

        public List<Property_Img_Model> GetAllUserPropertyImage(int userID)
        {
            var city = new List<Property_Img_Model>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetAllUser_Property_Images";
            cmd.Parameters.AddWithValue("@UserID", userID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                city.Add(new Property_Img_Model()
                {
                    ImageID = Convert.ToInt32(reader["ImageID"]),
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    ImagePath = reader["ImagePath"].ToString(),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return city;

        }


        public List<Property_Img_Model> GetPropertyImageByID(int PropertyID)
        {
            var city = new List<Property_Img_Model>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine("Connection successful!");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_GetByID_Property_Images";
            cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = PropertyID;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                city.Add(new Property_Img_Model()
                {
                    ImageID = Convert.ToInt32(reader["ImageID"]),
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    ImagePath = Convert.ToString (reader["ImagePath"]),
                    Created = Convert.ToDateTime(reader["Created"])
                });
            }

            reader.Close();
            conn.Close();
            return city;

        }
        public bool DeletePropertyImage(int ID)
        {
            Console.WriteLine(ID);
            var image = new Property_Img_Model();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "LOC_GetByID_Images";
            cmd1.Parameters.AddWithValue("@ImageID", ID);
            SqlDataReader reader = cmd1.ExecuteReader();

            if (reader.Read())
            {
                image = new Property_Img_Model()
                {
                    ImageID = Convert.ToInt32(reader["ImageID"]),
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    ImagePath = Convert.ToString(reader["ImagePath"]),
                    Created = Convert.ToDateTime(reader["Created"])
                };                                          
            }
            reader.Close();

            if (string.IsNullOrEmpty(image.ImagePath))
            {
                return false;
            }

            var fullPath = Path.Combine(_environment.ContentRootPath, image.ImagePath.Replace("/", "\\"));

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);  // Delete image file
            }
            else
            {
                
            }
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "LOC_Delete_Property_Images";
            cmd2.Parameters.AddWithValue("@ImageID", ID);
            int countEdit = cmd2.ExecuteNonQuery();
            conn.Close();
            return countEdit > 0;
        }


        public bool UpdatePropertyImage(Property_Img_Model property_Img_Model)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOC_Update_Property_Images";
            cmd.Parameters.AddWithValue("@ImageID", property_Img_Model.ImageID);
            cmd.Parameters.AddWithValue("@PropertyID", property_Img_Model.PropertyID);
            cmd.Parameters.AddWithValue("@ImagePath", property_Img_Model.ImagePath);
            int countEdit = cmd.ExecuteNonQuery();

            conn.Close();
            return countEdit > 0;
        }

        public async Task UploadImage(ImgModel model)
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
                    var command = new SqlCommand("AddImages", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@PropertyID", model.PropertyID);
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
    }
}
