using MediatorFromScratch.Handler;
using MediatorFromScratch.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MediatorFromScratch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Produces("application/json", "text/plain")]
        [HttpGet("getname")]
        [ProducesResponseType((int)HttpStatusCode.OK,Type=typeof(string))]
        public async Task<IActionResult> GetName()
        {
            var result =await _mediator.SendAsync(new GetNameRequest() { Name = "Semih" });
            return Ok(result);
        }

        [Produces("application/json", "text/plain")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<IActionResult> GetName(CommandExampleRequest getNameRequest)
        {
            var result = await _mediator.SendAsync(getNameRequest);

            return Ok(result);
        }

    }
}
