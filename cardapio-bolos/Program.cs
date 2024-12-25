using CardapioBolos.Banco;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<CardapioBolosContext>();

app.MapGet("/", () => "Hello World!");

app.Run();
