using EntityFramework.Entities;

namespace EntityFramework.Repository.Interface;

public interface IUserRepository
{
	Task<List<User>> GetAllUsersAsync();
	Task<User> GetUserAsync(int id);
	Task<bool> UserExistsAsync(int? id, string? userName);
	Task<User> RegisterUserAsync(User user);
	Task<(string, User)> LoginAsync(string userName, string password);
}
