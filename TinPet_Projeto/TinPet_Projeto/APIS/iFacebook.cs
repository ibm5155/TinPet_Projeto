using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinPet_Projeto.APIS
{
    public interface IFacebook
    {
        Task Login();
        string GetId();
        string GetNome();
    }
}
