using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace ComanGo
{
    public partial class FormLogin : Form
    {
        private string connectionString;
        public FormLogin()
        {
            InitializeComponent();
            this.FormClosed += (s, args) => Application.Exit();
            CargarConfProbarCon();
            lblError.Visible = false;
            timerError.Tick += timerError_Tick;

        }

        private void CargarConfProbarCon()
        {
            try
            {
                // 1. Conexión sin base de datos, solo para verificar si ComanGo existe
                string serverConnection = "Server=127.0.0.1;Port=3307;User Id=root;Password=abc123.;";
                using (var conn = new MySqlConnection(serverConnection))
                {
                    conn.Open();


                    // Verificar si la base de datos existe
                    string checkDbQuery = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'ComanGoDB'";
                    using (var cmd = new MySqlCommand(checkDbQuery, conn))
                    {
                        object existe = cmd.ExecuteScalar();
                        if (existe == null)
                        {
                            string createDbQuery = "CREATE DATABASE ComanGo;";
                            new MySqlCommand(createDbQuery, conn).ExecuteNonQuery();
                            MessageBox.Show("Base de datos 'ComanGo' creada correctamente.");
                        }
                    }
                }

                // 2. Guardar la cadena de conexión global con base de datos incluida
                Conexion.ConnectionString = "Server=127.0.0.1;Port=3307;Database=ComanGoDB;User Id=root;Password=abc123.;";

                // 3. Conectarse a la base de datos para verificar y crear tablas
                using (var conn = new MySqlConnection(Conexion.ConnectionString))
                {
                    conn.Open();

                    string checkTables = "SHOW TABLES;";
                    using (var cmd = new MySqlCommand(checkTables, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) return;
                    }

                    string sqlPath = Path.Combine(Application.StartupPath, "ComanGOBD.sql");
                    if (!File.Exists(sqlPath))
                    {
                        MessageBox.Show("No se encontró el archivo ComanGOBD.sql.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string[] comandos = File.ReadAllText(sqlPath).Split(';');
                    foreach (string sql in comandos)
                    {
                        string cleanSql = sql.Trim();
                        if (!string.IsNullOrEmpty(cleanSql))
                        {
                            using var sqlCmd = new MySqlCommand(cleanSql, conn);
                            sqlCmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Tablas creadas correctamente desde el script SQL.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Error al conectar a la base de datos:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void FormLogin_Load(object sender, EventArgs e)
        {


        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContraseña.Text.Trim();

            if (usuario == "" || contrasena == "")
            {
                lblError.Text = "Por favor, completa todos los campos.";
                lblError.Visible = true;
                return;
            }

            try
            {
                using var conn = new MySqlConnection(Conexion.ConnectionString);
                conn.Open();

                string sql = "SELECT * FROM Empleados WHERE Usuario = @usuario AND Contrasena = @contrasena";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@contrasena", contrasena);

                using var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    int idEmpleado = 0;

                    if (reader.Read())
                    {
                        idEmpleado = Convert.ToInt32(reader["IdEmpleado"]);
                        Conexion.UsuarioActual = reader["Usuario"].ToString(); // Guarda el usuario
                        Conexion.RolUsuarioActual = reader["Rol"].ToString();  // Guarda el rol

                        this.Hide();
                        FormMenu menu = new FormMenu(idEmpleado);
                        menu.FormClosed += (s, args) => this.Show();  //  Al cerrar el menú, vuelve el login
                        menu.Show();

                    }


                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                    lblError.Visible = true;
                    timerError.Start();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar iniciar sesión:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FormLogin_Load_1(object sender, EventArgs e)
        {

        }

        private void timerError_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            timerError.Stop();
        }
    }
}
