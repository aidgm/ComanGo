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
        private bool esAdmin;

        public UserControlEmpleados(bool usuarioEsAdmin)
        {
            InitializeComponent();
            esAdmin = usuarioEsAdmin;
            btnEliminar.Enabled = esAdmin;
            CargarEmpleados();
        }

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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var form = new FormEmpleado(); // Ventana para crear/editar
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarEmpleados();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un empleado para editar.");
                return;
            }

            var fila = dgvEmpleados.SelectedRows[0];
            int id = Convert.ToInt32(fila.Cells["IdEmpleado"].Value);
            string nombre = fila.Cells["Nombre"].Value.ToString();
            string usuario = fila.Cells["Usuario"].Value.ToString();
            string rol = fila.Cells["Rol"].Value.ToString();

            var form = new FormEmpleado(id, nombre, usuario, rol);
            if (form.ShowDialog() == DialogResult.OK)
                CargarEmpleados();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un empleado para eliminar.");
                return;
            }

            int id = Convert.ToInt32(dgvEmpleados.SelectedRows[0].Cells["IdEmpleado"].Value);

            if (MessageBox.Show("¿Eliminar este empleado?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
