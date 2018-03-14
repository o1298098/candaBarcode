using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang;
using System.Runtime.InteropServices;


namespace SerialPort
{
   public class SerialPortFinder
    {
        public class Driver
        {
            public string mDriverName { get; set; }
            public string mDeviceRoot { get; set; }
            List<File> mDevices = null;
            public List<File> getDevices()
            {
                if (mDevices == null)
                {
                    mDevices = new List<File>();
                    File dev = new File("/dev");
                    File[] files = dev.ListFiles();
                    int i;
                    for (i = 0; i < files.Length; i++)
                    {
                        if (files[i].AbsolutePath.StartsWith(mDeviceRoot))
                        {
                            Log.Debug(TAG, "Found new device: " + files[i]);
                            mDevices.Add(files[i]);
                        }
                    }
                }
                return mDevices;
            }
            public string getName()
            {
                return mDriverName;
            }
        }
        private static string TAG = "SerialPort";
        private List<Driver> mDrivers = null;
        List<Driver> getDrivers()
        {
		if (mDrivers == null) {
			mDrivers = new List<Driver>();
			LineNumberReader r = new LineNumberReader(new FileReader("/proc/tty/drivers"));
                string l;
                while ((l = r.ReadLine()) != null)
                {
                    // Issue 3:
                    // Since driver name may contain spaces, we do not extract driver name with split()
                    string drivername = l.Substring(0, 0x15).Trim();
                    string[] w = System.Text.RegularExpressions.Regex.Split(l, @"\s{1,}"); 
                    if ((w.Length >= 5) && (w[w.Length - 1].Equals("serial")))
                    {
                        Log.Debug(TAG, "Found new driver " + drivername + " on " + w[w.Length - 4]);
                        mDrivers.Add(new Driver {mDriverName=drivername,mDeviceRoot= w[w.Length - 4] });
                    }
                }
                r.Close();
            }
            return mDrivers;
        }
        public string[] getAllDevices()
        {
            List<string> devices = new List<string>();
            // Parse each driver
            var itdrivs = getDrivers();
            foreach (var itdriv in itdrivs)
            {
                try
                {
                   
                    Driver driver = itdriv;
                    List<File> itdevs = driver.getDevices();
                    foreach (File itdev in itdevs)
                    {
                        string device = itdev.Name;
                        string value = string.Format("%s (%s)", device, driver.getName());
                        devices.Add(value);
                    }
                       
                   
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }
            }
            return devices.ToArray();
        }
        public string[] getAllDevicesPath()
        {
            List<string> devices = new List<string>();
            // Parse each driver
            var itdrivs = getDrivers();
            foreach (var itdriv in itdrivs)
            {
                try
                {

                    Driver driver = itdriv;
                    List<File> itdevs = driver.getDevices();
                    foreach (File itdev in itdevs)
                    {
                        string device = itdev.AbsolutePath;
                        devices.Add(device);
                    }


                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }              
            }
            return devices.ToArray();
        }
    }
}