using backend.Models.Dtos;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Repositories.Interfaces;
using BackEnd;

namespace backend.Repositories.Services
{
    public class ReportService : IReportInterface
    {
        private readonly userreportsContext userreportsContext;

        public ReportService(userreportsContext userreportsContext)
        {
            this.userreportsContext = userreportsContext;
        }

        public async Task<ReportDto> Post(CreateReportDto createReportDto)
        {
            Report Report = new Report
            {
                ID = Guid.NewGuid(),
                Title = createReportDto.Title,
                Occurrence = DateTime.Now,
                Description = createReportDto.Description,
                Customer = createReportDto.Customer,
                Resolved = false,
                UserID = createReportDto.UserID
            };

            await userreportsContext.reports.AddAsync(Report);
            await userreportsContext.SaveChangesAsync();
            return Report.AsDto();
        }

        public async Task<IEnumerable<Report>> GetAll()
        {
            return await userreportsContext.reports.ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetResolved()
        {
            return await userreportsContext.reports.Where(x => x.Resolved == true).ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetUnresolved()
        {
            return await userreportsContext.reports.Where(x => x.Resolved == false).ToListAsync();
        }

        public async Task<Report> GetById(Guid id)
        {
            return await userreportsContext.reports.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<ReportDto> Put(Guid id, ModifyReportDto modifyReportDto)
        {
            Report? existingReport = await userreportsContext.reports.FirstOrDefaultAsync(x => x.ID == id);

            if (existingReport != null)
            {
                existingReport.Title = modifyReportDto.Title;
                existingReport.Description = modifyReportDto.Description;
                existingReport.Customer = modifyReportDto.Customer;
                existingReport.Resolved = modifyReportDto.Resolved;

                userreportsContext.Update(existingReport);
                await userreportsContext.SaveChangesAsync();

                return existingReport.AsDto();
            }

            return null;
        }

        public async Task<ReportDto?> Put(Guid id)
        {
            Report? existingReport = await userreportsContext.reports.FirstOrDefaultAsync(x => x.ID == id);

            if (existingReport != null)
            {
                existingReport.Resolved = !existingReport.Resolved;

                userreportsContext.Update(existingReport);
                await userreportsContext.SaveChangesAsync();

                return existingReport.AsDto();
            }

            return null;
        }

        public async Task<Report> DeleteById(Guid id)
        {
            Report? Report = await userreportsContext.reports.FirstOrDefaultAsync(x => x.ID == id);

            if (Report != null)
            {
                userreportsContext.reports.Remove(Report);
                await userreportsContext.SaveChangesAsync();
            }

            return Report;
        }
    }
}