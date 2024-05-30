using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitWebApp.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Diagnostics;

namespace FruitWebApp.Pages
{
	public class DeleteModel : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        // Add the data model and bind the form data to the page model properties
        [BindProperty]
        public FruitModel FruitModels { get; set; }


        // Retrieve the data to populate the form for deletion
        public async Task OnGet(int id)
        {

            // Create the HTTP client using the FruitAPI named factory
            var httpClient = _httpClientFactory.CreateClient("FruitAPI");

            // Retrieve record information
            using HttpResponseMessage response = await httpClient.GetAsync(id.ToString());

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response to populate the form
                using var contentStream = await response.Content.ReadAsStreamAsync();
                FruitModels = await JsonSerializer.DeserializeAsync<FruitModel>(contentStream);
            }
        }
		

		// Begin DELETE operation code
        public async Task<IActionResult> OnPost(){
            //Crear el cliente http usando el nombre de fabrica
            var httpClient = _httpClientFactory.CreateClient("FruitAPI");

            // Appends the data Id for deletion to the base address and performs the operation
            using HttpResponseMessage respuesta = await httpClient.DeleteAsync(FruitModels.id.ToString());

             //retornar a la vista del Index y mostrar un mensaje de exito o falla
            if(respuesta.IsSuccessStatusCode){
                TempData["success"] = "Se Elimino con exito la fruta: " +FruitModels.id.ToString();
                return RedirectToPage("Index");
            }else{
                TempData["failure"] = "Ocurrio un error, no se Elimino, la fruta " +FruitModels.id.ToString();
                return RedirectToPage("Index");
            }

        }
        // End DELETE operation code

	}
}

