using EntityFramework.Repository.Interface;
using EntityFramework.DbContext;
using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Repository;

public class CategorieRepository : ICategorieRepository
{
	private readonly AppDbContext _context;
	public bool AutoSaveChanges { get; set; } = true;

	public CategorieRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<int> AddCategoryAsync(Categorie categorie)
	{
		categorie.CreateDate = DateTime.Now;

		await _context.Categories.AddAsync(categorie);

		return await AutoSaveChangesAsync();
	}

	public async Task<int> UpdateCategoryAsync(Categorie categorie)
	{
		categorie.CreateDate = DateTime.Now;

		_context.Categories.Update(categorie);

		return await AutoSaveChangesAsync();
	}

	public async Task<int> DeleteCategoryAsync(Categorie categorie)
	{
		_context.Categories.Remove(categorie);

		int grabo = await AutoSaveChangesAsync();

		if(grabo == 0)
		{
			throw new Exception("No fue posible eliminar la categoria seleccionada.");
		}

		return grabo;
	}

	public async Task<Categorie> GetCategoryAsync(int id)
	{
		return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<bool> CategoryExistsAsync(int? id, string? name)
	{
		bool encontro = false;
		var categorie = await _context.Categories.Where(c => (id != null ? c.Id == id : c.Name == name)).FirstOrDefaultAsync();

		if (categorie != null) return encontro = true;

		return encontro;
	}

	public async Task<List<Categorie>> GetAllCategoriesAsync()
	{
		return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
	}

	protected virtual async Task<int> AutoSaveChangesAsync()
	{
		return AutoSaveChanges ? await _context.SaveChangesAsync() : (int)0;
	}
}
