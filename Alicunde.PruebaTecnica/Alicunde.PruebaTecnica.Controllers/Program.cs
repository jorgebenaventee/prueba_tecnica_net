using System.Text.Json;
using Alicunde.PruebaTecnica.Database;
using Alicunde.PruebaTecnica.Services.Exceptions;
using Alicunde.PruebaTecnica.Services.Repositories;
using Alicunde.PruebaTecnica.Services.Services;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient("opendata",
    client => { client.BaseAddress = new Uri("https://api.opendata.esett.com/"); });
builder.Services.AddScoped<RetailerService>();
builder.Services.AddScoped<RetailerRepository>();
builder.Services.AddDbContext<RetailersContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("RetailerConnection")));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        if (ex is InvalidRemoteResponseException)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            context.Response.ContentType = "text/plain";
            var error = new { error = "The remote server returned an unexpected response. Please, try again later." };
            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "text/plain";
            var error = new { error = "An unexpected error occurred. Please, try again later." };
            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
});

app.UseAuthorization();
app.MapControllers();

app.Run();