using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Parking
    {
        Form1 _MainForm;
        CommonUtil MyUtil = new CommonUtil();

        string url = "https://gtw.aptner.com/pc/access/in";
        string AuthToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJORVdLT1JFQSIsInJvbGVzIjoiUEMiLCJpc3MiOiJBUFRORVIiLCJleHAiOjMxNjQ2NDc4ODYsImlkeCI6IiIsImlhdCI6MTU4Njg0MzQ4Nn0.xzwYK7pKMemnmqQqdX9mVqsuxs4tUywmAMq88uxKf2A";
        string post_data;
        string KaptCode; //아파트코드

        readonly int HostPort;

        public int GetHostPort()
        {
            return HostPort;
        }


        public Parking(Form1 _form1)
        {
            string HomeNetMode = MyUtil.Get_Ini("System Config", "HomeNetMode");
            HostPort = Int32.Parse(MyUtil.Get_Ini("System Config", "HostPort"));

            _MainForm = _form1;

            if (HomeNetMode == "14") //아파트너
            {
                KaptCode = MyUtil.Get_Ini("APTNER", "KAPTCODE"); //아파트 고유코드
                _MainForm.lbl_Code.Text = "APT Code : " + KaptCode;
                
            }
            else
            {
                _MainForm.lbl_Code.Text = "APT Code : Error";
                _MainForm.HomeLogger("APT Code Error: " + HomeNetMode);
            }


            //MySock = new MySocket(HostPort);
            //Console.WriteLine(HomeNetMode);
        }


        public void POST(string Dong, string Ho, string Carno)
        {


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", AuthToken);

            post_data = "kaptCode=A10026547&carNo=25구5401&dong=200&ho=200";
            //post_data = "kaptCode=" + KaptCode + "&carNo=" + Carno + "&dong=" + Dong + "&ho=" + Ho;

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(post_data);
            request.ContentLength = byteArray.Length;
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                    //Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
                    //Console.WriteLine(response.StatusCode);
                    _MainForm.HomeLogger("[APTner] RCV : " + response.StatusDescription);
                }
            }
            catch (WebException ex)
            {
                if (ex.Message.Contains("400"))
                    //Console.WriteLine("Bad Request");
                    _MainForm.HomeLogger("[APTner] RCV : Bad Request(400)");
                else if (ex.Message.Contains("401"))
                    //Console.WriteLine("Unauthorized");
                    _MainForm.HomeLogger("[APTner] RCV : Unauthorized Request(401)");
                else if (ex.Message.Contains("403"))
                    //Console.WriteLine("Forbidden");
                    _MainForm.HomeLogger("[APTner] RCV : Forbidden(403)");
                else if (ex.Message.Contains("404"))
                    //Console.WriteLine("Resource Not Found");
                    _MainForm.HomeLogger("[APTner] RCV : Resource Not Found(404)");
                else if (ex.Message.Contains("405"))
                    //Console.WriteLine("Method Not Allowed");
                    _MainForm.HomeLogger("[APTner] RCV : Method Not Allowed(405)");
                else if (ex.Message.Contains("416"))
                    //Console.WriteLine("Invalid Parameter");
                    _MainForm.HomeLogger("[APTner] RCV : Invalid Parameter(416)");
                else if (ex.Message.Contains("500"))
                    //Console.WriteLine("Internal Server Error");
                    _MainForm.HomeLogger("[APTner] RCV : Internal Server Error(500)");
                else
                    //Console.WriteLine("UnKnow Error : " + ex.Message);
                    //_MainForm.listBox1.Items.Insert(0, DateTime.Now.ToString() + "  Recv : " + ex.Message);
                    _MainForm.HomeLogger("[APTner] RCV : UnKnow Error:" + ex.Message);
            }
        }

    }
}
