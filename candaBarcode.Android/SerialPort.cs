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

	    /*
	     * Do not remove or rename the field mFd: it is used by native method close();
	     */
	    private FileDescriptor mFd;
        private FileInputStream mFileInputStream;
        private FileOutputStream mFileOutputStream;
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

        [DllImport("serial_port", EntryPoint = "JNICALL Java_com_scan_scan_serialport_SerialPort_open")]
        private static extern int openSerialPort(System.Text.StringBuilder path, int baudrate, int flags);

        [DllImport("serial_port", EntryPoint = "Java_com_scan_scan_serialport_SerialPort_close")]
        private static extern void closeSerialPort(int handle);

        private FileDescriptor GetFileDescriptor(string path, int baudrate, int flags, out int Handle)
        {
            int sHandle = openSerialPort(new System.Text.StringBuilder(path), baudrate, flags);
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
            closeSerialPort(serialPortHandle);
#else
             close(IntPtr.Zero, JNIEnv.ToJniHandle(this));
#endif
        }
    }
}