using Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities;

namespace Services.Interfaces;

public interface IProductsRepository
{
    void AddProduct(Producto product);
    void UpdateProduct(Producto product);
    void DeleteProduct(Producto product);
    Task<Producto> GetProductAsync(int id);
    Task<RecordList<Producto>> ListProductsAsync();
    Task<List<SelectListItem>> ComboCategoriesAsync();

    Task<bool> SaveChangesAsync();
}
