using EntityFramework.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models;

public class CarDto
{
	public int Id { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string Name { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string Description { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string Modelo { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public int Año { get; set; }


	public string Image { get; set; }


	public int IdCategoria { get; set; }
}
