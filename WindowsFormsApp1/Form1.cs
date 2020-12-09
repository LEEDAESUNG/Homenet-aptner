using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        CommonUtil MyUtil = new CommonUtil();
        MySocket MySock;// = new MySocket();
        Parking MyPark;// = new Parking();

        //string post_data = "kaptCode=A10026547&carNo=25구5401&dong=200&ho=200";
        //string AuthToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJORVdLT1JFQSIsInJvbGVzIjoiUEMiLCJpc3MiOiJBUFRORVIiLCJleHAiOjMxNjQ2NDc4ODYsImlkeCI6IiIsImlhdCI6MTU4Njg0MzQ4Nn0.xzwYK7pKMemnmqQqdX9mVqsuxs4tUywmAMq88uxKf2A";
        //string url = "https://gtw.aptner.com/pc/access/in";

        //string kaptCode;

        


        public void HomeLogger(string log)
        {
            string sLog;
            
            sLog = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + log;

            listBox1.Items.Insert(0, sLog);

            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            MyUtil.FileLogger(log);
        }
        
        public Form1()
        {
            InitializeComponent();

            


            lbl_DateTime.Text = "";
            HomeLogger("[HomeNet Program ] APTner HomeNet Start..!!");

            MyPark = new Parking(this);

            MySock = new MySocket(MyPark.GetHostPort(), ref MyPark, this);

            //MyUtil = new CommonUtil();


            //string HomeNetMode = MyUtil.Get_Ini("System Config", "HomeNetMode");
            //int HostPort = Int32.Parse(MyUtil.Get_Ini("System Config", "HostPort"));

            //MySock = new MySocket(HostPort);

            //if (HomeNetMode == "14") //아파트너
            //    kaptCode = MyUtil.Get_Ini("APTNER", "KAPTCODE"); //아파트 고유코드

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //HttpWebRequest request = (HttpWebRequest)
            //WebRequest.Create(url);
            //request.KeepAlive = false;
            //request.ProtocolVersion = HttpVersion.Version10;
            //request.Method = "POST";
            //request.Headers.Add("Authorization", AuthToken);
            //byte[] postBytes = Encoding.ASCII.GetBytes(post_data);
            
            //request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            //request.ContentLength = postBytes.Length;
            //Stream requestStream = request.GetRequestStream();

            //requestStream.Write(postBytes, 0, postBytes.Length);
            //requestStream.Close();

            //// grab te response and print it out to the console along with the status code
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
            //Console.WriteLine(response.StatusCode);

            //POST(url, post_data,AuthToken);
            
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            HomeLogger("[APTner] SND : " + txt_Dong.Text + " " + txt_Ho.Text + " " + "12가1234");
            MyPark.POST(txt_Dong.Text, txt_Ho.Text, "12가1234");
        }

        //private void btn_Exit_Click(object sender, EventArgs e)
        //{
        //this.Close();
        //1.Application.Exit();

        //2.Application.ExitThread();
        //  Environment.Exit(0);

        //3,System.Diagnostics.Process[] mProcess = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);
        //foreach (System.Diagnostics.Process p in mProcess)
        //    p.Kill();
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_DateTime.Text = DateTime.Now.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.BackColor = System.Drawing.Color.MistyRose;
            //this.StartPosition = FormStartPosition.CenterScreen;
        }



        //void POST(string url, string post_data, string authToken)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.Method = "POST";
        //    request.Headers.Add("Authorization", authToken);

        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        //    Byte[] byteArray = encoding.GetBytes(post_data);
        //    request.ContentLength = byteArray.Length;
        //    request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
        //    using (Stream dataStream = request.GetRequestStream())
        //    {
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //    }
        //    long length = 0;
        //    try
        //    {
        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {

        //            length = response.ContentLength;
        //            Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
        //            Console.WriteLine(response.StatusCode);
        //        }
        //    }
        //    catch (WebException ex)
        //    {

        //        //MessageBox.Show(ex.Message);

        //        if (ex.Message.Contains("400"))
        //            Console.WriteLine("Bad Request");
        //        else if (ex.Message.Contains("401"))
        //            Console.WriteLine("Unauthorized");
        //        else if (ex.Message.Contains("403"))
        //            Console.WriteLine("Forbidden");
        //        else if (ex.Message.Contains("404"))
        //            Console.WriteLine("Resource Not Found");
        //        else if (ex.Message.Contains("405"))
        //            Console.WriteLine("Method Not Allowed");
        //        else if (ex.Message.Contains("416"))
        //            Console.WriteLine("Invalid Parameter");
        //        else if (ex.Message.Contains("500"))
        //            Console.WriteLine("Internal Server Error");
        //        else
        //            Console.WriteLine("UnKnow Error : " + ex.Message);
        //    }
        //}
    }
}
