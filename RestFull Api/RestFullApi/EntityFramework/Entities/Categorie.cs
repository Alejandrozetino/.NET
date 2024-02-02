using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Entities;

public class Categorie
{
	[Key]
	public int Id { get; set; }


	[Required]
	[StringLength(255)]
	public string Name { get; set; }


	[Required]
	public DateTime CreateDate { get; set; }
}
