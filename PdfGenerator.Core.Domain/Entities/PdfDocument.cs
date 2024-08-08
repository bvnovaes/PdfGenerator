namespace PdfGenerator.Core.Domain.Entities;

public class PdfDocument
{
    public string HtmlContent { get; set; }
    public byte[] PdfContent { get; set; }
}
