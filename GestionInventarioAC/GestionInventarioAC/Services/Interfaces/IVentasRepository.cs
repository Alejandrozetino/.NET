using Entities;
using Helper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Services.Interfaces;

public interface IVentasRepository
{
    Task<RecordList<Venta>> ListVentasAsync();
    Task<Venta> GetVenta(Int32 Id);
    Task<List<SelectListItem>> ComboClientesAsync();
    void AddVenta(Venta Venta);
    void RemoveVenta(Venta Venta);
    void UpdateVenta(Venta Venta);
    Task<bool> SaveChangesAsync();
}
