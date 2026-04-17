# **Приложение для учета рабочего времени** 

**Frontend: https://github.com/sda0704/WorkingTimeTracker.Frontend**

## Структура проекта  

Приложение использует многослойную архитектуру (N-tier / onion)  

**WorkingTimeTracker.Core** - Ядро приложения. Содержит Domain модели и интерфейсы репозитория. Не содержит бизнес-логики. Не зависит от других слоев  
**WorkingTimeTracker.DataAccess** - Слой работы с БД. Содержит DbContext, реализацию репозиториев для Core, сущности (Entities), конфигурации для БД. Не содержит бизнес-логики  
**WorkingTimeTracker.Application** - Слой работы с бизнес-логикой. Содержит сервисы (Все проверки из ТЗ), абстракции (интерфейсы сервисов), DTO (Объекты для передачи данных между API и клиентом), Мапперы (преобразуют DTO в Domain Model и обратно)  
**WorkingTimeTracker.API** - Точка входа в приложение. Обрабатывает HTTP запросы. Содержит контроллеры (принятие запроса и вызов метода), Program.cs - Регистрация зависимостей, настройка CORS и Swagger  

## Технологии:   

- **C# / NET 10** - основная платформа  
- **ASP.NET Web API** - REST API  
- **Entity Framework Core** - ORM для работы с БД  
- **SQLite** - база данных
- **Swagger** - автодокументация


## Запуск проекта  

### Требования:  

- .NET 10 SDK  
- SQLite (встроенный)  

### Шаги  

```bash  
git clone https://github.com/sda0704/WorkingTimeTracker.API  
cd WorkingTimeTracker.API  

dotnet restore  

dotnet ef database update  

dotnet run --project WorkingTimeTracker.API  
