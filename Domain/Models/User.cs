using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required, MaxLength(50)]
    [Column(TypeName = "nvarchar(50)")]
    public string Username { get; set; }

    [Required, MaxLength(200)]
    [Column(TypeName = "nvarchar(200)")]
    public string PasswordHash { get; set; }

    [Required, MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public string Role { get; set; } // "Admin", "User", "Dealer"
}
