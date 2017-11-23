using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TinPet_Projeto.Models;
using System.IO;
using System.Threading.Tasks;

namespace TinPet_Projeto.Droid.Models
{
    public class PicturePickerImplementation : IPicturePicker
    {
        public Task<Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            //            MainActivity activity = MainActivity.Instance;

            // Start the picture-picker activity (resumes in MainActivity.cs)

            APIS.FileHelper_Assets.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Selecione uma Imagem"),
                100);

            // Save the TaskCompletionSource object as a MainActivity property
            APIS.FileHelper_Assets.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            // Return Task object
            return APIS.FileHelper_Assets.PickImageTaskCompletionSource.Task;
        }

    }
}