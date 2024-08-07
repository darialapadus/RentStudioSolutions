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

        public User Create(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return user;
        }
       
        public User FindByCNP(string cnp)
        {
            return _context.Users.FirstOrDefault(u => u.CNP == cnp);
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
