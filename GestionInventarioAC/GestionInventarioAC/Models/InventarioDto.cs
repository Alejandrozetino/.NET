using Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Constants = Helper.Constants;

namespace Models;
public class InventarioDto
{
    public int Id { get; set; }


    [ForeignKey("IdProducto")]
    public ProductoDto? Producto { get; set; }
    public int IdProducto { get; set; }


    [Required(ErrorMessage = Constants.CampoRequerido)]
    public int Stock { get; set; }


    public Boolean Estado { get; set; }


    [NotMapped]
    public string tipoManto { get; set; }  = Constants.Agregar;

    public List<SelectListItem>? Products { get; set; }
}

public class InventariosDto
{
    public InventariosDto()
    {
        Inventory = new List<InventarioDto>();
    }

    public List<InventarioDto> Inventory { get; set; }
}