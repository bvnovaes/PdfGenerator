using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;
using PdfGenerator.Core.Domain.Interfaces;

namespace PdfGenerator.Core.Application.UseCases
{
    public class GeneratePdfUseCase(IPdfGenerator pdfGenerator) : IGeneratePdfUseCase
    {
        private readonly IPdfGenerator _pdfGenerator = pdfGenerator;

        public async Task<byte[]> Handle(GeneratePdfRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.HtmlContent))
            {
                throw new ArgumentException("Conteúdo do HTML não pode ser vazio.");
            }

            return await _pdfGenerator.GeneratePdfAsync(request.HtmlContent);
        }
    }
}
