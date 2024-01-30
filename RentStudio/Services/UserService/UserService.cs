using RentStudio.DataAccesLayer;
using RentStudio.Helpers.JwtUtils;
using RentStudio.Models.DTOs;
using RentStudio.Models.Enums;
using BCryptNet = BCrypt.Net.BCrypt;
using RentStudio.Repositories.UserRepository;

namespace RentStudio.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<User> GetById(Guid id)
        {
            return await _userRepository.FindById(id);
        }

        public async Task<UserLoginResponse> Login(UserLoginDto userDto)
        {
            var user = await _userRepository.FindByUsername(userDto.UserName);

            if (user == null || !BCryptNet.Verify(userDto.Password, user.Password))
            {
                return null; // or throw exception
            }
            if (user == null) return null;

            var token = _jwtUtils.GenerateJwtToken(user);
            return new UserLoginResponse(user, token);
        }

        public async Task<bool> Register(UserRegisterDto userRegisterDto, Role userRole)
        {
            var userToCreate = new User
            {
                Username = userRegisterDto.UserName,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                Role = userRole,
                Password = BCryptNet.HashPassword(userRegisterDto.Password)
            };

            var isCreated = _userRepository.Create(userToCreate);
            if (isCreated != null)
            {
                return true;
            }
            return false;
        }
    }
}
