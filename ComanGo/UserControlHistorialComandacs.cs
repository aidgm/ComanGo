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
    }
}
