using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;

namespace RentStudio.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly RentDbContext _context;

        public UserRepository(RentDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> FindAllActive()
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByUsername(string username)
        {
            return (await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username)))!;
        }
        public async Task<User> FindById(Guid id)
        {
            return (await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id)))!;
        }
        /* public void Create(User user)
         {
             _context.Users.Add(user);
             _context.SaveChanges();
         }*/

        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

    }
}
