using Alicunde.PruebaTecnica.Database;
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

app.UseAuthorization();
app.MapControllers();

app.Run();