﻿using FluentValidation.TestHelper;
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
        public void Should_Not_Have_Error_When_HtmlContent_Is_Provided()
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
