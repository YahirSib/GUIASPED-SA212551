using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guia9___Monticulos
{
    public partial class Monticulos : Form
    {
        int n, i;
        int xo, yo, tam;
        bool ec = false;
        bool estado = false;
        int[] Arreglo_numeros;
        Button[] Arreglo;

        public Monticulos()
        {
            InitializeComponent();
        }

        public void InicializarCampos()
        {
            n = 0;
            i = 1;
            tam = tabPage2.Width / 2;
            xo = tam;
            yo = 20;
            tabPage2.Controls.Clear();
            tabPage2.Refresh();
            txtNumero.Focus();
        }

    }
}
