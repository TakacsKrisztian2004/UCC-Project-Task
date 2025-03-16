using backend.Models;
using backend.Models.Dtos;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Services
{
    public class ConnectionService : IConnectionInterface
    {
        private readonly userreportsContext _context;

        public ConnectionService(userreportsContext context)
        {
            _context = context;
        }

        public async Task<object> GetAllReportDetails()
        {
            var reports = await _context.reports
                .Include(r => r.User)
                .Select(r => new
                {
                    reportId = r.ID,
                    title = r.Title,
                    occurrence = r.Occurrence,
                    description = r.Description,
                    customer = r.Customer,
                    resolved = r.Resolved,
                    user = new
                    {
                        userId = r.User.ID,
                        userName = r.User.Name
                    }
                })
                .ToListAsync();

            return reports;
        }

        public async Task<object> GetAllResolvedReportDetails()
        {
            var report = await _context.reports
                .Include(r => r.User)
                .Where(r => r.Resolved == true)
                .Select(r => new
                {
                    reportId = r.ID,
                    title = r.Title,
                    occurrence = r.Occurrence,
                    description = r.Description,
                    customer = r.Customer,
                    resolved = r.Resolved,
                    user = new
                    {
                        userId = r.User.ID,
                        userName = r.User.Name
                    }
                })
                .FirstOrDefaultAsync();

            if (report == null)
            {
                return $"No resolved reports were found.";
            }
            else
            {
                return report;
            }
        }

        public async Task<object> GetAllUnresolvedReportDetails()
        {
            var report = await _context.reports
                .Include(r => r.User)
                .Where(r => r.Resolved == false)
                .Select(r => new
                {
                    reportId = r.ID,
                    title = r.Title,
                    occurrence = r.Occurrence,
                    description = r.Description,
                    customer = r.Customer,
                    resolved = r.Resolved,
                    user = new
                    {
                        userId = r.User.ID,
                        userName = r.User.Name
                    }
                })
                .FirstOrDefaultAsync();

            if (report == null)
            {
                return $"No unresolved reports were found.";
            }
            else
            {
                return report;
            }
        }

        public async Task<object> GetReportDetailsById(Guid Id)
        {
            var report = await _context.reports
                .Include(r => r.User)
                .Where(r => r.ID == Id)
                .Select(r => new
                {
                    reportId = r.ID,
                    title = r.Title,
                    occurrence = r.Occurrence,
                    description = r.Description,
                    customer = r.Customer,
                    resolved = r.Resolved,
                    user = new
                    {
                        userId = r.User.ID,
                        userName = r.User.Name
                    }
                })
                .FirstOrDefaultAsync();

            if (report == null)
            {
                return $"Report with ID {Id} not found!";
            }
            else
            {
                return report;
            }
        }

        public async Task<List<UserWithReportsDTO>> GetAllUserDetails()
        {
            var users = await _context.users
                .Include(u => u.Reports)
                .Select(u => new UserWithReportsDTO
                {
                    UserId = u.ID,
                    UserName = u.Name,
                    Reports = u.Reports.Select(r => new ReportDTO
                    {
                        ReportId = r.ID,
                        Title = r.Title,
                        Occurrence = r.Occurrence,
                        Description = r.Description,
                        Customer = r.Customer,
                        Resolved = r.Resolved
                    }).ToList()
                })
                .ToListAsync();

            return users;
        }

        public async Task<UserWithReportsDTO?> GetUserDetailsByUsername(string Name)
        {
            var user = await _context.users
                .Include(u => u.Reports)
                .Where(u => u.Name == Name)
                .Select(u => new UserWithReportsDTO
                {
                    UserId = u.ID,
                    UserName = u.Name,
                    Reports = u.Reports.Select(r => new ReportDTO
                    {
                        ReportId = r.ID,
                        Title = r.Title,
                        Occurrence = r.Occurrence,
                        Description = r.Description,
                        Customer = r.Customer,
                        Resolved = r.Resolved
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return user ?? null;
        }

        public async Task<UserWithReportsDTO?> GetResolvedUserDetailsByUsername(string Name)
        {
            var user = await _context.users
                .Include(u => u.Reports)
                .Where(u => u.Name == Name)
                .Select(u => new UserWithReportsDTO
                {
                    UserId = u.ID,
                    UserName = u.Name,
                    Reports = u.Reports
                        .Where(r => r.Resolved)
                        .Select(r => new ReportDTO
                        {
                            ReportId = r.ID,
                            Title = r.Title,
                            Occurrence = r.Occurrence,
                            Description = r.Description,
                            Customer = r.Customer,
                            Resolved = r.Resolved
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return user ?? null;
        }

        public async Task<UserWithReportsDTO?> GetUnresolvedUserDetailsByUsername(string Name)
        {
            var user = await _context.users
                .Include(u => u.Reports)
                .Where(u => u.Name == Name)
                .Select(u => new UserWithReportsDTO
                {
                    UserId = u.ID,
                    UserName = u.Name,
                    Reports = u.Reports
                        .Where(r => !r.Resolved)
                        .Select(r => new ReportDTO
                        {
                            ReportId = r.ID,
                            Title = r.Title,
                            Occurrence = r.Occurrence,
                            Description = r.Description,
                            Customer = r.Customer,
                            Resolved = r.Resolved
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return user ?? null;
        }
    }
}