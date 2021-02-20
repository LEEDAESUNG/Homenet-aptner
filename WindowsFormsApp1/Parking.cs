using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.Odbc;

namespace WindowsFormsApp1
{
    public class Parking
    {
        Form1 _MainForm;
        CommonUtil MyUtil = new CommonUtil();
        Database MyDB;


        //실서버
        //string CarInUrl = "https://gtw.aptner.com/pc/access/in";
        //string CarOutUrl = "https://gtw.aptner.com/pc/access/out";
        //string VisitAllUrl = "https://gtw.aptner.com/pc/visit/all";
        //string VisitCheckUrl = "https://gtw.aptner.com/pc/visit/check";
        //string AuthToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJORVdLT1JFQSIsInJvbGVzIjoiUEMiLCJpc3MiOiJBUFRORVIiLCJleHAiOjMxNjQ2NDc4ODYsImlkeCI6IiIsImlhdCI6MTU4Njg0MzQ4Nn0.xzwYK7pKMemnmqQqdX9mVqsuxs4tUywmAMq88uxKf2A";

        //테스트서버
        //string CarInUrl = "https://devgtw.aptner.com/pc/access/in";
        //string CarOutUrl = "https://devgtw.aptner.com/pc/access/out";
        //string VisitAllUrl = "https://devgtw.aptner.com/pc/visit/all";
        //string VisitCheckUrl = "https://devgtw.aptner.com/pc/visit/check";
        //string AuthToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJKQVdPT1RFSyIsInJvbGVzIjoiUEMiLCJpc3MiOiJBUFRORVIiLCJleHAiOjMxNjI5MTgzMjgsImlkeCI6IiIsImlhdCI6MTU4NTExMzkyOH0.nZqIiIuTJto8BfBln3HInNN26axzrlWTv3dE7kM_WVU";

        string CarInUrl;
        string CarOutUrl;
        string VisitAllUrl;
        string VisitCheckUrl;
        string AuthToken;

        string post_data;
        string KaptCode; //아파트코드
        string KaptName; //아파트명
        

        readonly int HostPort;

        public int GetHostPort()
        {
            return HostPort;
        }


        public Parking(Form1 _form1)
        {
            //string HomeNetMode = MyUtil.Get_Ini("System Config", "HomeNetMode");
            //HostPort = Int32.Parse(MyUtil.Get_Ini("System Config", "HostPort"));
            HostPort = 18597; //서버포트

            _MainForm = _form1;

            //if (HomeNetMode == "14") //아파트너
            {
                KaptCode = MyUtil.Get_Ini("APTNER", "KAPTCODE"); //아파트 고유코드
                KaptName = MyUtil.Get_Ini("APTNER", "KAPTNAME"); //아파트명
                _MainForm.lbl_Code.Text = "아파트코드 : " + KaptCode + "(" + KaptName + ")";

                if(KaptCode== "T77777777") //테스트서버 아파트코드
                {
                    CarInUrl = "https://devgtw.aptner.com/pc/access/in";
                    CarOutUrl = "https://devgtw.aptner.com/pc/access/out";
                    VisitAllUrl = "https://devgtw.aptner.com/pc/visit/all";
                    VisitCheckUrl = "https://devgtw.aptner.com/pc/visit/check";
                    AuthToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJKQVdPT1RFSyIsInJvbGVzIjoiUEMiLCJpc3MiOiJBUFRORVIiLCJleHAiOjMxNjI5MTgzMjgsImlkeCI6IiIsImlhdCI6MTU4NTExMzkyOH0.nZqIiIuTJto8BfBln3HInNN26axzrlWTv3dE7kM_WVU";
                }
                else //실서버 아파트코드
                {
                    CarInUrl = "https://gtw.aptner.com/pc/access/in";
                    CarOutUrl = "https://gtw.aptner.com/pc/access/out";
                    VisitAllUrl = "https://gtw.aptner.com/pc/visit/all";
                    VisitCheckUrl = "https://gtw.aptner.com/pc/visit/check";
                    AuthToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJORVdLT1JFQSIsInJvbGVzIjoiUEMiLCJpc3MiOiJBUFRORVIiLCJleHAiOjMxNjQ2NDc4ODYsImlkeCI6IiIsImlhdCI6MTU4Njg0MzQ4Nn0.xzwYK7pKMemnmqQqdX9mVqsuxs4tUywmAMq88uxKf2A";
                }
            }
            //else
            //{
            //    _MainForm.lbl_Code.Text = "APT Code : Error";
            //    _MainForm.HomeLogger("APT Code Error: " + HomeNetMode);
            //}
             
            MyDB = new Database();
        }
         
        public bool VisitCheck(string sCarno)
        {
            //Console.WriteLine("**********************************************************************************");
            //_MainForm.HomeLogger("**********************************************************************************");
            _MainForm.HomeLogger("[APTner] VisitCheck SND : 방문예약 확인");
            string Url = VisitCheckUrl + "?kaptCode=" + KaptCode + "&carNo=" + sCarno;
            string httpwbRequestResult = callHttpWebRequest(Url);

            if(httpwbRequestResult=="")
            {
                return false;
            }
            var r = JObject.Parse(httpwbRequestResult);
            //_MainForm.HomeLogger("[APTner] VisitCheck RCV : 방문자 확인");
            //var list = r["result"];
            _MainForm.HomeLogger("[APTner] VisitCheck RCV : 방문예약 확인");

            bool bResult = false;

            try
            {
                if(r["isVisitor"].ToString() ==  "Y")
                {
                    Console.WriteLine(string.Format("{0} : {1}", "방문예약", r["isVisitor"]));  //방문여부
                    Console.WriteLine(string.Format("{0} : {1}", "동", r["dong"]));             //동
                    Console.WriteLine(string.Format("{0} : {1}", "호", r["ho"]));               //호
                    Console.WriteLine(string.Format("{0} : {1}", "방문목적", r["purpose"]));    //방문목적

                    _MainForm.HomeLogger("[APTner] [" + sCarno + "] 방문예약:Y, 동:" + r["dong"].ToString() + ", 호:" + r["ho"].ToString() + ", 방문목적:" + r["purpose"].ToString());

                    Definition.Dong = r["dong"].ToString();
                    Definition.Ho = r["ho"].ToString();
                    Definition.Purpose = r["purpose"].ToString();

                    bResult = true;
                }
                else
                {
                    Console.WriteLine(string.Format("{0} : {1}", "방문예약", r["isVisitor"]));  //방문여부
                    _MainForm.HomeLogger("[APTner] [" + sCarno + "] 방문예약:N");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("방문예약 데이터 없음");
                _MainForm.HomeLogger("[APTner] 방문예약 데이터 없음");
            }

            return bResult;
        }
        public void VisitReservedAll(string sDate)
        {
            Console.WriteLine("**********************************************************************************");
            _MainForm.HomeLogger("**********************************************************************************");
            _MainForm.HomeLogger("[APTner] VisitAll SND : 방문예약 전체목록");
            string Url = VisitAllUrl + "?kaptCode=" + KaptCode + "&searchDate=" + sDate;
            string httpwbRequestResult = callHttpWebRequest(Url);
            if (httpwbRequestResult == "")
            {
                return;
            }
            var r = JObject.Parse(httpwbRequestResult);
            var list = r["result"];
            _MainForm.HomeLogger("[APTner] VisitAll RCV : 방문예약 전체목록");
            //Console.WriteLine(r);

            string Carno, isVisitor, Dong, Ho, Purpose;

            try
            {
                foreach (var o in list)
                {
                    Console.WriteLine(string.Format("{0} : {1}", "차량번호", o["carNo"]));      //입차시도한 차량의 번호
                    Console.WriteLine(string.Format("{0} : {1}", "방문여부", o["isVisitor"]));  //방문여부
                    Console.WriteLine(string.Format("{0} : {1}", "동", o["dong"]));             //동
                    Console.WriteLine(string.Format("{0} : {1}", "호", o["ho"]));               //호
                    Console.WriteLine(string.Format("{0} : {1}", "방문목적", o["purpose"]));    //방문목적

                    Carno= o["carNo"].ToString();
                    isVisitor = o["isVisitor"].ToString();
                    Dong = o["dong"].ToString();
                    Ho = o["ho"].ToString();
                    Purpose = o["purpose"].ToString();

                    _MainForm.HomeLogger("[APTner] " + "[차량번호:" + Carno + "] 방문여부:" + isVisitor + ", 동:" + Dong + ", 호:" + Ho + ", 방문목적:" + Purpose);
                    
                    //호스트 DB저장 또는 업데이트
                    //bool bRes;
                    //OdbcConnection tmpConn;
                    //OdbcCommand tmpCmd;
                    //using (tmpConn = new OdbcConnection(MyDB.strConn))
                    //{
                    //    tmpConn.Open();
                    //    tmpCmd = new OdbcCommand("SELECT * FROM tb_reg WHERE Car_no =?", tmpConn);
                    //    tmpCmd.Parameters.AddWithValue("@CarNo", o["carNo"]);
                    //    OdbcDataReader reader = tmpCmd.ExecuteReader();
                    //    while (reader.Read())
                    //    {
                    //        if (reader.Read())
                    //        {
                    //            bRes = true;
                    //        }
                    //        else
                    //        {
                    //            bRes = false;
                    //        }
                    //    }
                    //}

                    //HOST DB에서 검색
                    //using (MyDB.oConnection = new OdbcConnection(MyDB.strConn)) 
                    //{
                    //    MyDB.oConnection.Open();
                    //    //var enc_euc = Encoding.GetEncoding(51949);
                    //    //byte[] raw = Encoding.Default.GetBytes("SELECT * FROM tb_reg WHERE CAR_NO ='11가1111'; ");
                    //    //var cmd = enc_euc.GetString(raw);
                    //    //MyDB.oCommand = new OdbcCommand(cmd, MyDB.oConnection);

                    //    MyDB.oCommand = new OdbcCommand("SELECT * FROM tb_reg WHERE CAR_NO = ? ", MyDB.oConnection);
                    //    MyDB.oCommand.Parameters.AddWithValue("@carno", o["carNo"].ToString());
                    //    //MyDB.oCommand.Parameters["@CAR_NO"].Value = "11가1111";
                    //    OdbcDataReader reader = MyDB.oCommand.ExecuteReader();

                    //    if (reader.Read())
                    //    {
                    //        _MainForm.HomeLogger("[HOST DB] 정기권검색 : 성공!! " + o["carNo"].ToString());
                    //        Console.WriteLine(reader.GetString(reader.GetOrdinal("CAR_MODEL")));

                            
                    //        //정기권 -> 패스
                    //        //정기권 -> 방문예약 -> 업데이트
                    //    }
                    //    else
                    //    {
                    //        _MainForm.HomeLogger("[HOST DB] 정기권검색 : 실패!! " + o["carNo"].ToString());
                    //        //정기권 추가
                    //    }
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("방문예약 전체목록 데이터 없음");
                _MainForm.HomeLogger("[APTner] 방문예약 전체목록 데이터 없음");
            }
        }

        public string callHttpWebRequest(string sUrl)
        {
            string responseFromServer = string.Empty;

            string responseText;
            //string SearchDate = DateTime.Now.ToString("yyyy-MM-dd");
            //string SearchDate = "2020-03-26"; //테스트

            //string Url = VisitAllUrl + "?kaptCode=" + KaptCode + "&searchDate=" + SearchDate;
            string Url = sUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.Headers.Add("Authorization", AuthToken);
            //Console.WriteLine(VisitAllUrl);
            //string post_data = "kaptCode=" + KaptCode + "&searchDate=" + SearchDate;

            try
            {
                using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
                {
                    //HttpStatusCode status = resp.StatusCode;
                    //Console.WriteLine(status);  // 정상이면 "OK"

                    Stream respStream = resp.GetResponseStream();
                    using (StreamReader sr = new StreamReader(respStream))
                    {
                        responseText = sr.ReadToEnd();
                        //Console.WriteLine(responseText); //JSON 스트링 결과
                        responseFromServer = responseText;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Message.Contains("400"))
                    _MainForm.HomeLogger("[APTner] VisitAll RCV : Bad Request(400)");
                else if (ex.Message.Contains("401"))
                    _MainForm.HomeLogger("[APTner] VisitAll RCV : Unauthorized Request(401)");
                else if (ex.Message.Contains("403"))
                    _MainForm.HomeLogger("[APTner] VisitAll RCV : Forbidden(403)");
                else if (ex.Message.Contains("404"))
                    _MainForm.HomeLogger("[APTner] VisitAll RCV : Resource Not Found(404)");
                else if (ex.Message.Contains("405"))
                    _MainForm.HomeLogger("[APTner] VisitAll RCV : Method Not Allowed(405)");
                else if (ex.Message.Contains("416"))
                    _MainForm.HomeLogger("[APTner] VisitAll RCV : Invalid Parameter(416)");
                else if (ex.Message.Contains("500"))
                    _MainForm.HomeLogger("[APTner] VisitAll RCV : Internal Server Error(500)");
                else
                    _MainForm.HomeLogger("[APTner] VisitAll RCV : UnKnow Error:" + ex.Message);

                responseFromServer = "";
            }

            return responseFromServer;
        }

        //public void VisitReservedAllList() // 방문예약전체목록
        //{
        //    //string SearchDate = DateTime.Now.ToString("yyyy-MM-dd");
        //    string responseText;
        //    string SearchDate = "2020-03-26";
        //    //string VisitAllUrl = "https://devgtw.aptner.com/pc/visit/all?kaptCode=" + KaptCode + "&searchDate=" + SearchDate;
        //    string VisitAllUrl = "https://devgtw.aptner.com/pc/visit/all";
        //    KaptCode = "T77777777";
        //    AuthToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJKQVdPT1RFSyIsInJvbGVzIjoiUEMiLCJpc3MiOiJBUFRORVIiLCJleHAiOjMxNjI5MTgzMjgsImlkeCI6IiIsImlhdCI6MTU4NTExMzkyOH0.nZqIiIuTJto8BfBln3HInNN26axzrlWTv3dE7kM_WVU";

        //    VisitAllUrl= VisitAllUrl + "?kaptCode=" + KaptCode + "&searchDate=" + SearchDate;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(VisitAllUrl);
        //    request.Method = "GET";
        //    request.Headers.Add("Authorization", AuthToken);
        //    Console.WriteLine(VisitAllUrl);
        //    post_data = "kaptCode=" + KaptCode + "&searchDate=" + SearchDate;

        

        //    using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
        //    {
        //        HttpStatusCode status = resp.StatusCode;
        //        Console.WriteLine(status);  // 정상이면 "OK"

        //        Stream respStream = resp.GetResponseStream();
        //        using (StreamReader sr = new StreamReader(respStream))
        //        {
        //            responseText = sr.ReadToEnd();
        //            Console.WriteLine(responseText);
        //        }
        //    }
        //}

        public void CarOut(string Carno)
        {
            Console.WriteLine("**********************************************************************************");
            _MainForm.HomeLogger("**********************************************************************************");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(CarOutUrl);
            request.Method = "POST";
            request.Headers.Add("Authorization", AuthToken);

            post_data = "kaptCode=" + KaptCode + "&carNo=" + Carno;

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
                    _MainForm.HomeLogger("[APTner] CarOut RCV : " + response.StatusDescription);
                }
            }
            catch (WebException ex)
            {
                if (ex.Message.Contains("400"))
                    _MainForm.HomeLogger("[APTner] CarOut RCV : Bad Request(400)");
                else if (ex.Message.Contains("401"))
                    _MainForm.HomeLogger("[APTner] CarOut RCV : Unauthorized Request(401)");
                else if (ex.Message.Contains("403"))
                    _MainForm.HomeLogger("[APTner] CarOut RCV : Forbidden(403)");
                else if (ex.Message.Contains("404"))
                    _MainForm.HomeLogger("[APTner] CarOut RCV : Resource Not Found(404)");
                else if (ex.Message.Contains("405"))
                    _MainForm.HomeLogger("[APTner] CarOut RCV : Method Not Allowed(405)");
                else if (ex.Message.Contains("416"))
                    _MainForm.HomeLogger("[APTner] CarOut RCV : Invalid Parameter(416)");
                else if (ex.Message.Contains("500"))
                    _MainForm.HomeLogger("[APTner] CarOut RCV : Internal Server Error(500)");
                else
                    _MainForm.HomeLogger("[APTner] CarOut RCV : UnKnow Error:" + ex.Message);
            }
        }
        public void CarIn(string Dong, string Ho, string Carno)
        {
            Console.WriteLine("**********************************************************************************");
            _MainForm.HomeLogger("**********************************************************************************");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(CarInUrl);
            request.Method = "POST";
            request.Headers.Add("Authorization", AuthToken);

            //post_data = "kaptCode=A10026547&carNo=25구5401&dong=200&ho=200";
            post_data = "kaptCode=" + KaptCode + "&carNo=" + Carno + "&dong=" + Dong + "&ho=" + Ho;

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
                    _MainForm.HomeLogger("[APTner] CarIn RCV : " + response.StatusDescription);
                }
            }
            catch (WebException ex)
            {
                if (ex.Message.Contains("400"))
                    //Console.WriteLine("Bad Request");
                    _MainForm.HomeLogger("[APTner] CarIn RCV : Bad Request(400)");
                else if (ex.Message.Contains("401"))
                    //Console.WriteLine("Unauthorized");
                    _MainForm.HomeLogger("[APTner] CarIn RCV : Unauthorized Request(401)");
                else if (ex.Message.Contains("403"))
                    //Console.WriteLine("Forbidden");
                    _MainForm.HomeLogger("[APTner] CarIn RCV : Forbidden(403)");
                else if (ex.Message.Contains("404"))
                    //Console.WriteLine("Resource Not Found");
                    _MainForm.HomeLogger("[APTner] CarIn RCV : Resource Not Found(404)");
                else if (ex.Message.Contains("405"))
                    //Console.WriteLine("Method Not Allowed");
                    _MainForm.HomeLogger("[APTner] CarIn RCV : Method Not Allowed(405)");
                else if (ex.Message.Contains("416"))
                    //Console.WriteLine("Invalid Parameter");
                    _MainForm.HomeLogger("[APTner] CarIn RCV : Invalid Parameter(416)");
                else if (ex.Message.Contains("500"))
                    //Console.WriteLine("Internal Server Error");
                    _MainForm.HomeLogger("[APTner] CarIn RCV : Internal Server Error(500)");
                else
                    //Console.WriteLine("UnKnow Error : " + ex.Message);
                    //_MainForm.listBox1.Items.Insert(0, DateTime.Now.ToString() + "  Recv : " + ex.Message);
                    _MainForm.HomeLogger("[APTner] CarIn RCV : UnKnow Error:" + ex.Message);
            }
        }

    }
}
