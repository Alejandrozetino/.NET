using Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Constants = Helper.Constants;

namespace Models;
public class CategoriaDto
{

    public int Id { get; set; }


    [StringLength(255)]
    [Required(ErrorMessage = Constants.CampoRequerido)]
    public string Nombre { get; set; }


    [StringLength(255)]
    [Required(ErrorMessage = Constants.CampoRequerido)]
    public string Descripcion { get; set; }


    [NotMapped]
    public string tipoManto { get; set; } = Constants.Agregar;
}

public class CategoriasDto
{
    public CategoriasDto()
    {
        Categories = new List<CategoriaDto>();
    }

    public List<CategoriaDto> Categories { get; set; }
}
