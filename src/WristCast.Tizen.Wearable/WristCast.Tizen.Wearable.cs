using System;
using MvvmCross.Forms.Platforms.Tizen.Views;
using MvvmCross.Platforms.Tizen.Core;

namespace WristCast.Tizen.Wearable
{
    class Program : MvxProgram
    {
        static void Main(string[] args)
        {
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(Application);
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            Application.Run(args);
        }
    }
}
