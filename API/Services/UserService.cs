using API.DTOs;
using API.Repositories;
using Microsoft.AspNetCore.Http.Features;

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

            return users.Select(user => new MemberDto
            {
                Id = user.Id,
                Username = user.UserName,
                Role = user.Role,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
                Location = user.Location,
            });
        }

        public async Task<PagedResultDto<MemberDto>> GetUsersPagedAsync(
            int pageNumber,
            int pageSize
        )
        {
            var result = await _userRepository.GetUsersPagedAsync(pageNumber, pageSize);

            var members = result
                .Items.Select(user => new MemberDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Role = user.Role,
                    Latitude = user.Latitude,
                    Longitude = user.Longitude,
                    Location = user.Location,
                })
                .ToList();

            return new PagedResultDto<MemberDto>
            {
                Items = members,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
            };
        }

        public async Task<MemberDto?> GetUserAsync(int id)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
                return null;

            return new MemberDto
            {
                Id = user.Id,
                Username = user.UserName,
                Role = user.Role,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
                Location = user.Location,
            };
        }

        public async Task<MemberDto?> UpdateUserAsync(int id, MemberUpdateDto updateDto)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
                return null;

            user.UserName = updateDto.Username;
            user.Role = updateDto.Role;

            await _userRepository.UpdateUserAsync(user);

            return new MemberDto
            {
                Id = user.Id,
                Username = user.UserName,
                Role = user.Role,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
                Location = user.Location,
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<MemberDto?> UpdateLocationAsync(int id, UpdateLocationDto updateDto)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
                return null;

            user.Latitude = updateDto.Latitude;
            user.Longitude = updateDto.Longitude;
            user.Location = updateDto.Location;

            await _userRepository.UpdateUserAsync(user);

            return new MemberDto
            {
                Id = user.Id,
                Username = user.UserName,
                Role = user.Role,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
                Location = user.Location,
            };
        }

        public async Task<MemberDto?> UpdateUsernameAsync(int id, UpdateUsernameDto updateDto)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
                return null;

            user.UserName = updateDto.Username.ToLower();

            await _userRepository.UpdateUserAsync(user);

            return new MemberDto
            {
                Id = user.Id,
                Username = user.UserName,
                Role = user.Role,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
                Location = user.Location,
            };
        }
    }
}
