using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supai.model
{
    /// <summary>
    /// “关联单号”：“分运单号”结构
    /// </summary>
    public class FYModel
    {
        private string _GLid;
        private string _FYid;

        public string GLid
        {
            get { return _GLid; }
            set
            {
                _GLid = value;
            }
        }

        public string FYid
        {
            get
            {
                return _FYid;
            }
            set
            {
                _FYid = value;
            }
        }
    }
}
