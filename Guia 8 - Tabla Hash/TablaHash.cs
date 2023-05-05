using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Guia_8___Tabla_Hash
{
    public partial class TablaHash : Form
    {
        String word, def;
        Dictionary<String, String> Diccionario = new Dictionary<string, string>();

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            word = txtPalabra.Text.Trim().ToUpper();
            def = txtDefinicion.Text;
            if (!string.IsNullOrEmpty(word))
            {
                if (!string.IsNullOrEmpty(def))
                {
                    try
                    {
                        Diccionario.Add(word, def);
                        lstEliminar.DataSource = new List<string>(this.Diccionario.Keys);
                        lstVer.DataSource = new List<string>(Diccionario.Keys);
                        txtPalabra.Clear();
                        txtDefinicion.Clear();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Palabra ya existente");
                    }
                }
                else
                {
                    MessageBox.Show("Definicion no ingresada porfavor ingresar una");
                }
            }
            else
            {
                MessageBox.Show("Palabra no ingresada porfavor ingresar una.");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            word = txtBuscar.Text.Trim().ToUpper();
            String Mostar, Resultado;
            if (!Diccionario.ContainsKey(word))
            {
                MessageBox.Show("No existe esa palabra.");
            }
            else
            {
                Diccionario.TryGetValue(word, out Resultado);
                Mostar = Resultado.ToString();
                txtRespuesta.Text = word + ":\t" + Mostar;
            }
            txtBuscar.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            word = txtEliminar.Text.Trim().ToUpper();
            if (!Diccionario.ContainsKey(word))
            {
                MessageBox.Show("No existe esa palabra.");
            }
            else
            {
                Diccionario.Remove(word);
                lstEliminar.DataSource = new List<string>(this.Diccionario.Keys);
                lstVer.DataSource = new List<string>(Diccionario.Keys);
            }
            txtEliminar.Clear();
        }

        private void lstVer_SelectedValueChanged(object sender, EventArgs e)
        {
            String Resultado, Mostar;
            txtVerPalabra.Text = lstVer.Text.Trim();
            Diccionario.TryGetValue(lstVer.Text.Trim(), out Resultado);
            Mostar = Resultado.ToString();
            txtVerDefinicion.Text = Mostar;
        }

        public TablaHash()
        {
            InitializeComponent();
        }
    }
}
