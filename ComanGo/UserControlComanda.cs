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
        // info de quién hace la comanda y de la mesa
        private string nombreMesa;
        private int idMesaActual;
        private int idEmpleadoActual;
        private int idComandaActual;
        private bool modoEdicion;

        private decimal total = 0;

        //

        //lista para productos que ya están
        private List<(int idProducto, string nombre, int cantidad, decimal precio)> productosExistentes = new();
        //lista para los nuevos productos que se añaden
        private List<(int idProducto, string nombre, int cantidad, decimal precio)> productosNuevos = new();

        //constructor
        public UserControlComanda(string nombreMesa, int idMesa, int idEmpleado, int idComanda = 0, bool modoEdicion = false)
        {
            InitializeComponent();

            this.nombreMesa = nombreMesa;
            this.idMesaActual = idMesa;
            this.idEmpleadoActual = idEmpleado;
            this.modoEdicion = modoEdicion;
            this.idComandaActual = idComanda;

            lblTitulo.Text = $"Comanda - {nombreMesa}";
            lblTotal.Text = "Total: 0.00 €";

            //método para cargar los productos en el selector(comboBox)
            CargarProductosDesdeBD();

            if (modoEdicion)
            {
                //si estamos editando, cargar comanda
                CargarComandaExistente();
            }
            else
            {
                //si es nueva, se crea la comanda
                CrearNuevaComanda();
            }
        }

        /// <summary>
        /// Método para rellenar el comboBox con los productos que hay en la BD
        /// </summary>
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

        /// <summary>
        /// Si se está modificando la comanda una vez creada se cargan los productos nuevos de la comanda
        /// </summary>
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

            productosExistentes.Clear();
            lstProductos.Items.Clear();
            total = 0;

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


        /// <summary>
        /// Crear una comanda nueva
        /// </summary>
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

        /// <summary>
        /// Después de agregar o eliminar se actualiza la lista de productos
        /// </summary>
        private void ActualizarListaProductos()
        {
            lstProductos.Items.Clear();

            foreach (var p in productosExistentes)
            {
                decimal subtotal = p.precio * p.cantidad;
                lstProductos.Items.Add($"{p.nombre} x{p.cantidad} - {subtotal:0.00} €");
            }

            foreach (var p in productosNuevos)
            {
                decimal subtotal = p.precio * p.cantidad;
                lstProductos.Items.Add($"{p.nombre} x{p.cantidad} - {subtotal:0.00} €");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cbProductos.SelectedItem is not ValueTuple<int, string, decimal> seleccionado)
            {
                MessageBox.Show("Error al obtener el producto.");
                return;
            }

            int idProducto = seleccionado.Item1;
            string nombreProducto = seleccionado.Item2;
            decimal precio = seleccionado.Item3;
            int cantidadNueva = (int)nudCantidad.Value;

            

            //Comprobar si ya estaba ese producto en la comanda
            var existente = productosExistentes.FirstOrDefault(p => p.idProducto == idProducto);
            if (existente != default)
            {
                productosExistentes.Remove(existente);
                productosExistentes.Add((idProducto, nombreProducto, existente.cantidad + cantidadNueva, precio));
            }
            else
            {
                // Si no está en productosExistentes, revisar si ya estaba en nuevos
                var nuevo = productosNuevos.FirstOrDefault(p => p.idProducto == idProducto);
                if (nuevo != default)
                {
                    productosNuevos.Remove(nuevo);
                    productosNuevos.Add((idProducto, nombreProducto, nuevo.cantidad + cantidadNueva, precio));
                }
                else
                {
                    productosNuevos.Add((idProducto, nombreProducto, cantidadNueva, precio));
                }
            }

            total += cantidadNueva * precio;
            ActualizarListaProductos();
            lblTotal.Text = $"Total: {total:0.00} €";
            nudCantidad.Value = 1;


        }

        /// <summary>
        /// Guardar los productos añadidos en la comanda pero se mantiene Activa no se finaliza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (idComandaActual == 0)
            {
                MessageBox.Show("Error al guardar la comanda.");
                return;
            }

            if (lstProductos.Items.Count == 0)
            {
                MessageBox.Show("No se ha añadido ningún producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            //insertar los nuevos productos
            foreach (var p in productosNuevos)
            {
                var checkCmd = new MySqlCommand(
                    "SELECT Cantidad FROM DetalleComanda WHERE IdComanda = @com AND IdProducto = @prod", conn);
                checkCmd.Parameters.AddWithValue("@com", idComandaActual);
                checkCmd.Parameters.AddWithValue("@prod", p.idProducto);

                var result = checkCmd.ExecuteScalar();

                if (result != null)
                {
                    // Si ya estaba, actualizamos la cantidad (sumamos)
                    int cantidadAnterior = Convert.ToInt32(result);
                    int nuevaCantidad = cantidadAnterior + p.cantidad;

                    var updateCmd = new MySqlCommand(
                        "UPDATE DetalleComanda SET Cantidad = @cant WHERE IdComanda = @com AND IdProducto = @prod", conn);
                    updateCmd.Parameters.AddWithValue("@cant", nuevaCantidad);
                    updateCmd.Parameters.AddWithValue("@com", idComandaActual);
                    updateCmd.Parameters.AddWithValue("@prod", p.idProducto);
                    updateCmd.ExecuteNonQuery();
                }
                else
                {
                    // Si no estaba, lo insertamos como nuevo
                    var insertCmd = new MySqlCommand(
                        "INSERT INTO DetalleComanda (IdComanda, IdProducto, Cantidad) VALUES (@com, @prod, @cant)", conn);
                    insertCmd.Parameters.AddWithValue("@com", idComandaActual);
                    insertCmd.Parameters.AddWithValue("@prod", p.idProducto);
                    insertCmd.Parameters.AddWithValue("@cant", p.cantidad);
                    insertCmd.ExecuteNonQuery();
                }
            }

            //actualizar productos que ya estaban
            foreach (var p in productosExistentes)
            {
                var cmdUpdate = new MySqlCommand(
                    "UPDATE DetalleComanda SET Cantidad = @cant WHERE IdComanda = @com AND IdProducto = @prod", conn);
                cmdUpdate.Parameters.AddWithValue("@cant", p.cantidad);
                cmdUpdate.Parameters.AddWithValue("@com", idComandaActual);
                cmdUpdate.Parameters.AddWithValue("@prod", p.idProducto);
                cmdUpdate.ExecuteNonQuery();
            }
            // Limpiar listas
            productosExistentes.Clear();
            productosNuevos.Clear();
            lstProductos.Items.Clear();
            total = 0;
            lblTotal.Text = "Total: 0.00 €";

            MessageBox.Show("Comanda finalizada correctamente.");

            


        }

        /// <summary>
        /// Cerrar la comanda, esto sería ya en estado finalizada no se puede modificar más
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnCerrarComanda_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Seguro que quieres cerrar esta comanda?", "Cerrar comanda", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("UPDATE Comandas SET Estado = 'Finalizada' WHERE IdComanda = @id", conn);
            cmd.Parameters.AddWithValue("@id", idComandaActual);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Comanda cerrada.");

            //Volver al panel de mesas para ver las mesas actualizadas
            var mesas = new UserControlMesas(idEmpleadoActual);
            ((FormMenu)this.ParentForm).CargarEnPanel(mesas);
        }

        /// <summary>
        /// Eliminar un producto de la lista de la comanda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBorrarPrdComanda_Click(object sender, EventArgs e)
        {
            if (lstProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un producto para eliminar.");
                return;
            }

            string textoProducto = lstProductos.SelectedItem.ToString();
            string nombre = textoProducto.Split('x')[0].Trim();

            //Eliminar productos existentes
            var prodExistente = productosExistentes.FirstOrDefault(p => p.nombre == nombre);
            if (prodExistente != default)
            {
                productosExistentes.Remove(prodExistente);
                total -= prodExistente.precio * prodExistente.cantidad;
            }
            else
            {
                // Si no estaba en los existentes, buscar en nuevos
                var prodNuevo = productosNuevos.FirstOrDefault(p => p.nombre == nombre);
                if (prodNuevo != default)
                {
                    productosNuevos.Remove(prodNuevo);
                    total -= prodNuevo.precio * prodNuevo.cantidad;
                }
            }

            // Actualizar la interfaz
            ActualizarListaProductos();
            lblTotal.Text = $"Total: {total:0.00} €";
        }
    }
}
