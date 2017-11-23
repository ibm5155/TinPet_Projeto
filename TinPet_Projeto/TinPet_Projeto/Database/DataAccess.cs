using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TinPet_Projeto.Models;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using TinPet_Projeto.TypeConv;
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
            int AnoHoje = DateTime.Now.Year;
            int FiltroIdadeMinima = DateTime.Now.Year + Globais.MeuFiltro.IdadeMinima;
            int FiltroIdadeMaxima = DateTime.Now.Year + Globais.MeuFiltro.IdadeMaxima;
            List<Cachorro> LC = _bandodedados.Table<Cachorro>().ToList();
            for(int i=0; i < LC.Count; i++)
            {
                if(LC[i].Genero != Globais.MeuFiltro.CachorroGenero)
                {
                    LC.RemoveAt(i);
                    i--;
                }
                else if(AnoHoje - LC[i].AnoNascimento  < Globais.MeuFiltro.IdadeMinima)
                {
                    LC.RemoveAt(i);
                    i--;
                }
                else if (AnoHoje - LC[i].AnoNascimento > Globais.MeuFiltro.IdadeMaxima)
                {
                    LC.RemoveAt(i);
                    i--;
                }
                else if(DistanceAlgorithm.DistanceBetweenPlaces(LC[i].Regiao_Longitude, LC[i].Regiao_Longitude, Globais.MeusDados.CachorroLongitude, Globais.MeusDados.CachorroLatitude)/1000 > Globais.MeuFiltro.DistanciaMaxima)
                {
                    double x = DistanceAlgorithm.DistanceBetweenPlaces(LC[i].Regiao_Longitude, LC[i].Regiao_Longitude, Globais.MeusDados.CachorroLongitude, Globais.MeusDados.CachorroLatitude);
                    LC.RemoveAt(i);
                    i--;

                }

            }
            return LC;
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
