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

using TinPet_Projeto;

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
            return _bandodedados.Table<Cachorro>().Where(c => c.Genero == Globais.MeuFiltro.CachorroGenero
                                                            && c.AnoNascimento >= Globais.MeuFiltro.IdadeMinima
                                                            && c.AnoNascimento <= Globais.MeuFiltro.IdadeMaxima
                                                            && Models.DistanceAlgorithm.DistanceBetweenPlaces(c.Regiao_Longitude, c.Regiao_Longitude, Globais.MeusDados.CachorroLongitude, Globais.MeusDados.CachorroLatitude) <= Globais.MeuFiltro.DistanciaMaxima ).ToList();
        }

        public void GetFiltroUsuario()
        {
            if (_bandodedados != null)
            {
                Globais.MeuFiltro = _bandodedados.Table<FiltroUsuario>().Where(c => c.IdDono == Globais.MeusDados.IdDono).FirstOrDefault();
            }
        }

        public bool PrimeiroAcesso(string IdFacebook)
        {
            bool retorno = true;
            if (_bandodedados != null)
            {
                DadosPessoais MeusDados2 = null;
                MeusDados2 = _bandodedados.Table<DadosPessoais>().Where(c => c.IdDono == IdFacebook).FirstOrDefault();
                Globais.MeusDados = MeusDados2;
                retorno = MeusDados2 == null;

            }
            return retorno;
        }

        #endregion
        #region Atualizar Banco de Dados
        public void UpdateFiltrousuario()
        {
            //Não precisa checar se não existe o campo filtro pois ele deverá existir de qualquer antes dessa chamada...
            var existingUser = _bandodedados.Query<FiltroUsuario>("select * from FiltroUsuario where IdDono = ?", Globais.MeuFiltro.IdDono).FirstOrDefault();
            if (existingUser != null)
            {
                //pegamos o dado do banco de dados
                //hora de atualizar ele
                existingUser.CachorroGenero = Globais.MeuFiltro.CachorroGenero;
                existingUser.DistanciaMaxima= Globais.MeuFiltro.DistanciaMaxima;
                existingUser.IdadeMaxima = Globais.MeuFiltro.IdadeMaxima;
                existingUser.IdadeMinima = Globais.MeuFiltro.IdadeMinima;

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
                existingUser.CachorroId = Globais.MeusDados.CachorroId ;
                existingUser.CachorroAnoNascimento = Globais.MeusDados.CachorroAnoNascimento;
                existingUser.CachorroGenero = Globais.MeusDados.CachorroGenero;
                existingUser.CachorroLatitude = Globais.MeusDados.CachorroLatitude;
                existingUser.CachorroLongitude = Globais.MeusDados.CachorroLongitude;
                existingUser.CachorroNome = Globais.MeusDados.CachorroNome;
                existingUser.CachorroRaca= Globais.MeusDados.CachorroRaca;
                existingUser.Foto = Globais.MeusDados.Foto;

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
