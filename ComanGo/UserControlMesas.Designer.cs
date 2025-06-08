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
            panelMesas.SuspendLayout();
            SuspendLayout();
            // 
            // btnAgregarMesa
            // 
            btnAgregarMesa.Dock = DockStyle.Top;
            btnAgregarMesa.Location = new Point(0, 0);
            btnAgregarMesa.Name = "btnAgregarMesa";
            btnAgregarMesa.Size = new Size(800, 40);
            btnAgregarMesa.TabIndex = 0;
            btnAgregarMesa.Text = "Agregar mesa";
            btnAgregarMesa.UseVisualStyleBackColor = true;
            btnAgregarMesa.Click += btnAgregarMesa_Click;
            // 
            // panelMesas
            // 
            panelMesas.AutoScroll = true;
            panelMesas.Controls.Add(btnEliminar);
            panelMesas.Dock = DockStyle.Fill;
            panelMesas.Location = new Point(0, 40);
            panelMesas.Name = "panelMesas";
            panelMesas.Padding = new Padding(10);
            panelMesas.Size = new Size(800, 460);
            panelMesas.TabIndex = 1;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(13, 13);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(800, 40);
            btnEliminar.TabIndex = 0;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // UserControlMesas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelMesas);
            Controls.Add(btnAgregarMesa);
            Name = "UserControlMesas";
            Size = new Size(800, 500);
            panelMesas.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnAgregarMesa;
        private FlowLayoutPanel panelMesas;
        private Button btnEliminar;
    }
}
