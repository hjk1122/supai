using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supai.model
{
    public class token
    {
        private string _access_token;
        public string access_token
        {
            get
            {
                return _access_token;
            }
            set
            {
                _access_token = value;
            }
        }
    }
}
