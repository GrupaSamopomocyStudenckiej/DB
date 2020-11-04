using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    class Pracownik
    {
        public string IdPracownika { get; set; }
        public string IdFirmy { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NrTelefonu { get; set; }
        public string Email { get; set; }




        public Pracownik(string idpracownika, string idfirmy, string imie, string nazwisko, string nrtelefonu, string email)
        {
            IdPracownika = idpracownika;
            IdFirmy = idfirmy;
            Imie = imie;
            Nazwisko = nazwisko;
            NrTelefonu = nrtelefonu;
            Email = email;
        }



    }
}
