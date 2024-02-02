using EntityFramework.Repository.Interface;
using EntityFramework.DbContext;
using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Repository;

public class CarRepository : ICarRepository
{
	private readonly AppDbContext _context;
	public bool AutoSaveChanges { get; set; } = true;

	public CarRepository(AppDbContext context)
	{
		_context = context;
	}

	protected virtual async Task<int> AutoSaveChangesAsync()
	{
		return AutoSaveChanges ? await _context.SaveChangesAsync() : (int)0;
	}

	public async Task<int> AddCarAsync(Car car)
	{
		await _context.Cars.AddAsync(car);

		return await AutoSaveChangesAsync();
	}

	public async Task<int> UpdateCarAsync(Car car)
	{
		_context.Cars.Update(car);

		return await AutoSaveChangesAsync();
	}

	public async Task<int> DeleteCarAsync(Car car)
	{
		_context.Cars.Remove(car);

		int grabo = await AutoSaveChangesAsync();

		if (grabo == 0)
		{
			throw new Exception("No fue posible eliminar el auto seleccionado.");
		}

		return grabo;
	}

	public async Task<Car> GetCarAsync(int id)
	{
		return await _context.Cars
			.Include(c => c.Categorie)
			.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<bool> CarExistsAsync(int? id, string? name)
	{
		bool encontro = false;
		var car = await _context.Cars.Where(c => (id != null ? c.Id == id : c.Name == name)).FirstOrDefaultAsync();

		if (car != null) return encontro = true;

		return encontro;
	}

	public async Task<List<Car>> GetAllCarsAsync()
	{
		return await _context.Cars
			.Include(c => c.Categorie)
			.OrderBy(c => c.Name)
			.ToListAsync();
	}

	public async Task<List<Car>> SearchCarsForCategorieAsync(int idCategorie)
	{
		return await _context.Cars
			.Include(c => c.Categorie)
			.Where(c => c.IdCategoria == idCategorie)
			.OrderBy(c => c.Name)
			.ToListAsync();
	}

	public async Task<List<Car>> SearchCarsForNameAsync(string name)
	{
		return await _context.Cars
			.Include(c => c.Categorie)
			.Where(c => c.Name.Contains(name))
			.OrderBy(c => c.Name)
			.ToListAsync();
	}
}
