using PdfGenerator.Core.Domain.Interfaces;
using PuppeteerSharp;
using PuppeteerSharp.BrowserData;
using PuppeteerSharp.Media;
using System.Diagnostics.CodeAnalysis;

namespace PdfGenerator.Infrastructure.Adapters;

[ExcludeFromCodeCoverage]
public class PdfAdapter : IPdfGenerator
{
    public async Task<byte[]> GeneratePdfAsync(string htmlContent)
    {
        var browserFetcher = new BrowserFetcher(new BrowserFetcherOptions
        {
            Browser = SupportedBrowser.ChromeHeadlessShell
        });

        InstalledBrowser revisionInfo = await browserFetcher.DownloadAsync();

        var executablePath = revisionInfo.GetExecutablePath();

        if (!Path.IsPathRooted(executablePath))
        {
            executablePath = Path.Combine(Directory.GetCurrentDirectory(), executablePath);
        }

        await using IBrowser browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            ExecutablePath = executablePath
        });

        await using IPage page = await browser.NewPageAsync();

        await page.SetContentAsync(htmlContent);

        GC.Collect();

        return await page.PdfDataAsync(new PdfOptions { Format = PaperFormat.A4 });
    }
}