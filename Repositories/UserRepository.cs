using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using UserManagementAPI.Models;

namespace UserManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users
                    .Where(x => x.IsDeleted == false)
                    .OrderBy(x => x.Name)
                    .ToListAsync();
        }

        public async Task<User> GetUserByID(int id)
        {
            return await  _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }

        
        public async Task<User> CreateUser(User user)
        {
            if (user.Age < 18)
                throw new Exception("User must be at least 18 years old");

            var emailExist = await _context.Users
                .AnyAsync(x => x.Email == user.Email && x.IsDeleted == false);


            if (emailExist)
                throw new Exception(" Email already exist");

          
            user.CreatedDate = DateTime.Now;
            user.IsDeleted = false;

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message);
            }
            return user;
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (existingUser == null)
                throw new Exception("User not found");

            var emailExists = await _context.Users
                .AnyAsync(x => x.Email == user.Email && x.Id != id);

            if (emailExists)
                throw new Exception("Email already used");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Age = user.Age;

            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (user == null)
                throw new Exception("User not found");

            // SOFT DELETE BUSINESS LOGIC
            user.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }


    }
}
