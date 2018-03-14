using Android.App;
using Android.Widget;
using Android.OS;
using SerialPort;
using Com.Rodinbell.Module;
using Android.Views;
using Java.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace candaBarcode.Droid
{
    [Activity(Label = "candaBarcode.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        
        private Readerbase mReader;
        System.DateTime? lastBackKeyDownTime;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            ListView list = FindViewById<ListView>(Resource.Id.listView);
            ObservableCollection<model.info> items = new ObservableCollection<model.info>();
            //for(int i = 0; i < 100; i++) { items.Add(new model.info { EMSNUM = "20752640558" }); }            
            //list.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
           list.Adapter = new ListAdapter(this, items); 
            try
            {
                SerialPortFinder serialPortFinder = new SerialPortFinder();
                string[] entryValues = serialPortFinder.getAllDevicesPath();
                string[] entries = serialPortFinder.getAllDevices();
                SerialPort.SerialPort serialPort = new SerialPort.SerialPort(new File(entryValues[7]), 115200, 0);
                ModuleManager.NewInstance().SetUHFStatus(false);
                ModuleManager.NewInstance().SetScanStatus(true);
                mReader = new Readerbase(serialPort.InputStream, serialPort.OutputStream, out items);
                //System.Timers.Timer timer = new System.Timers.Timer(10000);
                //timer.Elapsed += delegate
                //{
                //    RunOnUiThread(() => {
                //        list.Adapter = new ListAdapter(this, items);
                //    });
                //};
                //timer.Enabled = true;
                for (int i = 0; i < 100; i++) { items.Add(new model.info { EMSNUM = "20752640558" });Thread.Sleep(10); }

            }
            catch (Java.Lang.Exception ex)
            {
                Toast.MakeText(this.ApplicationContext, ex.ToString(), ToastLength.Long).Show();
            }
            Button btn = FindViewById<Button>(Resource.Id.button1);
            btn.Click += delegate
             {
                 list.Adapter = new ListAdapter(this, items);
             };

            }
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.Action == KeyEventActions.Down)
            {
                if (!lastBackKeyDownTime.HasValue || System.DateTime.Now - lastBackKeyDownTime.Value > new System.TimeSpan(0, 0, 2))
                {
                    Toast.MakeText(this.ApplicationContext, "再按一次退出程序", ToastLength.Short).Show();
                    lastBackKeyDownTime = System.DateTime.Now;
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
       

    }
}

