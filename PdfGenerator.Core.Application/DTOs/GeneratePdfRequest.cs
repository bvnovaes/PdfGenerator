namespace PdfGenerator.Core.Application.DTOs;

public class GeneratePdfRequest
{
    public string HtmlContent { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}