using FluentValidation.TestHelper;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Validators;

namespace PdfGenerator.Tests.PdfGenerator.Core.Application.DTOs.Tests
{
    public class GeneratePdfRequestTests
    {
        private readonly GeneratePdfRequestValidator _validator;

        public GeneratePdfRequestTests()
        {
            _validator = new GeneratePdfRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_HtmlContent_Is_Empty()
        {
            //arrange
            var model = new GeneratePdfRequest { HtmlContent = "" };
            
            //act
            var result = _validator.TestValidate(model);
            
            //assert
            result.ShouldHaveValidationErrorFor(x => x.HtmlContent);
        }

        [Fact]
        public void Should_Not_Have_Error_When_HtmlContent_Is_Provided()
        {
            //arrange
            var model = new GeneratePdfRequest { HtmlContent = "<html><body>Conteúdo de teste</body></html>" };
            
            //act
            var result = _validator.TestValidate(model);
            
            //assert
            result.ShouldNotHaveValidationErrorFor(x => x.HtmlContent);
        }
    }
}
