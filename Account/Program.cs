using Microsoft.EntityFrameworkCore;
using SystemManage.DbContexts;
using SystemManage.Repository.Interface;
using SystemManage.Repository.Repositories;
using JwtTokenAuthentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SystemContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConn")));
builder.Services.AddScoped<IGORepository, GORepository>();
builder.Services.AddControllers();
builder.Services.AddJwtAuthentication();
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
