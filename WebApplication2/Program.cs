using CustomerApi.CasosDeUsos;
using CustomerApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CustomerDataBaseContext>(sqlServerBuilder =>
{
    sqlServerBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Connection1"));
});
builder.Services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

var app = builder.Build();

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
