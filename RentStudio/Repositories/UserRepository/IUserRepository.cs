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
        /*Task<User> FindByCNPAsync(string cNP);
        Task AddAsync(User user);*/

        User FindByCNP(string cnp);
        void Add(User user);
        void Save();
    }
}
