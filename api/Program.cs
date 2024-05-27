using api.Data;
using api.interfaces;
using api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplacationDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnect")));
builder.Services.AddCors(options => { options.AddPolicy("MyCors", builder => { builder
            .AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[] {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () => {
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
app.UseAuthentication();
app.UseCors("MyCors");
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) {
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
