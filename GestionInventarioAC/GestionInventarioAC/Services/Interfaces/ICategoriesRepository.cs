using Helper;
using Entities;

namespace Services.Interfaces;

public interface ICategoriesRepository
{
    void AddCategory(Categoria category);
    void UpdateCategory(Categoria category);
    void DeleteCategory(Categoria category);
    Task<Categoria> GetCategoryAsync(int id);
    Task<RecordList<Categoria>> ListCategoriesAsync();

    Task<bool> SaveChangesAsync();
}
