using backend.Models.Dtos;

namespace backend.Repositories.Interfaces
{
    public interface IConnectionInterface
    {
        Task<object> GetAllReportDetails();
        Task<object> GetAllResolvedReportDetails();
        Task<object> GetAllUnresolvedReportDetails();
        Task<object> GetReportDetailsById(Guid Id);
        Task<List<UserWithReportsDTO>> GetAllUserDetails();
        Task<UserWithReportsDTO?> GetUserDetailsByUsername(string Name);
        Task<UserWithReportsDTO?> GetResolvedUserDetailsByUsername(string Name);
        Task<UserWithReportsDTO?> GetUnresolvedUserDetailsByUsername(string Name);
    }
}