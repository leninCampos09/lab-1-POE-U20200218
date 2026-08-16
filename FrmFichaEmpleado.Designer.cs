namespace lab_1_POE_U20200218
{
    partial class FrmFichaEmpleado
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
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblDUI = new Label();
            txtDUI = new MaskedTextBox();
            lblEdad = new Label();
            txtEdad = new TextBox();
            lblGenero = new Label();
            rbiMasculino = new RadioButton();
            rbiFemenino = new RadioButton();
            lblFechaIngreso = new Label();
            dtpFechaIngreso = new DateTimePicker();
            lblCargo = new Label();
            txtCargo = new TextBox();
            btnGuardar = new Button();
            lblDuiPlaceholder = new Label();
            SuspendLayout();
            // 
            // lblNombre
            // 
            lblNombre.Location = new Point(20, 20);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(120, 23);
            lblNombre.TabIndex = 16;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(150, 20);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(340, 23);
            txtNombre.TabIndex = 15;
            txtNombre.KeyPress += txtNombre_KeyPress;
            // 
            // lblApellido
            // 
            lblApellido.Location = new Point(20, 60);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(120, 23);
            lblApellido.TabIndex = 14;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(150, 60);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(340, 23);
            txtApellido.TabIndex = 13;
            txtApellido.KeyPress += txtApellido_KeyPress;
            // 
            // lblDUI
            // 
            lblDUI.Location = new Point(20, 100);
            lblDUI.Name = "lblDUI";
            lblDUI.Size = new Size(120, 23);
            lblDUI.TabIndex = 12;
            lblDUI.Text = "DUI:";
            // 
            // txtDUI
            // 
            txtDUI.HidePromptOnLeave = true;
            txtDUI.Location = new Point(150, 100);
            txtDUI.Mask = "00000000-0";
            txtDUI.Name = "txtDUI";
            txtDUI.PromptChar = ' ';
            txtDUI.Size = new Size(120, 23);
            txtDUI.TabIndex = 10;
            txtDUI.TextChanged += txtDUI_TextChanged;
            txtDUI.Enter += txtDUI_Enter;
            txtDUI.Leave += txtDUI_Leave;
            // 
            // lblEdad
            // 
            lblEdad.Location = new Point(20, 140);
            lblEdad.Name = "lblEdad";
            lblEdad.Size = new Size(120, 23);
            lblEdad.TabIndex = 9;
            lblEdad.Text = "Edad:";
            // 
            // txtEdad
            // 
            txtEdad.Location = new Point(150, 140);
            txtEdad.Name = "txtEdad";
            txtEdad.Size = new Size(80, 23);
            txtEdad.TabIndex = 8;
            txtEdad.KeyPress += txtEdad_KeyPress;
            // 
            // lblGenero
            // 
            lblGenero.Location = new Point(20, 180);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(120, 23);
            lblGenero.TabIndex = 7;
            lblGenero.Text = "Género:";
            // 
            // rbiMasculino
            // 
            rbiMasculino.Location = new Point(150, 180);
            rbiMasculino.Name = "rbiMasculino";
            rbiMasculino.Size = new Size(90, 23);
            rbiMasculino.TabIndex = 6;
            rbiMasculino.Text = "Masculino";
            // 
            // rbiFemenino
            // 
            rbiFemenino.Location = new Point(250, 180);
            rbiFemenino.Name = "rbiFemenino";
            rbiFemenino.Size = new Size(90, 23);
            rbiFemenino.TabIndex = 5;
            rbiFemenino.Text = "Femenino";
            // 
            // lblFechaIngreso
            // 
            lblFechaIngreso.Location = new Point(20, 220);
            lblFechaIngreso.Name = "lblFechaIngreso";
            lblFechaIngreso.Size = new Size(120, 23);
            lblFechaIngreso.TabIndex = 4;
            lblFechaIngreso.Text = "Fecha de Ingreso:";
            // 
            // dtpFechaIngreso
            // 
            dtpFechaIngreso.CustomFormat = "'Seleccione fecha'";
            dtpFechaIngreso.Format = DateTimePickerFormat.Custom;
            dtpFechaIngreso.Location = new Point(150, 220);
            dtpFechaIngreso.Name = "dtpFechaIngreso";
            dtpFechaIngreso.Size = new Size(200, 23);
            dtpFechaIngreso.TabIndex = 3;
            dtpFechaIngreso.ValueChanged += dtpFechaIngreso_ValueChanged;
            dtpFechaIngreso.DropDown += dtpFechaIngreso_DropDown;
            // 
            // lblCargo
            // 
            lblCargo.Location = new Point(20, 260);
            lblCargo.Name = "lblCargo";
            lblCargo.Size = new Size(120, 23);
            lblCargo.TabIndex = 2;
            lblCargo.Text = "Cargo:";
            // 
            // txtCargo
            // 
            txtCargo.Location = new Point(150, 260);
            txtCargo.Name = "txtCargo";
            txtCargo.Size = new Size(340, 23);
            txtCargo.TabIndex = 1;
            txtCargo.KeyPress += txtCargo_KeyPress;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.SteelBlue;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(200, 310);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 35);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // lblDuiPlaceholder
            // 
            lblDuiPlaceholder.BackColor = SystemColors.Window;
            lblDuiPlaceholder.Cursor = Cursors.IBeam;
            lblDuiPlaceholder.ForeColor = Color.Gray;
            lblDuiPlaceholder.Location = new Point(153, 101);
            lblDuiPlaceholder.Name = "lblDuiPlaceholder";
            lblDuiPlaceholder.Size = new Size(115, 20);
            lblDuiPlaceholder.TabIndex = 11;
            lblDuiPlaceholder.Text = "00000000-0";
            lblDuiPlaceholder.TextAlign = ContentAlignment.MiddleLeft;
            lblDuiPlaceholder.Click += lblDuiPlaceholder_Click;
            // 
            // FrmFichaEmpleado
            // 
            ClientSize = new Size(520, 380);
            Controls.Add(btnGuardar);
            Controls.Add(txtCargo);
            Controls.Add(lblCargo);
            Controls.Add(dtpFechaIngreso);
            Controls.Add(lblFechaIngreso);
            Controls.Add(rbiFemenino);
            Controls.Add(rbiMasculino);
            Controls.Add(lblGenero);
            Controls.Add(txtEdad);
            Controls.Add(lblEdad);
            Controls.Add(txtDUI);
            Controls.Add(lblDuiPlaceholder);
            Controls.Add(lblDUI);
            Controls.Add(txtApellido);
            Controls.Add(lblApellido);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmFichaEmpleado";
            Text = "Ficha de Empleado";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblDUI;
        private System.Windows.Forms.MaskedTextBox txtDUI;
        private System.Windows.Forms.Label lblDuiPlaceholder;
        // lblDuiPlaceholder declaration removed (no such declaration present in this file)
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.RadioButton rbiMasculino;
        private System.Windows.Forms.RadioButton rbiFemenino;
        private System.Windows.Forms.Label lblFechaIngreso;
        private System.Windows.Forms.DateTimePicker dtpFechaIngreso;
        private System.Windows.Forms.Label lblCargo;
        private System.Windows.Forms.TextBox txtCargo;
        private System.Windows.Forms.Button btnGuardar;
    }
}
