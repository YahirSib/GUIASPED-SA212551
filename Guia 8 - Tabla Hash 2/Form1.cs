using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guia_8___Tabla_Hash_2
{
    public partial class Form1 : Form
    {
        String DUI, Nombre;
        Dictionary<String,String> Personas = new Dictionary<String,String>();

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DUI = txtDui.Text.Trim();
            Nombre = txtNombreCompleto.Text;
            if (!string.IsNullOrEmpty(DUI))
            {
                if (!string.IsNullOrEmpty(Nombre))
                {
                    if (Regex.IsMatch(DUI, Pat))
                    {
                        Personas.Add(DUI, Nombre);
                        lstVer.DataSource = new List<String>(this.Personas.Keys);
                        lstEliminar.DataSource = new List<String>(this.Personas.Keys);
                        lstDuiEditar.DataSource = new List<String>(this.Personas.Keys);
                        txtDui.Clear();
                        txtNombreCompleto.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Formato incorrecto (XXXXXXXX-X)");
                    }
                }
                else
                {
                   MessageBox.Show("Nombre Completo no ingresado");
                }
            }
            else
            {
                MessageBox.Show("DUI no ingresado");
            }
        }



        private const string Pat = "^\\d{8}-\\d$";

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            String nombreNuevo;
            nombreNuevo = txtNombreCompletoEditar.Text;
            if (!string.IsNullOrEmpty(nombreNuevo) && lstDuiEditar.Items.Count > 0)
            {
                Personas[txtDuiEditar.Text] = nombreNuevo;
                MessageBox.Show("Actualizado con exito");
            }
            else
            {
                MessageBox.Show("Datos incorrectos");
            }

            txtNombreCompletoEditar.Clear();
            txtDuiEditar.Clear();
        }

        private void lstDuiEditar_SelectedValueChanged(object sender, EventArgs e)
        {
            String Resultado, Mostar;
            txtDuiEditar.Text = lstDuiEditar.Text.Trim();
            Personas.TryGetValue(lstDuiEditar.Text.Trim(), out Resultado);
            Mostar = Resultado.ToString();
            txtNombreCompletoEditar.Text = Mostar;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DUI = txtEliminar.Text;
            if (!Personas.ContainsKey(DUI))
            {
               MessageBox.Show("DUI inexistente");
            }
            else
            {
                Personas.Remove(DUI);
                lstVer.DataSource = new List<String>(this.Personas.Keys);
                lstEliminar.DataSource = new List<String>(this.Personas.Keys);
                lstDuiEditar.DataSource = new List<String>(this.Personas.Keys);
                MessageBox.Show("Eliminado correctamente");
            }
            txtEliminar.Clear();
        }

        private void lstVer_SelectedValueChanged(object sender, EventArgs e)
        {
            String Resultado, Mostar;
            txtVerPalabra.Text = lstVer.Text.Trim();
            Personas.TryGetValue(lstVer.Text.Trim(), out Resultado);
            Mostar = Resultado.ToString();
            txtVerDefinicion.Text = Mostar;
        }

        private void lstEliminar_SelectedValueChanged(object sender, EventArgs e)
        {
            txtEliminar.Text = lstEliminar.Text.Trim();
        }

        public Form1()
        {
            InitializeComponent();
        }
    }
}
