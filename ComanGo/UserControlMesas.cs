using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComanGo
{
    public partial class UserControlMesas : UserControl
    {
        private int numMesa = 1;
        public UserControlMesas()
        {
            InitializeComponent();
        }

        private void btnAgregarMesa_Click(object sender, EventArgs e)
        {
            // Crear un nuevo botón que represente una mesa
            Button btnMesa = new Button();
            btnMesa.Text = $"Mesa {numMesa}";
            btnMesa.Size = new Size(100, 60);
            btnMesa.Margin = new Padding(10);
            btnMesa.BackColor = Color.Bisque;
            btnMesa.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnMesa.Tag = numMesa; // útil para identificarla luego
            btnMesa.Click += Mesa_Click;

            // Añadirlo al panel
            panelMesas.Controls.Add(btnMesa);
            numMesa++;

        }

        private void Mesa_Click(object sender, EventArgs e)
        {
            // Cast del botón que fue pulsado
            Button mesa = sender as Button;
            string nombreMesa = mesa.Text;

            // Acción al hacer clic
            MessageBox.Show($"Abrir comanda para {nombreMesa}", "Mesa seleccionada");

            ((FormMenu)this.ParentForm).CargarEnPanel(new UserControlComanda(nombreMesa));
        }
    }
}
