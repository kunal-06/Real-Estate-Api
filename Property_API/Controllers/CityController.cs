using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Property_API.Data;
using Property_API.Model;

namespace Property_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public readonly CityRepo _cityRepo;
        public CityController(CityRepo _cityRepo)
        {
            this._cityRepo = _cityRepo;
        }


        [HttpGet]
        public IActionResult getAll()
        {

            var cities = _cityRepo.GetAllCity();

            return Ok(cities);

        }

        [HttpGet("{CityID}")]
        public IActionResult getByID(int CityID)
        {

            var cities = _cityRepo.GetCityByID(CityID);

            return Ok(cities);

        }
        [HttpGet("/CityByState/{StateID}")]
        public IActionResult getByStateID(int StateID)
        {

            var cities = _cityRepo.GetCityByStateID(StateID);

            return Ok(cities);

        }
        

        [HttpDelete(("{CityID}"))]
        [Authorize]
        public IActionResult deleteByID(int CityID)
        {

            var cities = _cityRepo.DeleteCity(CityID);
            if (cities)
            {
                return Ok(cities);
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpPost]
        [Authorize]
        public IActionResult InsertCity([FromBody] CityModel cityModel)
        {
            bool is_Insrted = _cityRepo.InsertCity(cityModel);

            if (!is_Insrted)
            {
                return BadRequest();
            }
            return Ok("Inserted");
        }

        [HttpPut(("{CityID}"))]
        [Authorize]
        public IActionResult UpdateCity([FromBody] CityModel cityModel)
        {
            bool is_Updated = _cityRepo.UpdateCity(cityModel);

            if (!is_Updated)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
