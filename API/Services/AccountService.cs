using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Entities;
using API.Repositories;

namespace API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(
            IUserRepository userRepository,
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var exists = await _userRepository.UserExistsAsync(registerDto.Username); //tell repository to check if username exist

            if (exists)
                throw new Exception("Username is taken"); //if username exist registration are not allowed

            using var hmac = new HMACSHA512();

            var user = new AppUser //converting registerDto to AppUser
            {
                UserName = registerDto.Username.ToLower(),
                FullName = registerDto.FullName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
            };

            await _userRepository.AddUserAsync(user); //save user in database

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                Role = user.Role,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
                Location = user.Location,
            };
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);

            if (user == null)
                throw new Exception("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    throw new Exception("Invalid password");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                Role = user.Role,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
            };
        }

        public async Task<UserDto> GetCurrentUserAsync()
        {
            var userIdClaim = _httpContextAccessor
                .HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)
                ?.Value;

            if (userIdClaim == null)
                throw new Exception("User ID not found in token.");

            var userId = int.Parse(userIdClaim);

            var user = await _userRepository.GetUserAsync(userId);

            if (user == null)
                throw new Exception("User not found.");

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                Role = user.Role,
            };
        }
    }
}
