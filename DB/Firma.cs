using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    class Firma
    {
        public string IdFirmy { get; set; }
        public string IdSiedzibyFirmy { get; set; }
        public string NazwaFirmy { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public string NrBudynku { get; set; }
        public string NrLokalu { get; set; }
        public string KodPocztowy { get; set; }
        public string Poczta { get; set; }
        public string NrTelefonu { get; set; }
        public string Kraj { get; set; }
        public string Email { get; set; }
        public string StronaWWW { get; set; }
        public string NrKonta { get; set; }
        

        public Firma()
        { }

        public Firma(string idfirmy, string idsiedzibyfirmy, string nazwafirmy, string nip, string regon, string miasto, string ulica, string nrbudynku,
            string nrlokalu, string kodpocztowy, string poczta, string nrtelefonu, string kraj, string email, string stronawww, string nrkonta)
        {
            IdFirmy = idfirmy;
            IdSiedzibyFirmy = idsiedzibyfirmy;
            NazwaFirmy = nazwafirmy;
            Nip = nip;
            Regon = regon;
            Miasto = miasto;
            Ulica = ulica;
            NrBudynku = nrbudynku;
            NrLokalu = nrlokalu;
            KodPocztowy = kodpocztowy;
            Poczta = poczta;
            NrTelefonu = nrtelefonu;
            Kraj = kraj;
            Email = email;
            StronaWWW = stronawww;
            NrKonta = nrkonta;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Firma))
                return false;

            var other = obj as Firma;

            if (IdFirmy != other.IdFirmy || NazwaFirmy != other.NazwaFirmy || Nip != other.Nip || Regon != other.Regon || Miasto != other.Miasto || Ulica != other.Ulica || NrBudynku != other.NrBudynku || NrLokalu != other.NrLokalu || KodPocztowy != other.KodPocztowy || Poczta != other.Poczta || NrTelefonu != other.NrTelefonu || Kraj != other.Kraj || Email != other.Email || StronaWWW != other.StronaWWW || NrKonta != other.NrKonta || IdSiedzibyFirmy != other.IdSiedzibyFirmy)
                return false;

            return true;
        }

        public static bool operator ==(Firma x, Firma y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Firma x, Firma y)
        {
            return !(x == y);
        }
    }
}