using DBContext;
using Helper;
using Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Services;

public class ProductsRepository : IProductsRepository
{
    private readonly InventarioDbContext _context;
    public ProductsRepository(InventarioDbContext context)
    {
        _context = context;
    }

    public async Task<Producto> GetProductAsync(int id)
    {
        return await _context.Productos.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<RecordList<Producto>> ListProductsAsync()
    {
        var recordList = new RecordList<Producto>();

        var products = await _context.Productos.Include(categorie => categorie.Categoria)
            .OrderBy(order => order.Nombre)
            .ToListAsync();

        recordList.Data.AddRange(products);

        return recordList;
    }

    public async Task<List<SelectListItem>> ComboCategoriesAsync()
    {
        var categories = await _context.Categorias.ToListAsync();
        var returnList = new List<SelectListItem>();
        returnList.Add(new SelectListItem("Seleccione una categoria", ""));

        var listCombo = categories.Select(category => new SelectListItem(category.Nombre, category.Id.ToString())).ToList();

        returnList.AddRange(listCombo);
        return returnList;
    }

    public void AddProduct(Producto product)
    {
        _context.Productos.Add(product);
    }

    public void UpdateProduct(Producto product)
    {
        _context.Productos.Update(product);
    }

    public void DeleteProduct(Producto product)
    {
        _context.Productos.Remove(product);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }
}
