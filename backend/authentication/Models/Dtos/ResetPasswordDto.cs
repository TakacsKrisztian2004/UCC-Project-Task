namespace Auth.Models.Dtos
{
    public class ResetPasswordDto
    {
        public required string EmailAddress { get; set; }
        public required string NewPassword { get; set; }
        public required string resetToken { get; set; }
    }
}