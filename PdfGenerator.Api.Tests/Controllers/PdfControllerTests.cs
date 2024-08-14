using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PdfGenerator.Api.Controllers;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;

namespace PdfGenerator.Api.Tests.Controllers;

public class PdfControllerIntegrationTests
{
    private readonly Mock<IGeneratePdfUseCase> _generatePdfUseCaseMock;
    private readonly PdfController _pdfController;

    public PdfControllerIntegrationTests()
    {
        _generatePdfUseCaseMock = new Mock<IGeneratePdfUseCase>();
        _pdfController = new PdfController(_generatePdfUseCaseMock.Object);
    }

    [Fact]
    public async Task GeneratePdf_ShouldReturnPdfFile_WhenHtmlContentIsValid()
    {
        // Arrange
        const string htmlContent = "<html><body>Conteúdo de teste</body></html>";
        const string fileName = "arquivo";
        var pdfContent = new byte[] { 1, 2, 3 };
        var generatePdfRequest = new GeneratePdfRequest { HtmlContent = htmlContent, FileName = fileName};

        _generatePdfUseCaseMock.Setup(x => x.Handle(generatePdfRequest)).ReturnsAsync(pdfContent);

        // Act
        var generatePdfAsyncResult = await _pdfController.GeneratePdfAsync(generatePdfRequest);

        // Assert
        generatePdfAsyncResult.Should().BeOfType<FileContentResult>();
        generatePdfAsyncResult.As<FileContentResult>().ContentType.Should().Be("application/pdf");
        generatePdfAsyncResult.As<FileContentResult>().FileContents.Should().BeEquivalentTo(pdfContent);
    }
        
    [Fact]
    public async Task GeneratePdf_ShouldReturnInternalServerError_WhenUseCaseThrowsException()
    {
        // Arrange
        const string htmlContent = "<html><body>Conteúdo de teste</body></html>";
        const string fileName = "arquivo";
        var generatePdfRequest = new GeneratePdfRequest { HtmlContent = htmlContent, FileName = fileName };

        _generatePdfUseCaseMock.Setup(x => x.Handle(generatePdfRequest))
            .ThrowsAsync(new Exception("Erro ao gerar o arquivo PDF"));

        // Act
        var generatePdfAsyncResult = await _pdfController.GeneratePdfAsync(generatePdfRequest);

        // Assert
            
        generatePdfAsyncResult.Should().NotBeOfType<FileContentResult>();
        generatePdfAsyncResult.As<ObjectResult>().StatusCode.Should().Be(500);
    }
}
