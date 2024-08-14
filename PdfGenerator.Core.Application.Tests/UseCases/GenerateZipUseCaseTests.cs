using FluentAssertions;
using Moq;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.UseCases;
using PdfGenerator.Core.Domain.Interfaces;

namespace PdfGenerator.Core.Application.Tests.UseCases;

public class GenerateZipUseCaseTests
{
    private readonly Mock<IZipGenerator> _zipGeneratorMock;
    private readonly Mock<IPdfGenerator> _pdfGeneratorMock;
    private readonly GenerateZipUseCase _generateZipUseCase;

    public GenerateZipUseCaseTests()
    {
        _zipGeneratorMock = new Mock<IZipGenerator>();
        _pdfGeneratorMock = new Mock<IPdfGenerator>();
        var generatePdfUseCase = new GeneratePdfUseCase(_pdfGeneratorMock.Object);
        _generateZipUseCase = new GenerateZipUseCase(generatePdfUseCase, _zipGeneratorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldGenerateZipWithPdfFiles()
    {
        // Arrange
        const string htmlContent = "<html><body>Conteúdo de teste</body></html>";
        const string fileName = "arquivo";
        var pdfContent = new byte[] { 1, 2, 3 };
        var zipContent = new byte[] { 4, 5, 6 };
        var generatePdfRequests = new List<GeneratePdfRequest>();
        var pdfContents = new List<Tuple<string, byte[]>>();

        for (var i = 1; i <= 3; i++)
        {
            var generatePdfRequest = new GeneratePdfRequest { HtmlContent = htmlContent, FileName = $"{fileName}_{i}" };
            generatePdfRequests.Add(generatePdfRequest);
            pdfContents.Add(new Tuple<string, byte[]>(generatePdfRequest.FileName, pdfContent));
        }

        var generateZipRequest = new GenerateZipRequest { PdfRequests = generatePdfRequests };

        _zipGeneratorMock.Setup(x => x.GenerateZipAsync(pdfContents))
            .ReturnsAsync(zipContent);

        _pdfGeneratorMock.Setup(x => x.GeneratePdfAsync(htmlContent))
            .ReturnsAsync(pdfContent);

        // Act
        var handleResult = await _generateZipUseCase.Handle(generateZipRequest);

        // Assert
        handleResult.Should().BeEquivalentTo(zipContent);
        _pdfGeneratorMock.Verify(x => x.GeneratePdfAsync(htmlContent), Times.Exactly(3));
        _zipGeneratorMock.Verify(x => x.GenerateZipAsync(pdfContents), Times.Once);
    }
}