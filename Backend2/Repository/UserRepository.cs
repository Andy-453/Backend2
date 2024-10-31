using Backend2.Context;
using Backend2.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Repository
{
	public interface IUserRepository
	{
		Task<IEnumerable<Users>> GetAllUsersAsync();
		Task<Users> GetByEmailAsync(string email);
		Task<Users> GetUsersByIdAsync(int id);
		Task CreateUsersAsync(Users user);
		Task<Users> GetUsersByPermissionsAsync(string name, string password);
	}
	public class UserRepository : IUserRepository
	{

		private readonly JuegoDbContext _context;
		public UserRepository(JuegoDbContext context)
		{
			_context = context;
		}

		public async Task CreateUsersAsync(Users user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Users>> GetAllUsersAsync() 
		{
            return await _context.Users
                .ToListAsync();
        }

		public async Task<Users> GetByEmailAsync(string email)
		{
			return await _context.Users
				.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task<Users> GetUsersByIdAsync(int id) 
		{

            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

        }

		public async Task<Users> GetUsersByPermissionsAsync(string name, string password)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(u => u.Name == name);
			if (user == null || !VerifyPassword(password, user.Password))
			{
				return null;
			}
			return user;
		}

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
			return inputPassword == storedPassword;     
		}
    }
}
