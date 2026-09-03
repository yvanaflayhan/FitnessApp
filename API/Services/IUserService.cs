using API.DTOs;

namespace API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<MemberDto>> GetUsersAsync();
        Task<PagedResultDto<MemberDto>> GetUsersPagedAsync(int pageNumber, int pageSize);
        Task<MemberDto?> GetUserAsync(int id);

        Task<MemberDto?> UpdateUserAsync(int id, MemberUpdateDto updateDto);
        Task<bool> DeleteUserAsync(int id);
        Task<MemberDto?> UpdateLocationAsync(int id, UpdateLocationDto updateDto);
        Task<MemberDto?> UpdateUsernameAsync (int id, UpdateUsernameDto updateDto);
    }
}
