using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Property_API.Data;
using Property_API.Model;

namespace Property_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public readonly CountryRepo _countryRepo;
        public CountryController(CountryRepo _countryRepo)
        {
            this._countryRepo = _countryRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var country = _countryRepo.GetAllCountry();

            return Ok(country);
        }

        [HttpGet("{CountryID}")]
        [Authorize]
        public IActionResult getByID(int CountryID)
        {

            var cities = _countryRepo.GetCountryByID(CountryID);

            return Ok(cities);

        }


        [HttpDelete(("{CountryID}"))]
        [Authorize]
        public IActionResult deleteByID(int CountryID)
        {

            var cities = _countryRepo.DeleteCountry(CountryID);
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
        public IActionResult InsertCountry([FromBody] CountryModel CountryModel)
        {
            bool is_Insrted = _countryRepo.InsertCountry(CountryModel);

            if (!is_Insrted)
            {
                return BadRequest();
            }
            return Ok("Inserted");
        }

        [HttpPut(("{CountryID}"))]
        [Authorize]
        public IActionResult UpdateCountry([FromBody] CountryModel CountryModel)
        {
            bool is_Updated = _countryRepo.UpdateCountry(CountryModel);

            if (!is_Updated)
            {
                return BadRequest();
            }
            return Ok("Updated");
        }
    }
}
