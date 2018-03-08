using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing.Mobile;
using SerialPort;
using Java.IO;

namespace candaBarcode.Droid
{
    [Activity(Label = "candaBarcode", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {        
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;


            base.OnCreate(bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
           
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            try
            {
                SerialPortFinder serialPortFinder = new SerialPortFinder();
                var entryValues = serialPortFinder.getAllDevicesPath();
                String[] entries = serialPortFinder.getAllDevices();
                SerialPort.SerialPort serialPort = new SerialPort.SerialPort(new File(entryValues[7]), 9600,0);
                var a=serialPort.InputStream;
                Toast.MakeText(this.ApplicationContext, "hahahha", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.ApplicationContext, ex.ToString(), ToastLength.Long).Show();
            }
           
                  
            return base.OnKeyDown(keyCode, e);
        }

    }
}

