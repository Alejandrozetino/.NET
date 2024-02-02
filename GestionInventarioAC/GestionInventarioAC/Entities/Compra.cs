using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;
public class Compra
{
    [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public Int32 Id { get; set; }
    
    [ForeignKey("IdProveedor")]
    public Proveedor? Proveedor { get; set; }
    public Int32 IdProveedor { get; set; }
    
    public DateTime FechaCompra { get; set; }
    
    public Decimal Total { get; set; }
    
    public Boolean Estado { get; set; }

}
