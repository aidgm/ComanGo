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
    public partial class UserControlHistorialComandacs : UserControl
    {
        public UserControlHistorialComandacs()
        {
            InitializeComponent();
            CargarComandas();

            dgvComandas.CellContentClick += dgvComandas_CellContentClick;
            dgvComandas.CellValueChanged += dgvComandas_CellValueChanged;
            dgvComandas.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvComandas.IsCurrentCellDirty)
                    dgvComandas.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

        }
        private void dgvComandas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvComandas.Columns[e.ColumnIndex].Name == "Estado")
            {
                int idComanda = Convert.ToInt32(dgvComandas.Rows[e.RowIndex].Cells["IdComanda"].Value);
                string nuevoEstado = dgvComandas.Rows[e.RowIndex].Cells["Estado"].Value.ToString();

                using var conn = new MySqlConnection(Conexion.ConnectionString);
                conn.Open();

                var cmd = new MySqlCommand("UPDATE Comandas SET Estado = @estado WHERE IdComanda = @id", conn);
                cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                cmd.Parameters.AddWithValue("@id", idComanda);
                cmd.ExecuteNonQuery();
            }
        }

        private void dgvComandas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string estado = dgvComandas.Rows[e.RowIndex].Cells["Estado"].Value.ToString();
                if (estado != "Abierta")
                {
                    MessageBox.Show("Esta comanda ya está finalizada.");
                    return;
                }

                int idComanda = Convert.ToInt32(dgvComandas.Rows[e.RowIndex].Cells["IdComanda"].Value);
                string nombreMesa = dgvComandas.Rows[e.RowIndex].Cells["NombreMesa"].Value.ToString();
                int idEmpleado = Conexion.IdUsuarioActual;

                // Necesitamos también el IdMesa
                int idMesa = ObtenerIdMesaDesdeNombre(nombreMesa); // lo añades abajo
                var comanda = new UserControlComanda(nombreMesa, idMesa, idEmpleado, idComanda, modoEdicion: true);
                ((FormMenu)this.ParentForm).CargarEnPanel(comanda);

            }
        }

        private int ObtenerIdMesaDesdeNombre(string nombreMesa)
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT IdMesa FROM Mesas WHERE NombreMesa = @nombre", conn);
            cmd.Parameters.AddWithValue("@nombre", nombreMesa);
            var result = cmd.ExecuteScalar();

            return result != null ? Convert.ToInt32(result) : 0;
        }




        private void CargarComandas()
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            var query = @"
                SELECT c.IdComanda, m.NombreMesa, e.Nombre AS Empleado, c.Fecha, c.Estado
                FROM Comandas c
                JOIN Mesas m ON c.IdMesa = m.IdMesa
                JOIN Empleados e ON c.IdEmpleado = e.IdEmpleado
                ORDER BY c.Fecha DESC";

            var adapter = new MySqlDataAdapter(query, conn);
            var dt = new DataTable();
            adapter.Fill(dt);
            dgvComandas.DataSource = dt;

            if (dgvComandas.Columns.Contains("Estado"))
            {
                // Guardar el índice de la columna actual Estado
                int index = dgvComandas.Columns["Estado"].Index;
                dgvComandas.Columns.Remove("Estado");

                // Crear ComboBoxColumn
                var combo = new DataGridViewComboBoxColumn();
                combo.HeaderText = "Estado";
                combo.Name = "Estado";
                combo.DataPropertyName = "Estado";
                combo.DataSource = new string[] { "Abierta", "Finalizada" };

                // Reemplazar columna

                dgvComandas.Columns.Insert(index, combo);
            }

            dgvComandas.CellValueChanged += dgvComandas_CellValueChanged;
            dgvComandas.CellDoubleClick += dgvComandas_CellDoubleClick;



        }

        private void dgvComandas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idComanda = Convert.ToInt32(dgvComandas.Rows[e.RowIndex].Cells["IdComanda"].Value);
                MostrarDetalleComanda(idComanda);
            }
        }

        private void MostrarDetalleComanda(int idComanda)
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            var query = @"
                SELECT p.Nombre AS Producto, dc.Cantidad, p.Precio, (p.Precio * dc.Cantidad) AS Subtotal
                FROM DetalleComanda dc
                JOIN Productos p ON dc.IdProducto = p.IdProducto
                WHERE dc.IdComanda = @id";

            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", idComanda);

            var adapter = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            dgvDetalle.DataSource = dt;
        }

        private void lblHistorial_Click(object sender, EventArgs e)
        {

        }
    }
}
