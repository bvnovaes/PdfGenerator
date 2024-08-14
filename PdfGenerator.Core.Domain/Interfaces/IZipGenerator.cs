namespace PdfGenerator.Core.Domain.Interfaces;

public interface IZipGenerator
{
    Task<byte[]> GenerateZipAsync(IDictionary<string, byte[]> pdfContents);
}