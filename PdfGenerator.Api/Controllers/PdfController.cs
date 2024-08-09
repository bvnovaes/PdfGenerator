using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;

namespace PdfGenerator.Api.Controllers
{
    [ApiController]
    [Route("v1/api/pdf")]
    public class PdfController(IGeneratePdfUseCase generatePdfUseCase) : ControllerBase
    {
        private readonly IGeneratePdfUseCase _generatePdfUseCase = generatePdfUseCase;

        [HttpPost]
        [Route("gerar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GeneratePdfAsync([FromBody] GeneratePdfRequest request)
        {
            try
            {
                var pdfContent = await _generatePdfUseCase.Handle(request);
                return File(pdfContent, "application/pdf", "document.pdf");
            }
            catch
            {
                return StatusCode(500, new { Message = "Houve um erro ao gerar o arquivo PDF." });
            }
        }
    }
}
