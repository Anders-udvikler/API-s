using authorquery;
using bookquery;
using authormutation;
using bookmutation;
using publishingmutation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddGraphQLServer()
    .AddQueryType<publishingquery>()
    .AddQueryType<authorquery.authorquery>()
    .AddQueryType<bookquery.bookquery>()
    .AddMutationType<publishingmutation.publishingmutation>()
    .AddMutationType<authormutation.authormutation>()
    .AddMutationType<bookmutation.bookmutation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

