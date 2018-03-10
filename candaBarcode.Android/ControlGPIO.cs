using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang;
using Android.Util;
using System.Runtime.InteropServices;

namespace candaBarcode.Droid
{
   public class ControlGPIO : Java.Lang.Object
    {

        private static ControlGPIO mControlGPIO = new ControlGPIO();


        public static ControlGPIO newInstance()
        {
            return mControlGPIO;
        }
        [DllImport("ControlGPIO", EntryPoint = "Java_com_reader_helper_ControlGPIO_JNIreadGPIO")]
        public static extern int JNIreadGPIO(int devNo);
        [DllImport("ControlGPIO", EntryPoint = "Java_com_reader_helper_ControlGPIO_JNIwriteGPIO")]
        public static extern int JNIwriteGPIO(int value, int devNo);

        public  int writeGPIO(int value, int devNo)
        {
           return JNIwriteGPIO(value,devNo);
        }
    }
       
}