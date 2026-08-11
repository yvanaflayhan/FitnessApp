using API.DTOs;

namespace API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<MemberDto>> GetUsersAsync();
        Task<MemberDto?> GetUserAsync(int id);
    }
}