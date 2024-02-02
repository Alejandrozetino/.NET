using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;
public class Venta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public Int32 Id { get; set; }
    
    [ForeignKey("IdCliente")]
    public Cliente? Cliente { get; set; }
    public Int32 IdCliente { get; set; }
    
    public DateTime FechaVenta { get; set; }
    
    public Decimal Total { get; set; }

}
