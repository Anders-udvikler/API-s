using GRPAuthorAPI.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
//TODO logging
builder.Services.AddSingleton<BookNotifier>();
var app = builder.Build();

app.MapGrpcService<BookService>();

app.Run();