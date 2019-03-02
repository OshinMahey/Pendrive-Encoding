using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PIA
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Form3 f3;

        private void button1_Click(object sender, EventArgs e)
        {
            if (f3 == null)
            {

                f3 = new Form3(null);
                //f2.MdiParent = this;
                f3.FormClosed += new FormClosedEventHandler(f3_FormClosed);
                f3.ShowDialog();

            }
            else
            {

                f3.Activate();
            }
        }

        void f3_FormClosed(object sender, FormClosedEventArgs e)
        {
            f3 = null;
            //throw new NotImplementedException();
        }

    }
}
