using System;
using System.IO;
using System.Runtime.InteropServices;
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
                // Determine whether the directory exists.
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
                    //showPracownicy();
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
                    pokazPracownika();
                    return true;
                case "3":
                    zmienPracownika();
                    return true;
                case "4":
                    usunPracownika();
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
        static void zapiszPracownika(Pracownik pracownik)
        {
            try
            {
                string pathPracownik = @"c:\kontrahenci\pracownicy\" + pracownik.IdPracownika;
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


        static void pokazPracownika()
        {
            try
            {
                Console.WriteLine("Podaj ID pracownika do odczytania: ");
                string pracownikDoPokazania = pathPracownicy + "\\" + Console.ReadLine();

                string pracownikObjRead = File.ReadAllText(pracownikDoPokazania);
                string[] pracownikObjFields = pracownikObjRead.Split(';');
                Pracownik pracownikRead = new Pracownik(pracownikObjFields[0], pracownikObjFields[1], pracownikObjFields[2], pracownikObjFields[3], pracownikObjFields[4], pracownikObjFields[5]);

                Console.Clear();
                Console.WriteLine("ID pracownika: " + pracownikRead.IdPracownika);
                Console.WriteLine("ID firmy: " + pracownikRead.IdFirmy);
                Console.WriteLine("Imię pracownika: " + pracownikRead.Imie);
                Console.WriteLine("Nazwisko pracownika: " + pracownikRead.Nazwisko);
                Console.WriteLine("Numer telefonu pracownika: " + pracownikRead.NrTelefonu);
                Console.WriteLine("Adres e-mail pracownika: " + pracownikRead.Email);

                Console.WriteLine("\r\nNaciśnij dowolny przycisk, by wrócić do listy pracowników");
                Console.ReadKey();
                showPracownicy();
                showMenuPracownicy();
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
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
                    string pracownikDoUsuniecia = pathPracownicy + "\\" + pracownikRead.IdPracownika;

                    if (File.Exists(pracownikDoUsuniecia))
                    {
                        File.Delete(pracownikDoUsuniecia);
                    }

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

        static void usunPracownika()
        {
            try
            {
                Console.WriteLine("Podaj ID pracownika do usunięcia: ");
                string pracownikDoUsuniecia = pathPracownicy + "\\" + Console.ReadLine();
                if (File.Exists(pracownikDoUsuniecia))
                {
                    File.Delete(pracownikDoUsuniecia);
                }
                showPracownicy();
                showMenuPracownicy();
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }


        }









        public static void Main()
        {
            // Create directories for DB
            createDir(pathGlowna);
            createDir(pathFirmy);
            createDir(pathPracownicy);


            //TODO: CIN
            //TODO: zapis w pliku txt CHECK 
            //TODO: odczyt z pliku CHECK
            //TODO: wyszukiwanie pliku CHECK
            //TODO:  modyfikacja = porównanie 2 obj (odczytanego i cin) żeby niepotrzebnie nie deletować
            //TODO: usuwanie pliku CHECK

            //ID jako nazwa pliku


            Pracownik pracownik1 = new Pracownik("1", "3", "weqqw", "jhgjhgh", "1234569", "123@sdf.jon");
            Pracownik pracownik2 = new Pracownik("2", "2", "dsadas", "dsadasd", "1234569", "123@sdf.jon");
            Pracownik pracownik3 = new Pracownik("3", "1", "weqqw", "cxzcxzzxc", "1234569", "123@sdf.jon");

            zapiszPracownika(pracownik1);
            zapiszPracownika(pracownik2);
            zapiszPracownika(pracownik3);

            showMenuGlowne();



            /*


                        Pracownik pracownik = new Pracownik("22", "2", "weqqw", "dsadasd", "1234569", "123@sdf.jon");
                        //Console.WriteLine();

                        string pathPracownik = @"c:\kontrahenci\pracownicy\" + pracownik.IdPracownika;
                        string pracownikObj = pracownik.IdPracownika + ";" + pracownik.IdFirmy + ";" + pracownik.Imie + ";" + pracownik.Nazwisko + ";" + pracownik.NrTelefonu + ";" + pracownik.Email;

                        // zapis do pliku z obiektu
                        if (!File.Exists(pathPracownik))
                        {
                            using (StreamWriter sw = File.CreateText(pathPracownik))
                            {
                                sw.WriteLine(pracownikObj);
                            }
                        }

                        // odczyt z pliku do objektu
                        string pracownikObjRead = File.ReadAllText(pathPracownik);
                        string[] pracownikObjFields = pracownikObjRead.Split(';');

                        Pracownik pracownikRead = new Pracownik(pracownikObjFields[0], pracownikObjFields[1], pracownikObjFields[2], pracownikObjFields[3], pracownikObjFields[4], pracownikObjFields[5]);

                        //Console.WriteLine(pracownikRead.Imie);

                        // wyszukiwanie
                        DirectoryInfo d = new DirectoryInfo(pathPracownicy);
                        foreach (var field in d.GetFiles("2")) // zmienna do wyszukania 
                        {
                            Console.WriteLine(field.Name);
                            string aaa = pathPracownicy + '\'+ field.Name;
                        }

                        //Usuwanie
                        if (File.Exists(pathPracownik))
                        {
                            File.Delete(pathPracownik);

                        }




                        //System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", text);

                        /*foreach (var field in pracownik)
                        {
                            Console.WriteLine(pracownikObj);
                        }*/
        }
    }
}
