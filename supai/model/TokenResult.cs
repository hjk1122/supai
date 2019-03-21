using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supai.model
{
    public class TokenResult
    {
        private string _success;
        public token data;

        /// <summary>
        /// 返回状态
        /// </summary>
        public string success
        {
            get { return _success; }
            set { _success = value; }
        }

    }
}
