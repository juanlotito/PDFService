using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PDFService.API.IoC;
using System.Net;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependency(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    options.OrderActionsBy((apiDesc) =>
    {
        string methodOrder = apiDesc.HttpMethod switch
        {
            "GET" => "1",
            "PUT" => "2",
            "POST" => "3",
            "DELETE" => "4",
            _ => "5"
        };
        return $"{methodOrder}-{apiDesc.RelativePath}";
    });

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API PDF", Version = "v2" });
});

builder.Services.AddHealthChecks();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToList();

        var result = new
        {
            Error = true,
            Status = (int)HttpStatusCode.BadRequest,
            Errors = errors
        };

        return new BadRequestObjectResult(result);
    };

}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder
                //.WithOrigins("http://localhost:62416", "http://localhost:57877", "https://192.168.0.8:45457", "https://192.168.0.7:45457", "http://localhost:54246")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PDF API v 1.0.0");
});

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();

app.Run();