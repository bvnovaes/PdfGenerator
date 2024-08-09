using FluentValidation.TestHelper;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Validators;

namespace PdfGenerator.Tests.PdfGenerator.Core.Application.Validators.Tests
{
    public class GeneratePdfRequestValidatorTest
    {
        private readonly GeneratePdfRequestValidator _validator;

        public GeneratePdfRequestValidatorTest()
        {
            _validator = new GeneratePdfRequestValidator();
        }

        [Fact]
        public void Validate_GeneratePdfRequestWithEmptyHtmlContent_ShouldFail()
        {
            //arrange
            var request = new GeneratePdfRequest
            {
                HtmlContent = string.Empty
            };

            //act
            var result = _validator.TestValidate(request);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.HtmlContent);
        }

        [Fact]
        public void Validate_GeneratePdfRequestWithNonEmptyHtmlContent_ShouldPass()
        {
            //arrange
            var request = new GeneratePdfRequest
            {
                HtmlContent = "<html><body>Conteúdo de teste</body></html>"
            };

            //act
            var result = _validator.TestValidate(request);

            //assert
            result.ShouldNotHaveValidationErrorFor(x => x.HtmlContent);
        }
    }
}
