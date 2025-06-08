
namespace ComanGo
{
    partial class FormLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pbLogo = new PictureBox();
            label1 = new Label();
            panelUsuario = new Panel();
            txtUsuario = new TextBox();
            pbIconUsuario = new PictureBox();
            panelContraseña = new Panel();
            txtContraseña = new TextBox();
            pbContraseña = new PictureBox();
            lblError = new Label();
            btnLogin = new Button();
            timerError = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            panelUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbIconUsuario).BeginInit();
            panelContraseña.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbContraseña).BeginInit();
            SuspendLayout();
            // 
            // pbLogo
            // 
            pbLogo.Image = Properties.Resources.logo;
            pbLogo.Location = new Point(400, 0);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(150, 150);
            pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogo.TabIndex = 0;
            pbLogo.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(320, 165);
            label1.Name = "label1";
            label1.Size = new Size(330, 41);
            label1.TabIndex = 1;
            label1.Text = "Bienvenido a ComanGo";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelUsuario
            // 
            panelUsuario.BorderStyle = BorderStyle.FixedSingle;
            panelUsuario.Controls.Add(txtUsuario);
            panelUsuario.Controls.Add(pbIconUsuario);
            panelUsuario.Location = new Point(333, 250);
            panelUsuario.Name = "panelUsuario";
            panelUsuario.Size = new Size(300, 40);
            panelUsuario.TabIndex = 2;
            // 
            // txtUsuario
            // 
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Dock = DockStyle.Fill;
            txtUsuario.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsuario.Location = new Point(32, 0);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(266, 40);
            txtUsuario.TabIndex = 1;
            // 
            // pbIconUsuario
            // 
            pbIconUsuario.Dock = DockStyle.Left;
            pbIconUsuario.Image = Properties.Resources.profile;
            pbIconUsuario.Location = new Point(0, 0);
            pbIconUsuario.Name = "pbIconUsuario";
            pbIconUsuario.Size = new Size(32, 38);
            pbIconUsuario.SizeMode = PictureBoxSizeMode.Zoom;
            pbIconUsuario.TabIndex = 0;
            pbIconUsuario.TabStop = false;
            // 
            // panelContraseña
            // 
            panelContraseña.BorderStyle = BorderStyle.FixedSingle;
            panelContraseña.Controls.Add(txtContraseña);
            panelContraseña.Controls.Add(pbContraseña);
            panelContraseña.Location = new Point(333, 315);
            panelContraseña.Name = "panelContraseña";
            panelContraseña.Size = new Size(300, 40);
            panelContraseña.TabIndex = 3;
            // 
            // txtContraseña
            // 
            txtContraseña.BorderStyle = BorderStyle.None;
            txtContraseña.Dock = DockStyle.Fill;
            txtContraseña.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtContraseña.Location = new Point(32, 0);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(266, 40);
            txtContraseña.TabIndex = 2;
            txtContraseña.UseSystemPasswordChar = true;
            // 
            // pbContraseña
            // 
            pbContraseña.Dock = DockStyle.Left;
            pbContraseña.Image = Properties.Resources.locked;
            pbContraseña.Location = new Point(0, 0);
            pbContraseña.Name = "pbContraseña";
            pbContraseña.Size = new Size(32, 38);
            pbContraseña.SizeMode = PictureBoxSizeMode.Zoom;
            pbContraseña.TabIndex = 1;
            pbContraseña.TabStop = false;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(334, 490);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 28);
            lblError.TabIndex = 5;
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            lblError.Visible = false;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.ForestGreen;
            btnLogin.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.Yellow;
            btnLogin.Location = new Point(388, 399);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(184, 75);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Inicio Sesión";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // timerError
            // 
            timerError.Interval = 3000;
            timerError.Tick += timerError_Tick;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 192);
            ClientSize = new Size(982, 553);
            Controls.Add(btnLogin);
            Controls.Add(lblError);
            Controls.Add(panelContraseña);
            Controls.Add(panelUsuario);
            Controls.Add(label1);
            Controls.Add(pbLogo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ComanGo";
            Load += FormLogin_Load_1;
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            panelUsuario.ResumeLayout(false);
            panelUsuario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbIconUsuario).EndInit();
            panelContraseña.ResumeLayout(false);
            panelContraseña.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbContraseña).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private PictureBox pbLogo;
        private Label label1;
        private Panel panelUsuario;
        private TextBox txtUsuario;
        private PictureBox pbIconUsuario;
        private Panel panelContraseña;
        private TextBox txtContraseña;
        private PictureBox pbContraseña;
        private Label lblError;
        private Button btnLogin;
        private System.Windows.Forms.Timer timerError;
    }
}
