using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Rainfall.Api;
using Rainfall.Dtos;
using Rainfall_Api.Helpers;
using static System.Net.WebRequestMethods;

namespace Rainfall.Controllers
{
    // Removed "api" from route to follow the requirement.
    [Route("[controller]")]
    [ApiController]
    public class RainfallController : ControllerBase
    {
        private readonly IApi _api;
        public RainfallController(IApi api)
        {
            _api = api;
        }
        [HttpGet("id/{stationId}/readings")]
        public async Task<ActionResult<Responses>> Get(int stationId, [FromQuery] int count = 10)
        {
            try
            {
                if (this.HttpContext != null)
                {
                    if (!HttpRequestHelper.ValidateQueries(new string[]
{
                    "count"
}, this.HttpContext.Request.Query.Select(y => y.Key).ToArray()
)) return BadRequest();
                }

                if (count < 1 || count > 100) return BadRequest();
                var result = await _api.GetReadings(stationId, count);
                if (result.items.Count() == 0) return NotFound();

                return new Responses()
                {
                    RainfallReadingResponse = new RainfallReadingResponse()
                    {
                        Readings = result.items.Select(y => new RainfallReading()
                        {
                            AmountMeasured = y.value,
                            DateMeasured = y.dateTime
                        }).ToArray()
                    }
                };
            }
            catch (Exception err)
            {
                return StatusCode(500);
            }
        }
    }
}
