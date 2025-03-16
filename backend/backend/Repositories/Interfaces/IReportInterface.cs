using backend.Models.Dtos;
using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IReportInterface
    {
        Task<IEnumerable<Report>> GetAll();
        Task<IEnumerable<Report>> GetResolved();
        Task<IEnumerable<Report>> GetUnresolved();
        Task<Report> GetById(Guid id);
        Task<ReportDto> Post(CreateReportDto createReportDto);
        Task<ReportDto> Put(Guid id, ModifyReportDto modifyReportDto);
        Task<ReportDto> Put(Guid id);
        Task<Report> DeleteById(Guid id);
    }
}