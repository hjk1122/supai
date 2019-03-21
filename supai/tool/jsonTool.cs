using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using supai.model;

namespace supai.tool
{
    public class jsonTool
    {
        /// <summary>
        /// 得到访问webApi结果
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public string GetToken(string strJson)
        {
            string result = "";
            try
            {
                string toDes = strJson;
                //string to = "{\"ID\":\"1\",\"Name\":\"曹操\",\"Sex\":\"男\",\"Age\":\"1230\"}";
                TokenResult tokenResult = JsonHelper.DeserializeJsonToObject<TokenResult>(strJson);
                if (tokenResult.success.ToLower() == "true")
                {
                    result = tokenResult.data.access_token;
                }
            }
            catch (Exception e)
            {
                result = "error:" + e.Message;
            }

            return result;

        }

        /// <summary>
        /// 得到关联单号json
        /// </summary>
        /// <param name="glId"></param>
        /// <returns></returns>
        public string GLJsonIn(GuanlianIn guanlian)
        {
            string result = "";
            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(GuanlianIn));
                MemoryStream msObj = new MemoryStream();
                //将序列化之后的Json格式数据写入流中
                js.WriteObject(msObj, guanlian);
                msObj.Position = 0;
                //从0这个位置开始读取流中的数据
                StreamReader sr = new StreamReader(msObj, Encoding.UTF8);
                result = sr.ReadToEnd();
                sr.Close();
                msObj.Close();
            }
            catch (Exception e)
            {
                result = "error:" + e.Message;
            }
            return result;
        }
        /// <summary>
        /// 得到返回的分运单列表
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public List<FYModel> GLJsonOutData(string strJson,out string errMsg)
        {
            List<FYModel> lstFyModels=new List<FYModel>();
            errMsg = "";
            try
            {
                string toDes = strJson;
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(toDes)))
                {
                    //strJson =
                    //    "{\"success\":\"false\",\"data\":{\"field\":\"original_no\",\"message\":\"DS0245498\"}}";
                    //GuanlianOut guanlianOut = JsonHelper.DeserializeJsonToObject<GuanlianOut>(strJson);
                    GuanlianOut guanlianOut = JsonHelper.DeserializeJsonGLOut(strJson);
                    //if (guanlianOut.success == true)
                    //{
                    lstFyModels = this.GetFYData(guanlianOut.data.msg);
                    //}
                }
            }
            catch (Exception e)
            {
                errMsg=e.Message;
            }

            return lstFyModels;

        }
        /// <summary>
        /// 获得分运单数据
        /// </summary>
        /// <param name="strdata"></param>
        /// <returns></returns>
        private List<FYModel> GetFYData(Dictionary<string,string> dict)
        {
            List<FYModel> lstFyModels=new List<FYModel>();
            foreach (var item in dict)
            {
                FYModel fyModel = new FYModel();
                fyModel.GLid = item.Key;
                fyModel.FYid = item.Value;
                lstFyModels.Add(fyModel);

            }
            return lstFyModels;
        }
    }

}
