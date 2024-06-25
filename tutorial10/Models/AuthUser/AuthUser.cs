using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial10.Models
{
    [Table("AuthUser")]
    public class AuthUsers
    {
        [Key]
        public int IdUser { get; set; } 
        public string Username { get; set; } 
        public string Password { get; set; }
        public string Salt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }

    }
}
