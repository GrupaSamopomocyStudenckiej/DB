using System;
using System.IO;


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

        static void showPracownicy()
        {
            try
            {
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

        static bool showMenu()
        {
            Console.Clear();
            Console.WriteLine("Wybierz opcje:");
            Console.WriteLine("1) Wyswietl firmy");
            Console.WriteLine("2) Wyswietl pracownikow");
            Console.Write("\r\nWybrano opcje: ");
            switch (Console.ReadLine())
            {
                case "1":
                    //showPracownicy();
                    return true;
                case "2":
                    showPracownicy();
                    return true;
                default:
                    Console.WriteLine("Nie wybrano nic. Sprubuj ponownie.");
                    showMenu();
                    return true;

            }

        }

        static void zapiszPracownika(Pracownik pracownik)
        {
            try
            {
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

            showMenu();






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
