using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TinPet_Projeto.Models;
using SQLite;
using System.IO;
using TinPet_Projeto.Droid.Models;
using System.Threading.Tasks;

namespace TinPet_Projeto.Droid.Models
{
    public class DataBase_Android : IDataBase
    {
        private SQLiteConnection _database; 


        public SQLiteConnection GetConexao(string nomeDB)
        {
            /*Caminho da pasta + nome do arquivo*/
            var caminhoDB = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),nomeDB);

            return new SQLiteConnection(caminhoDB);
        }

    }
}