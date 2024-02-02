using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;
public class Inventario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
    public int IdProducto { get; set; }


    public int Stock { get; set; }


    public Boolean Estado { get; set; }
}
