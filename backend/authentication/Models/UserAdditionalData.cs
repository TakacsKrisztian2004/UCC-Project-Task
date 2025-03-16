using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Models
{
    [Table("users", Schema = "userreports")]
    public class UserReport
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}