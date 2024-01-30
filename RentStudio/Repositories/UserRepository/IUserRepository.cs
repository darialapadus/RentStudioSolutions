using RentStudio.DataAccesLayer;

namespace RentStudio.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> FindByUsername(string username);
        //Task<User> FindByUsernameAndPassword(string username, string password);
        Task<User> FindById(Guid id);

        Task<List<User>> FindAll();

        Task<List<User>> FindAllActive();

        User Create(User user);
    }
}
