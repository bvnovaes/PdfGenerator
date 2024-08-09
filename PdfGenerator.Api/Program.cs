using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Caching.Memory;
using PdfGenerator.Core.Application.DTOs;
using PdfGenerator.Core.Application.Interfaces;
using PdfGenerator.Core.Application.UseCases;
using PdfGenerator.Core.Application.Validators;
using PdfGenerator.Core.Domain.Interfaces;
using PdfGenerator.Infrastructure.Adapters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddSingleton<IPdfGenerator, PuppeteerAdapter>();
builder.Services.AddTransient<IValidator<GeneratePdfRequest>, GeneratePdfRequestValidator>();
builder.Services.AddScoped<IGeneratePdfUseCase, GeneratePdfUseCase>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();