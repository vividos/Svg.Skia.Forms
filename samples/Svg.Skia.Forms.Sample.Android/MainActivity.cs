using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Xamarin.Essentials;

namespace Svg.Skia.Forms.Sample.Droid
{
    /// <summary>
    /// Android main activity that hosts the Xamarin.Forms app
    /// </summary>
    [Activity(
        Label = "Svg.Skia.Forms Samples for Android",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        /// Called when the activity is about to be created
        /// </summary>
        /// <param name="savedInstanceState">saved instance state</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            this.LoadApplication(new App());
        }

        /// <summary>
        /// Called by Android when permissions request has a result
        /// </summary>
        /// <param name="requestCode">request code</param>
        /// <param name="permissions">list of permissions</param>
        /// <param name="grantResults">list of grant results</param>
        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
