using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Constants = Helper.Constants;

namespace Models;
public class ProductoDto
{
    public int Id { get; set; }


    [ForeignKey("IdCategoria")]
    public CategoriaDto? Categoria { get; set; }
    public int IdCategoria { get; set; }


    [StringLength(255)]
    [Required(ErrorMessage = Constants.CampoRequerido)]
    public string Nombre { get; set; }


    [StringLength(255)]
    [Required(ErrorMessage = Constants.CampoRequerido)]
    public string Descripcion { get; set; }


    [Required(ErrorMessage = Constants.CampoRequerido)]
    public Decimal PrecioUni { get; set; }


    [NotMapped]
    public string tipoManto { get; set; } = Constants.Agregar;

    public List<SelectListItem>? Categories { get; set; }
}

public class ProductosDto
{
    public ProductosDto()
    {
        Products = new List<ProductoDto>();
    }

    public List<ProductoDto> Products { get; set; }
}
