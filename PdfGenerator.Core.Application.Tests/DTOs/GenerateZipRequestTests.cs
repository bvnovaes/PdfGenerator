using FluentValidation.TestHelper;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Validators;

namespace PdfGenerator.Core.Application.Tests.DTOs;

public class GenerateZipRequestTests
{
    private readonly GenerateZipRequestValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_PdfRequests_Is_Empty()
    {
        // Arrange
        var generateZipRequest = new GenerateZipRequest
        {
            PdfRequests = new List<GeneratePdfRequest>()
        };

        // Act
        var testValidateResult = _validator.TestValidate(generateZipRequest);

        // Assert
        testValidateResult.ShouldHaveValidationErrorFor(x => x.PdfRequests);
    }

    [Fact]
    public void Should_Not_Have_Error_When_PdfRequests_Are_Provided()
    {
        // Arrange
        const string htmlContent = "<html><body>Conteúdo de teste</body></html>";
        const string fileName = "arquivo";
        var generatePdfRequests = new List<GeneratePdfRequest>();

        for (var i = 1; i <= 3; i++)
        {
            var generatePdfRequest = new GeneratePdfRequest { HtmlContent = htmlContent, FileName = $"{fileName}_{i}" };
            generatePdfRequests.Add(generatePdfRequest);
        }

        var generateZipRequest = new GenerateZipRequest { PdfRequests = generatePdfRequests };

        // Act
        var testValidateResult = _validator.TestValidate(generateZipRequest);

        // Assert
        testValidateResult.ShouldNotHaveValidationErrorFor(x => x.PdfRequests);
    }
}