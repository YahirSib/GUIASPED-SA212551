using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guia10___Grafos
{
    public partial class Arco : Form
    {
        public bool control;
        public int dato;
        
        public Arco()
        {
            InitializeComponent();
            control = false;
            dato = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dato = Convert.ToInt16(textBox1.Text.Trim()) ;
                if (dato < 0)
                {
                    MessageBox.Show("Debe ingresar un valor positivo", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else
                {
                    control = true;
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debes ingresar un valor numerico");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            control = false;
            Hide();
        }

        private void Arco_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void Arco_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Arco_Shown(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Focus();
        }

        private void Arco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
    }
}
