using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Content;
using Android.Views;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.button1);

            button.Click += delegate
            {
                button.Text = string.Format($"{count++} clicks!");
            };
        }
    }
}

