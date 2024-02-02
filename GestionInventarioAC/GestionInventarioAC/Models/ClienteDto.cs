using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;
using Helper;

namespace Models;
public class ClienteDto
{
        
    [Required(ErrorMessage = Constants.CampoRequerido)]

    public Int32 Id { get; set; }


    [StringLength(255)]
    [Required(ErrorMessage = Constants.CampoRequerido)]

    public String Nombre { get; set; }

    
    [Required(ErrorMessage = Constants.CampoRequerido)]

    public Int32 Telefono { get; set; }


    [StringLength(255)]
    [Required(ErrorMessage = Constants.CampoRequerido)]

    public String Direccion { get; set; }


    [StringLength(255)]
    [Required(ErrorMessage = Constants.CampoRequerido)]

    public String Correo { get; set; }


    [NotMapped]
    public string TipoManto { get; set; } = Constants.Agregar;
}


public class ClientesDto
{
    public ClientesDto()
    {
       Clientes = new List<ClienteDto>();
    }

    public List<ClienteDto> Clientes{ get; set; }
}
