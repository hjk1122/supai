using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using supai.tool;

namespace supai.class1
{
    public class Post
    {
        public string GetToken()
        {
            string result = "";
            try
            {
                string url =
                    "http://123.56.237.80:85/getAccessToken/index";
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "POST";
                request.Accept = "text/html,application/xhtml+xml,*/*";
                request.ContentType = "application/json";
                //写入body
                string jsonStr="{\"api_username\":\"fastgo\", \"api_password\":\"LWw0hlWI0eq7sly8LPmtJVL3uRpB92\"}";
                byte[] buffer = encoding.GetBytes(jsonStr);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
                {
                    jsonTool jsonTool1 = new jsonTool();
                    result = sr.ReadToEnd();
                    result = jsonTool1.GetToken(result);


                }
            }
            catch (WebException ex)
            {
                var res = (HttpWebResponse) ex.Response;
                StringBuilder sb = new StringBuilder();
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                sb.Append(sr.ReadToEnd());
                result = "error:"+sb.ToString();
            }

            return result;

        }

        public string GetToken1()
        {
            string url = "http://123.56.237.80:85/getAccessToken/index";
            string jsonStr = "{\"api_username\":\"fastgo\", \"api_password\":\"LWw0hlWI0eq7sly8LPmtJVL3uRpB92\"}";
            jsonTool jsonTool = new jsonTool();
            return jsonTool.GetToken(HttpPost(url, jsonStr));
        }
        public static string HttpPost(string url, string body)
          {
              try
              {
                  //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                  Encoding encoding = Encoding.UTF8;
                  HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                 request.Method = "POST";
                 request.Accept = "application/json, text/javascript, */*"; //"text/html, application/xhtml+xml, */*";
                 request.ContentType = "application/json; charset=utf-8";
 
                 byte[] buffer = encoding.GetBytes(body);
                 request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                 HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                 using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                 {
                     return reader.ReadToEnd();
                 }
             }
             catch (WebException ex)
             {
                 var res = (HttpWebResponse)ex.Response;
                 StringBuilder sb = new StringBuilder();
                 StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                 sb.Append(sr.ReadToEnd());
                 //string ssb = sb.ToString();
                 throw new Exception(sb.ToString());
             }
         }

        /// <summary>
        /// 得到分运单编号
        /// </summary>
        /// <param name="strToken"></param>
        /// <returns></returns>
        public string GetFYJson(string strToken,string strjson)
        {
            string result = "";
            try
            {
                string url =
                    "http://123.56.237.80:85/api/declaration/getNo?access-token="+strToken.Trim();
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Accept = "text/html,application/xhtml+xml,*/*";
                request.ContentType = "application/json";
                //写入body
                //string jsonStr = "{\"original_no\":[\"FG1046851LH\",\"FG0408532AU\"]}";
                string jsonStr = strjson;
                byte[] buffer = encoding.GetBytes(jsonStr);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var res = (HttpWebResponse)ex.Response;
                StringBuilder sb = new StringBuilder();
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                sb.Append(sr.ReadToEnd());
                result = "error:"+sb.ToString();
            }

            return result;

        }
        /// <summary>
        /// 获取excel文件的json数据
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            string result = "";
            npoi npoi=new npoi();
            Post post = new Post();
            string strToken = "";

            StringBuilder strjson =new StringBuilder();
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "FTJ388.xls";
            List<string> listDH = npoi.GetDH(filePath);
            if (listDH.Count > 100)
            {
                int count = listDH.Count;
                //int pagecount = count / 100;
                //int ys = count % 100;
                strToken = post.GetToken();
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                strjson.Append("{\"original_no\":[");
                for (int i = 0; i <= count; i++)
                {
                    if (listDH[i].Trim() != "")
                    {
                        strjson.AppendFormat("{0}{1}{2}", "\"", listDH[i], "\",");
                        if (i % 100 == 0)
                        {
                            //string str=strjson.ToString();
                            //str.Substring(0, str.Length - 2);
                            //strjson.Clear();
                            //strjson.Append(str);
                            strjson.Remove(strjson.Length - 1, 1);
                            strjson.Append(@"]}");
                            string token = GetNewToken(ts);
                            if (token != "1")
                            {
                                strToken = token;
                                ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                            }
                            //strToken = post.GetToken();
                            if (strToken.Substring(0, 5) != "error")
                            {
                                result = post.GetFYJson(strToken, strjson.ToString());
                            }
                            strjson.Clear();
                            strjson.Append("{\"original_no\":[");
                        }
                        //最后数量大于100的最后一个关联单
                        else if ((i / 100 == 0) && (listDH[i + 1].Trim()==""))
                        {
                            strjson.Remove(strjson.Length - 1, 1);
                            strjson.Append(@"]}");
                            string token = GetNewToken(ts);
                            if (token != "1")
                            {
                                strToken = token;
                                ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                            }
                            //strToken = post.GetToken();
                            if (strToken.Substring(0, 5) != "error")
                            {
                                result = post.GetFYJson(strToken, strjson.ToString());
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                int count = listDH.Count;
                //int pagecount = count / 100;
                //int ys = count % 100;
                strToken = post.GetToken();
                strjson.Append("{\"original_no\":[");
                for (int i = 0; i <= count; i++)
                {
                    if (listDH[i].Trim() != "")
                    {
                        strjson.AppendFormat("{0}{1}{2}", "\"", listDH[i], "\",");

                        if ((i / 100 == 0) && (listDH[i + 1].Trim() == ""))
                        {
                            strjson.Remove(strjson.Length - 1, 1);
                            strjson.Append(@"]}");
                            if (strToken.Substring(0, 5) != "error")
                            {
                                result = post.GetFYJson(strToken, strjson.ToString());
                            }

                            break;
                        }
                    }

                }
            }

            return result;
            //strjson.Append("{\"original_no\":[");

        }
        /// <summary>
        /// 在时间差值上获得新的Token
        /// </summary>
        /// <returns></returns>
        private string GetNewToken(TimeSpan ts)
        {
            string result = "1";
            TimeSpan ts1 = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan tsSub = ts1.Subtract(ts).Duration();
            Post post = new Post();
            //间隔设为1100秒
            if (Convert.ToInt32(tsSub.TotalSeconds) > 1100)
            {
                result = post.GetToken();
            }
            return result;
        }
    }
}
