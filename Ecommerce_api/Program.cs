
using Ecommerce.api;
using Ecommerce.Application;
using Ecommerce.Application.Dtos.Auth;
using Ecommerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));

// Add Infrastructure services 
builder.Services.AddInfraStructureServices(builder.Configuration);

builder.Services.AddApplicationServices();

var app = builder.Build();

// seed DB Data
await app.SeedAndMigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
