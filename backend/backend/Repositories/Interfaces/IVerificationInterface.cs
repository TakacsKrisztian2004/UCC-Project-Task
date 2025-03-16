using backend.Models;
using BackEnd.Models.Dtos;
using Email_Test_API.Models.Dtos;

namespace BackEnd.Repositories.Interfaces
{
    public interface IVerificationInterface
    {
        Task<IEnumerable<verification>> GetAll();
        Task<verification> GetById(int id);
        Task<verification> GetByEmail(string email);
        Task<verification> Post(CreateVerificationDto createVerificationDto);
        Task<bool> SendVerificationEmail(EmailDto request, string code);
        Task<bool> VerifyCodeAsync(string code, string email);
        Task<verification> Put(int id, ModifyVerificationDto modifyVerificationDto);
        Task<verification> DeleteById(int id);
    }
}