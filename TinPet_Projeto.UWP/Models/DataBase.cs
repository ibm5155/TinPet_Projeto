using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TinPet_Projeto.Models;
using Windows.Storage;
using System.IO;
using TinPet_Projeto.UWP.Models;
using System.Threading;
using Windows.ApplicationModel;

namespace TinPet_Projeto.UWP.Models
{
    public class DataBase : IDataBase
    {
        private SQLiteConnection _database;

        public async Task GetConnectionAsync()
        {

            var caminhoDB2 = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "DataBase.db3");
            _database = new SQLiteConnection(caminhoDB2);
            _database.CreateTable<teste>();
            _database.Commit();
            _database.Close();






            Console.WriteLine("oi db");
            var nomeDB = "Cachorros.db";
            /*Caminho da pasta + nome do arquivo*/
            await SetGambiarraPraCopiarBancoDeDados (nomeDB);
            var caminhoDB = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, nomeDB);
            Console.WriteLine("oi db carregando");
            _database = new SQLiteConnection(caminhoDB);
            _database.CreateTable<Cachorro>(); 
        }

        public List<Cachorro> GetCachorros(int genero)
        {
            return _database.Table<Cachorro>().Where(c => c.Genero == genero).ToList();
        }

        public async Task SetGambiarraPraCopiarBancoDeDados(string NomeDB)
        {
            bool isExisting = false;
            try
            {
                StorageFile storage = await ApplicationData.Current.LocalFolder.GetFileAsync(NomeDB);
                isExisting = true;
            }
            catch (Exception)
            {
                isExisting = false;
            }
            if (!isExisting)
            {
                StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync(NomeDB);
                await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder, NomeDB, NameCollisionOption.ReplaceExisting);
            }

        }
    }
}