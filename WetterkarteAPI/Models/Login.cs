using System.ComponentModel.DataAnnotations;

namespace WetterkarteAPI.Models
{
    public class Login
    {
        [Required]
        public string Nutzername { get; set; }

        [Required]
        public string Passwort { get; set; }
    }
}
