using System;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;


namespace WindowsFormsApp1
{
    class tempParking
    {
        CommonUtil MyUtil = new CommonUtil();

        string post_data = "kaptCode=A10026547&carNo=25구5401&dong=200&ho=200";
        string AuthToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJORVdLT1JFQSIsInJvbGVzIjoiUEMiLCJpc3MiOiJBUFRORVIiLCJleHAiOjMxNjQ2NDc4ODYsImlkeCI6IiIsImlhdCI6MTU4Njg0MzQ4Nn0.xzwYK7pKMemnmqQqdX9mVqsuxs4tUywmAMq88uxKf2A";
        string url = "https://gtw.aptner.com/pc/access/in";

        string kaptCode; //아파트코드

        public tempParking()
        {
            string HomeNetMode = MyUtil.Get_Ini("System Config", "HomeNetMode");
            int HostPort = Int32.Parse(MyUtil.Get_Ini("System Config", "HostPort"));

            MySock = new MySocket(HostPort);

            if (HomeNetMode == "14") //아파트너
                kaptCode = MyUtil.Get_Ini("APTNER", "KAPTCODE"); //아파트 고유코드


            //Console.WriteLine(HomeNetMode);
        }


        void POST(string url, string post_data, string authToken)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", authToken);

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
                    Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
                    Console.WriteLine(response.StatusCode);
                }
            }
            catch (WebException ex)
            {

                //MessageBox.Show(ex.Message);

                if (ex.Message.Contains("400"))
                    Console.WriteLine("Bad Request");
                else if (ex.Message.Contains("401"))
                    Console.WriteLine("Unauthorized");
                else if (ex.Message.Contains("403"))
                    Console.WriteLine("Forbidden");
                else if (ex.Message.Contains("404"))
                    Console.WriteLine("Resource Not Found");
                else if (ex.Message.Contains("405"))
                    Console.WriteLine("Method Not Allowed");
                else if (ex.Message.Contains("416"))
                    Console.WriteLine("Invalid Parameter");
                else if (ex.Message.Contains("500"))
                    Console.WriteLine("Internal Server Error");
                else
                    Console.WriteLine("UnKnow Error : " + ex.Message);
            }
        }
    }
}
