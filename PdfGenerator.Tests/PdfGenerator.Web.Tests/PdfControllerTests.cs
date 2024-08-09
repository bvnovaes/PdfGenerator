using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PdfGenerator.Api.Controllers;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;

namespace PdfGenerator.Tests.PdfGenerator.Web.Tests
{
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
            var htmlContent = "<html><body>Conteúdo de teste</body></html>";
            var pdfContent = new byte[] { 1, 2, 3 };
            var request = new GeneratePdfRequest { HtmlContent = htmlContent };

            _generatePdfUseCaseMock.Setup(x => x.Handle(request)).ReturnsAsync(pdfContent);

            // Act
            var result = await _pdfController.GeneratePdfAsync(request);

            // Assert
            result.Should().BeOfType<FileContentResult>();
            result.As<FileContentResult>().ContentType.Should().Be("application/pdf");
            result.As<FileContentResult>().FileContents.Should().BeEquivalentTo(pdfContent);
        }
        
        [Fact]
        public async Task GeneratePdf_ShouldReturnInternalServerError_WhenUseCaseThrowsException()
        {
            // Arrange
            var htmlContent = "<html><body>Conteúdo de teste</body></html>";
            var pdfContent = new byte[] { 1, 2, 3 };
            var request = new GeneratePdfRequest { HtmlContent = htmlContent };

            _generatePdfUseCaseMock.Setup(x => x.Handle(request)).ThrowsAsync(new Exception("Erro ao gerar o arquivo PDF"));

            // Act
            var result = await _pdfController.GeneratePdfAsync(request);

            // Assert
            result.Should().NotBeOfType<FileContentResult>();
            result.As<ObjectResult>().StatusCode.Should().Be(500);
        }
    }
}
