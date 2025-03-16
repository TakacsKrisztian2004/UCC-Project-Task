namespace backend.Models.Dtos
{
    public class UserWithReportsDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<ReportDTO> Reports { get; set; } = new List<ReportDTO>();
    }

    public class ReportDTO
    {
        public Guid ReportId { get; set; }
        public string Title { get; set; }
        public DateTime Occurrence { get; set; }
        public string Description { get; set; }
        public string Customer { get; set; }
        public Boolean Resolved { get; set; }
    }
}