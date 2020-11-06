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

        public Pracownik()
        { }

        public Pracownik(string idpracownika, string idfirmy, string imie, string nazwisko, string nrtelefonu, string email)
        {
            IdPracownika = idpracownika;
            IdFirmy = idfirmy;
            Imie = imie;
            Nazwisko = nazwisko;
            NrTelefonu = nrtelefonu;
            Email = email;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Pracownik))
                return false;

            var other = obj as Pracownik;

            if (IdPracownika != other.IdPracownika || IdFirmy != other.IdFirmy || Imie != other.Imie || Nazwisko != other.Nazwisko || NrTelefonu != other.NrTelefonu || Email != other.Email)
                return false;

            return true;
        }

        public static bool operator ==(Pracownik x, Pracownik y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Pracownik x, Pracownik y)
        {
            return !(x == y);
        }
    }
}
