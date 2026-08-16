namespace lab_1_POE_U20200218
{
    partial class FrmAdminEmpleados
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEmpleados
            // 
            // Hacer que el DataGridView ocupe todo el espacio disponible
            this.dgvEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpleados.Location = new System.Drawing.Point(12, 12);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.ReadOnly = true;
            // Ajustar columnas para ocupar todo el ancho disponible
            this.dgvEmpleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            // Estilo de encabezado
            this.dgvEmpleados.EnableHeadersVisualStyles = false;
            var headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            // Color personalizado para el encabezado
            headerStyle.BackColor = System.Drawing.Color.DarkSlateBlue;
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dgvEmpleados.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpleados.MultiSelect = false;
            this.dgvEmpleados.Size = new System.Drawing.Size(760, 380);
            this.dgvEmpleados.RowTemplate.Height = 28;
            // Ocultar la columna de encabezado de fila (la columna vacía de la izquierda)
            this.dgvEmpleados.RowHeadersVisible = false;
            this.dgvEmpleados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpleados_CellContentClick);
            this.dgvEmpleados.TabIndex = 0;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNuevo.Location = new System.Drawing.Point(12, 9);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(90, 30);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // Nota: Se han eliminado los botones "Editar" y "Eliminar" del formulario
            // 
            // btnRefrescar
            // 
            // Colocado al lado del botón "Nuevo" (anclado a la izquierda)
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefrescar.Location = new System.Drawing.Point(108, 9);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(90, 30);
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);

            // pnlBottom
            this.pnlBottom.SuspendLayout();
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 48;
            this.pnlBottom.Controls.Add(this.btnNuevo);
            this.pnlBottom.Controls.Add(this.btnRefrescar);
            this.pnlBottom.Name = "pnlBottom";
            // 
            // FrmAdminEmpleados
            // 
            this.ClientSize = new System.Drawing.Size(784, 451);
            // Añadir controles: DataGridView rellena y panel inferior con botones
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.pnlBottom);
            this.Name = "FrmAdminEmpleados";
            this.Text = "Administrar Empleados";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Panel pnlBottom;
    }
}
