using DBContext;
using Helper;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Services;

public class ClientesRepository : IClientesRepository
{
    private readonly InventarioDbContext _context;
    public ClientesRepository(InventarioDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente> GetCliente(int id)
    {
        return await _context.Clientes.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<RecordList<Cliente>> ListClientesAsync()
    {
        var recordList = new RecordList<Cliente>();

        var categories = await _context.Clientes
            .OrderBy(order => order.Nombre)
            .ToListAsync();

        recordList.Data.AddRange(categories);

        return recordList;
    }

    public void AddCliente(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
    }

    public void UpdateCliente(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
    }

    public void RemoveCliente(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }
}
