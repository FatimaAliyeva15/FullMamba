using System.ComponentModel.DataAnnotations;

namespace FullMamba.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
