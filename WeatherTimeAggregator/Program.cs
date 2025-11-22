using WeatherTimeAggregator.Interfaces;
using WeatherTimeAggregator.Providers;
using WeatherTimeAggregator.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавяне на контролери
builder.Services.AddControllers();

// Swagger – за тестове и демонстрация
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpClient-и за доставчиците
builder.Services.AddHttpClient<OpenWeatherTimeProvider>(client =>
{
    client.BaseAddress = new Uri("https://api.openweathermap.org/");
});

builder.Services.AddHttpClient<WeatherApiTimeProvider>(client =>
{
    client.BaseAddress = new Uri("https://api.weatherapi.com/");
});

builder.Services.AddHttpClient<WeatherbitTimeProvider>(client =>
{
    client.BaseAddress = new Uri("https://api.weatherbit.io/");
});

// Регистриране на доставчиците
builder.Services.AddScoped<ITimeProvider, OpenWeatherTimeProvider>();
builder.Services.AddScoped<ITimeProvider, WeatherApiTimeProvider>();
builder.Services.AddScoped<ITimeProvider, WeatherbitTimeProvider>();

// Основният service за агрегация
builder.Services.AddScoped<TimeAggregationService>();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
