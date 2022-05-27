// 1. Usings to work with EF
using Microsoft.EntityFrameworkCore;
using UniversityAPIBackend.Config;
using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// 2. DB Conection
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3. Add DB Context to services
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// 4. Add Custom Services (folder services)
// builder.Services.AddScoped<IStudentsService, StudentsService>(); // Example -> moved to CustomServicesConfig class
CustomServicesConfig.DeclareCustomServices(builder);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// 5. CORS Config
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});


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

// 6. Tell App to use CORS
app.UseCors("CorsPolicy");

app.Run();
