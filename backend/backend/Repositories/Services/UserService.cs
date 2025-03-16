using backend.Models;
using backend.Models.Dtos;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories.Services
{
    public class UserService : IUserInterface
    {
        private readonly userreportsContext UserReportsContext;

        public UserService(userreportsContext UserReportsContext)
        {
            this.UserReportsContext = UserReportsContext;
        }

        public async Task<UserDto> Post(CreateUserDto createUserDto)
        {
            user user = new user
            {
                ID = Guid.NewGuid(),
                Name = createUserDto.Name,
            };

            await UserReportsContext.users.AddAsync(user);
            await UserReportsContext.SaveChangesAsync();
            return user.AsDto();
        }

        public async Task<IEnumerable<user>> GetAll()
        {
            return await UserReportsContext.users.ToListAsync();
        }

        public async Task<user> GetById(Guid id)
        {
            return await UserReportsContext.users.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<user> GetByUsername(string name)
        {
            return await UserReportsContext.users.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<UserDto> Put(Guid id, ModifyUserDto modifyUserDto)
        {
            user? existingUser = await UserReportsContext.users.FirstOrDefaultAsync(x => x.ID == id);

            if (existingUser != null)
            {
                existingUser.Name = modifyUserDto.Name;

                UserReportsContext.Update(existingUser);
                await UserReportsContext.SaveChangesAsync();

                return existingUser.AsDto();
            }

            return null;
        }

        public async Task<user> DeleteById(Guid id)
        {
            user? user = await UserReportsContext.users.FirstOrDefaultAsync(x => x.ID == id);

            if (user != null)
            {
                UserReportsContext.users.Remove(user);
                await UserReportsContext.SaveChangesAsync();
            }

            return user;
        }
    }
}