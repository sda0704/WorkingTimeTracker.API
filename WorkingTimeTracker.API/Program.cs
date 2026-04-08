using Microsoft.EntityFrameworkCore;
using WorkingTimeTracker.DataAccess;
using WorkingTimeTracker.Application.Abstractions;
using WorkingTimeTracker.Application.Services;
using WorkingTimeTracker.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<ITimeService, TimeService>();





var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors(policy =>
policy.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch(InvalidOperationException ex)
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync($"{{\"message\": \"{ex.Message}\"}}");
    }
    catch(Exception ex)
    {
        context.Response.StatusCode=500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync($"{{\"message\": \"Внутренняя ошибка сервера\"}}" );
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
