using System;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire.WaitingAckState.SampleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        [HttpGet("{id}/success")]
        public IActionResult SetAsSuccess(string id)
        {
            WaitingAckJobClient.MarkAsSucceeded(id, null, 0, 0);
            return Ok();
        }

        [HttpGet("{id}/fail")]
        public IActionResult SetAsFailed(string id)
        {
            WaitingAckJobClient.MarkAsFailed(id, new Exception(""));
            return Ok();
        }
    }
}