using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Property_API.Data;

namespace Property_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        public readonly DashBoardRepo _dashBoardRepo;
        public DashboardController(DashBoardRepo dashBoardRepo)
        {
            _dashBoardRepo = dashBoardRepo;
        }

        [HttpGet("{UserID?}")]
        public ActionResult GetStatestics(int? UserID) { 
            var st = _dashBoardRepo.GetDashBoardStatestics(UserID);
            return Ok(st);
        }

        [HttpGet("TotalRent/{UserID}")]
        public ActionResult GetRentByMount(int UserID)
        {
            var st = _dashBoardRepo.RentStetestic(UserID);
            return Ok(st);
        }
        [HttpGet("MonthlyUserAdded")]
        public ActionResult GetUserCountByMount(int UserID)
        {
            var st = _dashBoardRepo.MonthlyUserStetestic();
            return Ok(st);
        }
        [HttpGet("TotalPropertyCount")]
        public ActionResult GetPropertyByMount(int UserID)
        {
            var st = _dashBoardRepo.PropertyStetestic();
            return Ok(st);
        }
        [HttpGet("TotalPropertyTypeCount/{UserID?}")]
        public ActionResult GetPropertyTypeCount(int? UserID)
        {
            var st = _dashBoardRepo.PropertyTypeStetestic(UserID);
            return Ok(st);
        }

        [HttpGet("VisiteCount/{UserID}")]
        public ActionResult GetVisiteCountByMonth(int UserID)
        {
            var st = _dashBoardRepo.VisitBookStatestic(UserID);
            return Ok(st);
        }

        [HttpGet("MonthWiseVisite/{UserID}")]
        public ActionResult MounthlyVisits(int UserID)
        {
            var st = _dashBoardRepo.MonthlyVisite(UserID);
            return Ok(st);
        }

    }
}
