using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;
using System.Net;

namespace PdfGenerator.Web.Controllers
{
    [ApiController]
    [Route("api/pdf")]
    public class PdfController(IGeneratePdfUseCase generatePdfUseCase, IMemoryCache memoryCache) : ControllerBase
    {
        private readonly IGeneratePdfUseCase _generatePdfUseCase = generatePdfUseCase;
        private readonly IMemoryCache _memoryCache = memoryCache;

        [HttpPost]
        [Route("gerar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestRateLimitPrimeiraRequisicao(Limit = 3, Period = 60)]
        public async Task<IActionResult> GeneratePdfAsync([FromBody] GeneratePdfRequest request)
        {
            try
            {
                var userId = GetUserIdFromRequest(request); // Assuming you have a method to extract the user ID from the request
                var cacheKey = $"RequestLimit:{userId}";

                if (!_memoryCache.TryGetValue(cacheKey, out int requestCount))
                {
                    requestCount = 0;
                }

                if (requestCount >= 3)
                {
                    return StatusCode(429, new { Message = "Too many requests. Please try again later." });
                }

                requestCount++;
                _memoryCache.Set(cacheKey, requestCount, TimeSpan.FromSeconds(60));

                var pdfContent = await _generatePdfUseCase.Handle(request);
                return File(pdfContent, "application/pdf", "document.pdf");
            }
            catch
            {
                return StatusCode(500, new { Message = "An error occurred while generating the PDF file." });
            }
        }

        private string GetUserIdFromRequest(GeneratePdfRequest request)
        {
            // Implement your logic to extract the user ID from the request
            // For example, if the user ID is in the request headers:
            // return Request.Headers["UserId"].ToString();

            // Replace this with your actual implementation
            return "UserId";
        }
    }


    // Custom attribute for request rate limiting
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequestRateLimitUltimaRequiscaoAttribute : ActionFilterAttribute
    {
        public int Limit { get; set; }
        public int Period { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = GetUserIdFromContext(context.HttpContext);
            var cacheKey = $"RequestLimit:{userId}";

            var memoryCache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

            if (!memoryCache.TryGetValue(cacheKey, out int requestCount))
            {
                requestCount = 0;
            }

            if (requestCount >= Limit)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.TooManyRequests);
                return;
            }

            requestCount++;
            memoryCache.Set(cacheKey, requestCount, TimeSpan.FromSeconds(Period));

            await next();
        }

        private string GetUserIdFromContext(HttpContext context)
        {
            return context.Request.Headers["UserId"].ToString();
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequestRateLimitPrimeiraRequisicaoAttribute : ActionFilterAttribute
    {
        public int Limit { get; set; }
        public int Period { get; set; }


        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = GetUserIdFromContext(context.HttpContext);
            var cacheKey = $"RequestLimit:{userId}";

            var memoryCache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

            if (!memoryCache.TryGetValue(cacheKey, out RequestLimitData requestLimitData))
            {
                requestLimitData = new RequestLimitData
                {
                    FirstRequestTime = DateTime.UtcNow,
                    RequestCount = 0
                };
            }

            var elapsedTime = DateTime.UtcNow - requestLimitData.FirstRequestTime;

            if (elapsedTime.TotalSeconds >= Period)
            {
                // Reset the request count and update the first request time
                requestLimitData.FirstRequestTime = DateTime.UtcNow;
                requestLimitData.RequestCount = 0;
            }

            if (requestLimitData.RequestCount >= Limit)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.TooManyRequests);
                return;
            }

            requestLimitData.RequestCount++;
            memoryCache.Set(cacheKey, requestLimitData, TimeSpan.FromSeconds(Period));

            await next();
        }

        public class RequestLimitData
        {
            public DateTime FirstRequestTime { get; set; }
            public int RequestCount { get; set; }
        }

        private string GetUserIdFromContext(HttpContext context)
        {
            return context.Request.Headers["UserId"].ToString();
        }
    }
}