using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComanGo
{
    public partial class FormEmpleado : Form
    {
        //saber si se estña editando o creando
        private int? idEmpleado = null;

        public FormEmpleado()
        {
            InitializeComponent();
            if (cbRol.Items.Count == 0) //Evitar duplicados
            {
                cbRol.Items.AddRange(new string[] { "admin", "empleado" });
            }
            cbRol.SelectedIndex = 1; // "empleado" por defecto
            lblTitulo.Text = "Agregar Empleado";
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="usuario"></param>
        /// <param name="rol"></param>
        public FormEmpleado(int id, string nombre, string usuario, string rol)
            : this()
        {
            idEmpleado = id;
            txtNombre.Text = nombre;
            txtUsuario.Text = usuario;
            cbRol.SelectedItem = rol;
            lblTitulo.Text = "Editar Empleado";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Recibir lo que escribe el usuario
            string nombre = txtNombre.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContraseña.Text.Trim();
            string rol = cbRol.SelectedItem?.ToString() ?? "";

            //comprobar que estén todos los campos llenos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(contrasena) || string.IsNullOrWhiteSpace(rol))
            {
                MessageBox.Show("Completa todos los campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //validar que el nombre solo tenga letras
            if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                MessageBox.Show("El nombre solo puede contener letras y espacios.", "Nombre no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = new MySqlConnection(Conexion.ConnectionString);
                conn.Open();

                //si el id es nulo, es que es nuevo empleado si no se crea
                if (idEmpleado == null)
                {
                    var insert = new MySqlCommand("INSERT INTO Empleados (Nombre, Usuario, Contrasena, Rol) VALUES (@nombre, @usuario, @contraseña, @rol)", conn);
                    insert.Parameters.AddWithValue("@nombre", nombre);
                    insert.Parameters.AddWithValue("@usuario", usuario);
                    insert.Parameters.AddWithValue("@contraseña", contrasena);
                    insert.Parameters.AddWithValue("@rol", rol);
                    insert.ExecuteNonQuery();
                }
                else
                {
                    var update = new MySqlCommand("UPDATE Empleados SET Nombre=@nombre, Usuario=@usuario, Contrasena=@contraseña, Rol=@rol WHERE IdEmpleado=@id", conn);
                    update.Parameters.AddWithValue("@nombre", nombre);
                    update.Parameters.AddWithValue("@usuario", usuario);
                    update.Parameters.AddWithValue("@contraseña", contrasena);
                    update.Parameters.AddWithValue("@rol", rol);
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
    }
}
