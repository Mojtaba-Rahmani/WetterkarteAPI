using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WetterKarte.DL.Entities.User
{
    public class User
    {
        public int NutzerId { get; set; }

        public string Nutzername { get; set; }

        public string Email { get; set; }

        public string Passwort { get; set; }
    }
}
