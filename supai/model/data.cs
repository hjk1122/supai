using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace supai.model
{
    public class Data
    {
        private Dictionary<string,string> _msg=new Dictionary<string, string>();

        public Dictionary<string, string> msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

    }
}
