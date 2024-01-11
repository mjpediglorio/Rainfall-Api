using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Rainfall.Api;
using Rainfall.Dtos;
using static System.Net.WebRequestMethods;

namespace Rainfall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallController : ControllerBase
    {
        private readonly IApi _api;
        public RainfallController(IApi api)
        {
            _api = api;
        }
        [HttpGet("id/{stationId}/readings")]
        public async Task<ActionResult<Responses>> Get(int stationId, int count = 10)
        {
            try
            {
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
