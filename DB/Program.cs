using System;
using System.IO;


namespace DB
{

    class Program
    {
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



        public static void Main()
        {
            // Create directories for DB
            string pathGlowna = @"c:\kontrahenci";
            string pathFirmy = @"c:\kontrahenci\firmy";
            string pathPracownicy = @"c:\kontrahenci\pracownicy";
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
