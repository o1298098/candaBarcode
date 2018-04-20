using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Java.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Android.Database;
using candaBarcode.apiHelper;
using Android.Content;
using candaBarcode.Droid.Action;
using System.Threading.Tasks;
using Com.Nativec.Tools;
using System;
using System.Linq;

namespace candaBarcode.Droid
{
    [Activity(Label = "candaBarcode.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        
        private Readerbase mReader;
        System.DateTime? lastBackKeyDownTime;
        static ObservableCollection<model.EmsNum>  items ;
        static ObservableCollection<model.EmsNum> items2;
        SqliteDataAccess dataAccess;
        ListAdapter listAdapter;
        Thread thread;
         private NotificationManager nMgr;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            ListView list = FindViewById<ListView>(Resource.Id.listView);
            list.StackFromBottom = true;
            list.TranscriptMode = TranscriptMode.AlwaysScroll;
            ActionBar.Hide();
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.InflateMenu(Resource.Menu.menu);
            toolbar.MenuItemClick += (s, e) => 
            {
                switch (e.Item.ItemId)
                {
                    case Resource.Id.menu_history:
                        Intent intent = new Intent(this, typeof(SearchActivity));
                        StartActivity(intent);
                        break;
                    case Resource.Id.menu_about:
                        Toast.MakeText(this, "关于都看，好人才", ToastLength.Short).Show();
                        break;
                }
            };
            dataAccess = new SqliteDataAccess();
            items = dataAccess.SelectAll();
            nMgr = (NotificationManager)GetSystemService(NotificationService);     
            try
            {
                //SerialPortFinder serialPortFinder = new SerialPortFinder();
                //string[] entryValues = serialPortFinder.GetAllDevicesPath();
                //string[] entries = serialPortFinder.GetAllDevices();
                //Com.Nativec.Tools.SerialPort serialPort = new Com.Nativec.Tools.SerialPort(new File(entryValues[7]), 115200, 0);
                //ModuleManager.NewInstance().SetUHFStatus(false);
                //ModuleManager.NewInstance().SetScanStatus(true);
                //mReader = new Readerbase(serialPort.InputStream, serialPort.OutputStream, items, items2, nMgr,this);
                listAdapter = new ListAdapter(this, items);
                list.Adapter = listAdapter;
                thread = new Thread(update);
                thread.Start();
                Button refreshbtn = FindViewById<Button>(Resource.Id.refresh);
                //int index = 0;
                refreshbtn.Click += delegate
                 {
                     //dataAccess.SaveOption(new model.EmsNum { datetime=DateTime.Now.ToLongDateString(),EMSNUM= "20752640558", state="未同步" });
                     //items.Add(new model.EmsNum { EMSNUM = "20752640558" + index, state = "未同步", index = index });
                     //items2.Add(new model.EmsNum { EMSNUM = "20752640558" + index, state = "未同步", index = index });
                     //SQliteHelper sql = new SQliteHelper();
                     //sql.insertAsync("2589" + index, "未同步");
                     //index++;
                     //Button lsvButton = FindViewById<Button>(Resource.Id.lsvButton);                          
                     listAdapter.NotifyDataSetChanged();
                 };
                Button submitbtn = FindViewById<Button>(Resource.Id.submit);
                EditText editText = FindViewById<EditText>(Resource.Id.editText);
                submitbtn.Click += delegate
                {
                    if (!string.IsNullOrWhiteSpace(editText.Text))
                    {
                        RunOnUiThread(() =>
                        {
                            string answer = updateToSystem(editText.Text);
                            if (answer!="err")
                            {
                                items.Add(new model.EmsNum() { EMSNUM = editText.Text, state = answer });
                                listAdapter.NotifyDataSetChanged();
                                editText.Text = "";
                                Toast.MakeText(this.ApplicationContext, "提交成功", ToastLength.Long).Show();
                            }
                            else
                            {
                                Toast.MakeText(this.ApplicationContext, "网络异常，请稍后重试", ToastLength.Long).Show();
                            }
                        });
                    }
                    else
                    {
                        Toast.MakeText(this.ApplicationContext, "请输入单号", ToastLength.Long).Show();
                    }
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
                    Toast.MakeText(this.ApplicationContext, "再按一次退出程序", ToastLength.Short).Show();
                    lastBackKeyDownTime = System.DateTime.Now;
                    
                }
                else
                {
                    thread.Interrupt();
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
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }

        public void update()
        {
            while (true)
            {
                int count = items.Select(x => x.state != "已同步").Count();
                if (count > 0)
                {
                    for (int i=0;i<items.Count;i++)
                    {
                        try
                        {
                            string answer = updateToSystem(items[i].EMSNUM);
                            if (answer!="err")
                            {
                                items[i].state = answer;
                                dataAccess.delete(items[i].EMSNUM);                                
                            }
                            else
                            {
                                Thread.Sleep(3000);
                                break;
                            }
                        }
                        catch (System.Exception ex)
                        {
                            ////throw ex;
                            break;
                        }

                    }
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }

        private string updateToSystem(string str)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    List<object> Parameters = new List<object>();
                    Parameters.Add(str);
                    string result = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.ExecuteService2", Parameters);
                    if (result == "0")
                    {
                        return "无记录";
                    }
                    else if (result == "1")
                    {
                        return "已同步";
                    }
                    else if (result == "2")
                    {
                        return "重复";
                    }
                    else if(result == "err")
                    {
                        return "err";
                    }
                }
                return "err";

            }
            catch { return "err"; }
        }
    }
}

