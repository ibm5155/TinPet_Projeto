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
using Android.Content.Res;
using System.Threading.Tasks;
using System.IO;

namespace TinPet_Projeto.Droid.APIS
{
    public class FileHelper_Assets
    {
        public static AssetManager assets;
        public static Activity Instance { get; set; }
        public static TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

    }
}