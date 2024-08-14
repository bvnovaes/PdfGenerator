namespace PdfGenerator.Core.Application.DTOs;

public class GenerateZipRequest
{
    public IEnumerable<GeneratePdfRequest> PdfRequests { get; set; } = new HashSet<GeneratePdfRequest>();
}