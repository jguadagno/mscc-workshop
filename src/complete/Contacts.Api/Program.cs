using Contacts.Data;
using Contacts.Domain.Interfaces;
using Contacts.Logic;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IContactRepository, ContactRepository>();
builder.Services.AddTransient<IContactManager, ContactManager>();
builder.Services.AddTransient<IContactDataStore, Contacts.Data.Sqlite.SqliteDataStore>();
builder.Services.AddTransient<IConfiguration>(_ => builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Contact API",
            Version = "v1",
            Description = "The API for the Contacts Application",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Joseph Guadagno",
                Email = "jguadagno@hotmail.com",
                Url = new Uri("https://www.josephguadagno.net"),
            }
        });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
