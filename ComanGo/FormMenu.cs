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
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }
        public FormMenu(Point posicionFormulario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = posicionFormulario;
        }

        public void CargarEnPanel(Control control)
        {
            panelContenido.Controls.Clear();      // Limpia lo que haya antes
            control.Dock = DockStyle.Fill;        // Ocupa todo el panel
            panelContenido.Controls.Add(control); // Añade el nuevo UserControl
        }
        private void FormMenu_Load(object sender, EventArgs e)
        {

        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            CargarEnPanel(new UserControlMesas());
        }
    }
}
