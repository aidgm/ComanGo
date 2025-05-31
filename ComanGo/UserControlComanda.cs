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

        private List<(string nombre, int cantidad, decimal precio)> listaPedido = new();


        // Simulación de productos disponibles
        private Dictionary<string, decimal> productosDisponibles = new Dictionary<string, decimal>
    {
        { "Café", 1.50m },
        { "Tostada", 2.00m },
        { "Zumo", 2.20m },
        { "Croissant", 1.80m }
    };

        public UserControlComanda(string nombreMesa, int idMesa, int idEmpleado)
        {
            InitializeComponent();

            this.nombreMesa = nombreMesa;
            this.idMesaActual = idMesa;
            this.idEmpleadoActual = idEmpleado;

            lblTitulo.Text = $"Comanda - {nombreMesa}";

            cbProductos.DataSource = new BindingSource(productosDisponibles, null);
            cbProductos.DisplayMember = "Key";
            cbProductos.ValueMember = "Value";

            lblTotal.Text = "Total: 0.00 €";

            CrearNuevaComanda(); // ⬅️ Importante
        }

        private void CrearNuevaComanda()
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(
                "INSERT INTO Comandas (IdEmpleado, IdMesa, Estado) VALUES (@emp, @mesa, 'Finalizada'); SELECT LAST_INSERT_ID();",
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
            if (cbProductos.SelectedItem == null) return;

            string nombreProducto = ((KeyValuePair<string, decimal>)cbProductos.SelectedItem).Key;
            decimal precioUnitario = ((KeyValuePair<string, decimal>)cbProductos.SelectedItem).Value;
            int cantidad = (int)nudCantidad.Value;

            decimal subtotal = precioUnitario * cantidad;
            total += subtotal;

            // Mostrar en la lista
            lstProductos.Items.Add($"{nombreProducto} x{cantidad} - {subtotal:0.00} €");

            listaPedido.Add((nombreProducto, cantidad, precioUnitario));


            // Actualizar total
            lblTotal.Text = $"Total: {total:0.00} €";

            // Reset cantidad
            nudCantidad.Value = 1;

        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (lstProductos.Items.Count == 0)
            {
                MessageBox.Show("No se ha añadido ningún producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Comanda de {nombreMesa} finalizada.\nTotal: {total:0.00} €", "Comanda cerrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            foreach (var item in listaPedido)
            {
                var cmd = new MySqlCommand(
                    "INSERT INTO DetalleComanda (IdComanda, IdProducto, Cantidad) " +
                    "VALUES (@com, (SELECT IdProducto FROM Productos WHERE Nombre = @nom), @cant)", conn);
                cmd.Parameters.AddWithValue("@com", idComandaActual);
                cmd.Parameters.AddWithValue("@nom", item.nombre);
                cmd.Parameters.AddWithValue("@cant", item.cantidad);
                cmd.ExecuteNonQuery();
            }

            listaPedido.Clear();

            // Aquí podrías guardar en base de datos o archivo CSV en el futuro
            lstProductos.Items.Clear();
            total = 0;
            lblTotal.Text = "Total: 0.00 €";
        }
    }
}
