using L1_W3_BU2.Data;
using L1_W3_BU2.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Syster", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(a => a.File("Log/Log_txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Async(a => a.Console())
    .CreateLogger();

try
{
    Log.Information("Starting application.....");

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

    builder.Services.AddScoped<StudentService>();

    builder.Host.UseSerilog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Error(ex.Message);
}
finally
{
    await Log.CloseAndFlushAsync();
}


