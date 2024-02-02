using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;
using Helper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models;
public class CompraDto
{
    public Int32 Id { get; set; }

    
    [ForeignKey("IdProveedor")]
    public ProveedorDto? Proveedor { get; set; }
    public Int32 IdProveedor { get; set; }


    
    [Required(ErrorMessage = Constants.CampoRequerido)]

    public DateTime FechaCompra { get; set; }

    
    [Required(ErrorMessage = Constants.CampoRequerido)]
    public Decimal Total { get; set; }

    
    [Required(ErrorMessage = Constants.CampoRequerido)]
    public Boolean Estado { get; set; }


    [NotMapped]
    public string TipoManto { get; set; } = Constants.Agregar;

    public List<SelectListItem>? Proveedores { get; set; }
}


public class ComprasDto
{
    public ComprasDto()
    {
        Compras = new List<CompraDto>();
    }

    public List<CompraDto> Compras { get; set; }
}
