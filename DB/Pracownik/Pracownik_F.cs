using System;
using System.Collections.Generic;
using System.Text;
using static DB.Program;
using static DB.ConnectToSQLite;
using static DB.Pracownik_DB;
using static DB.Firma_F;
using WindowsInput;

namespace DB
{
    class Pracownik_F
    {
        public static void ShowPracownicy()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Pracownicy:");
                Queue<Pracownik> pracownicy = new Queue<Pracownik>();
                pracownicy = ReadDaneAllPracownikow(CreateConnection());

                foreach (Pracownik pracownik in pracownicy) // zmienna do wyszukania 
                {
                    Console.WriteLine(pracownik.IdPracownika + ". " + pracownik.Imie + " " + pracownik.Nazwisko);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public static bool ShowMenuPracownicy()
        {
            Console.WriteLine("\r\nWybierz opcje:");
            Console.WriteLine("1) Dodaj nowego pracownika do bazy");
            Console.WriteLine("2) Wyświetl dane pracownika");
            Console.WriteLine("3) Zmień dane pracownika");
            Console.WriteLine("4) Usuń pracownika");
            Console.WriteLine("9) Wróć do menu głównego");
            Console.WriteLine("0) Wyjście");
            Console.Write("\r\nWybrano opcje: ");
            switch (Console.ReadLine())
            {
                case "1":
                    DodajPracownika();
                    return true;
                case "2":
                    Console.WriteLine("Podaj ID pracownika do odczytania: ");
                    PokazPracownika(Console.ReadLine());
                    return true;
                case "3":
                    ZmienPracownika();
                    return true;
                case "4":
                    Console.WriteLine("Podaj ID pracownika do usunięcia: ");
                    UsunPracownika(CreateConnection(),Console.ReadLine());
                    ShowPracownicy();
                    ShowMenuPracownicy();
                    return true;
                case "9":
                    ShowMenuGlowne();
                    return true;
                case "0":
                    System.Environment.Exit(1);
                    return true;
                default:
                    Console.WriteLine("Nie wybrano nic. Sprubuj ponownie.");
                    ShowMenuPracownicy();
                    return true;
            }
        }
        static void DodajPracownika()
        {
            Pracownik pracownikNowy = new Pracownik();

            Console.WriteLine("Podaj ID pracownika: ");
            pracownikNowy.IdPracownika = Console.ReadLine();

            Console.WriteLine("Podaj ID firmy: ");
            pracownikNowy.IdFirmy = Console.ReadLine();

            Console.WriteLine("Podaj imię pracownika: ");
            pracownikNowy.Imie = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko pracownika: ");
            pracownikNowy.Nazwisko = Console.ReadLine();

            Console.WriteLine("Podaj numer telefonu pracownika: ");
            pracownikNowy.NrTelefonu = Console.ReadLine();

            Console.WriteLine("Podaj adres e-mail pracownika: ");
            pracownikNowy.Email = Console.ReadLine();

            ZapiszPracownika(CreateConnection(), pracownikNowy);
            ShowPracownicy();
            ShowMenuPracownicy();
        }

        static bool PokazPracownika(string id_pracownika)
        {
            try
            {
                Pracownik pracownikRead = ReadDanePracownika(CreateConnection(), id_pracownika);

                Console.Clear();
                Console.WriteLine("ID pracownika: " + pracownikRead.IdPracownika);
                Console.WriteLine("ID firmy: " + pracownikRead.IdFirmy);
                Console.WriteLine("Imię pracownika: " + pracownikRead.Imie);
                Console.WriteLine("Nazwisko pracownika: " + pracownikRead.Nazwisko);
                Console.WriteLine("Numer telefonu pracownika: " + pracownikRead.NrTelefonu);
                Console.WriteLine("Adres e-mail pracownika: " + pracownikRead.Email.Replace("\n", "").Replace("\r", ""));

                Console.WriteLine("\n1) Wróć do listy pracowników");
                Console.WriteLine("2) Wyświetl dane firmy, w której pracuje");
                Console.Write("Wybrano opcje: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        ShowPracownicy();
                        ShowMenuPracownicy();
                        return true;
                    case "2":
                        WyszukajFirmePracownika(pracownikRead.IdFirmy);
                        return true;
                    default:
                        Console.WriteLine("Nie wybrano nic. Sprubuj ponownie.");
                        return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return false;
            }
        }
        static void ZmienPracownika()
        {
            try
            {
                Pracownik pracownikNowy = new Pracownik();
                InputSimulator sim = new InputSimulator();

                Console.WriteLine("Podaj ID pracownika do zmiany: ");
                Pracownik pracownikRead = ReadDanePracownika(CreateConnection(), Console.ReadLine());
                Console.Clear();

                Console.Write("ID pracownika: ");
                sim.Keyboard.TextEntry(pracownikRead.IdPracownika);
                pracownikNowy.IdPracownika = Console.ReadLine();

                Console.Write("ID firmy: ");
                sim.Keyboard.TextEntry(pracownikRead.IdFirmy);
                pracownikNowy.IdFirmy = Console.ReadLine();

                Console.Write("Imię pracownika: ");
                sim.Keyboard.TextEntry(pracownikRead.Imie);
                pracownikNowy.Imie = Console.ReadLine();

                Console.Write("Nazwisko pracownika: ");
                sim.Keyboard.TextEntry(pracownikRead.Nazwisko);
                pracownikNowy.Nazwisko = Console.ReadLine();

                Console.Write("Numer telefonu pracownika: ");
                sim.Keyboard.TextEntry(pracownikRead.NrTelefonu);
                pracownikNowy.NrTelefonu = Console.ReadLine();

                Console.Write("Adres e-mail pracownika: ");
                sim.Keyboard.TextEntry(pracownikRead.Email);
                pracownikNowy.Email = Console.ReadLine();

                if (pracownikNowy == pracownikRead)
                {
                    ShowPracownicy();
                    ShowMenuPracownicy();
                }
                else
                {
                    UsunPracownika(CreateConnection(),pracownikRead.IdPracownika);
                    ZapiszPracownika(CreateConnection(),pracownikNowy);
                    ShowPracownicy();
                    ShowMenuPracownicy();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public static void WyszukajPracownikowFirmy(string id_firmy)
       {
           bool czyZnalazl = false;

            Queue<Pracownik> pracownicy = new Queue<Pracownik>();
            pracownicy = ReadDaneAllPracownikow(CreateConnection());

            foreach (Pracownik pracownik in pracownicy)
            {
               if (pracownik.IdFirmy == id_firmy)
               {
                   Console.WriteLine(pracownik.IdPracownika + ". " + pracownik.Imie + " " + pracownik.Nazwisko);
                   czyZnalazl = true;
               }

           }
           if (czyZnalazl == false)
           {
               Console.WriteLine("Błąd wyszukania, brak odpowiadających rekordów w bazie");
           }
           Console.ReadKey(true);
           ShowMenuFirmy();
       }

    }
}
