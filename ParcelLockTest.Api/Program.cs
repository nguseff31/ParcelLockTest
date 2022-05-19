using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ParcelLockTest.Api.Common;
using ParcelLockTest.Api.Data;
using ParcelLockTest.Api.Services;
using ParcelLockTest.Api.Services.Interfaces;
using ParcelLockTest.Contract;

[assembly: ApiConventionType(typeof(ApiConventions))]

var builder = WebApplication.CreateBuilder(args);
// database
builder.Services.AddControllers();
builder.Services.AddDbContext<ParcelLockContext>(opts => {
    opts.UseNpgsql(builder.Configuration.GetConnectionString("Default"), p => {
        p.MigrationsAssembly("ParcelLockTest.Api");
    });
});
// register services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IParcelLockService, ParcelLockService>();

builder.Services.Configure<ApiBehaviorOptions>(o => {
    o.InvalidModelStateResponseFactory = actionContext => {
        var response = new DefaultValidationResponse();
        var errors = actionContext.ModelState
            .Select(p => new {
                p.Key,
                Errors = p.Value?.Errors.Select(e => e.ErrorMessage)
            })
            .ToDictionary(g => g.Key, g => g.Errors?.ToArray() ?? Array.Empty<string>());
        response.Errors = errors;
        return new BadRequestObjectResult(response);
    };
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Апи для сервиса постаматов",
        Description = "тестовое задание",
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
// run migrations
using (var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<ParcelLockContext>();
    await dbContext.Database.MigrateAsync();
}

var isDev = builder.Environment.IsDevelopment();
if (isDev) {
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseExceptionHandler(new ExceptionHandlerOptions {
    AllowStatusCode404Response = true,
    ExceptionHandler = async context => await HttpResponseExceptionHandler.Handle(context, isDev)
});
app.MapControllers();

app.Run();
