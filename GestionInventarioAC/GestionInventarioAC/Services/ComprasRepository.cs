using DBContext;
using Helper;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Services;

public class ComprasRepository : IComprasRepository
{
    private readonly InventarioDbContext _context;
    public ComprasRepository(InventarioDbContext context)
    {
        _context = context;
    }

    public async Task<Compra> GetCompra(int id)
    {
        return await _context.Compras.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<RecordList<Compra>> ListComprasAsync()
    {
        var recordList = new RecordList<Compra>();

        var categories = await _context.Compras.Include(proveedor => proveedor.Proveedor)
            .OrderBy(order => order.FechaCompra)
            .ToListAsync();

        recordList.Data.AddRange(categories);

        return recordList;
    }

    public async Task<List<SelectListItem>> ComboProveedoresAsync()
    {
        var categories = await _context.Proveedores.ToListAsync();
        var returnList = new List<SelectListItem>();
        returnList.Add(new SelectListItem("Seleccione un proveedor", ""));

        var listCombo = categories.Select(category => new SelectListItem(category.Nombre, category.Id.ToString())).ToList();

        returnList.AddRange(listCombo);
        return returnList;
    }

    public void AddCompra(Compra Compra)
    {
        _context.Compras.Add(Compra);
    }

    public void UpdateCompra(Compra Compra)
    {
        _context.Compras.Update(Compra);
    }   

    public void RemoveCompra(Compra Compra)
    {
        _context.Compras.Remove(Compra);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }
}
