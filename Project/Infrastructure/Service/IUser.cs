using Project.Model;
using Project.Model.DTO;

namespace Project.Infrastructure.Service
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> AddUserAsync(User user);

        LoginResponseDto Login(LoginRequestDto loginDto);

        Task<User> UpdateUserDetail(int id, User user);
    }
}
