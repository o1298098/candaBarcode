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
using Android.Database;
using candaBarcode.apiHelper;

namespace candaBarcode.Droid
{
    [Activity(Label = "candaBarcode.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        
        private Readerbase mReader;
        System.DateTime? lastBackKeyDownTime;
        ObservableCollection<model.info> items = new ObservableCollection<model.info>();
        ObservableCollection<model.info> items2 = new ObservableCollection<model.info>();
        ListAdapter listAdapter;
        Thread thread;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            ListView list = FindViewById<ListView>(Resource.Id.listView);
            list.StackFromBottom = true;
            list.TranscriptMode = TranscriptMode.AlwaysScroll;
            //list.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);          
            try
            {
                SerialPortFinder serialPortFinder = new SerialPortFinder();
                string[] entryValues = serialPortFinder.getAllDevicesPath();
                string[] entries = serialPortFinder.getAllDevices();
                SerialPort.SerialPort serialPort = new SerialPort.SerialPort(new File(entryValues[7]), 115200, 0);
                ModuleManager.NewInstance().SetUHFStatus(false);
                ModuleManager.NewInstance().SetScanStatus(true);
                mReader = new Readerbase(serialPort.InputStream, serialPort.OutputStream, out items,out items2);              
                listAdapter = new ListAdapter(this, items);
                list.Adapter = listAdapter;
                thread = new Thread(update);
                RunOnUiThread(() =>
                {
                    thread.Start();
                });
            Button refreshbtn = FindViewById<Button>(Resource.Id.refresh);
            refreshbtn.Click += delegate
             {
                 listAdapter.NotifyDataSetChanged();
             };
            Button submitbtn = FindViewById<Button>(Resource.Id.submit);
            EditText editText= FindViewById<EditText>(Resource.Id.editText);
            submitbtn.Click += delegate
            {
                if (string.IsNullOrWhiteSpace(editText.Text))
                    updateToSystem(editText.Text);

            };
            }
            catch (Java.Lang.Exception ex)
            {
                Toast.MakeText(this.ApplicationContext, ex.ToString(), ToastLength.Long).Show();
            }

        }
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.Action == KeyEventActions.Down)
            {
                if (!lastBackKeyDownTime.HasValue || System.DateTime.Now - lastBackKeyDownTime.Value > new System.TimeSpan(0, 0, 2))
                {
                    thread.Interrupt();
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

            if (keyCode.ToString()=="F4")
            {
               
                RunOnUiThread(() =>
                {
                    Thread.Sleep(150);
                    listAdapter.refresh(items);
                });
            }
            return base.OnKeyDown(keyCode, e);
        }

        public void update()
        {
            while (true)
            {
                if (items2.Count > 0)
                {
                    for (int i = 0; i < items2.Count; i++)
                    {
                        int j = items2[i].index;
                        try
                        {
                            updateToSystem(items2[i].EMSNUM);                            
                            items[j].state = "已同步";
                            items2.RemoveAt(i);
                        }
                        catch {
                            continue;
                        }
                        
                    }
                    
                   
                }
            }
        }

        private void updateToSystem(string str)
        {
            List<object> Parameters = new List<object>();
            Parameters.Add(str);
            string result = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.ExecuteService", Parameters);
        }
    }
}

