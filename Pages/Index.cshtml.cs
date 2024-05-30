using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitWebApp.Models;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace FruitWebApp.Pages
{
    public class IndexModel : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Add the data model and bind the form data to the page model properties
        // Enumerable since an array is expected as a response
        [BindProperty]
        public IEnumerable<FruitModel> FruitModels { get; set; }

        // Begin GET operation code
        //USAR OnGet() que es una solicitud HTTP Asincrona 
        public async Task OnGet(){
            //crear el cliente http usando el nombre de fabricacion
            var httpClient = _httpClientFactory.CreateClient("FruitAPI");
            //Hacer la solicitud GET y almacenar la respuesta con parametro vacio
            //GetAsyc no modifica la direccion base almacenada. 
            using HttpResponseMessage respuesta = await httpClient.GetAsync("");

            //Si la respuesta es exitosa deserializar la respuesta con el modelo
            if(respuesta.IsSuccessStatusCode){
                using var contenido = await respuesta.Content.ReadAsStreamAsync();
                FruitModels = await JsonSerializer.DeserializeAsync<IEnumerable<FruitModel>>(contenido); 
            }
        }
        // End GET operation code
    }
}

