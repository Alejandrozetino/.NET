using EntityFramework.Entities;

namespace EntityFramework.Repository.Interface;

public interface ICarRepository
{
	Task<int> AddCarAsync(Car car);
	Task<int> UpdateCarAsync(Car car);
	Task<int> DeleteCarAsync(Car car);
	Task<Car> GetCarAsync(int id);
	Task<bool> CarExistsAsync(int? id, string? name);
	Task<List<Car>> GetAllCarsAsync();
	Task<List<Car>> SearchCarsForCategorieAsync(int idCategorie);
	Task<List<Car>> SearchCarsForNameAsync(string name);
}
