using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;
public class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [ForeignKey("IdCategoria")]
    public Categoria? Categoria { get; set; }
    public int IdCategoria { get; set; }


    [StringLength(255)]
    public string Nombre { get; set; }


    [StringLength(255)]
    public string Descripcion { get; set; }


    public Decimal PrecioUni { get; set; }
}
