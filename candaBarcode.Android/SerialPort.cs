#define MyAlter
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

namespace SerialPort
{

    public class SerialPort:Java.Lang.Object
    {
        private  const string TAG = "SerialPort";
        public static Thread receiveThread = null;

        public static System.Boolean flag = false;

        public static string serialData;       
        private FileDescriptor mFd;
        public static FileInputStream mFileInputStream;
        public static FileOutputStream mFileOutputStream;
        private int serialPortHandle;
        public SerialPort():base()
        {

        }


        public SerialPort(File device, int baudrate, int flags):base()
        {            
            /* Check access permission */
            if (!device.CanRead() || !device.CanWrite())
            {
                try
                {
                    /* Missing read/write permission, trying to chmod the file */
                    Java.Lang.Process su;
                    su = Runtime.GetRuntime().Exec("/system/bin/su");
                    string cmd = "chmod 666 " + device.AbsolutePath + "\n"
                            + "exit\n";
                    byte[] cmdbytes = System.Text.Encoding.ASCII.GetBytes(cmd);
                    su.OutputStream.Write(cmdbytes,0,cmdbytes.Length);
                    if ((su.WaitFor() != 0) || !device.CanRead()
                            || !device.CanWrite())
                    {
                        throw new SecurityException();
                    }
                }
                catch (Java.Lang.Exception e)
                {
                    e.PrintStackTrace();
                    throw new SecurityException();
                }
            }
            IntPtr IntPtrClass= JNIEnv.FindClass(this.GetType());
#if MyAlter
            mFd = GetFileDescriptor(device.AbsolutePath, baudrate, flags, out serialPortHandle);
#else
            mFd = open(Java.Interop.JniEnvironment.EnvironmentPointer, IntPtrClass, device.AbsolutePath, baudrate, flags);
#endif
            if (mFd == null)
            {
                Log.Error(TAG, "native open returns null");
                throw new IOException();
            }
            mFileInputStream = new FileInputStream(mFd);
            mFileOutputStream = new FileOutputStream(mFd);
            flag = true;

        }

        // Getters and setters
        public InputStream InputStream
        {
            get
            {
                return mFileInputStream;
            }
        }

        public OutputStream OutputStream
        {
            get
            {
                return mFileOutputStream;
            }
        }

        [DllImport("serial_port", EntryPoint = "openSerialPort")]
        private static extern int open(System.Text.StringBuilder path, int baudrate, int flags);

        [DllImport("serial_port", EntryPoint = "closeSerialPort")]
        private static extern void close(int handle);

        private FileDescriptor GetFileDescriptor(string path, int baudrate, int flags, out int Handle)
        {
            int sHandle = open(new System.Text.StringBuilder(path), baudrate, flags);
            Handle = sHandle;
            IntPtr fp = JNIEnv.FindClass(typeof(FileDescriptor));
            IntPtr fpm = JNIEnv.GetMethodID(fp, "<init>", "()V");
            IntPtr fpObject = JNIEnv.NewObject(fp, fpm);
            IntPtr filed = JNIEnv.GetFieldID(fp, "descriptor", "I");
            JNIEnv.SetField(fpObject, filed, sHandle);

            FileDescriptor res = new Java.Lang.Object(fpObject, JniHandleOwnership.TransferGlobalRef).JavaCast<FileDescriptor>();

            return res;
        }

        public void Close()
        {
#if MyAlter
            close(serialPortHandle);
#else
             close(IntPtr.Zero, JNIEnv.ToJniHandle(this));
#endif
        }
        public static void sendSerialPort(byte[] data)
        {
            lock (mFileOutputStream)
            {
                Log.Info("test", "发送串口数据");
                try
                {
                    mFileOutputStream.Write(data, 0,data.Length);
                    mFileOutputStream.Flush();
                    Log.Info("test", "串口数据发送成功");
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                    Log.Info("test", "串口数据发送失败");
                }
            }
           
        }

        /**
         * 接收串口数据的方法
         */
        public static void receiveSerialPort()
        {
            Log.Info("test", "接收串口数据");
            if (receiveThread != null)
                return;

            /*创建子线程接收串口数据
             */
            receiveThread = new Thread(newsd);
        //启动接收线程
        receiveThread.Start();
    }

        public static void newsd()
        {
            while (flag)
            {
                try
                {
                    byte[] readData = new byte[1024];
                    if (mFileInputStream == null)
                    {
                        return;
                    }
                    int size = mFileInputStream.Read(readData);
                    string Data = Encoding.Default.GetString(readData);
                    if (size > 0 && flag)
                    {
                        Log.Info("test", "接收到串口数据:" + Data);
                        Thread.Sleep(1000);
                    }
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }
                catch (InterruptedException e)
                {
                    e.PrintStackTrace();
                }
            }
        }
}
}