using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ClientPolicy _clientPolicy;
        public RequestController(ClientPolicy clientPolicy)
        {
            _clientPolicy = clientPolicy;
        }
        // GET: api/request
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            var client = new HttpClient();

            //var response = await client.GetAsync("http://localhost:5002/api/response/25");
            var response = await _clientPolicy.LinearHttpRetry.ExecuteAsync(
                () => client.GetAsync("http://localhost:5002/api/response/25"));

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