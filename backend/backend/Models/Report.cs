namespace backend.Models;

public partial class Report
{
    public Guid ID { get; set; }

    public string Title { get; set; } = null!;

    public DateTime Occurrence { get; set; }

    public string? Description { get; set; }

    public string Customer { get; set; } = null!;

    public Boolean Resolved { get; set; } = false!;

    public Guid UserID { get; set; }

    public virtual user User { get; set; } = null!;
}
