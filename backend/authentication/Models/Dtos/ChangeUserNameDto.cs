namespace Auth.Models.Dtos
{
    public class ChangeUsernameDto
    {
        public required string OldUsername { get; set; }
        public required string NewUsername { get; set; }
    }
}