namespace Guia10___Grafos
{
    partial class Pizzara
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Simulador = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.CMSCrearVertice = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoVertice = new System.Windows.Forms.ToolStripMenuItem();
            this.CMSCrearVertice.SuspendLayout();
            this.SuspendLayout();
            // 
            // Simulador
            // 
            this.Simulador.Location = new System.Drawing.Point(12, 56);
            this.Simulador.Name = "Simulador";
            this.Simulador.Size = new System.Drawing.Size(765, 301);
            this.Simulador.TabIndex = 0;
            this.Simulador.Paint += new System.Windows.Forms.PaintEventHandler(this.Simulador_Paint);
            this.Simulador.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Simulador_MouseDown);
            this.Simulador.MouseLeave += new System.EventHandler(this.Simulador_MouseLeave);
            this.Simulador.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Simulador_MouseMove);
            this.Simulador.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Simulador_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(341, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Simulador de Grafos";
            // 
            // CMSCrearVertice
            // 
            this.CMSCrearVertice.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoVertice});
            this.CMSCrearVertice.Name = "contextMenuStrip1";
            this.CMSCrearVertice.Size = new System.Drawing.Size(181, 48);
           //
            // nuevoVertice
            // 
            this.nuevoVertice.Name = "nuevoVertice";
            this.nuevoVertice.Size = new System.Drawing.Size(180, 22);
            this.nuevoVertice.Text = "Nuevo Vertice";
            this.nuevoVertice.Click += new System.EventHandler(this.nuevoVertice_Click);
            // 
            // Pizzara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 369);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Simulador);
            this.Name = "Pizzara";
            this.Text = " Grafos";
            this.CMSCrearVertice.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Simulador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip CMSCrearVertice;
        private System.Windows.Forms.ToolStripMenuItem nuevoVertice;
    }
}

