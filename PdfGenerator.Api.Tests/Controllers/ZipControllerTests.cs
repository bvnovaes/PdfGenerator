using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PdfGenerator.Api.Controllers;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;

namespace PdfGenerator.Api.Tests.Controllers;

public class ZipControllerTests
{
    private readonly Mock<IGenerateZipUseCase> _generateZipUseCaseMock;
    private readonly ZipController _zipController;

    public ZipControllerTests()
    {
        _generateZipUseCaseMock = new Mock<IGenerateZipUseCase>();
        _zipController = new ZipController(_generateZipUseCaseMock.Object);
    }

    [Fact]
    public async Task GenerateZip_ShouldReturnZipFile_WhenPdfFilesAreValid()
    {
        // Arrange
        const string htmlContent = "<html><body>Conteúdo de teste</body></html>";
        const string fileName = "arquivo";
        var zipContent = new byte[] { 4, 5, 6 };
        var generatePdfRequests = new List<GeneratePdfRequest>();

        for (var i = 1; i <= 3; i++)
        {
            var generatePdfRequest = new GeneratePdfRequest { HtmlContent = htmlContent, FileName = $"{fileName}_{i}" };
            generatePdfRequests.Add(generatePdfRequest);
        }

        var generateZipRequest = new GenerateZipRequest { PdfRequests = generatePdfRequests };

        _generateZipUseCaseMock.Setup(x => x.Handle(generateZipRequest)).ReturnsAsync(zipContent);

        // Act
        var generateZipResult = await _zipController.GenerateZipAsync(generateZipRequest);

        // Assert
        generateZipResult.Should().BeOfType<FileContentResult>();
        generateZipResult.As<FileContentResult>().ContentType.Should().Be("application/zip");
        generateZipResult.As<FileContentResult>().FileContents.Should().BeEquivalentTo(zipContent);
    }

    [Fact]
    public async Task GenerateZip_ShouldReturnInternalServerError_WhenUseCaseThrowsException()
    {
        // Arrange
        const string htmlContent = "<html><body>Conteúdo de teste</body></html>";
        const string fileName = "arquivo";
        var zipContent = new byte[] { 4, 5, 6 };
        var generatePdfRequests = new List<GeneratePdfRequest>();

        for (var i = 1; i <= 3; i++)
        {
            var generatePdfRequest = new GeneratePdfRequest { HtmlContent = htmlContent, FileName = $"{fileName}_{i}" };
            generatePdfRequests.Add(generatePdfRequest);
        }

        var generateZipRequest = new GenerateZipRequest { PdfRequests = generatePdfRequests };

        _generateZipUseCaseMock.Setup(x => x.Handle(generateZipRequest))
            .ThrowsAsync(new Exception("Erro ao gerar o arquivo ZIP"));

        // Act
        var generateZipAsyncResult = await _zipController.GenerateZipAsync(generateZipRequest);

        // Assert
        generateZipAsyncResult.Should().NotBeOfType<FileContentResult>();
        generateZipAsyncResult.As<ObjectResult>().StatusCode.Should().Be(500);
    }
}
