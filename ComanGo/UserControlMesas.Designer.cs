namespace ComanGo
{
    partial class UserControlMesas
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
            btnAgregarMesa = new Button();
            panelMesas = new FlowLayoutPanel();
            btnEliminar = new Button();
            SuspendLayout();
            // 
            // btnAgregarMesa
            // 
            btnAgregarMesa.BackColor = Color.ForestGreen;
            btnAgregarMesa.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAgregarMesa.ForeColor = Color.Yellow;
            btnAgregarMesa.Location = new Point(22, 28);
            btnAgregarMesa.Name = "btnAgregarMesa";
            btnAgregarMesa.Size = new Size(231, 65);
            btnAgregarMesa.TabIndex = 0;
            btnAgregarMesa.Text = "Agregar";
            btnAgregarMesa.UseVisualStyleBackColor = false;
            btnAgregarMesa.Click += btnAgregarMesa_Click;
            // 
            // panelMesas
            // 
            panelMesas.AutoScroll = true;
            panelMesas.Location = new Point(22, 99);
            panelMesas.Name = "panelMesas";
            panelMesas.Padding = new Padding(10);
            panelMesas.Size = new Size(663, 353);
            panelMesas.TabIndex = 1;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Red;
            btnEliminar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.Yellow;
            btnEliminar.Location = new Point(454, 28);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(231, 65);
            btnEliminar.TabIndex = 0;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // UserControlMesas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnEliminar);
            Controls.Add(panelMesas);
            Controls.Add(btnAgregarMesa);
            Name = "UserControlMesas";
            Size = new Size(800, 500);
            ResumeLayout(false);
        }

        #endregion

        private Button btnAgregarMesa;
        private FlowLayoutPanel panelMesas;
        private Button btnEliminar;
    }
}
