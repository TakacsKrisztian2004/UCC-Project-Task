namespace backend.Models.Dtos
{
        public record ReportDto(Guid Id, string? Title, DateTime Occurrence, string? Description, string Customer, Boolean Resolved, Guid UserID, user User);
        public record CreateReportDto(string? Title, string? Description, string Customer, Guid UserID);
        public record RemoveReportDto(Guid Id);
        public record ModifyReportDto(Guid Id, string? Title, string? Description, string Customer, Boolean Resolved);
}