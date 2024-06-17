using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities;

public class User
{
    [Key]
    public int IdUser { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
}