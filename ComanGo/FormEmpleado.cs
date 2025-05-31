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
    public partial class FormEmpleado : Form
    {
        private int? idEmpleado = null;

        public FormEmpleado()
        {
            InitializeComponent();
            cbRol.Items.AddRange(new string[] { "admin", "empleado" });
            cbRol.SelectedIndex = 1; // "empleado" por defecto
            lblTitulo.Text = "Agregar Empleado";
        }

        public FormEmpleado(int id, string nombre, string usuario, string rol)
            : this()
        {
            idEmpleado = id;
            txtNombre.Text = nombre;
            txtUsuario.Text = usuario;
            cbRol.SelectedItem = rol;
            lblTitulo.Text = "Editar Empleado";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContraseña.Text.Trim();
            string rol = cbRol.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(contrasena) || string.IsNullOrWhiteSpace(rol))
            {
                MessageBox.Show("Completa todos los campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = new MySqlConnection(Conexion.ConnectionString);
                conn.Open();

                if (idEmpleado == null)
                {
                    var insert = new MySqlCommand("INSERT INTO Empleados (Nombre, Usuario, Contrasena, Rol) VALUES (@n, @u, @c, @r)", conn);
                    insert.Parameters.AddWithValue("@n", nombre);
                    insert.Parameters.AddWithValue("@u", usuario);
                    insert.Parameters.AddWithValue("@c", contrasena);
                    insert.Parameters.AddWithValue("@r", rol);
                    insert.ExecuteNonQuery();
                    MessageBox.Show("Empleado agregado.");
                }
                else
                {
                    var update = new MySqlCommand("UPDATE Empleados SET Nombre=@n, Usuario=@u, Contrasena=@c, Rol=@r WHERE IdEmpleado=@id", conn);
                    update.Parameters.AddWithValue("@n", nombre);
                    update.Parameters.AddWithValue("@u", usuario);
                    update.Parameters.AddWithValue("@c", contrasena);
                    update.Parameters.AddWithValue("@r", rol);
                    update.Parameters.AddWithValue("@id", idEmpleado);
                    update.ExecuteNonQuery();
                    MessageBox.Show("Empleado actualizado.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                    MessageBox.Show("El usuario ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Error SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    
    }
}
