using FluentValidation;
using PdfGenerator.Core.Application.DTOs;

namespace PdfGenerator.Core.Application.Validators;

public class GenerateZipRequestValidator : AbstractValidator<GenerateZipRequest>
{
    public GenerateZipRequestValidator()
    {
        RuleFor(x => x.PdfRequests)
            .NotEmpty().WithMessage("PdfRequests não pode ser vazio.")
            .NotNull().WithMessage("PdfRequests é obrigatório.")
            .ForEach(x => x.SetValidator(new GeneratePdfRequestValidator()));
    }
        
        
}