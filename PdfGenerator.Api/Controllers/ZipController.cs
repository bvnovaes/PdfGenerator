using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;

namespace PdfGenerator.Api.Controllers;

[ApiController]
[Route("v1/api/zip")]
public class ZipController(IGenerateZipUseCase generateZipUseCase) : ControllerBase
{
    private readonly IGenerateZipUseCase _generateZipUseCase = generateZipUseCase;

    [HttpPost]
    [Route("gerar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GenerateZipAsync([FromBody] GenerateZipRequest request)
    {
        try
        {
            var zipContent = await _generateZipUseCase.Handle(request);
            return File(zipContent, "application/zip", "documents.zip");
        }
        catch
        {
            return StatusCode(500, new { Message = "Houve um erro ao gerar o arquivo ZIP." });
        }
    }
}