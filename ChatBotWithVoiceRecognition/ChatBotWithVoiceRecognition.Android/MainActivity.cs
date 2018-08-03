using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using ChatBotWithVoiceRecognition.Droid.CustomRenderers;
using Prism;
using Prism.Ioc;
using System;

namespace ChatBotWithVoiceRecognition.Droid
{
    [Activity(Label = "ChatBotWithVoiceRecognition", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public event EventHandler<ActivityResultEventArgs> ActivityResult = delegate { };

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            ActivityResult(this, new ActivityResultEventArgs
            {
                RequestCode = requestCode,
                ResultCode = resultCode,
                Data = data
            });
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
            }
        }
    }
}

