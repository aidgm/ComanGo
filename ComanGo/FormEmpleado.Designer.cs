namespace ComanGo
{
    partial class FormEmpleado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblContraseña = new Label();
            txtContraseña = new TextBox();
            lblRol = new Label();
            cbRol = new ComboBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(286, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(188, 47);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Agregar Empleado";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNombre.Location = new Point(164, 74);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(85, 28);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(286, 75);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(250, 27);
            txtNombre.TabIndex = 2;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsuario.Location = new Point(164, 141);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(79, 28);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(286, 142);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(250, 27);
            txtUsuario.TabIndex = 4;
            // 
            // lblContraseña
            // 
            lblContraseña.AutoSize = true;
            lblContraseña.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblContraseña.Location = new Point(164, 204);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(110, 28);
            lblContraseña.TabIndex = 5;
            lblContraseña.Text = "Contraseña";
            // 
            // txtContraseña
            // 
            txtContraseña.Location = new Point(286, 204);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(250, 27);
            txtContraseña.TabIndex = 6;
            txtContraseña.UseSystemPasswordChar = true;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRol.Location = new Point(164, 272);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(40, 28);
            lblRol.TabIndex = 7;
            lblRol.Text = "Rol";
            // 
            // cbRol
            // 
            cbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRol.FormattingEnabled = true;
            cbRol.Items.AddRange(new object[] { "admin", "empleado" });
            cbRol.Location = new Point(286, 272);
            cbRol.Name = "cbRol";
            cbRol.Size = new Size(151, 28);
            cbRol.TabIndex = 8;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.ForestGreen;
            btnGuardar.DialogResult = DialogResult.OK;
            btnGuardar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.Yellow;
            btnGuardar.Location = new Point(164, 346);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(144, 60);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.ForestGreen;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancelar.ForeColor = Color.Yellow;
            btnCancelar.Location = new Point(406, 346);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 60);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // FormEmpleado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 192);
            ClientSize = new Size(782, 453);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(cbRol);
            Controls.Add(lblRol);
            Controls.Add(txtContraseña);
            Controls.Add(lblContraseña);
            Controls.Add(txtUsuario);
            Controls.Add(lblUsuario);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormEmpleado";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ComanGo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblContraseña;
        private TextBox txtContraseña;
        private Label lblRol;
        private ComboBox cbRol;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}