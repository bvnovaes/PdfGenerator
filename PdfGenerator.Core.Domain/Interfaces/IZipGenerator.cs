namespace PdfGenerator.Core.Domain.Interfaces;

public interface IZipGenerator
{
    Task<byte[]> GenerateZipAsync(IEnumerable<Tuple<string, byte[]>> pdfContents);
}