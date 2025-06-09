using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ComanGo
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            this.FormClosed += (s, args) => Application.Exit(); //se cierra la app si se cierra la ventana de inicio(login)
            VerificarCrearBD();
            lblError.Visible = false;
            timerError.Tick += timerError_Tick;

        }

        // Método para comprobar si existe la BD, si no crearla con las correspondientes tablas
        private void VerificarCrearBD()
        {
            try
            {
                // Conexión sin base de datos, solo para verificar si ComanGo existe
                string sinBD = Conexion.ConnectionString.Replace("Database=ComanGoBD;", "");

                using (var conn = new MySqlConnection(sinBD))
                {
                    conn.Open();


                    // Verificar si ya está creada
                    var cmdCheck = new MySqlCommand("SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'ComanGoBD'", conn);
                    object exist = cmdCheck.ExecuteScalar();

                    //Si no existe, se crea
                    if (exist == null)
                    {
                        new MySqlCommand("CREATE DATABASE ComanGoBD;", conn).ExecuteNonQuery();
                        MessageBox.Show("Base de datos creada correctamente");
                    }

                }

                // Conectar con la BD una vez creada
                using (var conn = new MySqlConnection(Conexion.ConnectionString))
                {
                    conn.Open();

                    //Si ya hay tablas no se hace nada
                    var cmdTablas = new MySqlCommand("SHOW TABLES;", conn);
                    var reader = cmdTablas.ExecuteReader();
                    if (reader.HasRows) return;
                    reader.Close();

                    //Ejecutar el script si no hay tablas (1ºconexión de la aplicación)
                    string pathSql = Path.Combine(Application.StartupPath, "ComanGoBD.sql");
                    if (!File.Exists(pathSql))
                    {
                        MessageBox.Show("Falta el archivo ComanGoBD.sql");
                        return;
                    }

                    string[] sentencias = File.ReadAllText(pathSql).Split(';');
                    foreach (string sql in sentencias)
                    {
                        string limpio = sql.Trim();
                        if (!string.IsNullOrEmpty(limpio))
                        {
                            new MySqlCommand(limpio, conn).ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Tablas creadas correctamente");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al conectarse o crear la BD:\n" + e.Message);
            }
        }

        //Botón de login
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContraseña.Text.Trim();

            //Validación 
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

                var cmd = new MySqlCommand("SELECT * FROM Empleados WHERE Usuario = @usuario AND Contrasena = @contrasena", conn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@contrasena", contrasena);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    //Guardar los datos de usuario
                    Conexion.UsuarioActual = reader["Usuario"].ToString();
                    Conexion.RolUsuarioActual = reader["Rol"].ToString();
                    int idEmpeado = Convert.ToInt32(reader["IdEmpleado"]);

                    this.Hide();
                    var menu = new FormMenu(idEmpeado);
                    menu.FormClosed += (s, arga) => this.Show(); //vuelta al login después de cerrar el FormMenu
                    menu.Show();
                }
                else
                {
                    //Nueva validación por si no existe o está mal escrito
                    lblError.Text = "Usuario o contraseña incorrectos.";
                    lblError.Visible = true;
                    timerError.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión:\n" + ex.Message);
            }
        }

        private void FormLogin_Load_1(object sender, EventArgs e)
        {

        }

        // Temporizador para ocultar el label de error 
        private void timerError_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            timerError.Stop();
        }
    }
}
