using Entities;
using Helper;


namespace Services.Interfaces;

public interface IProveedoresRepository
{
    Task<RecordList<Proveedor>> ListProveedoresAsync();
    Task<Proveedor> GetProveedor(Int32 Id);
    void AddProveedor(Proveedor Proveedor);
    void RemoveProveedor(Proveedor Proveedor);
    void UpdateProveedor(Proveedor Proveedor);
    Task<bool> SaveChangesAsync();
}
