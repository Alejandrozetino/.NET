using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;
public class Cliente
{
    [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public Int32 Id { get; set; }
    
    public String Nombre { get; set; }
    
    public Int32 Telefono { get; set; }
    
    public String Direccion { get; set; }
    
    public String Correo { get; set; }

}
