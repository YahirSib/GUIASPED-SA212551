using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            InicializarCampos();
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

        public void intercambio(ref Button[] botones, int a, int b)
        {
            string temp = botones[a].Text;
            Point pa = botones[a].Location;
            Point pb = botones[b].Location;

            int diferenciaX = Math.Abs(pa.X - pb.X);
            int diferenciaY = Math.Abs(pa.Y - pb.Y);

            int x = 10;
            int y = 10;
            int t = 70;

            while (y != diferenciaY + 10)
            {
                Thread.Sleep(t);
                botones[a].Location += new Size(0, -10);
                botones[b].Location += new Size(0, +10);
                y += 10;
            }

            while (x != diferenciaX - diferenciaX % 10 + 10)
            {
                Thread.Sleep(t);
                if (pa.X < pb.X)
                {
                    botones[a].Location += new Size(+10, 0);
                    botones[b].Location += new Size(-10, 0);
                }
                else
                {
                    botones[a].Location += new Size(-10, 0);
                    botones[b].Location += new Size(+10, 0);
                }
                x += 10;
            }
            botones[a].Text = botones[b].Text;
            botones[b].Text = temp;
            botones[b].Location = pb;
            botones[a].Location = pa;
            estado = true;
            tabPage2.Refresh();
        }

        public void Max_Num(int[]a, int x, int indice, ref Button[] botones)
        {
            int izquierdo = indice * 2;
            int derecho = indice * 2 + 1;
            int mayor = 0;
            if (izquierdo < x && a[izquierdo] > a[indice])
            {
                mayor = izquierdo;
            }
            else
            {
                mayor = indice;
            }
            if (derecho < x && a[derecho] > a[mayor])
            {
                mayor = derecho;
            }
            if (mayor != indice)
            {
                int temp = a[indice];
                a[indice] = a[mayor];
                a[mayor] = temp;
                intercambio(ref Arreglo, mayor, indice);
                Max_Num(a, x, mayor, ref botones);
            }
        }

        public void Min_Num(int[] a, int x, int indice, ref Button[] botones)
        {
            int izquierdo = indice * 2;
            int derecho = indice * 2 + 1;
            int menor = 0;
            if (izquierdo < x && a[izquierdo] < a[indice])
            {
                menor = izquierdo;
            }
            else
            {
                menor = indice;
            }
            if (derecho < x && a[derecho] < a[menor])
            {
                menor = derecho;
            }
            if (menor != indice)
            {
                int temp = a[indice];
                a[indice] = a[menor];
                a[menor] = temp;
                intercambio(ref Arreglo, menor, indice);
                Min_Num(a, x, menor, ref botones);
            }
        }

        public void Heap_Num()
        {
            ec = true;
            int x = Arreglo.Length;
            for (int i = x/2; i > 0; i--)
            {
                Max_Num(Arreglo_numeros, x, i, ref Arreglo);
            }
        }

        public void Heap_Num2()
        {
            ec = true;
            int x = Arreglo.Length;
            for (int i = x / 2; i > 0; i--)
            {
                Min_Num(Arreglo_numeros, x, i, ref Arreglo);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int num = int.Parse(txtNumero.Text);
            Array.Resize<int>(ref Arreglo_numeros, i + 1);
            Arreglo_numeros[i] = num;
            Array.Resize<Button>(ref Arreglo, i + 1);

            Arreglo[i] = new Button();
            Arreglo[i].Text = Arreglo_numeros[i].ToString();
            Arreglo[i].Width = 50;
            Arreglo[i].Height = 50;
            Arreglo[i].BackColor = Color.GreenYellow;
            Arreglo[i].Location = new Point(xo, yo) + new Size(-20, 0);

            if ((i + 1) == Math.Pow(2, n+1))
            {
                n++;
                tam = tam / 2;
                xo = tam;
                yo += 60;
            }
            else
            {
                xo += (2 * tam);
            }
            tabPage2.Controls.Add(Arreglo[i]);
            i++;
            estado = true;
            ec = false;
            tabPage2.Refresh();
            txtNumero.Clear();
            txtNumero.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            InicializarCampos();
            Array.Resize<int>(ref Arreglo_numeros, 1) ;
            Array.Resize<Button>(ref Arreglo, 1);
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            if (i == 1)
            {
                MessageBox.Show("No hay elementos que ordenar");
            }
            else
            {
                btnAgregar.Enabled = false;
                btnLimpiar.Enabled = false;
                btnOrdenar.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                if (!ec)
                {
                    if (radioButton1.Checked)
                    {
                        Heap_Num();
                    }
                    else
                    {
                        Heap_Num2();
                    }
                    
                }
                else
                {
                    HPN();
                }

                btnAgregar.Enabled = true;
                btnLimpiar.Enabled = true;
                btnOrdenar.Enabled = true;
                this.Cursor= Cursors.Default;

            }
        }

        private void Monticulos_Paint(object sender, PaintEventArgs e)
        {
            if (estado)
            {
                try
                {
                    Dibujar_Arreglo(ref Arreglo, ref tabPage2);
                    Dibujar_Ramas(ref Arreglo, ref tabPage2, e);
                }catch (Exception ex)
                {
                    
                }

                estado = false;
            }
        }

        public void HPN()
        {
            int temp;
            int x = Arreglo_numeros.Length;
            for (int i = x-1; i>=1; i--)
            {
                intercambio(ref Arreglo, i, 1);
                temp = Arreglo_numeros[i];
                Arreglo_numeros[i] = Arreglo_numeros[1];
                Arreglo_numeros[1] = temp;
                x--;
            }
        }

        public void Dibujar_Arreglo(ref Button[] botones, ref TabPage tb)
        {
            for (int j = 1; j < botones.Length; j++)
            {
                tb.Controls.Add(this.Arreglo[j]);
            }
        }

        public void Dibujar_Ramas(ref Button[] botones, ref TabPage tb, PaintEventArgs e)
        {
            Pen Lapiz = new Pen(Color.Red, 1.5f);
            Graphics g = e.Graphics;
            for (int j = 1; j < Arreglo.Length; j++)
            {
                if (Arreglo[j * 2] != null)
                {
                    g.DrawLine(Lapiz, Arreglo[j].Location.X, Arreglo[j].Location.Y + 20, Arreglo[2 * j].Location.X + 20, Arreglo[2 * j].Location.Y);
                }
                if (Arreglo[2 * j + 1] != null)
                {
                    g.DrawLine(Lapiz, Arreglo[j].Location.X + 40, Arreglo[j].Location.Y + 20, Arreglo[2 * j + 1].Location.X + 20, Arreglo[2 * j + 1].Location.Y);
                }
            }
        }



    }
}
