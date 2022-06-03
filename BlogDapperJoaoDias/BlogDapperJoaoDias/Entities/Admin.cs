using System.ComponentModel.DataAnnotations;

namespace BlogDapperJoaoDias.Entities
{
    public class Admin
    {
        [Dapper.Contrib.Extensions.Key]
        public int AdminId { get; set; }
        [Required(ErrorMessage = "Please enter your username")]
        [MinLength(4, ErrorMessage = "Min 4 characters")]
        [MaxLength(15, ErrorMessage = "Max 15 characters")]
        [Display(Name = "Your Username")]
        public string Username { get; set; }
        [MinLength(4, ErrorMessage = "Min 4 characters")]
        [MaxLength(15, ErrorMessage = "Max 15 characters")]
        [Required(ErrorMessage = "Please enter your password")]
        [Display(Name = "Your Password")]
        public string Password { get; set; }
    }
}
