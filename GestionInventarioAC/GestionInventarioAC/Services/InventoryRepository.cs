using DBContext;
using Helper;
using Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Services;

public class InventoryRepository : IInventoryRepository
{
    private readonly InventarioDbContext _context;
    public InventoryRepository(InventarioDbContext context)
    {
        _context = context;
    }

    public async Task<Inventario> GetInventoryAsync(int id)
    {
        return await _context.Inventarios.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<RecordList<Inventario>> ListInventoryAsync()
    {
        var recordList = new RecordList<Inventario>();

        var inventory = await _context.Inventarios.Include(inventory => inventory.Producto)
            .OrderBy(order => order.Stock)
            .ToListAsync();

        recordList.Data.AddRange(inventory);

        return recordList;
    }

    public async Task<List<SelectListItem>> ComboProductsAsync()
    {
        var products = await _context.Productos.ToListAsync();
        var returnList = new List<SelectListItem>();
        returnList.Add(new SelectListItem("Seleccione un producto", ""));

        var listCombo = products.Select(product => new SelectListItem(product.Nombre, product.Id.ToString())).ToList();

        returnList.AddRange(listCombo);
        return returnList;
    }

    public void AddInventory(Inventario inventory)
    {
        _context.Inventarios.Add(inventory);
    }

    public void UpdateInventory(Inventario inventory)
    {
        _context.Inventarios.Update(inventory);
    }

    public void DeleteInventory(Inventario inventory)
    {
        _context.Inventarios.Remove(inventory);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }
}
