using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models;

public class UserDto
{
	public int Id { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string NameUser { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string Name { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string Password { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string Role { get; set; }


	public DateTime CreateDate { get; set; }


	public string? Token { get; set; }
}

public class CreateUserDto
{
	[Required(ErrorMessage = "El campo es obligatorio")]
	public string NameUser { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string Name { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string Password { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string Role { get; set; }
}

public class LoginUserDto
{
	[Required(ErrorMessage = "El campo es obligatorio")]
	public string NameUser { get; set; }


	[Required(ErrorMessage = "El campo es obligatorio")]
	public string Password { get; set; }
}
