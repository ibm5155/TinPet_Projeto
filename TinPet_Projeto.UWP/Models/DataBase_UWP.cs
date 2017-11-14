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
    public class DataBase_UWP : IDataBase
    {
//        private SQLiteConnection _database;
        private Semaphore EsperaCopia = new Semaphore(0, 1);
        /*
        public async Task GetConnectionAsync()
        {

            Console.WriteLine("oi db");
            var nomeDB = "Cachorros.db";
            /*Caminho da pasta + nome do arquivo*/
        /*    await SetGambiarraPraCopiarBancoDeDados (nomeDB);
            EsperaCopia.WaitOne();
            var caminhoDB = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, nomeDB);
            Console.WriteLine("oi db carregando");
            _database = new SQLiteConnection(caminhoDB);
        }*/

        public SQLiteConnection GetConexao(string NomeDB)
        {
            /*Caminho da pasta + nome do arquivo*/
            SetGambiarraPraCopiarBancoDeDados(NomeDB);
            EsperaCopia.WaitOne(); //O uso de semaforo vai evitar o uso de await nesta chamada
            var caminhoDB = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, NomeDB);
            return new SQLiteConnection(caminhoDB);

        }

    /*    public List<Cachorro> GetCachorros(TipoGenero genero)
        {
            return _database.Table<Cachorro>().Where(c => c.Genero == genero).ToList();
        }*/

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
            EsperaCopia.Release();
        }

    }
}