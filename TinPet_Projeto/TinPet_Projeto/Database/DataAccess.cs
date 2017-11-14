using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TinPet_Projeto.Models;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Core.ViewModels;

namespace TinPet_Projeto.Database
{
    public class DataAccess : MvxViewModel
    {
        private SQLiteConnection _bandodedados;

        public IDataBase _iDB = Mvx.Resolve<IDataBase>();

        public DataAccess()
        {
            _bandodedados = _iDB.GetConexaoLocal("Cachorros.db");
        }

        public DataAccess(string NomeBandoDeDados)
        {
            _bandodedados = _iDB.GetConexaoLocal(NomeBandoDeDados);
        }

        public List<Cachorro> GetCachorros(TipoGenero genero)
        {
            return _bandodedados.Table<Cachorro>().Where(c => c.Genero == genero).ToList();
        }
    }
}
