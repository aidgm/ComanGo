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
        private int numMesa = 1;
        private int idEmpleadoActual;

        public UserControlMesas(int idEmpleado)
        {
            InitializeComponent();
            this.idEmpleadoActual = idEmpleado;
            CargarMesas(); //  AÑADE ESTA LÍNEA AQUÍ
        }

        private void CargarMesas()
        {
            panelMesas.Controls.Clear(); // Evita duplicados visuales

            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT IdMesa, NombreMesa FROM Mesas", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["IdMesa"]);
                string nombre = reader["NombreMesa"].ToString();

                Button btnMesa = new Button();
                btnMesa.Text = nombre;
                btnMesa.Size = new Size(100, 60);
                btnMesa.Margin = new Padding(10);
                btnMesa.BackColor = Color.Bisque;
                btnMesa.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btnMesa.Tag = id;
                btnMesa.Click += Mesa_Click;

                panelMesas.Controls.Add(btnMesa);
            }
        }


        private void btnAgregarMesa_Click(object sender, EventArgs e)
        {
            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            // Obtener el siguiente número secuencial
            var cmdCount = new MySqlCommand("SELECT COUNT(*) FROM Mesas", conn);
            int count = Convert.ToInt32(cmdCount.ExecuteScalar());

            string nombreMesa = $"Mesa {count + 1}";

            var cmd = new MySqlCommand("INSERT INTO Mesas (NombreMesa) VALUES (@nombre)", conn);
            cmd.Parameters.AddWithValue("@nombre", nombreMesa);
            cmd.ExecuteNonQuery();

            // Recargar mesas
            CargarMesas();

        }

        private void Mesa_Click(object sender, EventArgs e)
        {
            // Cast del botón que fue pulsado
            Button mesa = sender as Button;
            string nombreMesa = mesa.Text;

            // Acción al hacer clic

            int idMesa = Convert.ToInt32(mesa.Tag); // ya lo guardaste al crearla
            var comanda = new UserControlComanda(nombreMesa, idMesa, idEmpleadoActual);
            ((FormMenu)this.ParentForm).CargarEnPanel(comanda);

        }

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

            // Verificar si tiene comandas asociadas
            var cmdCheck = new MySqlCommand("SELECT COUNT(*) FROM Comandas WHERE IdMesa = @id", conn);
            cmdCheck.Parameters.AddWithValue("@id", idMesa);
            int total = Convert.ToInt32(cmdCheck.ExecuteScalar());

            if (total > 0)
            {
                MessageBox.Show("No se puede eliminar esta mesa porque tiene comandas asociadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Eliminar de la base de datos
            var cmd = new MySqlCommand("DELETE FROM Mesas WHERE IdMesa = @id", conn);
            cmd.Parameters.AddWithValue("@id", idMesa);
            cmd.ExecuteNonQuery();

            // Refrescar visual
            CargarMesas();
        }
    }
}
