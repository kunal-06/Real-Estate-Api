using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Property_API.Data;
using Property_API.Model;

namespace Property_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        public readonly StateRepo _stateRepo;
        public StateController(StateRepo _stateRepo)
        {
            this._stateRepo = _stateRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var state = _stateRepo.GetAllState();
            if (state != null)
            {
                return Ok(state);
            }
            return BadRequest();
        }

        [HttpGet("/StateByCountry/{CountryID}")]
        [Authorize]
        public IActionResult getbyCountryID(int CountryID) {
            var states = _stateRepo.GetStateByCountry(CountryID);
            if (states != null) { 
                return Ok(states);
            }
            return BadRequest();
        }

        [HttpGet("{StateID}")]
        [Authorize]
        public IActionResult getByID(int StateID)
        {

            var cities = _stateRepo.GetStateByID(StateID);

            return Ok(cities);

        }


        [HttpDelete(("{StateID}"))]
        [Authorize]
        public IActionResult deleteByID(int StateID)
        {

            var cities = _stateRepo.DeleteState(StateID);
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
        public IActionResult InsertState([FromBody] StateModel stateModel)
        {
            bool is_Insrted = _stateRepo.InsertState(stateModel);

            if (!is_Insrted)
            {
                return BadRequest();
            }
            return Ok("Inserted");

        }

        [HttpPut(("{StateID}"))]
        [Authorize]
        public IActionResult UpdateState([FromBody] StateModel stateModel)
        {
            bool is_Updated = _stateRepo.UpdateState(stateModel);

            if (!is_Updated)
            {
                return BadRequest();
            }
            return Ok("Updated");
        }
    }
}
