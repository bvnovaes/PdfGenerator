using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;
using System.IO.Compression;

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

        [HttpPost]
        [Route("gerarzip")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateZipAsync([FromBody] List<GeneratePdfRequest> requests)
        {
            try
            {
                var pdfFiles = new List<string>();

                foreach (var request in requests)
                {
                    var pdfContent = await _generatePdfUseCase.Handle(request);
                    var pdfFileName = $"{DateTime.Now:ddMMyyyyHHmmss}.pdf";
                    var pdfFilePath = Path.Combine(Path.GetTempPath(), pdfFileName);
                    await System.IO.File.WriteAllBytesAsync(pdfFilePath, pdfContent);
                    pdfFiles.Add(pdfFilePath);
                }

                var zipFilePath = Path.Combine(Path.GetTempPath(), "documents.zip");

                using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {
                    foreach (var pdfFile in pdfFiles)
                    {
                        var entryName = Path.GetFileName(pdfFile);
                        zipArchive.CreateEntryFromFile(pdfFile, entryName);
                    }
                }

                var zipContent = await System.IO.File.ReadAllBytesAsync(zipFilePath);
                System.IO.File.Delete(zipFilePath);

                return File(zipContent, "application/zip", "documents.zip");
            }
            catch
            {
                return StatusCode(500, new { Message = "Houve um erro ao gerar o arquivo ZIP." });
            }
        }
    }
}
