using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ClientPolicy _clientPolicy;
        private readonly IHttpClientFactory _clientFactory;

        public RequestController(ClientPolicy clientPolicy, IHttpClientFactory clientFactory)
        {
            _clientPolicy = clientPolicy;
            _clientFactory = clientFactory;
        }
        // GET: api/request
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            //var client = new HttpClient();
            var client = _clientFactory.CreateClient();

            //var response = await client.GetAsync("http://localhost:5002/api/response/25");
            var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(
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