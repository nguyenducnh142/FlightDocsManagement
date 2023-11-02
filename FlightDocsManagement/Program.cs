using FlightDocsManagement.DbContexts;
using FlightDocsManagement.Repository.Interface;
using FlightDocsManagement.Repository.Repositories;
using JwtTokenAuthentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FlightDocsMngContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConn")));
builder.Services.AddScoped<IGORepository, GORepository>();
builder.Services.AddScoped<IPilotCrewRepository, PilotCrewRepository>();
builder.Services.AddJwtAuthentication();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
