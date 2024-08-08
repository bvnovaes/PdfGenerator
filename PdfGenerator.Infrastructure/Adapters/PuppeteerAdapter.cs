using PdfGenerator.Core.Domain.Interfaces;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PdfGenerator.Infrastructure.Adapters;

public class PuppeteerAdapter : IPdfGenerator
{    
    public async Task<byte[]> GeneratePdfAsync(string htmlContent)
    {
        await new BrowserFetcher().DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
        await using var page = await browser.NewPageAsync();
        await page.SetContentAsync(htmlContent);

        GC.Collect();

        return await page.PdfDataAsync(new PdfOptions { Format = PaperFormat.A4 });
    }
}
