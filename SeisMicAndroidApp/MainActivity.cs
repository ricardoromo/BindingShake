using System;
using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Square.Seismic;
using static Square.Seismic.ShakeDetector;

namespace SeisMicAndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Action ShakeAction;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);

            ShakeAction = new Action(ShakeDetection);
            SensorManager sensorManager = (SensorManager)this.ApplicationContext.GetSystemService(Context.SensorService);
            ShakeDetector shakeDetector = new ShakeDetector(new ListenerAction(ShakeAction));
            shakeDetector.Start(sensorManager);
        }

        private void ShakeDetection()
        {
            System.Diagnostics.Debug.WriteLine("Its Working");
        }
    }

    public class ListenerAction : Java.Lang.Object, IListener
    {

        private readonly Action shakeAction;

        public ListenerAction(Action shakeAction)
        {
            this.shakeAction = shakeAction;
        }

        public void HearShake()
        {
            shakeAction?.Invoke();
        }
    }

}

