using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Property_API.Data;
using Property_API.Model;

namespace Property_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitBookingController : ControllerBase
    {
        public readonly VisitBookRepo _visitBookRepo;
        public VisitBookingController(VisitBookRepo visitBookRepo)
        {
            this._visitBookRepo = visitBookRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var state = _visitBookRepo.GetAllBooking();
            return Ok(state);
        }

        [HttpGet("{BookingID}")]
        [Authorize]
        public IActionResult getByID(int BookingID)
        {

            var booking = _visitBookRepo.GetBookingByID(BookingID);

            return Ok(booking);

        }


        [HttpDelete(("{BookingID}"))]
        [Authorize]
        public IActionResult deleteByID(int BookingID)
        {

            var booking = _visitBookRepo.DeleteBooking(BookingID);
            if (booking)
            {
                return Ok(booking);
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpPost]
        [Authorize]
        public IActionResult InsertState([FromBody] Visit_Booking_Model visit_Booking_Model)
        {
            bool is_Insrted = _visitBookRepo.InsertBooking(visit_Booking_Model);

            if (!is_Insrted)
            {
                return BadRequest();
            }
            return Ok("Inserted");

        }

        [HttpPut(("{BookingID}"))]
        [Authorize]
        public IActionResult UpdateState([FromBody] Visit_Booking_Model visit_Booking_Model)
        {
            bool is_Updated = _visitBookRepo.UpdateBooking(visit_Booking_Model);

            if (!is_Updated)
            {
                return BadRequest();
            }
            return Ok("Updated");
        }
    }
}
