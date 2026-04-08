using Library.SoapApi.Contracts;
using Library.SoapApi.Db;
using Library.SoapApi.Services;
using Microsoft.EntityFrameworkCore;
using SoapCore;


var builder = WebApplication.CreateBuilder(args);

// ---------------- DB ----------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=db/library.db"));

// ---------------- SOAP ----------------
builder.Services.AddSoapCore();
builder.Services.AddScoped<ILibrarySoapService, LibrarySoapService>();

var app = builder.Build();

// ---------------- PIPELINE ----------------
app.UseRouting();

// ---------------- ENDPOINT ----------------
app.UseEndpoints(endpoints =>
{
    _ = endpoints.UseSoapEndpoint<ILibrarySoapService>(
        "/soap",
        new SoapEncoderOptions(),
        SoapSerializer.DataContractSerializer
    );
});

app.Run();