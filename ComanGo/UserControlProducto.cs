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
    public partial class UserControlProducto : UserControl
    {
        public UserControlProducto()
        {
            InitializeComponent();
            CargarProductos();
        }

        private void CargarProductos()
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            string query = "SELECT IdProducto, Nombre, Precio FROM Productos";
            var adapter = new MySqlDataAdapter(query, conn);
            var dt = new DataTable();
            adapter.Fill(dt);
            dgvProductos.DataSource = dt;
        }
        private void btnAñadir_Click(object sender, EventArgs e)
        {
            var form = new FormProducto(); // Ventana para añadir producto
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarProductos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un producto para eliminar.");
                return;
            }

            int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProducto"].Value);

            if (MessageBox.Show("¿Eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = new MySqlConnection(Conexion.ConnectionString);
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM Productos WHERE IdProducto = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                CargarProductos();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un producto para actualizar.");
                return;
            }

            var fila = dgvProductos.SelectedRows[0];
            int id = Convert.ToInt32(fila.Cells["IdProducto"].Value);
            string nombre = fila.Cells["Nombre"].Value.ToString();
            decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);

            var form = new FormProducto(id, nombre, precio);
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarProductos();
            }
        }
    }
}
