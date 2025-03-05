using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_API.Data;
using Property_API.Model;

namespace Property_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        public readonly RequestRepo _requestRepo;
        public RequestController(RequestRepo requestRepo)
        {
            this._requestRepo = requestRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var state = _requestRepo.GetAllRequests();
            return Ok(state);
        }


        [HttpGet("{RequestID}")]
        [Authorize]
        public IActionResult getByID(int RequestID)
        {

            var requests = _requestRepo.GetRequestsByID(RequestID);

            return Ok(requests);

        }

        [HttpGet("User/{UserID}")]
        [Authorize]
        public IActionResult getByUserID(int UserID)
        {

            var requests = _requestRepo.GetRequestsByUserID(UserID);

            return Ok(requests);

        }


        [HttpDelete(("{RequestID}"))]
        [Authorize]
        public IActionResult deleteByID(int RequestID)
        {

            var requests = _requestRepo.DeleteRequests(RequestID);
            if (requests)
            {
                return Ok(requests);
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpPost]
        [Authorize]
        public IActionResult InsertState([FromBody] RequestModel requestModel)
        {
            bool is_Insrted = _requestRepo.InsertRequests(requestModel);

            if (!is_Insrted)
            {
                return BadRequest();
            }
            return Ok("Inserted");

        }

        [HttpPut(("{RequestID}"))]
        [Authorize]
        public IActionResult UpdateState([FromBody] RequestModel requestModel)
        {
            bool is_Updated = _requestRepo.UpdateRequests(requestModel);

            if (!is_Updated)
            {
                return BadRequest();
            }
            return Ok("Updated");
        }

        [HttpPut(("Status/{RequestID}/{Status}"))]
        [Authorize]
        public IActionResult UpdateStatus(int RequestID,string Status)
        {
            bool is_Updated = _requestRepo.UpdateRequestStatus(RequestID,Status);

            if (!is_Updated)
            {
                return BadRequest();
            }
            return Ok("Updated");
        }
    }
}
