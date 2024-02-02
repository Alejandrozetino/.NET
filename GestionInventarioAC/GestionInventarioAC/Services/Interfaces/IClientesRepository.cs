using Entities;
using Helper;


namespace Services.Interfaces;

public interface IClientesRepository
{
    #region -Clientes-
    Task<RecordList<Cliente>> ListClientesAsync();
    Task<Cliente> GetCliente(Int32 Id);
    void AddCliente(Cliente cliente);
    void RemoveCliente(Cliente cliente);
    void UpdateCliente(Cliente cliente);

    Task<bool> SaveChangesAsync();
    #endregion

}
