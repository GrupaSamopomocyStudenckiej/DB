using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    class Firma
    {
        public string IdFirmy { get; set; }
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
        public string Rabat { get; set; }


        public Firma(string idfirmy, string nazwafirmy, string nip, string regon, string miasto, string ulica, string nrbudynku,
            string nrlokalu, string kodpocztowy, string poczta, string nrtelefonu, string kraj, string email, string stronawww, string nrkonta, string rabat)
        {
            IdFirmy = idfirmy;
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
            Email = email;
            StronaWWW = stronawww;
            NrKonta = nrkonta;
            Rabat = rabat;
        }


    }

}
