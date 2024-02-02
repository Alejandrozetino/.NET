using DBContext;
using Helper;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Services;

public class ProveedoresRepository : IProveedoresRepository
{
    private readonly InventarioDbContext _context;
    public ProveedoresRepository(InventarioDbContext context)
    {
        _context = context;
    }

    public async Task<Proveedor> GetProveedor(int id)
    {
        return await _context.Proveedores.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<RecordList<Proveedor>> ListProveedoresAsync()
    {
        var recordList = new RecordList<Proveedor>();

        var categories = await _context.Proveedores
            .OrderBy(order => order.Nombre)
            .ToListAsync();

        recordList.Data.AddRange(categories);

        return recordList;
    }

    public void AddProveedor(Proveedor Proveedor)
    {
        _context.Proveedores.Add(Proveedor);
    }

    public void UpdateProveedor(Proveedor Proveedor)
    {
        _context.Proveedores.Update(Proveedor);
    }

    public void RemoveProveedor(Proveedor Proveedor)
    {
        _context.Proveedores.Remove(Proveedor);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }
}
