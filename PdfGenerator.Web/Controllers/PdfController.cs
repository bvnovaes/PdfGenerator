using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;

namespace PdfGenerator.Web.Controllers
{
    [ApiController]
    [Route("api/pdf")]
    public class PdfController(IGeneratePdfUseCase generatePdfUseCase) : ControllerBase
    {
        private readonly IGeneratePdfUseCase _generatePdfUseCase = generatePdfUseCase;

        [HttpPost]
        [Route("gerar")]
        public async Task<IActionResult> GeneratePdf([FromBody] GeneratePdfRequest request)
        {
            try
            {
                var pdfContent = await _generatePdfUseCase.Handle(request);
                return File(pdfContent, "application/pdf", "document.pdf");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch
            {
                return StatusCode(500, new { Message = "Houve um erro ao gerar o arquivo PDF."});
            }
        }
    }
}
