using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Property_API.Data;
using Property_API.Model;
using System.Data;


namespace Property_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        public readonly PropertyRepo _propertyRepo;
       
        public PropertyController(PropertyRepo _propertyRepo)
        {
            this._propertyRepo = _propertyRepo;
            
        }


        [HttpGet]
        public IActionResult getAll()
        {

            var properties = _propertyRepo.GetAllProperty(); 

            return Ok(properties);

        }
        [HttpGet("User/{userID}")]
        [Authorize]
        public IActionResult getAllByUser(int userID)
        {

            var properties = _propertyRepo.GetAllPropertyByUser(userID);

            return Ok(properties);

        }

        [HttpGet("/Property/Topten/{UserID?}")]
        public IActionResult getTopten(int? UserID)
        {
            var properties = _propertyRepo.GetTopTenProperty(UserID);
            return Ok(properties);

        }

        [HttpGet("/Properties/AddbyDayCount")]
        public IActionResult getCountByDate()
        {
            var properties = _propertyRepo.PropertyCountByDate();
            return Ok(properties);
        }

        [HttpGet("{PropertyID}")]
        public IActionResult getByID(int PropertyID)
        {

            var properties = _propertyRepo.GetPropertyByID(PropertyID);

            return Ok(properties);

        }

        [HttpDelete(("{PropertyID}"))]
        [Authorize]
        public IActionResult deleteByID(int PropertyID)
        {

            var properties = _propertyRepo.DeleteProperty(PropertyID);
            if (properties)
            {
                return Ok(properties);
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpPost]
        [Authorize]
        public IActionResult InsertCity([FromBody] PropertyModel propertyModel)
        {
            bool is_Insrted = _propertyRepo.InsertProperty(propertyModel);

            if (!is_Insrted)
            {
                return BadRequest();
            }
            return Ok("Inserted");
        }

        [HttpPut("UpdatePropertyWithImages/{PropertyID}")]
        public async Task<IActionResult> UpdatePropertyWithImages(int PropertyID, [FromForm] PropertyUploadModel resourceUpload)
        {
            if (resourceUpload == null || PropertyID != resourceUpload.PropertyID)
            {
                return BadRequest();
            }
            _propertyRepo.UpdateResourceWithImage(resourceUpload);
            return Ok(new { message = "Resource updated  successfully." });
            //return Ok("inserted");
        }


        [HttpPost("UploadPropertyWithImages")]
        [Authorize]
        public async Task<IActionResult> UploadProperty([FromForm] PropertyUploadModel model)
        {
            if (model.Images == null || model.Images.Count == 0)
            {
                return BadRequest("No images provided.");
            }
            _propertyRepo.UploadPropertyWithImage(model);
            return Ok("inserted");
        }

    }
}
