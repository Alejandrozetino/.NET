using Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities;

namespace Services.Interfaces;

public interface IInventoryRepository
{
    void AddInventory(Inventario inventory);
    void UpdateInventory(Inventario inventory);
    void DeleteInventory(Inventario inventory);
    Task<Inventario> GetInventoryAsync(int id);
    Task<RecordList<Inventario>> ListInventoryAsync();
    Task<List<SelectListItem>> ComboProductsAsync();

    Task<bool> SaveChangesAsync();
}
