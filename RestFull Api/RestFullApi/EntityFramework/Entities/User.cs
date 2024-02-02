using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Entities;

public class User
{
	[Key]
	public int Id { get; set; }


	[Required]
	[StringLength(255)]
	public string NameUser { get; set; }
	
	
	[Required]
	[StringLength(255)]
	public string Name { get; set; }
	
	
	[Required]
	[StringLength(255)]
	public string Password { get; set; }


	[Required]
	[StringLength(255)]
	public string Role { get; set; }


	[Required]
	public DateTime CreateDate { get; set; }
}
