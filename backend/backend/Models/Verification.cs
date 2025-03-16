namespace backend.Models;

public partial class verification
{
    public int ID { get; set; }

    public string Code { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Created_at { get; set; }
}
