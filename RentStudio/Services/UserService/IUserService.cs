using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;
using RentStudio.Models.Enums;

namespace RentStudio.Services.UserService
{
    public interface IUserService
    {
        Task<UserLoginResponse> Login(UserLoginDto user);
        Task<User> GetById(Guid id);    //User GetById(Guid id);


        Task<bool> Register(UserRegisterDto userRegisterDto, Role userRole);
    }
}
