using System.Reflection;
using System.Text.Json.Serialization;
using EntityFrameworkCore.DataEncryption.Sample.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDbContext<SampleDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("DataEncryption", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "EntityFrameworkCore.DataEncryption",
        Version = "1.0.0",
        Description = "This package is a plugin that adds Data Encryption support to EntityFrameworkCore.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "furkan.dvlp@gmail.com",
            Url = new Uri("https://github.com/furkandeveloper/EntityFrameworkCore.DataEncryption")
        }
    });
    var docFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
    var filePath = Path.Combine(AppContext.BaseDirectory, docFile);

    if (File.Exists((filePath)))
    {
        options.IncludeXmlComments(filePath);
    }
    options.DescribeAllParametersInCamelCase();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.EnableDeepLinking();
    options.ShowExtensions();
    options.DisplayRequestDuration();
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    options.RoutePrefix = "api-docs";
    options.SwaggerEndpoint("/swagger/DataEncryption/swagger.json", "EasyProfilerSwagger");
});
app.MapControllers();

app.MapDefaultControllerRoute();

app.Run();