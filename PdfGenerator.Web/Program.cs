using FluentValidation;
using PdfGenerator.Core.Application.Interfaces;
using PdfGenerator.Core.Application.UseCases;
using PdfGenerator.Core.Application.Validators;
using PdfGenerator.Core.Domain.Interfaces;
using PdfGenerator.Infrastructure.Adapters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<GeneratePdfRequestValidator>();

builder.Services.AddSingleton<IPdfGenerator, PuppeteerAdapter>();
builder.Services.AddScoped<IGeneratePdfUseCase, GeneratePdfUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
