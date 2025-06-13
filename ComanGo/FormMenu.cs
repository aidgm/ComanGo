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
        private int idEmpleadoActual; //id del empleado que inicia sesión

        //constructor donde se guarda el id y se carga el diseño del form
        public FormMenu(int idEmpleado)
        {
            InitializeComponent();
            this.idEmpleadoActual = idEmpleado;
        }
        
        //Aquí cargan todos los UserControl 
        public void CargarEnPanel(Control control)
        {
            panelContenido.Controls.Clear();      // Limpia lo que haya antes
            control.Dock = DockStyle.Fill;        // Ocupa todo el panel
            panelContenido.Controls.Add(control); // Añade el nuevo UserControl
        }


        //Botones
        private void btnMesas_Click(object sender, EventArgs e)
        {
            CargarEnPanel(new UserControlMesas(idEmpleadoActual));
        }

        private void btnComandas_Click(object sender, EventArgs e)
        {
            CargarEnPanel(new UserControlHistorialComandacs());

        }
        private void btnProductos_Click(object sender, EventArgs e)
        {
            CargarEnPanel(new UserControlProducto());
        }
        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            bool esAdmin = Conexion.RolUsuarioActual == "admin"; //comprobar si es admin para acceder a permisos (eliminar empleados)
            CargarEnPanel(new UserControlEmpleados(esAdmin));
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new FormLogin();
            login.Show();
            this.Close();

        }


    }
}
