namespace ComanGo
{
    partial class UserControlComanda
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            cbProductos = new ComboBox();
            nudCantidad = new NumericUpDown();
            btnAgregarProducto = new Button();
            lblTotal = new Label();
            lstProductos = new ListBox();
            btnFinalizar = new Button();
            btnBorrarPrdComanda = new Button();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.LightBlue;
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(732, 40);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Comanda - Mesa ";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cbProductos
            // 
            cbProductos.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProductos.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbProductos.FormattingEnabled = true;
            cbProductos.Location = new Point(30, 60);
            cbProductos.Name = "cbProductos";
            cbProductos.Size = new Size(220, 36);
            cbProductos.TabIndex = 1;
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(260, 60);
            nudCantidad.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(60, 27);
            nudCantidad.TabIndex = 2;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAgregarProducto
            // 
            btnAgregarProducto.BackColor = Color.LightGray;
            btnAgregarProducto.FlatStyle = FlatStyle.Flat;
            btnAgregarProducto.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAgregarProducto.Location = new Point(330, 60);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(170, 35);
            btnAgregarProducto.TabIndex = 3;
            btnAgregarProducto.Text = "Agregar";
            btnAgregarProducto.UseVisualStyleBackColor = false;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            // 
            // lblTotal
            // 
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = Color.DarkGreen;
            lblTotal.Location = new Point(520, 60);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(170, 30);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "Total:";
            lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lstProductos
            // 
            lstProductos.BackColor = Color.WhiteSmoke;
            lstProductos.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstProductos.FormattingEnabled = true;
            lstProductos.Location = new Point(30, 110);
            lstProductos.Name = "lstProductos";
            lstProductos.Size = new Size(660, 284);
            lstProductos.TabIndex = 5;
            // 
            // btnFinalizar
            // 
            btnFinalizar.BackColor = Color.ForestGreen;
            btnFinalizar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFinalizar.ForeColor = Color.Yellow;
            btnFinalizar.Location = new Point(438, 414);
            btnFinalizar.Name = "btnFinalizar";
            btnFinalizar.Size = new Size(252, 56);
            btnFinalizar.TabIndex = 6;
            btnFinalizar.Text = "Guardar comanda";
            btnFinalizar.UseVisualStyleBackColor = false;
            btnFinalizar.Click += btnFinalizar_Click;
            // 
            // btnBorrarPrdComanda
            // 
            btnBorrarPrdComanda.BackColor = Color.Red;
            btnBorrarPrdComanda.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBorrarPrdComanda.ForeColor = Color.Yellow;
            btnBorrarPrdComanda.Location = new Point(30, 414);
            btnBorrarPrdComanda.Name = "btnBorrarPrdComanda";
            btnBorrarPrdComanda.Size = new Size(220, 55);
            btnBorrarPrdComanda.TabIndex = 0;
            btnBorrarPrdComanda.Text = "Borrar producto";
            btnBorrarPrdComanda.UseVisualStyleBackColor = false;
            btnBorrarPrdComanda.Click += btnBorrarPrdComanda_Click;
            // 
            // UserControlComanda
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnBorrarPrdComanda);
            Controls.Add(btnFinalizar);
            Controls.Add(lstProductos);
            Controls.Add(lblTotal);
            Controls.Add(btnAgregarProducto);
            Controls.Add(nudCantidad);
            Controls.Add(cbProductos);
            Controls.Add(lblTitulo);
            Name = "UserControlComanda";
            Size = new Size(732, 553);
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private ComboBox cbProductos;
        private NumericUpDown nudCantidad;
        private Button btnAgregarProducto;
        private Label lblTotal;
        private ListBox lstProductos;
        private Button btnFinalizar;
        private Button btnBorrarPrdComanda;
    }
}
