using API.DTOs;
using API.Repositories;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<MemberDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();

            return users.Select(user => new MemberDto { Id = user.Id, Username = user.UserName, Role = user.Role });
        }

        public async Task<MemberDto?> GetUserAsync(int id)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
                return null;

            return new MemberDto { Id = user.Id, Username = user.UserName, Role=user.Role };
        }
    }
}
