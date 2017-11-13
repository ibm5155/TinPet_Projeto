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
    public class DataBase : IDataBase
    {
        private SQLiteConnection _database; 


        public async Task GetConnectionAsync()
        {
            var nomeDB = "DataBase.db";
            /*Caminho da pasta + nome do arquivo*/
            var caminhoDB = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),nomeDB);

            _database = new SQLiteConnection(caminhoDB);
        }

    }
}