using PdfGenerator.Core.Application.DTOs;

namespace PdfGenerator.Core.Application.Interfaces
{
    public interface IGeneratePdfUseCase
    {
        Task<byte[]> Handle(GeneratePdfRequest request);
    }
}
