using DBContext;
using Helper;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Services;

public class VentasRepository : IVentasRepository
{
    private readonly InventarioDbContext _context;
    public VentasRepository(InventarioDbContext context)
    {
        _context = context;
    }

    public async Task<Venta> GetVenta(int id)
    {
        return await _context.Ventas.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<RecordList<Venta>> ListVentasAsync()
    {
        var recordList = new RecordList<Venta>();

        var categories = await _context.Ventas.Include(cliente => cliente.Cliente)
            .OrderBy(order => order.FechaVenta)
            .ToListAsync();

        recordList.Data.AddRange(categories);

        return recordList;
    }

    public async Task<List<SelectListItem>> ComboClientesAsync()
    {
        var categories = await _context.Clientes.ToListAsync();
        var returnList = new List<SelectListItem>();
        returnList.Add(new SelectListItem("Seleccione un cliente", ""));

        var listCombo = categories.Select(category => new SelectListItem(category.Nombre, category.Id.ToString())).ToList();

        returnList.AddRange(listCombo);
        return returnList;
    }

    public void AddVenta(Venta Venta)
    {
        _context.Ventas.Add(Venta);
    }

    public void UpdateVenta(Venta Venta)
    {
        _context.Ventas.Update(Venta);
    }   

    public void RemoveVenta(Venta Venta)
    {
        _context.Ventas.Remove(Venta);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }
}
