namespace PdfGenerator.Core.Domain.Interfaces
{
    public interface IPdfGenerator
    {
        Task<byte[]> GeneratePdfAsync(string htmlContent);
    }
}
