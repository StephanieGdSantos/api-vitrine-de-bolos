using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.EndPoints;
using System.Text.Json.Serialization;
using CardapioBolos.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using CardapioBolos.DTO;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDbContext<CardapioBolosContext>();
builder.Services.AddTransient<DAL<Bolo>>();
builder.Services.AddTransient<DAL<Encomenda>>();
builder.Services.AddTransient<DAL<Administrador>>();
builder.Services.AddTransient<DAL<Ingrediente>>();
builder.Services.AddTransient<DAL<EncomendaDTO>>();
builder.Services.AddTransient<DAL<BoloDTO>>();
builder.Services.AddTransient<DAL<IngredienteDTO>>();
builder.Services.AddTransient<DAL<EnderecoDTO>>();
builder.Services.AddTransient<DAL<BoloIngrediente>>();
builder.Services.AddTransient<DAL<BoloEncomenda>>();
builder.Services.AddScoped<EncomendaServices>();
builder.Services.AddScoped<AdministradorServices>();
builder.Services.AddScoped<BoloServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Vitrine de bolos", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
    });

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<CardapioBolosContext>();
db.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAllOrigins");

app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.AddEndpointsBolos();
app.AddEndpointsEncomendas();
app.AddEndPointsAdministrador();
app.AddEndpointsIngredientes();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
