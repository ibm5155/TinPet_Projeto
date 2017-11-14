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
using Android.Content.Res;

namespace TinPet_Projeto.Droid.Models
{
    public class DataBase_Android : IDataBase
    {

        public SQLiteConnection GetConexaoLocal(string nomeDB)
        {
            TransfereBandoDeDados(nomeDB);
            /*Caminho da pasta + nome do arquivo*/
            var caminhoDB = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),nomeDB);

            return new SQLiteConnection(caminhoDB);
        }

        /*Transfere o banco de dados interno do aplicativo para fora dele para que o SqLite possa usa-lo*/
        private void TransfereBandoDeDados(string NomeDB)
        {
            var dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), NomeDB);

            if (!System.IO.File.Exists(dbPath))
            {
                var dbAssetStream = APIS.FileHelper_Assets.assets.Open(NomeDB);
                var dbFileStream = new System.IO.FileStream(dbPath, System.IO.FileMode.OpenOrCreate);
                var buffer = new byte[1024];

                int b = buffer.Length;
                int length;

                while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                {
                    dbFileStream.Write(buffer, 0, length);
                }

                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
            }
        }
        
    }
}