using backend.Models;
using backend.Models.Dtos;

namespace BackEnd.Repositories.Interfaces
{
    public interface IUserInterface
    {
        Task<IEnumerable<user>> GetAll();
        Task<user> GetById(Guid id);
        Task<user> GetByUsername(string name);
        Task<UserDto> Post(CreateUserDto createUserDto);
        Task<UserDto> Put(Guid id, ModifyUserDto modifyUserDto);
        Task<user> DeleteById(Guid id);
    }
}