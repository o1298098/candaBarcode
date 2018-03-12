using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Scanner2d.Config;
using Java.IO;
using Java.Lang;
using SerialPort;

namespace candaBarcode.Droid
{
    public abstract class Readerbase
    {
        private Thread mWaitThread = null;
        private InputStream mInStream = null;
        private OutputStream mOutStream = null;
        private byte[] m_btAryBuffer = new byte[4096];
        private int m_nLength = 0;
        private bool mShouldRunning = true;
        public abstract void onLostConnect();
        public void ReaderBase(InputStream instream, OutputStream outstream)
        {
            this.mInStream = instream;
            this.mOutStream = outstream;
            StartWait();
        }
        public bool IsAlive()
        {
            return mWaitThread != null && mWaitThread.IsAlive;
        }

        public void StartWait()
        {
            mWaitThread = new Thread(getdata);            
            mWaitThread.Start();
        }
        public void getdata()
        {
            byte[] dataArray = new byte[1024];
            //add by lei.li 2016/11/17
            byte[] btAryBuffer = new byte[4096];
            int toall = 0;
            StringBuilder stringBuilder = new StringBuilder();
            while (mShouldRunning)
            {
                try
                {
                    int nLenRead = mInStream.Read(btAryBuffer);
                    if (nLenRead > 0)
                    {
                        byte[] btAryReceiveData = new byte[nLenRead];
                        Array.Copy(btAryBuffer, 0, btAryReceiveData, 0,
                                nLenRead);
                        //long wastTime = System.currentTimeMillis();
                        runNew2DCodeCallBack(btAryReceiveData);
                    }
                }
                catch (IOException e)
                {
                    onLostConnect();
                    return;
                }
                catch (Java.Lang.Exception e)
                {
                    onLostConnect();
                    return;
                }

            }
        }
        public  void signOut()
        {
            mWaitThread.Stop();
            try
            {
                mInStream.Close();
                mOutStream.Close();
            }
            catch (IOException e)
            {
                // TODO Auto-generated catch block
                e.PrintStackTrace();
            }
        }
        private void runNew2DCodeCallBack(byte[] btAryReceiveData)
        {
            try
            {
                int nCount = btAryReceiveData.Length;
                byte[] btAryBuffer = new byte[nCount + m_nLength];
                Array.Copy(m_btAryBuffer, 0, btAryBuffer, 0, m_nLength);
                Array.Copy(btAryReceiveData, 0, btAryBuffer, m_nLength,
                        btAryReceiveData.Length);
                Log.Debug("guolai", Com.Util.StringTool.ByteArrayToString(btAryBuffer, 0, btAryBuffer.Length));
                int nIndex = 0; //When there is the data A0, record the end point of data.
                int start = 0;
                int cmd_end = 0;
                int end = 0;
                for (int nLoop = 0; nLoop < btAryBuffer.Length; nLoop++)
                {
                    //if (btAryBuffer[nLoop] == HEAD.HEAD)
                    if (btAryBuffer[nLoop] == (byte)'^')
                    {
                        start = nLoop + 1;
                        for (int i = nLoop; i < btAryBuffer.Length; i++)
                        {
                            if (btAryBuffer[i] == (byte)'$')
                            {
                                end = i;
                                //recive2DCodeData(new String(Com.Util.StringTool.SubBytes(btAryBuffer, start, end), Com.Util.StringTool.charsetName(Com.Util.StringTool.SubBytes(btAryBuffer, start, end))));
                                //calculate the scan speed;
                                //CalculateSpeed.mTotalTime += System.currentTimeMillis() - CalculateSpeed.mStartTime;
                                nIndex = i + 1;
                            }
                        }
                    }
                    else
                    {
                        if (btAryBuffer[nLoop] == Command.Nck)
                        {
                            byte[] cmd = new byte[nLoop + 1];
                            Array.Copy(btAryBuffer, 0, cmd, 0, cmd.Length);
                            nIndex = nLoop + 1;
                            Log.Debug("NCK is here", Com.Util.StringTool.ByteArrayToString(cmd, 0, cmd.Length));
                        }
                        if (btAryBuffer[nLoop] == Command.Ack)
                        {
                            if ((nLoop + 1) < btAryBuffer.Length)
                            {
                                if (btAryBuffer[nLoop + 1] == Command.QuerySuccessed)
                                {
                                    byte[] cmd = new byte[nLoop + 2];
                                    Array.Copy(btAryBuffer, 0, cmd, 0, cmd.Length);
                                    nIndex = nLoop + 2;//this has 2E suffix
                                    analyData(new Com.Scanner2d.Bean.MessageReceiving(cmd));
                                }
                                if (btAryBuffer[nLoop + 1] == Command.CmdSuffix)
                                {
                                    byte[] cmd = new byte[nLoop + 2];
                                    Array.Copy(btAryBuffer, 0, cmd, 0, cmd.Length);
                                    nIndex = nLoop + 2;//this has 2E suffix
                                    analyData(new Com.Scanner2d.Bean.MessageReceiving(cmd));
                                }
                            }
                        }
                    }
                }

                if (nIndex <= btAryBuffer.Length)
                {
                    m_nLength = btAryBuffer.Length - nIndex;
                    Array.Clear(m_btAryBuffer, 0, 4096);
                    Array.Copy(btAryBuffer, nIndex, m_btAryBuffer, 0,
                            btAryBuffer.Length - nIndex);
                    Log.Debug("nIndex + m_nLength", m_nLength + ":::" + nIndex);
                }
            }
            catch (Java.Lang.Exception e)
            {

            }
        }

    [Deprecated]
    public  void reciveData(byte[] btAryReceiveData) { }

        /**
         * reciveBarCodeData
         */
        [Deprecated]
        public void reciveBarCodeData(string str) { }
        [Deprecated]
        public void recive2DCodeData(string str) { }
        [Deprecated]
        public abstract void analyData(Com.Scanner2d.Bean.MessageReceiving messageReceiving);
    }
   
}