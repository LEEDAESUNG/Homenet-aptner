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
        private delegate void SafeCallDelegate(string text);

        CommonUtil MyUtil = new CommonUtil();
        MySocket MySock;
        Parking MyPark;
        

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
        }

        public Form1()
        {
            InitializeComponent();


            lbl_DateTime.Text = "";
            HomeLogger("[APTner Agent] APTner Agent Start..!!");

            MyPark = new Parking(this);

            MySock = new MySocket(MyPark.GetHostPort(), ref MyPark, this);
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            HomeLogger("[APTner] SND : " + txt_Dong.Text + " " + txt_Ho.Text + " " + "12가1234");
            MyPark.CarIn(txt_Dong.Text, txt_Ho.Text, "12가1234");
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_DateTime.Text = DateTime.Now.ToString();

            if(DateTime.Now.ToString("mm:ss") == "00:01") //1시간마다
                listBox1.Items.Clear();
        }

        public void HomeLogger(string log)
        {
            string sLog;

            sLog = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + log;
            MyUtil.FileLogger(log);
            WriteTextSafe(sLog);

            
        }

        private void WriteTextSafe(string log)
        {
            if (listBox1.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                listBox1.Invoke(d, new object[] { log });
            }
            else
            {
                listBox1.Items.Insert(0, log);
            }
        }

        //해당날짜 방문차량 전체 목록
        private void btn_VisitAll_Click(object sender, EventArgs e)
        {
            DateTime dt1 = dateTimePicker1.Value;
            MyPark.VisitReservedAll(dt1.ToString("yyyy-MM-dd"));
        }

        //해당차량 방문차량유무 확인
        private void btn_VisitCheck_Click(object sender, EventArgs e)
        {
            MyPark.VisitCheck(txt_CarNo.Text); 
        }


        //private void btn_Exit_Click(object sender, EventArgs e)
        //{
        //0.this.Close();
        //
        //1.Application.Exit();
        //
        //2.Application.ExitThread();
        //  Environment.Exit(0);
        //
        //3,System.Diagnostics.Process[] mProcess = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);
        //foreach (System.Diagnostics.Process p in mProcess)
        //    p.Kill();
        //}
    }
}
