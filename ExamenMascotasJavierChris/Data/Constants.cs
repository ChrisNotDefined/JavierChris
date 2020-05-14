using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExamenMascotasJavierChris.Data
{
    public class Constants
    {
        //Constante para abrir nuestro archivo en sqlite en modo de lectura-escritura, crearlo si no existe y sea accesible en modo lectura-escritura
        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite |
                                                    SQLite.SQLiteOpenFlags.Create |
                                                    SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                // Forma la ruta completa donde se guardara el archivo de SQLite
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, "AppPetDatabaseSQLite.db3");
            }
        }
    }
}
