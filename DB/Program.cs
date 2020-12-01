using System;
using static DB.ConnectToSQLite;
using static DB.Firma_F;
using static DB.Pracownik_F;
namespace DB
{

    class Program
    {
        public static bool ShowMenuGlowne()
        {
            Console.Clear();
            Console.WriteLine("Wybierz opcje:");
            Console.WriteLine("1) Wyswietl firmy");
            Console.WriteLine("2) Wyswietl pracownikow");
            Console.WriteLine("0) Wyjście");
            Console.Write("\r\nWybrano opcje: ");
            switch (Console.ReadLine())
            {
                case "1":
                    ShowFirmy();
                    ShowMenuFirmy();
                    return true;
                case "2":
                    ShowPracownicy();
                    ShowMenuPracownicy();
                    return true;
                case "0":
                    Environment.Exit(1);
                    return true;

                default:
                    Console.WriteLine("Nie wybrano nic. Sprubuj ponownie.");
                    ShowMenuGlowne();
                    return true;
            }
        }

        public static void Main()
        {
            FirstRun();
            ShowMenuGlowne();
        }

    }
}
