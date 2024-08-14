using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;
using PdfGenerator.Core.Domain.Interfaces;

namespace PdfGenerator.Core.Application.UseCases;

public class GenerateZipUseCase(IGeneratePdfUseCase generatePdfUseCase, IZipGenerator zipGenerator)
    : IGenerateZipUseCase
{
    public async Task<byte[]> Handle(GenerateZipRequest request)
    {
        var pdfContents = new Dictionary<string, byte[]>();

        foreach (var pdfRequest in request.PdfRequests)
        {
            var pdfContent = await generatePdfUseCase.Handle(pdfRequest);
            pdfContents.Add(pdfRequest.FileName, pdfContent);
        }

        return await zipGenerator.GenerateZipAsync(pdfContents);
    }
}