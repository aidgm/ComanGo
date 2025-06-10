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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dgvComandas = new DataGridView();
            dgvDetalle = new DataGridView();
            lblHistorial = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvComandas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            SuspendLayout();
            // 
            // dgvComandas
            // 
            dgvComandas.BackgroundColor = Color.FromArgb(255, 255, 192);
            dgvComandas.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 255, 192);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvComandas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvComandas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvComandas.EnableHeadersVisualStyles = false;
            dgvComandas.GridColor = Color.FromArgb(255, 255, 192);
            dgvComandas.Location = new Point(44, 76);
            dgvComandas.Name = "dgvComandas";
            dgvComandas.RowHeadersVisible = false;
            dgvComandas.RowHeadersWidth = 51;
            dgvComandas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvComandas.Size = new Size(697, 173);
            dgvComandas.TabIndex = 0;
            dgvComandas.CellContentClick += dgvComandas_CellContentClick;
            // 
            // dgvDetalle
            // 
            dgvDetalle.BackgroundColor = Color.FromArgb(255, 255, 192);
            dgvDetalle.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 192);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.GridColor = Color.FromArgb(255, 255, 192);
            dgvDetalle.Location = new Point(44, 269);
            dgvDetalle.Name = "dgvDetalle";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(255, 255, 192);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvDetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvDetalle.RowHeadersVisible = false;
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.Size = new Size(697, 173);
            dgvDetalle.TabIndex = 1;
            // 
            // lblHistorial
            // 
            lblHistorial.AutoSize = true;
            lblHistorial.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHistorial.Location = new Point(292, 31);
            lblHistorial.Name = "lblHistorial";
            lblHistorial.Size = new Size(206, 28);
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
