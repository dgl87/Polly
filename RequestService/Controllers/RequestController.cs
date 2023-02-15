using Microsoft.AspNetCore.Mvc;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        // GET: api/request
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5002/api/response/25");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> ResponseService returned SUCESS");
                return Ok();
            }
            Console.WriteLine("--> ResponseService returned FAILURE");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}