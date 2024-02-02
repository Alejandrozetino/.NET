using EntityFramework.Repository.Interface;
using EntityFramework.DbContext;
using EntityFramework.Entities;
using EntityFramework.Helpers;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EntityFramework.Repository;

public class UserRepository : IUserRepository
{
	private readonly AppDbContext _context;
	public bool AutoSaveChanges { get; set; } = true;
	private string claveSecreta;


	public UserRepository(AppDbContext context, IConfiguration config)
	{
		_context = context;
		claveSecreta = config.GetSection("ApiSetting:Key").Value;
	}

	protected virtual async Task<int> AutoSaveChangesAsync()
	{
		return AutoSaveChanges ? await _context.SaveChangesAsync() : (int)0;
	}

	public async Task<List<User>> GetAllUsersAsync()
	{
		return await _context.Users
			.OrderBy(c => c.NameUser)
			.ToListAsync();
	}

	public async Task<User> GetUserAsync(int id)
	{
		return await _context.Users
			.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<bool> UserExistsAsync(int? id, string? userName)
	{
		bool encontro = false;
		var user = await _context.Users
			.Where(c => (id != null ? c.Id == id : c.NameUser == userName))
			.FirstOrDefaultAsync();

		if (user != null) return encontro = true;

		return encontro;
	}

	public async Task<User> RegisterUserAsync(User user)
	{
		var passwordEnc = GlobalMethods.ObtenerMd5(user.Password);
		
		user.Password = passwordEnc;
		user.CreateDate = DateTime.UtcNow;

		await _context.AddAsync(user);
		await _context.SaveChangesAsync();

		return user;
	}

	public async Task<(string, User)> LoginAsync(string userName, string password)
	{
		var passwordEnc = GlobalMethods.ObtenerMd5(password);
		var user = await _context.Users
			.Where(u => u.NameUser == userName && u.Password == passwordEnc)
			.FirstOrDefaultAsync();

		if (user == null)
		{
			return ("", user);
		}

		var initialToken = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(claveSecreta);
		var tokenDesc = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.Name, userName),
				new Claim(ClaimTypes.Role, user.Role),
			}),
			Expires = DateTime.UtcNow.AddDays(7),
			SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = initialToken.CreateToken(tokenDesc);

		return (initialToken.WriteToken(token), user);
	}
}
