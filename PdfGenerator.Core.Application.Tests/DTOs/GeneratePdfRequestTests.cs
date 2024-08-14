using FluentValidation.TestHelper;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Validators;

namespace PdfGenerator.Core.Application.Tests.DTOs;

public class GeneratePdfRequestTests
{
    private readonly GeneratePdfRequestValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_HtmlContent_Is_Empty()
    {
        // Arrange
        var generatePdfRequest = new GeneratePdfRequest
        {
            HtmlContent = string.Empty,
            FileName = string.Empty
        };

        // Act
        var testValidateResult = _validator.TestValidate(generatePdfRequest);

        // Assert
        testValidateResult.ShouldHaveValidationErrorFor(x => x.HtmlContent);
    }

    [Fact]
    public void Should_Not_Have_Error_When_HtmlContent_Is_Provided()
    {
        // Arrange
        var generatePdfRequest = new GeneratePdfRequest
        {
            HtmlContent = "<html><body>Conteúdo de teste</body></html>",
            FileName = "arquivo"
        };

        // Act
        var testValidateResult = _validator.TestValidate(generatePdfRequest);

        // Assert
        testValidateResult.ShouldNotHaveValidationErrorFor(x => x.HtmlContent);
    }
}