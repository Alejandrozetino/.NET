namespace Helper;

public class Constants
{
    public const string Agregar = "Agregar";
    public const string Modificar = "Modificar";
    public const string Eliminar = "Eliminar";
    public const string MensajeAgregar = "Ingrese los datos para crear un registro";
    public const string MensajeEditar = "Modifique los campos necesarios.";
    public const string MensajeEliminar = "Está seguro de eliminar el registro.";
    public const string CampoRequerido = "Ingrese información solicitada";

    public static string MantoBoton(string TipoManto)
    {
        string mensaje = string.Empty;
        switch (TipoManto)
        {
            case Constants.Agregar: mensaje = "Grabar";break;
            case Constants.Modificar: mensaje = "Modificar";break;
            case Constants.Eliminar: mensaje = "Eliminar";break;
        }

        return mensaje;
    }
}
