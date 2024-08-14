using PdfGenerator.Core.Application.DTOs;

namespace PdfGenerator.Core.Application.Interfaces;

public interface IGenerateZipUseCase
{
    Task<byte[]> Handle(GenerateZipRequest request);
}