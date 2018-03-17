﻿using Android.App;
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
using Android.Content;
using candaBarcode.Droid.Action;
using System.Threading.Tasks;

namespace candaBarcode.Droid
{
    [Activity(Label = "candaBarcode.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        
        private Readerbase mReader;
        private System.DateTime? lastBackKeyDownTime;
        private ObservableCollection<model.EmsNum> items = new ObservableCollection<model.EmsNum>();
        private ObservableCollection<model.EmsNum> items2 = new ObservableCollection<model.EmsNum>();
        private ListAdapter listAdapter;
        //Thread thread;
        private SQliteHelper sql;
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
            toolbar.MenuItemClick += (s, e) => //菜单项单击事件  
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
            nMgr = (NotificationManager)GetSystemService(NotificationService);               
            try
            {
                SerialPortFinder serialPortFinder = new SerialPortFinder();
                string[] entryValues = serialPortFinder.getAllDevicesPath();
                string[] entries = serialPortFinder.getAllDevices();
                SerialPort.SerialPort serialPort = new SerialPort.SerialPort(new File(entryValues[7]), 115200, 0);
                ModuleManager.NewInstance().SetUHFStatus(false);
                ModuleManager.NewInstance().SetScanStatus(true);
                mReader = new Readerbase(serialPort.InputStream, serialPort.OutputStream, out items, nMgr,this);
                listAdapter = new ListAdapter(this, items);
                list.Adapter = listAdapter;
                //thread = new Thread(update);
                //thread.Start();
                Button refreshbtn = FindViewById<Button>(Resource.Id.refresh);
                //int index = 0;
                refreshbtn.Click += delegate
                 {
                     //items.Add(new model.EmsNum { EMSNUM = "2589" + index, state = "未同步", index = index });
                     //items2.Add(new model.EmsNum { EMSNUM = "2589" + index, state = "未同步", index = index });
                     //SQliteHelper sql = new SQliteHelper();
                     //sql.insertAsync("2589" + index, "未同步");
                     //index++;Button lsvButton = FindViewById<Button>(Resource.Id.lsvButton);                          
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
                            bool answer = updateToSystem(editText.Text);
                            if (answer)
                            {
                                items.Add(new model.EmsNum() { EMSNUM = editText.Text, state = "已同步" });
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
                    //thread.Interrupt();
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
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }

       /*public void update()
        {
            while (true)
            {
                if (items2.Count > 0)
                {
                    for (int i = items2.Count-1; i >= 0; i--)
                    {
                        int j = items2[i].index;
                        try
                        {
                          bool answer=  updateToSystem(items2[i].EMSNUM);
                            if (answer)
                            {

                                items[j].state = "已同步";
                                //sql = new SQliteHelper();
                                //await sql.updateAsync(items[j].EMSNUM);
                                items2.RemoveAt(i);
                                Thread.Sleep(20);
                            }
                            else
                            {
                                Thread.Sleep(3000);
                                break;
                            }
                        }
                        catch
                        {
                            //    throw ex;
                            Thread.Sleep(3000);
                            break;                            
                        }
                        
                    }
                    
                   
                }
            }
        }*/

        private bool updateToSystem(string str)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    List<object> Parameters = new List<object>();
                    Parameters.Add(str);
                    string result = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.ExecuteService", Parameters);
                    if (result == "err")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
            catch { return false; }
        }
    }
}
