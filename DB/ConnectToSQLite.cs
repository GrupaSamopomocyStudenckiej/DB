using System.Data.SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace DB
{
    class ConnectToSQLite
    {
        static readonly string Path = "c:\\kontrahenci";
        static readonly string Database = "BazaKontrachentow.db";
        static readonly string DatabasePath = Path + "\\" + Database;

        public static void FirstRun()
        {
            try
            {
                if (!Directory.Exists(@Path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(@Path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            if (!System.IO.File.Exists(DatabasePath))
            {
                SQLiteConnection.CreateFile(DatabasePath);
                SQLiteConnection conn = CreateConnection();
                try
                {
                    CreateTable(conn);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }

                try
                {
                    InsertData(conn);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }
            }
            else
            {
                SQLiteConnection conn = CreateConnection();
                if (!TableExists("firmy", conn) && !TableExists("pracownicy", conn))
                {
                    try
                    {
                        CreateTable(conn);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }

                    try
                    {
                        InsertData(conn);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }
                }
            }
        }


        public static SQLiteConnection CreateConnection()
        {
            // Create a new database connection:
            SQLiteConnection sqlite_conn = new SQLiteConnection(@"Data Source =" + DatabasePath);
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return sqlite_conn;

        }

        public static void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            string CreateFirmy = "CREATE TABLE IF NOT EXISTS firmy (IdFirmy	INTEGER NOT NULL UNIQUE, IdSiedzibyFirmy	INTEGER, NazwaFirmy	TEXT NOT NULL, Nip	TEXT NOT NULL, Regon	TEXT NOT NULL, Miasto	TEXT NOT NULL, Ulica	TEXT,	NrBudynku	TEXT NOT NULL, NrLokalu	TEXT, KodPocztowy	TEXT NOT NULL, Poczta	TEXT NOT NULL, NrTelefonu	TEXT NOT NULL, Kraj	TEXT, Email	TEXT NOT NULL, StronaWWW	TEXT, NrKonta	TEXT NOT NULL, PRIMARY KEY(IdFirmy AUTOINCREMENT), FOREIGN KEY(IdSiedzibyFirmy) REFERENCES firmy(IdFirmy));";
            string CreatePracownicy = "CREATE TABLE pracownicy (IdPracownika	INTEGER NOT NULL UNIQUE, IdFirmy	INTEGER, Imie	TEXT NOT NULL, Nazwisko	TEXT NOT NULL, NrTelefonu	TEXT NOT NULL, Email	TEXT NOT NULL, FOREIGN KEY(IdFirmy) REFERENCES firmy(IdFirmy), PRIMARY KEY(IdPracownika AUTOINCREMENT));";

            sqlite_cmd.CommandText = CreateFirmy;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = CreatePracownicy;
            sqlite_cmd.ExecuteNonQuery();
        }


        public static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = "INSERT INTO firmy values (1, null, 'nazwa', '912213312', '123123123', 'Resovia', 'Szportowa', '2', '10', '22-222', 'Rzeszów', '123123123', 'Polska', 'test@test.com', 'www.firma.pl', '212345678654321345678765');";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO firmy values ('2', '1', 'nazwa1', '912214312', '12312343123', 'Rzeszów', 'Chopina', '2', '10', '12-222', 'Kielnarowa', '987987987', 'Polska', 'test2@test.com', 'www.firma2.pl', '212345678632321345678765');";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO firmy values ('3', '3', 'nazw35', '914312', '12312343', 'Rzeszów', 'Chopina', '2', '10', '12-222', 'Kielnarowa', '987987987', 'Polska', 'test2@test.com', 'www.firma2.pl', '212345678632321345678765');";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO pracownicy values('1', '3', 'weqqw', 'jhgjhgh', '1234569', '123@sdf.jon');";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO pracownicy values('2', '2', 'dsadas', 'dsadasd', '1234569', '123@sdf.jon');";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO pracownicy values('3', '1', 'weqqw', 'cxzcxzzxc', '1234569', '123@sdf.jon');";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO pracownicy values('4', '1', 'Samotny', 'Wilk', '1234569', '123@sdf.jon');";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO pracownicy values('5', null, 'Uszkodzony', 'Wilk', '1234569', '123@sdf.jon');";
            sqlite_cmd.ExecuteNonQuery();

        }

        public static bool TableExists(string tableName, SQLiteConnection conn)
        {
            bool exists;

            try
            {
                var cmd = new SQLiteCommand(
                  "select case when exists((select * from information_schema.tables where table_name = '" + tableName + "')) then 1 else 0 end", conn);

                exists = (int)cmd.ExecuteScalar() == 1;
            }
            catch
            {
                try
                {
                    exists = true;
                    var cmdOthers = new SQLiteCommand("select 1 from " + tableName + " where 1 = 0", conn);
                    cmdOthers.ExecuteNonQuery();
                }
                catch
                {
                    exists = false;
                }
            }
            return exists;
        }

    }
}