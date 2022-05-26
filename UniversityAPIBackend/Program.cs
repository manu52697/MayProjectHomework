// 1. Usings to work with EF
using Microsoft.EntityFrameworkCore;
using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// 2. DB Conection
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3. Add DB Context to services
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// 4. Add Custom Services (folder services)
builder.Services.AddScoped<IStudentsService, StudentsService>();

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
