using authorquery;
using AuthorsRepo;
using BooksRepo;
using publishRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddGraphQLServer()
    .AddQueryType<publishingquery>()
    .AddMutationType<publishingmutation.publishingmutation>();

builder.Services.AddScoped<AuthorRepo>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");
    return new AuthorRepo(connectionString);
});
builder.Services.AddScoped<BookRepo>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");
    return new BookRepo(connectionString);
});
builder.Services.AddScoped<publishRepo.publishRepo>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");
    return new publishRepo.publishRepo(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGraphQL("/graphql");

app.Run();

