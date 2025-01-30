using CollegeApp.Data;
using CollegeApp.MyLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region serilog settings
//Log.Logger = new LoggerConfiguration().
//    MinimumLevel.Information()
//    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
//    .CreateLogger();

////builder.Host.UseSerilog(); // use this line to override the built-in loggers
//builder.Logging.AddSerilog(); // use serilog along with built-in loggers meaning that serilog and default log will work together

#endregion
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net("log4net.config");

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMyLogger, LogToServerMemory>();
builder.Services.AddSingleton<IMyLogger, LogToServerMemory>();
builder.Services.AddTransient<IMyLogger, LogToServerMemory>();

var connectionString = builder.Configuration.GetConnectionString("MyConnection");

// Configure database context
builder.Services.AddDbContext<CollegeDBContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Test Database Connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CollegeDBContext>();
    try
    {
        Console.WriteLine("Attempting to connect to database...");
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Database connection successful!");
        }
        else
        {
            Console.WriteLine("Database connection failed!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Connection error: {ex.Message}");
    }
}

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
