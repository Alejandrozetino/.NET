using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models;

public class CategorieDto
{
	public int Id { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	[StringLength(255)]
	public string Name { get; set; }
}

public class AddCategorieDto
{
	[Required(ErrorMessage = "El campo es obligatorio")]
	[StringLength(255)]
	public string Name { get; set; }
}
