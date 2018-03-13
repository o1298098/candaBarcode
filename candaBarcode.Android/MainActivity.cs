using System;

using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;
using Com.Scanner2d;
using SerialPort;
using Java.Lang;
using Com.Rodinbell.Module;

namespace candaBarcode.Droid
{
    [Activity(Label = "candaBarcode", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Readerbase mReader;
        DateTime? lastBackKeyDownTime;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            try
            {
                SerialPortFinder serialPortFinder = new SerialPortFinder();
                string[] entryValues = serialPortFinder.getAllDevicesPath();
                string[] entries = serialPortFinder.getAllDevices();
                SerialPort.SerialPort serialPort = new SerialPort.SerialPort(new File(entryValues[7]), 115200, 0);
                ModuleManager.NewInstance().SetUHFStatus(false);
                ModuleManager.NewInstance().SetScanStatus(true);
                //byte[] WAKE_UP = { 0x00 };
                //byte[] HOST_MODE_SET = { 0x07, 0xC6, 0x04, 0x00, 0xFF, 0x8A, 0x08, 0xFD, 0x9E };
                //byte[] START_DECODE = { 0x04, 0xE4, 0X04, 0x00, 0xFF, 0x14 };
                mReader = new Readerbase(serialPort.InputStream,serialPort.OutputStream);   
               
            }
            catch (Java.Lang.Exception ex)
            {
                Toast.MakeText(this.ApplicationContext, ex.ToString(), ToastLength.Long).Show();
            }
            LoadApplication(new App());
           
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.Action == KeyEventActions.Down)
            {
                if (!lastBackKeyDownTime.HasValue || DateTime.Now - lastBackKeyDownTime.Value > new TimeSpan(0, 0, 2))
                {
                    Toast.MakeText(this.ApplicationContext, "再按一次退出程序", ToastLength.Short).Show();
                    lastBackKeyDownTime = DateTime.Now;
                }
                else
                {
                    mReader.signOut();
                    ModuleManager.NewInstance().SetScanStatus(false);
                    ModuleManager.NewInstance().SetUHFStatus(false);
                    Finish();
                }
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }
        //protected override void OnDestroy()
        //{
        //    mReader.signOut();
        //    ModuleManager.NewInstance().SetScanStatus(false);
        //    ModuleManager.NewInstance().SetUHFStatus(false);
        //}


    }
}

