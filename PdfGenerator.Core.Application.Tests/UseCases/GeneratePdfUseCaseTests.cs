using FluentAssertions;
using Moq;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.UseCases;
using PdfGenerator.Core.Domain.Interfaces;

namespace PdfGenerator.Core.Application.Tests.UseCases;

public class GeneratePdfUseCaseTests
{
    private readonly Mock<IPdfGenerator> _pdfGeneratorMock;
    private readonly GeneratePdfUseCase _generatePdfUseCase;

    public GeneratePdfUseCaseTests()
    {
        _pdfGeneratorMock = new Mock<IPdfGenerator>();
        _generatePdfUseCase = new GeneratePdfUseCase(_pdfGeneratorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPdfContent()
    {
        // Arrange
        const string htmlContent = "<html><body>Conte�do de teste</body></html>";
        var pdfContent = new byte[] { 1, 2, 3 };
        var generatePdfRequest = new GeneratePdfRequest { HtmlContent = htmlContent, FileName = "arquivo" };

        _pdfGeneratorMock.Setup(x => x.GeneratePdfAsync(htmlContent)).ReturnsAsync(pdfContent);

        // Act
        var handleResult = await _generatePdfUseCase.Handle(generatePdfRequest);

        // Assert
        handleResult.Should().BeEquivalentTo(pdfContent);
    }
}