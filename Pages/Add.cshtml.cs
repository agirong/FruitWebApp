using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitWebApp.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Diagnostics;


namespace FruitWebApp.Pages
{
    public class AddModel : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public AddModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        // Add the data model and bind the form data to the page model properties
        [BindProperty]
        public FruitModel FruitModels { get; set; }

        // Begin POST operation code
        public async Task<IActionResult> OnPost(){
            var jsonContenido = new StringContent(JsonSerializer.Serialize(FruitModels),
            Encoding.UTF8,
            "application/json");

            //crear el cliente http usando el nombre de fabricacion
            var httpClient = _httpClientFactory.CreateClient("FruitAPI");

            //Ejecutar la solicitud POST y almacenar la respuesta. Los parametos en PostAsync 
            //se dirgien al POST para que se use la ruta base de la API
            using HttpResponseMessage respuesta = await httpClient.PostAsync("",jsonContenido);

            //Retornar la pagina Index y agregar un mensaje temporal de exito o falla en la pagina 
            if(respuesta.IsSuccessStatusCode)
            {
                TempData["success"] = "Registro Agregado con Exito";
                return RedirectToPage("Index");
            }  
            else{
                TempData["failure"] = "No se Agrego el Registro";
                return RedirectToPage("Index");
            }  
         }
        // End POST operation code
    }
}

