using MySql.Data.MySqlClient;
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
    public partial class UserControlEmpleados : UserControl
    {
        //guardar si el usuario que ha iniciado es admin para que tenga una serie de permisos
        private bool esAdmin;

        public UserControlEmpleados(bool usuarioEsAdmin)
        {
            InitializeComponent();
            esAdmin = usuarioEsAdmin;
            //solo los admin pueden eliminar empleados
            //para el resto se desactiva ese botón
            btnEliminar.Enabled = esAdmin;
            CargarEmpleados();
        }

        /// <summary>
        /// Mostrar los empleados que están en la BD en la tabla
        /// </summary>
        private void CargarEmpleados()
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            string query = "SELECT IdEmpleado, Nombre, Usuario, Rol FROM Empleados";
            var adapter = new MySqlDataAdapter(query, conn);
            var dt = new DataTable();
            adapter.Fill(dt);
            dgvEmpleados.DataSource = dt;
        }
        /// <summary>
        /// Al pulsar el botón Agregar se abre el formulario FormEmpleado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var form = new FormEmpleado(); // Form para crear/editar
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarEmpleados();
            }
        }
        /// <summary>
        /// Editar un empleado, seleccionando en el DataGrid y dándole al botón en el cual se desplegará el Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un empleado para editar.");
                return;
            }

            //Recoger los datos de la fila seleccionada
            var fila = dgvEmpleados.SelectedRows[0];
            int id = Convert.ToInt32(fila.Cells["IdEmpleado"].Value);
            string nombre = fila.Cells["Nombre"].Value.ToString();
            string usuario = fila.Cells["Usuario"].Value.ToString();
            string rol = fila.Cells["Rol"].Value.ToString();

            var form = new FormEmpleado(id, nombre, usuario, rol);
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarEmpleados();
            }
                
        }

        /// <summary>
        /// Eliminar el empleado seleccionando también en el DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un empleado para eliminar.");
                return;
            }

            int id = Convert.ToInt32(dgvEmpleados.SelectedRows[0].Cells["IdEmpleado"].Value);

            var confirmacion = MessageBox.Show("¿Seguro qué quieres eliminar este empleado?", "Confirmar", MessageBoxButtons.YesNo);

            if (confirmacion == DialogResult.Yes)
            {
                using var conn = new MySqlConnection(Conexion.ConnectionString);
                conn.Open();

                var cmd = new MySqlCommand("DELETE FROM Empleados WHERE IdEmpleado = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                CargarEmpleados();
            }
        }
    }
}
