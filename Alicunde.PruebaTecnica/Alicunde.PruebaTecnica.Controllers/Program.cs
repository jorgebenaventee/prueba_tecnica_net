using Alicunde.PruebaTecnica.Services.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient("opendata",
    client => { client.BaseAddress = new Uri("https://api.opendata.esett.com/"); });
builder.Services.AddScoped<RetailerService>();

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