using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Guia10___Grafos
{
    public partial class Pizzara : Form
    {

        private CGrafo grafo;
        private CVertice nuevoNodo;
        private CVertice NodoOrigen;
        private CVertice NodoDestino;
        private int var_control = 0;

        private Vertice ventanaVertice;

        public Pizzara()
        {
            InitializeComponent();
            grafo = new CGrafo();
            nuevoNodo = null;
            var_control = 0;
            ventanaVertice = new Vertice();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,true);
        }

        private void Simulador_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                grafo.DibujarGrafo(e.Graphics);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Simulador_MouseLeave(object sender, EventArgs e)
        {
            Simulador.Refresh();
        }

        private void Simulador_MouseUp(object sender, MouseEventArgs e)
        {
            switch (var_control)
            {
                case 1:
                    if ((NodoDestino = grafo.DetectarPunto(e.Location)) != null && NodoOrigen != NodoDestino)
                    {
                        if (grafo.AgregarArco(NodoOrigen, NodoDestino))
                        {
                            int distancia = 0;
                            NodoOrigen.ListaAdyacencia.Find(v => v.nDestino == NodoDestino).peso = distancia;
                        }
                    }
                    var_control = 0;
                    NodoOrigen = null;
                    NodoDestino = null;
                    Simulador.Refresh();
                    break;
            }
        }

        private void Simulador_MouseMove(object sender, MouseEventArgs e)
        {
            switch (var_control)
            {
                case 2:
                    if (nuevoNodo != null)
                    {
                        int posX = e.Location.X;
                        int posY = e.Location.Y;
                        if (posX < nuevoNodo.Dimensiones.Width/2)
                        {
                            posX = nuevoNodo.Dimensiones.Width / 2;
                        }else if (posX > Simulador.Size.Width - nuevoNodo.Dimensiones.Width / 2)
                        {
                            posX = Simulador.Size.Width - nuevoNodo.Dimensiones.Width / 2;
                        }

                        if (posY < nuevoNodo.Dimensiones.Height / 2)
                        {
                            posY = nuevoNodo.Dimensiones.Height / 2;
                        }
                        else if (posY > Simulador.Size.Height - nuevoNodo.Dimensiones.Width / 2)
                        {
                            posY = Simulador.Size.Height - nuevoNodo.Dimensiones.Width / 2;
                        }

                        nuevoNodo.Posicion = new Point(posX,posY);
                        Simulador.Refresh();
                        nuevoNodo.DibujarVertice(Simulador.CreateGraphics());
                    }
                    break;

                case 1:
                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(4, 4, true);
                    bigArrow.BaseCap = LineCap.Triangle;
                    Simulador.Refresh();
                    Simulador.CreateGraphics().DrawLine(new Pen(Brushes.Black, 2)
                    {
                        CustomEndCap = bigArrow},
                        NodoOrigen.Posicion, e.Location
                    );
                    break;
            }
        }

        private void Simulador_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((NodoOrigen = grafo.DetectarPunto(e.Location)) != null)
                {
                    var_control = 1;
                }

                if (nuevoNodo != null && NodoOrigen == null)
                {
                    ventanaVertice.Visible = false;
                    ventanaVertice.control = false;
                    grafo.AgregarVertice(nuevoNodo);
                    ventanaVertice.ShowDialog();
                    if (ventanaVertice.control)
                    {
                        if (grafo.BuscarVertice(ventanaVertice.txtVertice.Text) != null)
                        {
                            nuevoNodo.Valor = ventanaVertice.txtVertice.Text;
                        }
                        else
                        {
                            MessageBox.Show("El nodo" + ventanaVertice.txtVertice.Text +
                                "ya existe el grafo", "Error nuevo Nodo", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                        }
                    }
                    nuevoNodo = null;
                    var_control = 0;
                    Simulador.Refresh();
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (var_control == 0)
                {
                    if ((NodoOrigen = grafo.DetectarPunto(e.Location)) != null)
                    {
                        CMSCrearVertice.Text = "Nodo" + NodoOrigen.Valor;
                    }
                    else
                    {
                        Simulador.ContextMenuStrip = this.CMSCrearVertice;
                    }
                }
            }
        }

        private void nuevoVertice_Click(object sender, EventArgs e)
        {
            nuevoNodo = new CVertice();
            var_control = 2;
        }
    }
}
