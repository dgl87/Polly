using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ResponseController : ControllerBase
    {
        // GET /api/response/3
        [Route("{id:int}")]
        [HttpGet]
        public ActionResult GetResponse(int id)
        {
            Random rnd = new Random();
            var rndInteger = rnd.Next(1, 101);
            if (rndInteger >= id)
            {
                System.Console.WriteLine("--> Failure - Generate a HTTP 500");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            System.Console.WriteLine("--> Sucess - Generate a HTTP 200");
            return Ok();
        }
    }
}