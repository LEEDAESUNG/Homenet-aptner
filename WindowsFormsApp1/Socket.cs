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

            //서버
            //Console.WriteLine("===> Socket.cs this:" + _MainForm.Handle);
            Thread serverThread = new Thread(ServerFunc);
            serverThread.IsBackground = true;
            serverThread.Start();

            
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


            //클라이언트
            Socket clntSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // udp client
            //serverEP = new IPEndPoint(GetCurrentIPAddress(), 18597);
            IPAddress serverIpAddress = IPAddress.Parse("0.0.0.0");
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 18598);
            //byte[] buff = new byte[1024];

            while(true)
            {
                int nRecv = srvSocket.ReceiveFrom(recvBytes, ref clientEP);
                string txt = Encoding.Default.GetString(recvBytes, 0, nRecv);
                //string txt = Encoding.Default.GetString(recvBytes); 
                txt = txt.Replace("\0", "");
                string[] words = txt.Split('_');


                //Definition.GateNo = words[0];
                //Definition.CarNo = words[1];
                //Definition.InOut= words[2];

                string GateNo = words[0];
                string CarNo = words[1];
                string InOut = words[2];


                _MainForm.HomeLogger("**********************************************************************************");
                _MainForm.HomeLogger("[HostSock] RCV : " + GateNo + " " + CarNo + " " + InOut);
                _MainForm.HomeLogger("[APTner] SND : " + GateNo + " " + CarNo + " " + InOut);
 
                

                //words[0]=words[0].Replace("\0", "");
                if (MyPark.VisitCheck(CarNo) == true)
                {
                    byte[] buff = Encoding.Default.GetBytes(GateNo + "_" + CarNo + "_" + InOut + "_" + Definition.Dong + "_" + Definition.Ho + "_" + "Y");
                    clntSocket.SendTo(buff, serverEP);

                    if (InOut == "IN")
                    {
                        MyPark.CarIn(Definition.Dong, Definition.Ho, CarNo);
                    }
                    else
                    {
                        MyPark.CarOut(CarNo);
                    }
                }
                else
                {
                    //테스트
                    //byte[] buff = Encoding.Default.GetBytes("0" + "_" + "11가1111" + "_" + "IN" + "_" + "101" + "_" + "202" + "_" + "Y");
                    //clntSocket.SendTo(buff, serverEP);
                }
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
    }
}
