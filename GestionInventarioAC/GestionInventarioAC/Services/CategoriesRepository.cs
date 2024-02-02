using DBContext;
using Helper;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Services;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly InventarioDbContext _context;
    public CategoriesRepository(InventarioDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria> GetCategoryAsync(int id)
    {
        return await _context.Categorias.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<RecordList<Categoria>> ListCategoriesAsync()
    {
        var recordList = new RecordList<Categoria>();

        var categories = await _context.Categorias
            .OrderBy(order => order.Nombre)
            .ToListAsync();

        recordList.Data.AddRange(categories);

        return recordList;
    }

    public void AddCategory(Categoria category)
    {
        _context.Categorias.Add(category);
    }

    public void UpdateCategory(Categoria category)
    {
        _context.Categorias.Update(category);
    }

    public void DeleteCategory(Categoria category)
    {
        _context.Categorias.Remove(category);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }
}
