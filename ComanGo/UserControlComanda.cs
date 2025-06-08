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
    public partial class UserControlComanda : UserControl
    {
        private string nombreMesa;
        private decimal total = 0;

        private int idEmpleadoActual = 1; // Reemplaza con valor real si puedes pasarlo
        private int idMesaActual = 0;
        private int idComandaActual = 0;
        private bool modoEdicion;

        private List<(int idProducto, string nombre, int cantidad, decimal precio)> productosExistentes = new();
        private List<(int idProducto, string nombre, int cantidad, decimal precio)> nuevosProductos = new();

        private Dictionary<string, (int idProducto, decimal precio)> productosDisponibles = new();

       

        public UserControlComanda(string nombreMesa, int idMesa, int idEmpleado, int idComanda=0, bool modoEdicion = false)
        {
            InitializeComponent();

            this.nombreMesa = nombreMesa;
            this.idMesaActual = idMesa;
            this.idEmpleadoActual = idEmpleado;
            this.modoEdicion = modoEdicion;
            this.idComandaActual = idComanda;
            lblTitulo.Text = $"Comanda - {nombreMesa}";

            CargarProductosDesdeBD();

            lblTotal.Text = "Total: 0.00 €";

            if (modoEdicion)
            {
                CargarComandaExistente(); // ya debe existir en la base de datos

            }
            else
            {
                CrearNuevaComanda(); // este genera un nuevo ID
            }
                
        }

        private void CargarProductosDesdeBD()
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT IdProducto, Nombre, Precio FROM Productos", conn);
            var reader = cmd.ExecuteReader();
            var items = new List<(int IdProducto, string Nombre, decimal Precio)>();

            while (reader.Read())
            {
                int id = reader.GetInt32("IdProducto");
                string nombre = reader.GetString("Nombre");
                decimal precio = reader.GetDecimal("Precio");

                items.Add((id, nombre, precio));
            }

            cbProductos.DataSource = items;
            cbProductos.DisplayMember = "Nombre";
        }
        private void CargarComandaExistente()
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(@"
                SELECT p.IdProducto, p.Nombre, dc.Cantidad, p.Precio
                FROM DetalleComanda dc
                JOIN Productos p ON dc.IdProducto = p.IdProducto
                WHERE dc.IdComanda = @id", conn);

            cmd.Parameters.AddWithValue("@id", idComandaActual);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int idProducto = reader.GetInt32("IdProducto");
                string nombre = reader.GetString("Nombre");
                int cantidad = reader.GetInt32("Cantidad");
                decimal precio = reader.GetDecimal("Precio");
                decimal subtotal = cantidad * precio;

                lstProductos.Items.Add($"{nombre} x{cantidad} - {subtotal:0.00} €");

                productosExistentes.Add((idProducto, nombre, cantidad, precio));
                total += subtotal;
            }

            lblTotal.Text = $"Total: {total:0.00} €";
        }



        private void CrearNuevaComanda()
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(
                "INSERT INTO Comandas (IdEmpleado, IdMesa, Estado) VALUES (@emp, @mesa, 'Abierta'); SELECT LAST_INSERT_ID();",
                conn);
            cmd.Parameters.AddWithValue("@emp", idEmpleadoActual);
            cmd.Parameters.AddWithValue("@mesa", idMesaActual);

            idComandaActual = Convert.ToInt32(cmd.ExecuteScalar());
        }

        private void UserControlComanda_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cbProductos.SelectedItem is not ValueTuple<int, string, decimal> seleccionado)
            {
                MessageBox.Show("Error al obtener el producto.");
                return;
            }

            int idProducto = seleccionado.Item1;
            string nombreProducto = seleccionado.Item2;
            decimal precioUnitario = seleccionado.Item3;
            int cantidad = (int)nudCantidad.Value;

            // Verificar duplicados
            if (productosExistentes.Any(p => p.idProducto == idProducto) ||
                nuevosProductos.Any(p => p.idProducto == idProducto))
            {
                MessageBox.Show("Este producto ya ha sido agregado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal subtotal = precioUnitario * cantidad;
            total += subtotal;

            nuevosProductos.Add((idProducto, nombreProducto, cantidad, precioUnitario));
            lstProductos.Items.Add($"{nombreProducto} x{cantidad} - {subtotal:0.00} €");

            nudCantidad.Value = 1;
            lblTotal.Text = $"Total: {total:0.00} €";


        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (idComandaActual == 0)
            {
                MessageBox.Show("Error: la comanda no está correctamente inicializada.");
                return;
            }

            if (lstProductos.Items.Count == 0)
            {
                MessageBox.Show("No se ha añadido ningún producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            foreach (var item in nuevosProductos)
            {
                var cmd = new MySqlCommand(
                    "INSERT INTO DetalleComanda (IdComanda, IdProducto, Cantidad) VALUES (@com, @prod, @cant)", conn);
                cmd.Parameters.AddWithValue("@com", idComandaActual);
                cmd.Parameters.AddWithValue("@prod", item.idProducto);
                cmd.Parameters.AddWithValue("@cant", item.cantidad);
                cmd.ExecuteNonQuery();
            }


            nuevosProductos.Clear();
            lstProductos.Items.Clear();
            total = 0;
            lblTotal.Text = "Total: 0.00 €";

        }
    }
}
