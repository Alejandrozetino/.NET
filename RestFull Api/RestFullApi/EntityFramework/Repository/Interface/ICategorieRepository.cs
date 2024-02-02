using EntityFramework.Entities;

namespace EntityFramework.Repository.Interface;

public interface ICategorieRepository
{
	Task<int> AddCategoryAsync(Categorie categorie);
	Task<int> UpdateCategoryAsync(Categorie categorie);
	Task<int> DeleteCategoryAsync(Categorie categorie);
	Task<Categorie> GetCategoryAsync(int id);
	Task<bool> CategoryExistsAsync(int? id, string? name);
	Task<List<Categorie>> GetAllCategoriesAsync();
}
