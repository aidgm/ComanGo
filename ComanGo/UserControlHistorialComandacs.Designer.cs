namespace ComanGo
{
    partial class UserControlHistorialComandacs
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
            dgvComandas = new DataGridView();
            dgvDetalle = new DataGridView();
            lblHistorial = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvComandas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            SuspendLayout();
            // 
            // dgvComandas
            // 
            dgvComandas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvComandas.Location = new Point(44, 76);
            dgvComandas.Name = "dgvComandas";
            dgvComandas.RowHeadersWidth = 51;
            dgvComandas.Size = new Size(697, 173);
            dgvComandas.TabIndex = 0;
            dgvComandas.CellContentClick += dgvComandas_CellContentClick;
            // 
            // dgvDetalle
            // 
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Location = new Point(44, 269);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.Size = new Size(697, 173);
            dgvDetalle.TabIndex = 1;
            // 
            // lblHistorial
            // 
            lblHistorial.AutoSize = true;
            lblHistorial.Location = new Point(316, 32);
            lblHistorial.Name = "lblHistorial";
            lblHistorial.Size = new Size(158, 20);
            lblHistorial.TabIndex = 2;
            lblHistorial.Text = "Historial de comandas";
            // 
            // UserControlHistorialComandacs
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblHistorial);
            Controls.Add(dgvDetalle);
            Controls.Add(dgvComandas);
            Name = "UserControlHistorialComandacs";
            Size = new Size(800, 500);
            ((System.ComponentModel.ISupportInitialize)dgvComandas).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvComandas;
        private DataGridView dgvDetalle;
        private Label lblHistorial;
    }
}
