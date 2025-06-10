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
    public partial class FormProducto : Form
    {
        //si es null se está creado un producto nuevo
        private int? idProducto = null;

        public FormProducto()
        {
            InitializeComponent();
            this.Text = "Nuevo producto";
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="precio"></param>
        public FormProducto(int id, string nombre, decimal precio) : this()
        {
            idProducto = id;
            txtNombre.Text = nombre;
            txtPrecio.Text = precio.ToString("0.00");
            this.Text = "Editar producto";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            //validar que el precio sea correcto, el campo de nombre no esté vacío
            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Introduce un precio válido.");
                return;
            }

            if (string.IsNullOrEmpty(nombre) || !Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                MessageBox.Show("El nombre no puede estar vacío ni contener números.");
                return;
            }

            using var conn = new MySqlConnection(Conexion.ConnectionString);
            conn.Open();

            //si el producto es nuevo se inserta y si no se actualiza
            string query;
            if (idProducto == null)
            {
                query = "INSERT INTO Productos (Nombre, Precio) VALUES (@nombre, @precio)";
            }
            else
            {
                query = "UPDATE Productos SET Nombre = @nombre, Precio = @precio WHERE IdProducto = @id";
            }

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@precio", precio);

            if (idProducto != null)
                cmd.Parameters.AddWithValue("@id", idProducto);

            cmd.ExecuteNonQuery();
            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
