using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;
using Helper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models;
public class VentaDto
{
    public Int32 Id { get; set; }

    
    [ForeignKey("IdCliente")]
    public ClienteDto? Cliente { get; set; }
    public Int32 IdCliente { get; set; }

    
    [Required(ErrorMessage = Constants.CampoRequerido)]
    public DateTime FechaVenta { get; set; }

    
    [Required(ErrorMessage = Constants.CampoRequerido)]
    public Decimal Total { get; set; }


    [NotMapped]
    public string TipoManto { get; set; } = Constants.Agregar;

    public List<SelectListItem>? Clientes { get; set; }
}


public class VentasDto
{
    public VentasDto()
    {
       Ventas = new List<VentaDto>();
    }

    public List<VentaDto> Ventas{ get; set; }
}
