using FluentValidation.TestHelper;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Validators;

namespace PdfGenerator.Core.Application.Tests.Validators;

public class GeneratePdfRequestValidatorTest
{
    private readonly GeneratePdfRequestValidator _validator = new();

    [Fact]
    public void Validate_GeneratePdfRequestWithEmptyHtmlContent_ShouldFail()
    {
        // Arrange
        var generatePdfRequest = new GeneratePdfRequest
        {
            HtmlContent = string.Empty,
            FileName = "arquivo"
        };

        // Act
        var testValidateResult = _validator.TestValidate(generatePdfRequest);

        // Assert
        testValidateResult.ShouldHaveValidationErrorFor(x => x.HtmlContent);
    }
        
    [Fact]
    public void Validate_GeneratePdfRequestWithEmptyFileName_ShouldFail()
    {
        // Arrange
        var generatePdfRequest = new GeneratePdfRequest
        {
            HtmlContent = "<html><body>Conteúdo de teste</body></html>",
            FileName = string.Empty
        };

        // Act
        var testValidateResult = _validator.TestValidate(generatePdfRequest);

        // Assert
        testValidateResult.ShouldHaveValidationErrorFor(x => x.FileName);
    }

    [Fact]
    public void Validate_GeneratePdfRequestWithNonEmptyHtmlContentAndFileName_ShouldPass()
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
        testValidateResult.ShouldNotHaveAnyValidationErrors();
    }
}
