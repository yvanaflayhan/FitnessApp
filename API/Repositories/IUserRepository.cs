using API.Entities;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser?> GetUserAsync(int id);

        Task<bool> UserExistsAsync(string username);
        Task AddUserAsync(AppUser user);
        Task<AppUser?> GetUserByUsernameAsync(string username);
    }
}
