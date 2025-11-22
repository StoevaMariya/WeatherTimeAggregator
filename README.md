🌦️ WeatherTimeAggregator
ASP.NET Core Web API • Aggregation of Time from 3 Weather APIs

WeatherTimeAggregator е ASP.NET Core Web API проект, който събира (агрегира) информация за време/час от три различни външни API-та:

OpenWeatherMap

WeatherAPI

Weatherbit.io

Проектът демонстрира реална работа с външни REST услуги, Dependency Injection, HttpClientFactory, асинхронност и добра архитектура.

🚀 Функционалност
✔ Приема заявка:
GET /api/time/{city}

✔ Заявката се обработва така:

Контролерът приема град (пример: Varna).

TimeAggregationService извиква трите провайдъра:

OpenWeatherTimeProvider

WeatherApiTimeProvider

WeatherbitTimeProvider

Всеки provider:

прави HTTP заявка към своя външен API

парсва JSON отговора

връща унифициран модел TimeResult

Сервизът агрегира резултатите и връща общ JSON обект:

{
  "providerResults": [
    { "source": "OpenWeatherMap", "time": "..." },
    { "source": "WeatherAPI", "time": "..." },
    { "source": "Weatherbit", "time": "..." }
  ],
  "finalTime": "..." 
}

🏗 Архитектура на проекта
WeatherTimeAggregator/
│
├── Controllers/
│   └── TimeController.cs
│
├── Services/
│   └── TimeAggregationService.cs
│
├── Providers/
│   ├── OpenWeatherTimeProvider.cs
│   ├── WeatherApiTimeProvider.cs
│   └── WeatherbitTimeProvider.cs
│
├── Interfaces/
│   └── ITimeProvider.cs
│
├── Models/
│   ├── TimeResult.cs
│   └── AggregatedTimeResult.cs
│
├── appsettings.json
└── Program.cs


Архитектурата следва принципите:

Separation of Concerns

Dependency Injection

SOLID

Interface-based design

🔑 Конфигурация на API ключове

Във appsettings.json се добавят:

"APIKeys": {
  "OpenWeather": "your-openweather-key",
  "WeatherAPI":  "your-weatherapi-key",
  "Weatherbit":  "your-weatherbit-key"
}

🔌 Използвани технологии

ASP.NET Core 8 Web API

HttpClientFactory

Swagger / OpenAPI

Newtonsoft / System.Text.Json

Dependency Injection

Async / Await

REST интеграции

C# 10 / .NET 8

▶️ Стартиране на проекта

Клонираш репото:

git clone https://github.com/<your-username>/WeatherTimeAggregator


Добавяш API ключове в appsettings.json.

Стартираш проекта:

от Visual Studio → F5

или през CLI:

dotnet run


Отваряш:

https://localhost:7298/swagger

🧪 Примерни заявки
GET https://localhost:7298/api/time/sofia
GET https://localhost:7298/api/time/varna
GET https://localhost:7298/api/time/london

🎯 Цел на проекта

Проектът демонстрира:

практическа интеграция с външни API-та

асинхронност и паралелни заявки

модулна архитектура

професионална структура на Web API

реална работа с HttpClient

агрегация на резултати
