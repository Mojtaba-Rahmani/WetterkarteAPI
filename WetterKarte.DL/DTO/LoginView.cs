using System.ComponentModel.DataAnnotations;

namespace WebClientAnwendung.Models
{
    public class LoginView
    {

        [Required]
        public string Nutzername { get; set; }
        [Required]
        public string Passwort { get; set; }
    }
}
