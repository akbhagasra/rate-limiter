﻿using Microsoft.AspNetCore.Mvc;
using RateLimiter.Services;
using System.IO;

namespace RateLimiter.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private RequestLimiterService limiterService;
        

        public HomeController(RequestLimiterService limiterService)
        {
            this.limiterService = limiterService;
        }

        [HttpGet("unlimited", Name = "Unlimited")]
        public ActionResult Unlimited()
        {
            return Ok("Response from unlimited route !!");
        }

        [HttpGet("limit/tokenbucket", Name = "TokenBucketLimit")]
        public async Task<ActionResult> TokenBucketLimit()
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress.ToString();

            Console.WriteLine($"Client Ip : {clientIp}");

            if (limiterService.processRequest(clientIp))
                return Ok("Response from token bucket limit route !!");

            return StatusCode(StatusCodes.Status429TooManyRequests, $"Too Many Requests for client : {clientIp}");
        }

        [HttpGet("limit/fixedwindowcounter", Name = "FixedWindowCounterLimit")]
        public async Task<ActionResult> FixedWindowCounterLimit()
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress.ToString();

            Console.WriteLine($"Client Ip : {clientIp}");

            if (limiterService.processRequest(clientIp))
                return Ok("Response from fixed window counter limit route !!");

            return StatusCode(StatusCodes.Status429TooManyRequests, $"Too Many Requests for client : {clientIp}");
        }
    }
}
