using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Data.SqlClient;
using Property_API.Controllers;
using Property_API.Data;
using Property_API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Property_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImgController : ControllerBase
    {
        
       
        public readonly PropertyImgRepo _propertyImgRepo;
        public readonly IWebHostEnvironment _environment;
        public PropertyImgController(PropertyImgRepo _propertyImgRepo, IWebHostEnvironment environment)
        {
            this._propertyImgRepo = _propertyImgRepo;
            _environment = environment;
        }


        [HttpGet]
        public IActionResult getAllImages()
        {

            var images = _propertyImgRepo.GetAllPropertyImage();

            return Ok(images);

        }
        [HttpGet("User/{userID}")]
        [Authorize]
        public IActionResult getAllUserImages(int userID)
        {
            try
            {
                var images = _propertyImgRepo.GetAllUserPropertyImage(userID);
                if (images == null)
                {
                    return NotFound(new { message = "Image not found." });
                }
                return Ok(images);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
            

        }
        

        [HttpGet("get-image/{propertyId}")]
        public IActionResult GetImageByPropertyID(int propertyId)
        {
            try
            {
                var list = _propertyImgRepo.GetPropertyImageByID(propertyId);
                if (list == null)
                {
                    return NotFound(new { message = "Image not found." });
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

      

        [HttpDelete(("{ImageID}"))]
        [Authorize]
        public IActionResult deleteImage(int ImageID)
        {

            var images = _propertyImgRepo.DeletePropertyImage(ImageID);
            if (images)
            {
                return Ok(images);
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpPut(("{ImageID}"))]
        [Authorize]
        public IActionResult UpdateImage([FromBody] Property_Img_Model propertyImgModel)
        {
            bool is_Updated = _propertyImgRepo.UpdatePropertyImage(propertyImgModel);

            if (!is_Updated)
            {
                return BadRequest();
            }
            return Ok("Updated");
        }

        [HttpPost("UploadImages")]
        [Authorize]
        public async Task<IActionResult> UploadProperty([FromForm] ImgModel model)
        {
            if (model.Images == null || model.Images.Count == 0)
            {
                return BadRequest("No images provided.");
            }
            _propertyImgRepo.UploadImage(model);
            return Ok("inserted");
        }

    }
}