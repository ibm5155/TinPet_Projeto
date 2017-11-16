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
        #region Pegar dados de Banco de Dados
        public List<Cachorro> GetCachorros(TipoGenero genero)
        {
            return _bandodedados.Table<Cachorro>().Where(c => c.Genero == genero).ToList();
        }

        public void GetFiltroUsuario()
        {
            if (_bandodedados != null)
            {
                Globais.MeuFiltro = _bandodedados.Table<FiltroUsuario>().Where(c => c.IdDono == Globais.MeusDados.IdDono).First();
            }
            //Else não foi inicializado o banco de dados
        }

        public bool PrimeiroAcesso()
        {
            bool retorno = false;
            if (_bandodedados != null)
            {
                DadosPessoais MeusDados2 = null;
                 MeusDados2 = _bandodedados.Table<DadosPessoais>().FirstOrDefault();
                retorno = MeusDados2 == null;
            }
            return retorno;
        }

        #endregion
        #region Atualizar Banco de Dados
        public void UpdateFiltrousuario()
        {
            //Não precisa checar se não existe o campo filtro pois ele deverá existir de qualquer antes dessa chamada...
            var existingUser = _bandodedados.Query<FiltroUsuario>("select * from FiltroUsuario where Id = ?", Globais.MeuFiltro.IdDono).FirstOrDefault();
            if (existingUser != null)
            {
                //pegamos o dado do banco de dados
                //hora de atualizar ele
                existingUser.CachorroId = Globais.MeuFiltro.CachorroId;
                existingUser.CachorroGenero = Globais.MeuFiltro.CachorroGenero;
                existingUser.CachorroAnoNascimento = Globais.MeuFiltro.CachorroAnoNascimento;
                existingUser.CachorroLatitude = Globais.MeuFiltro.CachorroLatitude;
                existingUser.CachorroLatitude = Globais.MeuFiltro.CachorroLongitude;


                _bandodedados.RunInTransaction(() =>
                {
                    _bandodedados.Update(existingUser);
                });
            }
        }

        public void UpdateDadosPessoais()
        {
            //Não precisa checar se não existe o campo filtro pois ele deverá existir antes dessa chamada...
            var existingUser = _bandodedados.Query<DadosPessoais>("select * from FiltroUsuario where Id = ?", Globais.MeusDados.IdDono).FirstOrDefault();
            if (existingUser != null)
            {
                //pegamos o dado do banco de dados
                //hora de atualizar ele
                existingUser.CachorroId = Globais.MeuFiltro.CachorroId;
                existingUser.CachorroGenero = Globais.MeuFiltro.CachorroGenero;
                existingUser.CachorroAnoNascimento = Globais.MeuFiltro.CachorroAnoNascimento;
                existingUser.CachorroLatitude = Globais.MeuFiltro.CachorroLatitude;
                existingUser.CachorroLatitude = Globais.MeuFiltro.CachorroLongitude;

                _bandodedados.RunInTransaction(() =>
                {
                    _bandodedados.Update(existingUser);
                });
            }
        }

        #endregion
        #region Criar Tabelas Banco de Dados
        public void CreateTable_DadosPessoais()
        {
            _bandodedados.CreateTable<DadosPessoais>();
        }

        public void CreateTable_FiltroUsuario()
        {
            _bandodedados.CreateTable<FiltroUsuario>();
        }
        #endregion
        #region Insert into Banco de Dados
        public void InsertInto_DadosPessoais()
        {
            _bandodedados.Insert(Globais.MeusDados);
        }
        public void InsertInto_FiltroUsuario()
        {
            _bandodedados.Insert(Globais.MeuFiltro);
        }

        #endregion
    }
}
