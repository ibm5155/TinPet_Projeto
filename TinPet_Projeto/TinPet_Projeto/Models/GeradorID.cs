using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinPet_Projeto.Models
{
    public class GeradorID
    {
        private  static int ID_Atual  = 0;/*somente funções da classe GeradorID podem usar a variavel*/
        public static int GetNovaID()
        {
            /*Bate um papo talvez com algum servidor e pega uma ID nova, mas por enquanto isso ta valendo.*/
            return ID_Atual++;
        }
    }
}
