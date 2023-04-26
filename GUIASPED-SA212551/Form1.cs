using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIASPED_SA212551
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Guia9___Monticulos.Monticulos frmGuia9 = new Guia9___Monticulos.Monticulos();
            frmGuia9.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Guia10___Grafos.Grafos frmGuia10 = new Guia10___Grafos.Grafos();
            frmGuia10.Show();
        }
    }
}
