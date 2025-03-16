using backend.Models;
using backend.Models.Dtos;

namespace BackEnd
{
    public static class Extensions
    {
        public static UserDto AsDto(this user user)
        {
            return new UserDto(user.ID, user.Name, user.Reports);
        }

        public static ReportDto AsDto(this Report report)
        {
            {
                return new ReportDto(report.ID, report.Title, report.Occurrence, report.Description, report.Customer, report.Resolved, report.UserID, report.User);
            }
        }
    }
}