using System;
using System.IO;
using WindowsInput;

namespace DB
{

    class Program
    {
        static string pathGlowna = @"c:\kontrahenci";
        static string pathFirmy = @"c:\kontrahenci\firmy";
        static string pathPracownicy = @"c:\kontrahenci\pracownicy";

        static void createDir(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
        static bool showMenuGlowne()
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
                    showFirmy();
                    showMenuFirmy();
                    return true;
                case "2":
                    showPracownicy();
                    showMenuPracownicy();
                    return true;
                case "0":
                    System.Environment.Exit(1);
                    return true;

                default:
                    Console.WriteLine("Nie wybrano nic. Sprubuj ponownie.");
                    showMenuGlowne();
                    return true;
            }
        }

        static void showPracownicy()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Pracownicy:");
                DirectoryInfo di = new DirectoryInfo(pathPracownicy);

                foreach (var field in di.GetFiles()) // zmienna do wyszukania 
                {
                    string Patch = pathPracownicy + '\\' + field.Name;
                    string pracownikObjRead = File.ReadAllText(Patch);
                    string[] pracownikObjFields = pracownikObjRead.Split(';');
                    Pracownik pracownikRead = new Pracownik(pracownikObjFields[0], pracownikObjFields[1], pracownikObjFields[2], pracownikObjFields[3], pracownikObjFields[4], pracownikObjFields[5]);
                    Console.WriteLine(pracownikRead.IdPracownika + ". " + pracownikRead.Imie + " " + pracownikRead.Nazwisko);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static void showFirmy()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Firmy:");
                DirectoryInfo di = new DirectoryInfo(pathFirmy);
                foreach (var field in di.GetFiles()) // zmienna do wyszukania 
                {
                    string Patch = pathFirmy + '\\' + field.Name;
                    string firmaObjRead = File.ReadAllText(Patch);
                    string[] firmaObjFields = firmaObjRead.Split(';');
                    Firma firmaRead = new Firma(firmaObjFields[0], firmaObjFields[1], firmaObjFields[2], firmaObjFields[3], firmaObjFields[4], firmaObjFields[5], firmaObjFields[6], firmaObjFields[7], firmaObjFields[8], firmaObjFields[9], firmaObjFields[10], firmaObjFields[11], firmaObjFields[12], firmaObjFields[13], firmaObjFields[14], firmaObjFields[15]);

                    Console.WriteLine(firmaRead.IdFirmy + ". " + firmaRead.NazwaFirmy + " " + firmaRead.Nip + " " + firmaRead.Regon);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static bool showMenuPracownicy()
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
                    dodajPracownika();
                    return true;
                case "2":
                    Console.WriteLine("Podaj ID pracownika do odczytania: ");
                    pokazPracownika(Console.ReadLine());
                    showPracownicy();
                    showMenuPracownicy();
                    return true;
                case "3":
                    zmienPracownika();
                    return true;
                case "4":
                    Console.WriteLine("Podaj ID pracownika do usunięcia: ");
                    usunPracownika(Console.ReadLine());
                    showPracownicy();
                    showMenuPracownicy();
                    return true;
                case "9":
                    showMenuGlowne();
                    return true;
                case "0":
                    System.Environment.Exit(1);
                    return true;
                default:
                    Console.WriteLine("Nie wybrano nic. Sprubuj ponownie.");
                    showMenuPracownicy();
                    return true;
            }
        }

        static bool showMenuFirmy()
        {
            Console.WriteLine("\r\nWybierz opcje:");
            Console.WriteLine("1) Dodaj nową firmę do bazy");
            Console.WriteLine("2) Wyświetl dane firmy");
            Console.WriteLine("3) Zmień dane firmy");
            Console.WriteLine("4) Usuń firmę");
            Console.WriteLine("9) Wróć do menu głównego");
            Console.WriteLine("0) Wyjście");
            Console.Write("\r\nWybrano opcje: ");
            switch (Console.ReadLine())
            {
                case "1":
                    dodajFirme();
                    return true;
                case "2":
                    Console.WriteLine("Podaj ID firmy do odczytania: ");
                    pokazFirme(Console.ReadLine());
                    showFirmy();
                    showMenuFirmy();
                    return true;
                case "3":
                    zmienFirme();
                    return true;
                case "4":
                    Console.WriteLine("Podaj ID firmy do usunięcia: ");
                    usunFirme(Console.ReadLine());
                    showFirmy();
                    showMenuFirmy();
                    return true;
                case "9":
                    showMenuGlowne();
                    return true;
                case "0":
                    System.Environment.Exit(1);
                    return true;
                default:
                    Console.WriteLine("Nie wybrano nic. Sprubuj ponownie.");
                    showMenuFirmy();
                    return true;
            }
        }

        static void zapiszPracownika(Pracownik pracownik)
        {
            try
            {
                string pathPracownik = pathPracownicy + "\\" + pracownik.IdPracownika;
                string pracownikObj = pracownik.IdPracownika + ";" + pracownik.IdFirmy + ";" + pracownik.Imie + ";" + pracownik.Nazwisko + ";" + pracownik.NrTelefonu + ";" + pracownik.Email;
                pracownikObj = pracownikObj.Replace("\n", "").Replace("\r", "");

                // zapis do pliku z obiektu
                if (!File.Exists(pathPracownik))
                {
                    using (StreamWriter sw = File.CreateText(pathPracownik))
                    {
                        sw.WriteLine(pracownikObj);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static void zapiszFirme(Firma firma)
        {
            try
            {
                string pathFirma = pathFirmy + "\\" + firma.IdFirmy;
                string firmaObj = firma.IdFirmy + ";" + firma.IdSiedzibyFirmy + ';' + firma.NazwaFirmy + ";" + firma.Nip + ";" + firma.Regon + ";" + firma.Miasto + ";" + firma.Ulica + ";" + firma.NrBudynku + ";" + firma.NrLokalu + ";" + firma.KodPocztowy + ";" + firma.Poczta + ";" + firma.NrTelefonu + ";" + firma.Kraj + ";" + firma.Email + ";" + firma.StronaWWW + ";" + firma.NrKonta;
                firmaObj = firmaObj.Replace("\n", "").Replace("\r", "");

                // zapis do pliku z obiektu
                if (!File.Exists(pathFirma))
                {
                    using (StreamWriter sw = File.CreateText(pathFirma))
                    {
                        sw.WriteLine(firmaObj);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static void dodajPracownika()
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

            zapiszPracownika(pracownikNowy);
            showPracownicy();
            showMenuPracownicy();
        }

        static void dodajFirme()
        {
            Firma firmyNowy = new Firma();

            Console.WriteLine("Podaj ID firmy: ");
            firmyNowy.IdFirmy = Console.ReadLine();

            Console.WriteLine("Podaj ID siedziby firmy: ");
            firmyNowy.IdSiedzibyFirmy = Console.ReadLine();

            Console.WriteLine("Podaj nazwę firmy: ");
            firmyNowy.NazwaFirmy = Console.ReadLine();

            Console.WriteLine("Podaj NIP firmy: ");
            firmyNowy.Nip = Console.ReadLine();

            Console.WriteLine("Podaj REGON firmy: ");
            firmyNowy.Regon = Console.ReadLine();

            Console.WriteLine("Podaj miasto firmy: ");
            firmyNowy.Miasto = Console.ReadLine();

            Console.WriteLine("Podaj ulicę firmy: ");
            firmyNowy.Ulica = Console.ReadLine();

            Console.WriteLine("Podaj numer budynku firmy: ");
            firmyNowy.NrBudynku = Console.ReadLine();

            Console.WriteLine("Podaj numer lokalu firmy: ");
            firmyNowy.NrLokalu = Console.ReadLine();

            Console.WriteLine("Podaj kod pocztowy firmy: ");
            firmyNowy.KodPocztowy = Console.ReadLine();

            Console.WriteLine("Podaj pocztę firmy: ");
            firmyNowy.Poczta = Console.ReadLine();

            Console.WriteLine("Podaj numer telefonu do firmy: ");
            firmyNowy.NrTelefonu = Console.ReadLine();

            Console.WriteLine("Podaj kraj firmy: ");
            firmyNowy.Kraj = Console.ReadLine();

            Console.WriteLine("Podaj adres e-mail firmy: ");
            firmyNowy.Email = Console.ReadLine();

            Console.WriteLine("Podaj stronę WWW firmy: ");
            firmyNowy.StronaWWW = Console.ReadLine();

            Console.WriteLine("Podaj numer konta firmy: ");
            firmyNowy.NrKonta = Console.ReadLine();



            zapiszFirme(firmyNowy);
            showFirmy();
            showMenuFirmy();
        }

        static void pokazPracownika(String id_pracownika)
        {
            try
            {
                string pracownikDoPokazania = pathPracownicy + "\\" + id_pracownika;

                string pracownikObjRead = File.ReadAllText(pracownikDoPokazania);
                string[] pracownikObjFields = pracownikObjRead.Split(';');
                Pracownik pracownikRead = new Pracownik(pracownikObjFields[0], pracownikObjFields[1], pracownikObjFields[2], pracownikObjFields[3], pracownikObjFields[4], pracownikObjFields[5]);

                Console.Clear();
                Console.WriteLine("ID pracownika: " + pracownikRead.IdPracownika);
                Console.WriteLine("ID firmy: " + pracownikRead.IdFirmy);
                Console.WriteLine("Imię pracownika: " + pracownikRead.Imie);
                Console.WriteLine("Nazwisko pracownika: " + pracownikRead.Nazwisko);
                Console.WriteLine("Numer telefonu pracownika: " + pracownikRead.NrTelefonu);
                Console.WriteLine("Adres e-mail pracownika: " + pracownikRead.Email.Replace("\n", "").Replace("\r", ""));

                Console.WriteLine("\r\n 1) Wróć do listy pracowników");
                Console.WriteLine("\n 2) Wyświetl dane firmy, w której pracuje");
                //  TO DO: podpiac do wyświetlania szczegółowych danych firmy (relacja) dzięki równości wartosci ID firmy.
                // TO DO : SWITCH
                //TO DO: FUNKCJA LINIA 642

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
        static void wyszukajPracownikowFirmy(String id_firmy)
        {
            DirectoryInfo di = new DirectoryInfo(pathPracownicy);
            foreach (var field in di.GetFiles())
            {
                string Patch = pathPracownicy + '\\' + field.Name;
                string pracownikObjRead = File.ReadAllText(Patch);
                string[] pracownikObjFields = pracownikObjRead.Split(';');
                if (pracownikObjFields[1] == id_firmy)
                {
                    Pracownik pracownikRead = new Pracownik(pracownikObjFields[0], pracownikObjFields[1], pracownikObjFields[2], pracownikObjFields[3], pracownikObjFields[4], pracownikObjFields[5]);
                    Console.WriteLine(pracownikRead.IdPracownika + ". " + pracownikRead.Imie + " " + pracownikRead.Nazwisko);
                }
            }
        }

        static bool pokazFirme(String id_firmy)
        {
            try
            {

                string firmaDoPokazania = pathFirmy + "\\" + id_firmy;
                string firmaObjRead = File.ReadAllText(firmaDoPokazania);
                string[] firmaObjFields = firmaObjRead.Split(';');
                Firma firmaRead = new Firma(firmaObjFields[0], firmaObjFields[1], firmaObjFields[2], firmaObjFields[3], firmaObjFields[4], firmaObjFields[5], firmaObjFields[6], firmaObjFields[7], firmaObjFields[8], firmaObjFields[9], firmaObjFields[10], firmaObjFields[11], firmaObjFields[12], firmaObjFields[13], firmaObjFields[14], firmaObjFields[15]);

                Console.Clear();
                Console.WriteLine("ID firmy: " + firmaRead.IdFirmy);
                Console.WriteLine("ID siedziby firmy: " + firmaRead.IdSiedzibyFirmy);
                Console.WriteLine("Nazwa firmy: " + firmaRead.NazwaFirmy);
                Console.WriteLine("NIP: " + firmaRead.Nip);
                Console.WriteLine("REGON: " + firmaRead.Regon);
                Console.WriteLine(firmaRead.Miasto + " " + "Ul. " + firmaRead.Ulica + " " + firmaRead.NrBudynku + "/" + firmaRead.NrLokalu + " " + firmaRead.KodPocztowy + " " + firmaRead.Poczta);
                Console.WriteLine("Numer telefonu: " + firmaRead.NrTelefonu);
                Console.WriteLine("Kraj: " + firmaRead.Kraj);
                Console.WriteLine("Email: " + firmaRead.Email);
                Console.WriteLine("Strona WWW: " + firmaRead.StronaWWW);
                Console.WriteLine("Numer konta: " + firmaRead.NrKonta.Replace("\n", "").Replace("\r", "") + "%");

                Console.WriteLine("\r\n1) Wróc do listy firm");
                Console.WriteLine("2) Wyświetl jej pracowników");
                Console.WriteLine("3) Wyświetl jej siedzibę");
                Console.Write("Wybrano opcje: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        showFirmy();
                        showMenuFirmy();
                        return true;
                    case "2":
                        wyszukajPracownikowFirmy(firmaRead.IdFirmy);
                        return true;
                        //TODO: WYRZUCA NAS Z POWROTEM, NIE WYSWIETLA DANYCH FIRMY  LINE 619
                    case "3":
                        //        if (firmaRead.IdSiedzibyFirmy=="0")
                        //         {
                        //             Console.WriteLine("To jest siedziba firmy");
                        //         }
                        //         else
                        //         {
                        Console.WriteLine(firmaRead.IdSiedzibyFirmy);
                        //wyszukajSiedzibeFirmy(firmaRead.IdSiedzibyFirmy);
                        //   }
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

        static void zmienPracownika()
        {
            try
            {
                Pracownik pracownikNowy = new Pracownik();
                InputSimulator sim = new InputSimulator();

                Console.WriteLine("Podaj ID pracownika do zmiany: ");

                string pracownikDoPokazania = pathPracownicy + "\\" + Console.ReadLine();
                string pracownikObjRead = File.ReadAllText(pracownikDoPokazania);
                string[] pracownikObjFields = pracownikObjRead.Split(';');
                Pracownik pracownikRead = new Pracownik(pracownikObjFields[0], pracownikObjFields[1], pracownikObjFields[2], pracownikObjFields[3], pracownikObjFields[4], pracownikObjFields[5].Replace("\n", "").Replace("\r", ""));

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
                    showPracownicy();
                    showMenuPracownicy();
                }
                else
                {
                    usunPracownika(pracownikRead.IdPracownika);
                    zapiszPracownika(pracownikNowy);
                    showPracownicy();
                    showMenuPracownicy();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static void zmienFirme()
        {
            try
            {
                Firma firmaNowy = new Firma();
                InputSimulator sim = new InputSimulator();

                Console.WriteLine("Podaj ID firmy do zmiany: ");

                string firmaDoPokazania = pathFirmy + "\\" + Console.ReadLine();
                string firmaObjRead = File.ReadAllText(firmaDoPokazania);
                string[] firmaObjFields = firmaObjRead.Split(';');
                Firma firmaRead = new Firma(firmaObjFields[0], firmaObjFields[1], firmaObjFields[2], firmaObjFields[3], firmaObjFields[4], firmaObjFields[5], firmaObjFields[6], firmaObjFields[7], firmaObjFields[8], firmaObjFields[9], firmaObjFields[10], firmaObjFields[11], firmaObjFields[12], firmaObjFields[13], firmaObjFields[14], firmaObjFields[15].Replace("\n", "").Replace("\r", ""));

                Console.Clear();

                Console.Write("ID firmy: ");
                sim.Keyboard.TextEntry(firmaRead.IdFirmy);
                firmaNowy.IdFirmy = Console.ReadLine();

                Console.Write("ID siedziby firmy: ");
                sim.Keyboard.TextEntry(firmaRead.IdSiedzibyFirmy);
                firmaNowy.IdSiedzibyFirmy = Console.ReadLine();

                Console.Write("Nazwa firmy: ");
                sim.Keyboard.TextEntry(firmaRead.NazwaFirmy);
                firmaNowy.NazwaFirmy = Console.ReadLine();

                Console.Write("NIP firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Nip);
                firmaNowy.Nip = Console.ReadLine();

                Console.Write("REGON firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Regon);
                firmaNowy.Regon = Console.ReadLine();

                Console.Write("Miasto firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Miasto);
                firmaNowy.Miasto = Console.ReadLine();

                Console.Write("Ulica firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Ulica);
                firmaNowy.Ulica = Console.ReadLine();

                Console.Write("Numer budynku firmy: ");
                sim.Keyboard.TextEntry(firmaRead.NrBudynku);
                firmaNowy.NrBudynku = Console.ReadLine();

                Console.Write("Numer lokalu firmy: ");
                sim.Keyboard.TextEntry(firmaRead.NrLokalu);
                firmaNowy.NrLokalu = Console.ReadLine();

                Console.Write("Kod pocztowy firmy: ");
                sim.Keyboard.TextEntry(firmaRead.KodPocztowy);
                firmaNowy.KodPocztowy = Console.ReadLine();

                Console.Write("Poczta firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Poczta);
                firmaNowy.Poczta = Console.ReadLine();

                Console.Write("Numer telefonu firmy: ");
                sim.Keyboard.TextEntry(firmaRead.NrTelefonu);
                firmaNowy.NrTelefonu = Console.ReadLine();

                Console.Write("Kraj firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Kraj);
                firmaNowy.Kraj = Console.ReadLine();

                Console.Write("E-mail firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Email);
                firmaNowy.Email = Console.ReadLine();

                Console.Write("Strona WWW firmy: ");
                sim.Keyboard.TextEntry(firmaRead.StronaWWW);
                firmaNowy.StronaWWW = Console.ReadLine();

                Console.Write("Numer konta firmy: ");
                sim.Keyboard.TextEntry(firmaRead.NrKonta);
                firmaNowy.NrKonta = Console.ReadLine();

                if (firmaNowy == firmaRead)
                {
                    showFirmy();
                    showMenuFirmy();
                }
                else
                {
                    usunFirme(firmaRead.IdFirmy);
                    zapiszFirme(firmaNowy);
                    showFirmy();
                    showMenuFirmy();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static void usunPracownika(string id_pracownika)
        {
            try
            {
                string pracownikDoUsuniecia = pathPracownicy + "\\" + id_pracownika;
                if (File.Exists(pracownikDoUsuniecia))
                {
                    File.Delete(pracownikDoUsuniecia);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static void usunFirme(string id_firmy)
        {
            try
            {
                string firmaDoUsuniecia = pathFirmy + "\\" + id_firmy;
                if (File.Exists(firmaDoUsuniecia))
                {
                    File.Delete(firmaDoUsuniecia);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static void wyszukajSiedzibeFirmy(String id_siedziby)
        {
            DirectoryInfo di = new DirectoryInfo(pathFirmy);
            foreach (var field in di.GetFiles())
            {
                string Patch = pathFirmy + '\\' + field.Name;
                string FirmaObjRead = File.ReadAllText(Patch);
                string[] firmaObjFields = FirmaObjRead.Split(';');
                if (firmaObjFields[1] == id_siedziby)
                {
                    Firma firmaRead = new Firma(firmaObjFields[0], firmaObjFields[1], firmaObjFields[2], firmaObjFields[3], firmaObjFields[4], firmaObjFields[5], firmaObjFields[6], firmaObjFields[7], firmaObjFields[8], firmaObjFields[9], firmaObjFields[10], firmaObjFields[11], firmaObjFields[12], firmaObjFields[13], firmaObjFields[14], firmaObjFields[15]);
                    Console.WriteLine(firmaRead.NazwaFirmy + ". " + firmaRead.Nip + " " + firmaRead.Regon);
                }
                else
                if (firmaObjFields[1] == "0")
                {
                    Console.WriteLine("To jest siedziba firmy");
                }



            }
        }
        static void wyszukajFirmePracownika(String id_firmy)
        {
            DirectoryInfo di = new DirectoryInfo(pathFirmy);
            foreach (var field in di.GetFiles())
            {
                string Patch = pathFirmy + '\\' + field.Name;
                string FirmaObjRead = File.ReadAllText(Patch);
                string[] firmaObjFields = FirmaObjRead.Split(';');
                if (firmaObjFields[1] == id_firmy)
                {
                    Firma firmaRead = new Firma(firmaObjFields[0], firmaObjFields[1], firmaObjFields[2], firmaObjFields[3], firmaObjFields[4], firmaObjFields[5], firmaObjFields[6], firmaObjFields[7], firmaObjFields[8], firmaObjFields[9], firmaObjFields[10], firmaObjFields[11], firmaObjFields[12], firmaObjFields[13], firmaObjFields[14], firmaObjFields[15]);
                    Console.WriteLine(firmaRead.NazwaFirmy + ". " + firmaRead.Nip + " " + firmaRead.Regon);
                }


            }
        }
        public static void Main()
        {

            createDir(pathGlowna);
            createDir(pathFirmy);
            createDir(pathPracownicy);

            //Dane pokazowe

            Pracownik pracownik1 = new Pracownik("1", "3", "weqqw", "jhgjhgh", "1234569", "123@sdf.jon");
            Pracownik pracownik2 = new Pracownik("2", "2", "dsadas", "dsadasd", "1234569", "123@sdf.jon");
            Pracownik pracownik3 = new Pracownik("3", "1", "weqqw", "cxzcxzzxc", "1234569", "123@sdf.jon");

            zapiszPracownika(pracownik1);
            zapiszPracownika(pracownik2);
            zapiszPracownika(pracownik3);

            Firma firma1 = new Firma("1", "0", "nazwa", "912213312", "123123123", "Resovia", "Szportowa", "2", "10", "22-222", "Rzeszów", "123123123", "Polska", "test@test.com", "www.firma.pl", "212345678654321345678765");
            Firma firma2 = new Firma("2", "1", "nazwa1", "912214312", "12312343123", "Rzeszów", "Chopina", "2", "10", "12-222", "Kielnarowa", "987987987", "Polska", "test2@test.com", "www.firma2.pl", "212345678632321345678765");

            zapiszFirme(firma1);
            zapiszFirme(firma2);

            showMenuGlowne();
        }

    }
}
