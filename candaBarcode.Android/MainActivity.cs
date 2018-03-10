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
using Java.Lang;
using Reader;
using Android.Util;

namespace candaBarcode.Droid
{
    [Activity(Label = "candaBarcode", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Reader.ReaderMethod mReader;
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
            if (keyCode.ToString()=="F4")

            {
                try
                {
                    
                    Thread.Sleep(1000);
                    SerialPortFinder serialPortFinder = new SerialPortFinder();
                    var entryValues = serialPortFinder.getAllDevicesPath();
                    string[] entries = serialPortFinder.getAllDevices();
                    SerialPort.SerialPort serialPort = new SerialPort.SerialPort(new File(entryValues[7]), 115200, 0);

                    Thread.Sleep(1000);
                    ControlGPIO.newInstance().writeGPIO(1, 204);
                    byte[] WAKE_UP = { (byte)0x00 };
                    byte[] HOST_MODE_SET = { (byte)0X07, (byte)0XC6, (byte)0X04, (byte)0X00, (byte)0XFF, (byte)0X8A, (byte)0x08, (byte)0xFD, (byte)0x9E };
                    byte[] START_DECODE = { (byte)0X04, (byte)0XE4, (byte)0X04, (byte)0X00, (byte)0XFF, (byte)0X14 };
                    byte[] TRIGGER_CMD_2D = { (byte)0X16, (byte)0X54, (byte)0X0D };
                    byte[] READ_GPIO_VALUE = { (byte)0x60 };
                    SerialPort.SerialPort.sendSerialPort(TRIGGER_CMD_2D);
                    SerialPort.SerialPort.sendSerialPort(WAKE_UP);
                    SerialPort.SerialPort.sendSerialPort(HOST_MODE_SET);
                    SerialPort.SerialPort.sendSerialPort(START_DECODE);
                    ControlGPIO.newInstance().writeGPIO(0, 204);
                    //SerialPort.SerialPort.receiveSerialPort();
                    //serialPort.Close();
                    Toast.MakeText(this.ApplicationContext,"hahahha", ToastLength.Short).Show();
                }
                catch (Java.Lang.Exception ex)
                {
                    Toast.MakeText(this.ApplicationContext, ex.ToString(), ToastLength.Long).Show();
                }


            }

            return base.OnKeyDown(keyCode, e);
        }
        public static byte[] StringArrayToByteArray(string[] strAryHex, int nLen)
        {
            if (strAryHex.Length < nLen)
            {
                nLen = strAryHex.Length;
            }

            byte[] btAryHex = new byte[nLen];

            try
            {
                int nIndex = 0;
                foreach (string strTemp in strAryHex)
                {
                    btAryHex[nIndex] = Convert.ToByte(strTemp, 16);
                    nIndex++;
                }
            }
            catch (System.Exception ex)
            {

            }

            return btAryHex;
        }

    }
}

