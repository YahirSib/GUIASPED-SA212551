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
using System.Collections;
using System.Threading;

namespace Guia10___Grafos
{
    public partial class Pizzara : Form
    {

        private CGrafo grafo;
        private CVertice nuevoNodo;
        private CVertice NodoOrigen;
        private CVertice NodoDestino;
        private int var_control = 0;
        public string ruta;

        private Vertice ventanaVertice;
        private Arco ventanaArco;
        List<CVertice> nodosRuta;
        List<CVertice> nodosOrdenados;
        bool buscarRuta = false, nuevoV = false, nuevoArco = false;
        private int numeronodos = 0;
        bool profundidad = false, anchura = false, nodoEncontrado = false;
        Queue cola = new Queue();
        private string destino = "", origen = "";
        private int distancia = 0;

        public Pizzara()
        {
            InitializeComponent();
            grafo = new CGrafo();
            nuevoNodo = null;
            var_control = 0;
            ventanaVertice = new Vertice();
            ventanaArco = new Arco();
            nodosRuta = new List<CVertice>();
            nodosOrdenados = new List<CVertice>();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,true);
        }

        public void ordenarNodos()
        {
            nodosOrdenados = new List<CVertice>();
            bool est = false;
            foreach (CVertice nodo in grafo.nodos)
            {
                if (nodo.Valor == origen)
                {
                    nodosOrdenados.Add(nodo);
                    est = true;
                }else if (est)
                {
                    nodosOrdenados.Add(nodo);
                }
            }
            foreach (CVertice nodo in grafo.nodos)
            {
                if (nodo.Valor == origen)
                {
                    est = false;
                    break;
                }else if (est)
                {
                    nodosOrdenados.Add(nodo);
                }
            }
        }

        private void recorridoProfundidad(CVertice vertice, Graphics g)
        {
            vertice.Visitado = true;
            vertice.colorear(g);
            ruta += vertice.Valor + " ";
            txtRuta.Text = ruta;
            Thread.Sleep(500);
            vertice.DibujarVertice(g);
            foreach (CArco adya in vertice.ListaAdyacencia)
            {
                if (!adya.nDestino.Visitado)
                {
                    recorridoProfundidad(adya.nDestino, g);
                }
                else
                {
                    ruta = "";
                }
            }
        }

        private void recorridoAnchura(CVertice vertice, Graphics g, string destino)
        {
            vertice.Visitado = true;
            cola.Enqueue(vertice);
            vertice.colorear(g);
            Thread.Sleep(500);
            vertice.DibujarVertice(g);
            ruta += vertice.Valor + " ";
            if (vertice.Valor == destino)
            {
                nodoEncontrado = true;
                ruta = "";
                return;
            }
            while (cola.Count > 0)
            {
                CVertice aux = (CVertice)cola.Dequeue();
                foreach (CArco adya in aux.ListaAdyacencia)
                {
                    if (!adya.nDestino.Visitado)
                    {
                        if (!nodoEncontrado)
                        {
                            adya.nDestino.Visitado = true;
                            adya.nDestino.colorear(g);
                            Thread.Sleep(1000);
                            adya.nDestino.DibujarVertice(g);
                            ruta += adya.nDestino.Valor;
                            txtRuta.Text = ruta;
                            if (destino != "")
                            {
                                distancia += adya.peso;
                            }
                            cola.Enqueue(adya.nDestino);
                            if (adya.nDestino.Valor == destino)
                            {
                                nodoEncontrado = true;
                                ruta = "";
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void Simulador_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                grafo.DibujarGrafo(e.Graphics);
                if (nuevoV)
                {
                    CBVertice.Items.Clear();
                    CBVertice.SelectedIndex = -1;
                    CBNodoPartida.Items.Clear();
                    CBNodoPartida.SelectedIndex = -1;
                    foreach (CVertice nodo in grafo.nodos)
                    {
                        CBVertice.Items.Add(nodo.Valor);
                        CBNodoPartida.Items.Add(nodo.Valor);
                    }
                    nuevoV = false;
                }
                if (nuevoArco)
                {
                    CBArco.Items.Clear();
                    CBArco.SelectedIndex = -1;
                    foreach (CVertice nodo in grafo.nodos)
                    {
                        foreach (CArco arco in nodo.ListaAdyacencia)
                        {
                            CBArco.Items.Add("(" + nodo.Valor + "," + arco.nDestino.Valor + ") peso: " + arco.peso);
                        }
                    }
                    nuevoArco = false;
                }
                if (buscarRuta)
                {
                    foreach (CVertice nodo in nodosRuta)
                    {
                        nodo.colorear(e.Graphics);
                        Thread.Sleep(1000);
                        nodo.DibujarVertice(e.Graphics);
                    }
                    buscarRuta = false;
                }
                if (profundidad)
                {
                    ordenarNodos();
                    foreach (CVertice nodo in nodosOrdenados)
                    {
                        if (!nodo.Visitado)
                        {
                            recorridoProfundidad(nodo, e.Graphics);
                        }
                    }
                    profundidad = false;
                    foreach (CVertice nodo in grafo.nodos)
                    {
                        nodo.Visitado = false;
                    }
                }
                if (anchura)
                {
                    distancia = 0;
                    cola = new Queue();
                    ordenarNodos();
                    foreach (CVertice nodo in nodosOrdenados)
                    {
                        if (!nodo.Visitado && !nodoEncontrado)
                        {
                            recorridoAnchura(nodo, e.Graphics, destino);
                        }
                    }
                    anchura = false;
                    nodoEncontrado = false;
                    foreach (CVertice nodo in grafo.nodos)
                    {
                        nodo.Visitado = false;
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CBVertice.SelectedIndex > -1)
            {
                foreach (CVertice nodo in grafo.nodos)
                {
                    if (nodo.Valor == CBVertice.SelectedItem.ToString())
                    {
                        grafo.nodos.Remove(nodo);
                        nodo.ListaAdyacencia = new List<CArco>();
                        break;
                    }
                }
                foreach (CVertice nodo in grafo.nodos)
                {
                    foreach (CArco arco in nodo.ListaAdyacencia)
                    {
                        if (arco.nDestino.Valor == CBVertice.SelectedItem.ToString())
                        {
                            nodo.ListaAdyacencia.Remove(arco);
                            break;
                        }
                    }
                }
                nuevoArco = true;
                nuevoV = true;
                CBVertice.SelectedIndex = -1;
                Simulador.Refresh();
            }
            else
            {
                lblRespuesta.Text = "Seleccione un nodo";
                lblRespuesta.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CBArco.SelectedIndex > -1)
            {
                foreach (CVertice nodo in grafo.nodos)
                {
                    foreach(CArco arco in nodo.ListaAdyacencia)
                    {
                        if ("(" + nodo.Valor + "," + arco.nDestino.Valor+ ") peso: " + arco.peso ==
                            CBArco.SelectedItem.ToString())
                        {
                            nodo.ListaAdyacencia.Remove(arco);
                            break;
                        }
                    }
                }
                nuevoV = true;
                nuevoArco = true;
                CBArco.SelectedIndex = -1;
                Simulador.Refresh();
            }
            else
            {
                lblRespuesta.Text = "Seleccione un arco";
                lblRespuesta.ForeColor = Color.Red;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CBNodoPartida.SelectedIndex > -1)
            {
                profundidad = true;
                origen = CBNodoPartida.SelectedItem.ToString();
                Simulador.Refresh();
                CBNodoPartida.SelectedIndex = -1;
            }
            else
            {
                lblRespuesta.Text = "Seleccione un nodo de partida";
                lblRespuesta.ForeColor = Color.Red;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (CBNodoPartida.SelectedIndex > -1)
            {
                origen = CBNodoPartida.SelectedItem.ToString();
                anchura = true;
                Simulador.Refresh();
                CBNodoPartida.SelectedIndex = -1;
            }
            else
            {
                lblRespuesta.Text = "Seleccione un nodo de partida";
                lblRespuesta.ForeColor = Color.Red;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "")
            {
                if (grafo.BuscarVertice(textBox1.Text) != null)
                {
                    lblRespuesta.Text = "Si se encuentra el vertice " + textBox1.Text;
                    lblRespuesta.ForeColor = Color.Blue;
                }
                else
                {
                    lblRespuesta.Text = "No se encuentra el vertice " + textBox1.Text;
                    lblRespuesta.ForeColor = Color.Red;
                }
            }
        }

        private int totalNodos;
        int[] parent;
        bool[] visitados;

        private void calcularMatricesIniciales()
        {
            nodosRuta = new List<CVertice>();
            totalNodos = grafo.nodos.Count;
            parent = new int[totalNodos];
            visitados = new bool[totalNodos];
            for (int i = 0; i < totalNodos; i++)
            {
                List<int> filaDistancia = new List<int>();
                for (int j = 0; j< totalNodos; j++)
                {
                    if (i == j)
                    {
                        filaDistancia.Add(0);
                    }
                    else
                    {
                        int distancia = -1;
                        for (int k = 0; k< grafo.nodos[i].ListaAdyacencia.Count; k++)
                        {
                            if (grafo.nodos[i].ListaAdyacencia[k].nDestino == grafo.nodos[j])
                            {
                                distancia = grafo.nodos[i].ListaAdyacencia[k].peso;
                            }
                        }
                        filaDistancia.Add(distancia);
                    }
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
        }
        int i = 0;
        private void Tabla()
        {  
            foreach (CVertice aux in grafo.nodos)
            {
                            
            }
        }

        private void Pizzara_Load(object sender, EventArgs e)
        {
            
           
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
                        ventanaArco.Visible = false;
                        ventanaArco.control = false;
                        ventanaArco.ShowDialog();
                        if (ventanaArco.control)
                        {
                            if (grafo.AgregarArco(NodoOrigen, NodoDestino, ventanaArco.dato))
                            {
                                int distancia = ventanaArco.dato;
                                NodoOrigen.ListaAdyacencia.Find(v => v.nDestino == NodoDestino).peso = distancia;
                            }
                            nuevoArco = true;
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
                    ventanaVertice.ShowDialog();
                    numeronodos = grafo.nodos.Count;
                    if (ventanaVertice.control)
                    {
                        if (grafo.BuscarVertice(ventanaVertice.dato) == null)
                        {
                            nuevoNodo.Valor = ventanaVertice.dato;
                            grafo.AgregarVertice(nuevoNodo);
                            Tabla();
                        }
                        else
                        {
                            lblRespuesta.Text = "El nodo" + ventanaVertice.dato + "ya existe en el grafo";
                            lblRespuesta.ForeColor = Color.Red;
                        }
                    }
                    nuevoNodo = null;
                    nuevoV = true;
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
                        nuevoVertice.Text = "Nodo" + NodoOrigen.Valor;
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
