using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainDbConnection"));
}
);

builder.Services.AddCors(options => 
{
    options.AddPolicy("MyAppCors", policy => 
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:3000");
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

app.UseCors("MyAppCors");

app.UseAuthorization();

app.MapControllers();

//Adding Migration Middleware

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;

try
{
    var AppDbContextSerivce = service.GetRequiredService<ApplicationDbContext>();
    await AppDbContextSerivce.Database.MigrateAsync();
    await Seed.SeedData(AppDbContextSerivce);
}
catch (Exception ex)
{
    var logger = service.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An Error Occured during The Migration");
}

app.Run();
