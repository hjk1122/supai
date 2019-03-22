using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using supai.class1;
using supai.tool;
using supai.model;

namespace supai
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strToken = "";
            string strFYid = "";
            Post post = new Post();
            //string strJson=post.GetFYData(); 
            //jsonTool jsonTool = new jsonTool();
            //string error = "";
            //List<FYModel> fyModel = jsonTool.GLJsonOutData(strJson,out error);
            List<FYModel> fyModel = post.GetFYData();
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "转运单号.xls";
            npoi npoi = new npoi();
            string result = npoi.WriteFile(filePath, fyModel);
            if (result == "1")
            {
                MessageBox.Show("输出成功", "信息", MessageBoxButtons.OK);
            }


            //strToken = post.GetToken();
            //if(strToken.Substring(0,5)!="error")
            //{
            //    strFYid = post.GetFYJson(strToken);
            //}
        }
    }
}
