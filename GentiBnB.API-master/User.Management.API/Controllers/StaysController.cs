using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using User.Management.API.Models;
using User.Management.API.Models.DTO;
using User.Management.API.Services;

namespace User.Management.API.Controllers
{
    [Route("api/stays")]
    [ApiController]
    public class StayController : ControllerBase
    {
        private readonly IStayService _stayService;

        public StayController(IStayService stayService)
        {
            _stayService = stayService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<StayDTO>>>> GetAllStays()
        {
            var response = await _stayService.GetAllStays();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<StayDTO>>> GetStayById(int id)
        {
            var response = await _stayService.GetStayById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ApiResponse<List<StayDTO>>>> GetStaysByUserId(string userId)
        {
            var response = await _stayService.GetStaysByUserId(userId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("country/{country}")]
        public async Task<ActionResult<ApiResponse<List<StayDTO>>>> GetStaysByCountry(string country)
        {
            var response = await _stayService.GetStaysByCountry(country);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("city/{city}")]
        public async Task<ActionResult<ApiResponse<List<StayDTO>>>> GetStaysByCity(string city)
        {
            var response = await _stayService.GetStaysByCity(city);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("cities")]
        public async Task<IActionResult> GetAllCityNames()
        {
            var response = await _stayService.GetAllCityNames();
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<StayDTO>>> CreateStay(StayDTO stayDTO)
        {
            var response = await _stayService.CreateStay(stayDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<StayDTO>>> DeleteStayById(int id)
        {
            var response = await _stayService.DeleteStayById(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
