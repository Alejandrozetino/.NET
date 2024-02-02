using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Entities;

public class Car
{
	[Key]
	public int Id { get; set; }


	[Required]
	[StringLength(255)]
	public string Name { get; set; }


	[Required]
	[StringLength(255)]
	public string Description { get; set; }


	[Required]
	[StringLength(255)]
	public string Modelo { get; set; }


	[Required]
	public int Año { get; set; }


	public string Image { get; set; }


	[ForeignKey("IdCategoria")]
	public Categorie Categorie { get; set; }
	public int IdCategoria { get; set; }
}
