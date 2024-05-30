using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitWebApp.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Diagnostics;

namespace FruitWebApp.Pages
{
	public class EditModel : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        // Add the data model and bind the form data to the page model properties
        [BindProperty]
        public FruitModel FruitModels { get; set; }


        // Retrieve the data to populate the form for editing
        public async Task OnGet(int id)
        {
 
            // Create the HTTP client using the FruitAPI named factory
            var httpClient = _httpClientFactory.CreateClient("FruitAPI");

            // Retrieve record information to populate the form
            using HttpResponseMessage response = await httpClient.GetAsync(id.ToString());

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response to populate the form
                using var contentStream = await response.Content.ReadAsStreamAsync();
                FruitModels = await JsonSerializer.DeserializeAsync<FruitModel>(contentStream);
            }
        }
		

		// Begin PUT operation code
        public async Task<IActionResult> OnPost()
        {
            //Serializar la informacion que sera editada en la base de datos
            var jsonContenido = new StringContent(JsonSerializer.Serialize(FruitModels),
            Encoding.UTF8,
            "application/json");

            //Crear el cliente http usando el nombre de fabrica
            var httpClient = _httpClientFactory.CreateClient("FruitAPI");
            //Ejecutar la solicitud PUT y almacenar la respuesta. Los parametos en PostAsync 
            // agrega el ID del artículo a la dirección base y pasa los datos serializados a la API
            using HttpResponseMessage respuesta = await httpClient.PutAsync(FruitModels.id.ToString(),jsonContenido);

            //retornar a la vista del Index y mostrar un mensaje de exito o falla
            if(respuesta.IsSuccessStatusCode){
                TempData["success"] = "Se edito con exito la fruta: " +FruitModels.id.ToString();
                return RedirectToPage("Index");
            }else{
                TempData["failure"] = "Ocurrio un error, no se edito, la fruta " +FruitModels.id.ToString();
                return RedirectToPage("Index");
            }

        }
        // End PUT operation code

	}
}

