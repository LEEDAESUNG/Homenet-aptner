using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace WindowsFormsApp1
{
    class MySocket
    {
        static int Port;
        Parking MyPark;
        Form1 _MainForm;

        public MySocket(int port, ref Parking park, Form1 _form1)
        {
            MyPark = park;
            Port = port;
            _MainForm = _form1;

            Thread serverThread = new Thread(ServerFunc);
            serverThread.IsBackground = true;
            serverThread.Start();
            //serverThread.Sleep(500);
        }

        private void ServerFunc(object obj)
        {
            Socket srvSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //IPAddress ipAddress = GetCurrentIPAddress();
            //if(ipAddress == null)
            //{
            //    Console.WriteLine("IPv4용 랜 카드가 없습니다.");
            //    return;
            //}
            IPAddress ipAddress = IPAddress.Parse("0.0.0.0");
            IPEndPoint endPoint = new IPEndPoint(ipAddress, Port);

            srvSocket.Bind(endPoint);
            
            byte[] recvBytes = new byte[1024];
            char[] recvChars = new char[1024];
            EndPoint clientEP = new IPEndPoint(IPAddress.None, 0);

            while(true)
            {
                int nRecv = srvSocket.ReceiveFrom(recvBytes, ref clientEP);
                //string txt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
                string txt = Encoding.Default.GetString(recvBytes); //Byte to String
                string[] words = txt.Split(' ');

                Definition.Dong= words[0];
                Definition.Ho = words[1];
                Definition.CarNo = words[2];

                //Console.WriteLine("recv:" + txt + ", " + nRecv + "byte");
                //Console.WriteLine("동:" + Definition.Dong);
                //Console.WriteLine("호:" + Definition.Ho);
                //Console.WriteLine("차량번호:" + Definition.CarNo);

                _MainForm.HomeLogger("[HostSock] Rcv : " + words[0] + " " + words[1] + " " + words[2]);
                _MainForm.HomeLogger("[APTner] SND : " + words[0] + " " + words[1] + " " + words[2]);
                MyPark.POST(words[0], words[1], words[2]);

            }
        }

        private static IPAddress GetCurrentIPAddress()
        {
            IPAddress[] addrs = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            foreach(IPAddress ipAddr in addrs)
            {
                if(ipAddr.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddr;
                }
            }

            return null;
        }

        public static string EUCKR_TO_UTF8(string strEUCKR)
        {
            return Encoding.UTF8.GetString(
                   Encoding.Convert(
                   Encoding.GetEncoding("euc-kr"),
                   Encoding.UTF8,
                   Encoding.GetEncoding("euc-kr").GetBytes(strEUCKR)));
        }
    }
}
