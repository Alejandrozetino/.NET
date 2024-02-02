using Entities;
using Helper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Services.Interfaces;

public interface IComprasRepository
{
    Task<RecordList<Compra>> ListComprasAsync();
    Task<List<SelectListItem>> ComboProveedoresAsync();
    Task<Compra> GetCompra(Int32 Id);
    void AddCompra(Compra Compra);
    void RemoveCompra(Compra Compra);
    void UpdateCompra(Compra Compra);
    Task<bool> SaveChangesAsync();
}
