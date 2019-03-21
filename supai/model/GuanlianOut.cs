using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supai.model
{
    public class GuanlianOut
    {
        private bool _success;
        //private string _data;
        public Data data=new Data();

        /// <summary>
        /// 返回状态
        /// </summary>
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        //public string data
        //{
        //    get { return _data; }
        //    set { _data = value; }
        //}
    }
}
