using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectRC4
{
    public partial class Form1 : Form
    {
        RC4 rc4 = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            rc4 = new RC4(textEdit2.Text,textEdit1.Text);
            rc4.Encryption();
            textEdit3.Text = rc4.ChipperText;
            textEdit4.Text = rc4.KeyStream;
            textEdit5.Text = rc4.GeneratedKey;
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (rc4 != null)
            {
                rc4.Decoding();
               textEdit6.Text= rc4.PlainText;
            }
        }
    }
}
