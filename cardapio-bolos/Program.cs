using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.EndPoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CardapioBolosContext>();
builder.Services.AddTransient<DAL<Bolo>>();
builder.Services.AddTransient<DAL<Ingrediente>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
var app = builder.Build();

app.AddEndpointsBolos();
app.AddEndpointsIngredientes();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
