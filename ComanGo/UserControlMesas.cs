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
    public partial class UserControlMesas : UserControl
    {
        private int idEmpleadoActual;

        //constructor donde se recibe el id del empleado que inicia sesión
        public UserControlMesas(int idEmpleado)
        {
            InitializeComponent();
            this.idEmpleadoActual = idEmpleado;
            CargarMesas();
        }

        //Se cargan los botones (mesas) desde la BD
        private void CargarMesas()
        {
            panelMesas.Controls.Clear(); 

            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            //listado de mesas
            var cmd = new MySqlCommand("SELECT IdMesa, NombreMesa FROM Mesas", conn);
            var reader = cmd.ExecuteReader();


            var mesas = new List<(int id, string nombre)>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["IdMesa"]);
                string nombre = reader["NombreMesa"].ToString();
                mesas.Add((id, nombre));
            }

            reader.Close();

            // Crear botón (mesa) 
            foreach (var mesa in mesas)
            {
                Button btnMesa = new Button();
                btnMesa.Text = mesa.nombre;
                btnMesa.Size = new Size(100, 60);
                btnMesa.Margin = new Padding(10);
                btnMesa.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btnMesa.Tag = mesa.id;
                btnMesa.Click += Mesa_Click;

                // Consultar si tiene comanda abierta
                var cmdCheck = new MySqlCommand("SELECT COUNT(*) FROM Comandas WHERE IdMesa = @id AND Estado = 'Abierta'", conn);
                cmdCheck.Parameters.AddWithValue("@id", mesa.id);
                int activas = Convert.ToInt32(cmdCheck.ExecuteScalar());

                // Si tiene comanda activa se pone en rojo que sería ocupada y si no en verde (libre)
                btnMesa.BackColor = activas > 0 ? Color.LightCoral : Color.LightGreen;

                panelMesas.Controls.Add(btnMesa);
            }

        } 
        
        // Método para "pulsar una mesa"
        private void Mesa_Click(object sender, EventArgs e)
        {
            Button mesa = sender as Button;
            string nombreMesa = mesa.Text;
            int idMesa = (int)mesa.Tag;

            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            //Asegurarse que no hay una comanda abierta en esa mesa
            var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM Comandas WHERE IdMesa = @id AND Estado = 'Abierta'", conn);
            cmd.Parameters.AddWithValue("@id", idMesa);

            int ComandaAbierta = Convert.ToInt32(cmd.ExecuteScalar());

            if (ComandaAbierta > 0)
            {
                MessageBox.Show($"La mesa '{nombreMesa}' ya tiene una comanda abierta.", "Mesa ocupada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //si está libre se abre el usercontrolComanda para crear la comanda
            var comanda = new UserControlComanda(nombreMesa, idMesa, idEmpleadoActual);
            ((FormMenu)this.ParentForm).CargarEnPanel(comanda);

        }

        //CRUD (crear, eliminar)
        private void btnAgregarMesa_Click(object sender, EventArgs e)
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            // Obtener el numero que le corresponde a la nueva mesa
            var cmdCount = new MySqlCommand("SELECT COUNT(*) FROM Mesas", conn);
            int count = Convert.ToInt32(cmdCount.ExecuteScalar());

            string nombreMesa = $"Mesa {count + 1}";

            var cmd = new MySqlCommand("INSERT INTO Mesas (NombreMesa) VALUES (@nombre)", conn);
            cmd.Parameters.AddWithValue("@nombre", nombreMesa);
            cmd.ExecuteNonQuery();

            //Recargar mesas
            CargarMesas();

        }
        /// <summary>
        /// Solo se puede eliminar una mesa si no tiene comanda asociada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (panelMesas.Controls.Count == 0)
            {
                MessageBox.Show("No hay mesas que eliminar.");
                return;
            }

            Button ultimaMesa = panelMesas.Controls.OfType<Button>().Last();
            int idMesa = (int)ultimaMesa.Tag;

            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            //Antes de eliminar comporbar que no haya ninguna comanda 
            //Evitar que se borren mesas con comandas abiertas
            var cmdCheck = new MySqlCommand("SELECT COUNT(*) FROM Comandas WHERE IdMesa = @id AND Estado = 'Abierta'", conn);
            cmdCheck.Parameters.AddWithValue("@id", idMesa);
            int total = Convert.ToInt32(cmdCheck.ExecuteScalar());

            if (total > 0)
            {
                MessageBox.Show("No se puede eliminar esta mesa porque tiene comandas asociadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Eliminar la base de datos
            var cmd = new MySqlCommand("DELETE FROM Mesas WHERE IdMesa = @id", conn);
            cmd.Parameters.AddWithValue("@id", idMesa);
            cmd.ExecuteNonQuery();

            //Volver a recargar
            CargarMesas();
        }

    }
}
