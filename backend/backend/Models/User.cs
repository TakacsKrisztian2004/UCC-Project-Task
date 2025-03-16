namespace backend.Models;

public partial class user
{
    public Guid ID { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
