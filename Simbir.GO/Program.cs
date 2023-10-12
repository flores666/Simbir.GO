using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Simbir.GO.DataAccess;
using Simbir.GO.Repositories;
using Simbir.GO.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v0.0.1",
        Title = "Simbir.GO API",
        Description = "Simbir.GO API (ASP.NET 7.0)"
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("default"))
        .UseSnakeCaseNamingConvention());
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddEntityFrameworkNpgsql();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        //options.SwaggerEndpoint("/swagger-original.json", "Simbir.GO API");
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Simbir.GO API");
        options.RoutePrefix = string.Empty;
    }
);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}

app.UseHttpsRedirection();
//app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();