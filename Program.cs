using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
// Configuración del cliente HTTP con credenciales de autenticación básica
IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

string usuario = config.GetValue<string>("credenciales:usuario");
string contrasena = config.GetValue<string>("credenciales:contrasena");

var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{usuario}:{contrasena}"));

// Add services to the container.
builder.Services.AddRazorPages();


// Begin HTTP client code
// Agregar el IHttpClientFactory al contenedo y establecer el nombre de fabricacion. 
builder.Services.AddHttpClient("FruitAPI", httpClient =>
{
    //Establecer la dirreccion base para las solicitudes HTTP
    httpClient.BaseAddress = new Uri("http://localhost:5050/fruitlist/");
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
});

// End of HTTP client code

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
