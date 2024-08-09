using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Extensions;
using WebApplication1.Filter;
using WebApplication1.Filters;
using WebApplication1.Logging;
using WebApplication1.Repositories;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => 
    {
        options.Filters.Add(typeof(ApiExceptionFilter));
    })
    .AddJsonOptions(options =>
        options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection))
);

builder.Services.AddTransient<IMyService, MyService>();

builder.Services.AddScoped<ApiLoggingFilter>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) => 
    {
        // adicionar codigo antes do request
        await next(context);
        // adicionar codigo depois do request
    });

app.MapControllers();

// app.Run(async (context) =>
//     {
//         await context.Response.WriteAsync("Middleware Final");
//     });

app.Run();
