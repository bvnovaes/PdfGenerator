using FluentValidation;
using PdfGenerator.Core.Application.DTOs;

namespace PdfGenerator.Core.Application.Validators
{
    public class GeneratePdfRequestValidator : AbstractValidator<GeneratePdfRequest>
    {
        public GeneratePdfRequestValidator()
        {
            RuleFor(x => x.HtmlContent)
                .NotEmpty().WithMessage("HtmlContent não pode ser vazio.")
                .NotNull().WithMessage("HtmlContent é obrigatório.");
        }
    }
}
