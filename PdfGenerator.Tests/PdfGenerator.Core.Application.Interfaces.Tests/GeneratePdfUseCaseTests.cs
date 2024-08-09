using FluentAssertions;
using Moq;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.UseCases;
using PdfGenerator.Core.Domain.Interfaces;

namespace PdfGenerator.Tests.PdfGenerator.Core.Application.Interfaces.Tests;

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
    public async Task Handle_ShouldReturnPdfContent_WhenHtmlContentIsValid()
    {
        //arrange
        var htmlContent = "<html><body>Conteúdo de teste</body></html>";
        var pdfContent = new byte[] { 1, 2, 3 };
        var request = new GeneratePdfRequest { HtmlContent = htmlContent };

        _pdfGeneratorMock.Setup(x => x.GeneratePdfAsync(htmlContent)).ReturnsAsync(pdfContent);

        //act
        var result = await _generatePdfUseCase.Handle(request);

        //assert
        result.Should().BeEquivalentTo(pdfContent);
    }
}