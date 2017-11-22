using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinPet_Projeto.Models
{
    public  class PicturePicker : MvxViewModel
    {
        public IPicturePicker GaleriaDeImagens = Mvx.Resolve<IPicturePicker>();
    }
}
