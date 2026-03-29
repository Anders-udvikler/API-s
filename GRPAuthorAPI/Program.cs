using GRPAuthorAPI.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddSingleton<BookNotifier>();
var app = builder.Build();

app.MapGrpcService<BookService>();

app.Run();