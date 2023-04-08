using System.ComponentModel.DataAnnotations;

namespace WebClientAnwendung.Models
{
    public class LoginViewModel
    {

        [Required]
        public string Nutzername { get; set; }
        [Required]
        public string Passwort { get; set; }
    }
}
