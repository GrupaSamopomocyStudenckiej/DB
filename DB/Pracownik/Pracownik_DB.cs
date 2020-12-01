using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace DB
{
    class Pracownik_DB
    {
        public static Pracownik ReadDanePracownika(SQLiteConnection conn, string id_pracownika)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = "select * from pracownicy where IdPracownika =" + id_pracownika + ";";

            Pracownik pracownik = new Pracownik();
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                pracownik.IdPracownika = sqlite_datareader.GetInt32(0).ToString();
                if (String.IsNullOrEmpty(sqlite_datareader.GetInt32(1).ToString()))
                {
                    pracownik.IdFirmy = "0";
                }
                else
                {
                    pracownik.IdFirmy = sqlite_datareader.GetInt32(1).ToString();
                }
                pracownik.Imie = sqlite_datareader.GetString(2);
                pracownik.Nazwisko = sqlite_datareader.GetString(3).ToString();
                pracownik.NrTelefonu = sqlite_datareader.GetString(4);
                pracownik.Email = sqlite_datareader.GetString(5);
            }
            return pracownik;
        }

        public static Queue<Pracownik> ReadDaneAllPracownikow(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = "select * from pracownicy;";

            Queue<Pracownik> pracownicy = new Queue<Pracownik>();

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                Pracownik pracownik = new Pracownik();
                pracownik.IdPracownika = sqlite_datareader.GetInt32(0).ToString();
                if (sqlite_datareader.IsDBNull(1))
                {
                    pracownik.IdFirmy = "0";
                }
                else
                {
                    pracownik.IdFirmy = sqlite_datareader.GetInt32(1).ToString();
                }
                pracownik.Imie = sqlite_datareader.GetString(2);
                pracownik.Nazwisko = sqlite_datareader.GetString(3);
                pracownik.NrTelefonu = sqlite_datareader.GetString(4);
                pracownik.Email = sqlite_datareader.GetString(5);
                pracownicy.Enqueue(pracownik);
            }
            return pracownicy;
        }

        public static bool ZapiszPracownika(SQLiteConnection conn, Pracownik pracownik)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO pracownicy values ('" + pracownik.IdFirmy + "', '" + pracownik.IdFirmy + "', '" + pracownik.Imie + "', '" + pracownik.Nazwisko + "', '" + pracownik.NrTelefonu + "', '" + pracownik.Email + "');";
            if (sqlite_cmd.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UsunPracownika(SQLiteConnection conn, string id_pracownika)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "Delete from pracownicy where IdPracownika =" + id_pracownika + ";";
            if (sqlite_cmd.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
