using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinPet_Projeto.Models
{
    public class DadosPessoais
    {
        public string IdDono;
        public int CachorroId;
        public TipoGenero CachorroGenero;
        public double CachorroLatitude;
        public double CachorroLongitude;
        public byte[] Foto;//cachorro
        public int CachorroAnoNascimento;
    }
}
