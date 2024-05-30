using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FruitWebApp.Models;

public class FruitModel
{
    [Key]
    public int id { get; set; }

    [Display(Name="Nombre")]
    public string? name { get; set; }
    [Display(Name ="Disponibilidad?")]
    public bool instock { get; set; }
    [Display(Name ="Precio")]
    public decimal price {get; set;}
    
}
