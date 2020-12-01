using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Text.Json;

namespace DB
{
    class Firma_DB
    {
        public static string DlaFaktur(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = "select * from firmy;";

            Queue<Firma> firmy = new Queue<Firma>();

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                Firma firma = new Firma();
                firma.IdFirmy = sqlite_datareader.GetInt32(0).ToString();
                firma.NazwaFirmy = sqlite_datareader.GetString(2);
                firma.Nip = sqlite_datareader.GetString(3);
                firma.Miasto = sqlite_datareader.GetString(5);
                firma.Ulica = sqlite_datareader.GetString(6);
                firma.NrBudynku = sqlite_datareader.GetString(7);
                firma.NrLokalu = sqlite_datareader.GetString(8);
                firma.KodPocztowy = sqlite_datareader.GetString(9);
                firma.Poczta = sqlite_datareader.GetString(10);
                firma.Kraj = sqlite_datareader.GetString(12);
                firma.NrKonta = sqlite_datareader.GetString(15);
                firmy.Enqueue(firma);
            }
            string json = JsonSerializer.Serialize(firmy);
            return json;
        }

        public static Firma ReadDaneFirmy(SQLiteConnection conn, string IdFirmy)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = "select * from firmy where IdFirmy =" + IdFirmy + ";";

            Firma firma = new Firma();

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                firma.IdFirmy = sqlite_datareader.GetInt32(0).ToString();
                if (sqlite_datareader.IsDBNull(1))
                {
                    firma.IdSiedzibyFirmy = "0";
                }
                else
                {
                    firma.IdSiedzibyFirmy = sqlite_datareader.GetInt32(1).ToString();
                }
                firma.NazwaFirmy = sqlite_datareader.GetString(2);
                firma.Nip = sqlite_datareader.GetString(3);
                firma.Regon = sqlite_datareader.GetString(4);
                firma.Miasto = sqlite_datareader.GetString(5);
                firma.Ulica = sqlite_datareader.GetString(6);
                firma.NrBudynku = sqlite_datareader.GetString(7);
                firma.NrLokalu = sqlite_datareader.GetString(8);
                firma.KodPocztowy = sqlite_datareader.GetString(9);
                firma.Poczta = sqlite_datareader.GetString(10);
                firma.NrTelefonu = sqlite_datareader.GetString(11);
                firma.Kraj = sqlite_datareader.GetString(12);
                firma.Email = sqlite_datareader.GetString(13);
                firma.StronaWWW = sqlite_datareader.GetString(14);
                firma.NrKonta = sqlite_datareader.GetString(15);

            }
            return firma;
        }

        public static Queue<Firma> ReadDaneAllFirm(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = "select * from firmy;";

            Queue<Firma> firmy = new Queue<Firma>();

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                Firma firma = new Firma();
                firma.IdFirmy = sqlite_datareader.GetInt32(0).ToString();

                if (sqlite_datareader.IsDBNull(1))
                {
                    firma.IdSiedzibyFirmy = "0";
                }
                else
                {
                    firma.IdSiedzibyFirmy = sqlite_datareader.GetInt32(1).ToString();
                }

                firma.NazwaFirmy = sqlite_datareader.GetString(2);
                firma.Nip = sqlite_datareader.GetString(3);
                firma.Regon = sqlite_datareader.GetString(4);
                firma.Miasto = sqlite_datareader.GetString(5);
                firma.Ulica = sqlite_datareader.GetString(6);
                firma.NrBudynku = sqlite_datareader.GetString(7);
                firma.NrLokalu = sqlite_datareader.GetString(8);
                firma.KodPocztowy = sqlite_datareader.GetString(9);
                firma.Poczta = sqlite_datareader.GetString(10);
                firma.NrTelefonu = sqlite_datareader.GetString(11);
                firma.Kraj = sqlite_datareader.GetString(12);
                firma.Email = sqlite_datareader.GetString(13);
                firma.StronaWWW = sqlite_datareader.GetString(14);
                firma.NrKonta = sqlite_datareader.GetString(15);
                firmy.Enqueue(firma);
            }
            return firmy;
        }


        public static bool ZapiszFirme(SQLiteConnection conn, Firma firma)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO firmy values ('" + firma.IdFirmy + "', '" + firma.IdSiedzibyFirmy + "', '" + firma.NazwaFirmy + "', '" + firma.Nip + "', '" + firma.Regon + "', '" + firma.Miasto + "', '" + firma.Ulica + "', '" + firma.NrBudynku + "', '" + firma.NrLokalu + "', '" + firma.KodPocztowy + "', '" + firma.Poczta + "', '" + firma.NrTelefonu + "', '" + firma.Kraj + "', '" + firma.Email + "', '" + firma.StronaWWW + "', '" + firma.NrKonta + "');";
            if (sqlite_cmd.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UsunFirme(SQLiteConnection conn, string id_firmy)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "Delete from firmy where IdFirmy =" + id_firmy + ";";
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
