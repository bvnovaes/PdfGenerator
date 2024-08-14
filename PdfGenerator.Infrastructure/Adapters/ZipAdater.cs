using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using PdfGenerator.Core.Domain.Interfaces;

namespace PdfGenerator.Infrastructure.Adapters;

[ExcludeFromCodeCoverage]
public class ZipAdapter : IZipGenerator
{
    public async Task<byte[]> GenerateZipAsync(IEnumerable<Tuple<string, byte[]>> pdfContents)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var pdfContent in pdfContents)
                {
                    var zipEntry = archive.CreateEntry($"{pdfContent.Item1}.pdf", CompressionLevel.Fastest);
                    await using var zipStream = zipEntry.Open();
                    await zipStream.WriteAsync(pdfContent.Item2);
                }
            }

            return memoryStream.ToArray();
        }
    }
}