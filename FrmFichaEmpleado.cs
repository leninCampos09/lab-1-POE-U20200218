using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace lab_1_POE_U20200218
{
    public partial class FrmFichaEmpleado : Form
    {
        // Cuando se use en modo diálogo, esta propiedad contendrá el empleado creado/actualizado
        public lab_1_POE_U20200218.Models.Empleado Empleado { get; private set; }

        private bool _isEditMode = false;
        private ErrorProvider _errorProvider;
        private ToolTip _helpTooltip;
        // Autoevaluación: Lenin Alberto Campos Guzman U20200218 - Nota: 9
        public const string Autoevaluacion = "Lenin Alberto Campos Guzman U20200218 - 9";
        // Usamos lblDuiPlaceholder para mostrar la guía "00000000-0" cuando el MaskedTextBox está vacío

        public FrmFichaEmpleado()
        {
            InitializeComponent();
            ApplyCardStyle();
            // Asegurar estado inicial del placeholder del DUI después de aplicar estilos y reparenting
            try { txtDUI_TextChanged(txtDUI, EventArgs.Empty); } catch { }
            // Asegurar estado inicial del placeholder visual del DUI
            try { txtDUI_TextChanged(txtDUI, EventArgs.Empty); } catch { }
            // Forzar que la etiqueta guía quede por encima después del reparenting
            try
            {
                var card = this.Controls.Find("cardPanel", true).FirstOrDefault() as Panel;
                if (card != null)
                {
                    var lbl = card.Controls.Find("lblDuiPlaceholder", true).FirstOrDefault() as Label;
                    if (lbl != null)
                    {
                        lbl.BringToFront();
                        card.Controls.SetChildIndex(lbl, 0);
                        lbl.Visible = !txtDUI.Focused && (txtDUI.MaskedTextProvider == null || txtDUI.MaskedTextProvider.AssignedEditPositionCount == 0);
                    }
                }
            }
            catch { }
        }

        private void TxtDuiMasked_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // ocultar la etiqueta guía y asegurar color de edición
                if (lblDuiPlaceholder != null) lblDuiPlaceholder.Visible = false;
                txtDUI.ForeColor = Color.Black;
            }
            catch { }

            // Validación simple: permitir dígitos, guion y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
                _errorProvider.SetError(txtDUI, "Solo números y guion");
                _helpTooltip.Show("Solo números y guion", txtDUI, txtDUI.Width / 2, -24, 1500);
            }
            else
            {
                _errorProvider.SetError(txtDUI, "");
            }
        }
        // Ya no usamos placeholder inline dentro del MaskedTextBox; usamos lblDuiPlaceholder

        public FrmFichaEmpleado(lab_1_POE_U20200218.Models.Empleado empleado) : this()
        {
            if (empleado != null)
            {
                _isEditMode = true;
                LoadEmpleado(empleado);
            }
        }

        private void ApplyCardStyle()
        {
            // Estilo general del formulario
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.Padding = new Padding(12);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Panel tipo ficha (tarjeta) donde se alojarán los controles
            var card = new Panel
            {
                Name = "cardPanel",
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(16, 16),
                Size = new Size(Math.Max(200, this.ClientSize.Width - 32), Math.Max(200, this.ClientSize.Height - 32)),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Reubicar controles existentes dentro del panel y aplicar estilo a cada tipo
            var existing = this.Controls.Cast<Control>().Where(c => c != card).ToList();
            this.Controls.Add(card);
            foreach (var c in existing)
            {
                // ajustar posición relativa al panel
                c.Location = new Point(c.Location.X - card.Location.X, c.Location.Y - card.Location.Y);
                card.Controls.Add(c);

                if (c is TextBox tb)
                {
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    // usar fondo blanco para mayor contraste (menos pálido)
                    tb.BackColor = Color.White;
                    tb.Font = new Font(tb.Font.FontFamily, 9F);
                    // Si existe un TextBox para DUI, conectar manejador para validar letras
                    if (tb.Name.Equals("txtDui", StringComparison.OrdinalIgnoreCase))
                    {
                        tb.KeyPress += txtDui_KeyPress;
                    }
                }
                else if (c is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackColor = Color.FromArgb(0, 120, 215);
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                }
                else if (c is Label lbl)
                {
                    // No reestilar el placeholder visual del DUI para mantener su apariencia
                if (!string.IsNullOrEmpty(lbl.Name) && lbl.Name.Equals("lblDuiPlaceholder", StringComparison.OrdinalIgnoreCase))
                    {
                        // conservar propiedades definidas en el diseñador (IBeam)
                        lbl.Cursor = Cursors.IBeam;
                        // asegurar que la etiqueta quede encima y tenga fondo igual al control
                        try { lbl.BackColor = SystemColors.Window; } catch { }
                        try { lbl.ForeColor = Color.DimGray; } catch { }
                        try { lbl.BringToFront(); } catch { }
                        continue;
                    }

                    lbl.ForeColor = Color.FromArgb(64, 64, 64);
                    lbl.Font = new Font(lbl.Font.FontFamily, 9F, FontStyle.Bold);
                    // Propiedades de ayuda: Tag y AccessibleDescription para describir qué dato se admite
                    string key = (lbl.Text ?? lbl.Name).ToLower();
                    string hint;
                    if (key.Contains("nombre")) hint = "Solo letras y espacios";
                    else if (key.Contains("apellido")) hint = "Solo letras y espacios";
                    else if (key.Contains("cargo")) hint = "Solo letras y espacios";
                    else if (key.Contains("edad")) hint = "Solo números";
                    else if (key.Contains("fecha") || key.Contains("ingreso")) hint = "Fecha de ingreso (dd/MM/yyyy)";
                    else hint = "Dato esperado: ver formato";

                    lbl.Tag = hint;
                    lbl.AccessibleDescription = hint;
                    lbl.Cursor = Cursors.Help;
                }
                else if (c is MaskedTextBox mtb && !string.IsNullOrEmpty(mtb.Name) && mtb.Name.Equals("txtDUI", StringComparison.OrdinalIgnoreCase))
                {
                    // Conectar eventos para mejorar la experiencia del placeholder en el MaskedTextBox
                    mtb.KeyPress += TxtDuiMasked_KeyPress;
                    mtb.Enter += txtDUI_Enter;
                    mtb.Leave += txtDUI_Leave;
                }
                else if (c is DateTimePicker dtp)
                {
                    dtp.Font = new Font(dtp.Font.FontFamily, 9F);
                }

            // Inicializar ErrorProvider y ToolTip para mostrar advertencias rápidas
            _errorProvider = new ErrorProvider
            {
                BlinkStyle = ErrorBlinkStyle.NeverBlink,
                ContainerControl = this
            };

            _helpTooltip = new ToolTip
            {
                IsBalloon = true,
                ToolTipTitle = "Formato"
            };
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir letras, espacios y teclas de control
            var tb = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
                if (tb != null)
                {
                    _errorProvider.SetError(tb, "Solo letras y espacios");
                    _helpTooltip.Show("Solo letras y espacios", tb, tb.Width / 2, -24, 1500);
                }
            }
            else
            {
                if (tb != null) _errorProvider.SetError(tb, "");
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
                if (tb != null)
                {
                    _errorProvider.SetError(tb, "Solo letras y espacios");
                    _helpTooltip.Show("Solo letras y espacios", tb, tb.Width / 2, -24, 1500);
                }
            }
            else
            {
                if (tb != null) _errorProvider.SetError(tb, "");
            }
        }

        private void txtCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
                if (tb != null)
                {
                    _errorProvider.SetError(tb, "Solo letras y espacios");
                    _helpTooltip.Show("Solo letras y espacios", tb, tb.Width / 2, -24, 1500);
                }
            }
            else
            {
                if (tb != null) _errorProvider.SetError(tb, "");
            }
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control
            var tb = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (tb != null)
                {
                    _errorProvider.SetError(tb, "Solo números");
                    _helpTooltip.Show("Solo números", tb, tb.Width / 2, -24, 1500);
                }
            }
            else
            {
                if (tb != null) _errorProvider.SetError(tb, "");
            }
        }

        private void txtDui_KeyPress(object sender, KeyPressEventArgs e)
        {
            // DUI: permitir dígitos, guion y teclas de control. Si el usuario intenta poner letras, mostrar advertencia.
            var tb = sender as TextBox;

            // Buscar label asociado al campo DUI (por nombre o texto) en controles hijos
            Label duiLabel = null;
            try
            {
                // Intentar buscar por nombre en todos los controles
                var found = this.Controls.Find("lblDui", true);
                if (found != null && found.Length > 0)
                    duiLabel = found.OfType<Label>().FirstOrDefault();

                // Si no se encontró por nombre, buscar por texto o nombre que contenga "dui"
                if (duiLabel == null)
                {
                    duiLabel = this.Controls.Cast<Control>()
                        .SelectMany(c => c.Controls.Cast<Control>())
                        .OfType<Label>()
                        .FirstOrDefault(l => (l.Name ?? "").ToLower().Contains("dui") || (l.Text ?? "").ToLower().Contains("dui"));
                }
            }
            catch { }

            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                if (tb != null)
                {
                    _errorProvider.SetError(tb, "No se admiten letras en el DUI");
                    _helpTooltip.Show("No se admiten letras en el DUI", tb, tb.Width / 2, -24, 1500);
                }
                if (duiLabel != null)
                {
                    duiLabel.ForeColor = Color.Red;
                    _helpTooltip.Show("No se admiten letras en el DUI", duiLabel, duiLabel.Width / 2, -24, 1500);
                }
            }
            else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
                if (tb != null)
                {
                    _errorProvider.SetError(tb, "Solo números y guion");
                    _helpTooltip.Show("Solo números y guion", tb, tb.Width / 2, -24, 1500);
                }
                if (duiLabel != null)
                {
                    duiLabel.ForeColor = Color.Red;
                    _helpTooltip.Show("Solo números y guion en el DUI", duiLabel, duiLabel.Width / 2, -24, 1500);
                }
            }
            else
            {
                if (tb != null) _errorProvider.SetError(tb, "");
                if (duiLabel != null)
                {
                    duiLabel.ForeColor = Color.FromArgb(64, 64, 64);
                }
            }
        }

        private void dtpFechaIngreso_DropDown(object sender, EventArgs e)
        {
            // Al abrir el calendario, mostrar el formato de fecha corto
            var dtp = sender as DateTimePicker;
            if (dtp != null)
            {
                dtp.Format = DateTimePickerFormat.Short;
            }
        }

        private void dtpFechaIngreso_ValueChanged(object sender, EventArgs e)
        {
            // Cuando se seleccione una fecha, asegurar que se muestre en formato corto
            var dtp = sender as DateTimePicker;
            if (dtp != null)
            {
                dtp.Format = DateTimePickerFormat.Short;
            }
        }

        // Placeholder handling for MaskedTextBox DUI: mostrar una etiqueta con el texto
        // "00000000-0" cuando no hay entrada y ocultarla cuando el usuario escribe.
        private void txtDUI_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var provider = txtDUI.MaskedTextProvider;
                bool hasUserInput = false;
                if (provider != null)
                {
                    hasUserInput = provider.AssignedEditPositionCount > 0;
                }
                else
                {
                    var txt = txtDUI.Text ?? string.Empty;
                    hasUserInput = txt.Any(char.IsDigit);
                }

                // Si hay entrada, asegurarse de que el texto se muestre en negro y ocultar la guía
                if (hasUserInput)
                {
                    try { txtDUI.ForeColor = Color.Black; } catch { }
                    if (lblDuiPlaceholder != null) { lblDuiPlaceholder.Visible = false; lblDuiPlaceholder.SendToBack(); }
                }
                else
                {
                    // Mostrar el placeholder (label) solo si el control no está enfocado
                    if (lblDuiPlaceholder != null)
                    {
                        lblDuiPlaceholder.Visible = !txtDUI.Focused;
                        if (lblDuiPlaceholder.Visible) lblDuiPlaceholder.BringToFront();
                        else lblDuiPlaceholder.SendToBack();
                    }
                    // mantener el color por defecto
                    try { txtDUI.ForeColor = Color.Black; } catch { }
                }
            }
            catch
            {
                // ignorar
            }
        }

        private void txtDUI_Enter(object sender, EventArgs e)
        {
            // Al entrar en el campo ocultar la etiqueta guía y asegurar color de edición
            try
            {
                if (lblDuiPlaceholder != null) lblDuiPlaceholder.Visible = false;
                try { txtDUI.ForeColor = Color.Black; } catch { }
            }
            catch { }
        }

        private void txtDUI_Leave(object sender, EventArgs e)
        {
            // Al salir, mostrar el hint si no hay entrada
            try
            {
                var provider = txtDUI.MaskedTextProvider;
                bool hasUserInput = provider != null ? provider.AssignedEditPositionCount > 0 : (txtDUI.Text ?? string.Empty).Any(char.IsDigit);
                // Si no hay entrada, mostrar sólo la etiqueta guía
                if (!hasUserInput)
                {
                    if (lblDuiPlaceholder != null) lblDuiPlaceholder.Visible = true;
                    // asegurar que el MaskedTextBox esté vacío y listo para editar
                    try { txtDUI.Text = string.Empty; txtDUI.ForeColor = Color.Black; } catch { }
                }
                else
                {
                    if (lblDuiPlaceholder != null) lblDuiPlaceholder.Visible = false;
                }
            }
            catch { }
        }

        private void lblDuiPlaceholder_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblDuiPlaceholder != null) lblDuiPlaceholder.Visible = false;
                txtDUI.Focus();
                // mover caret al inicio para comenzar a escribir
                txtDUI.SelectionStart = 0;
            }
            catch { }
        }

        // eliminado: no se usa placeholder label

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación mínima
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("Nombre y apellido son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Normalizar DUI: preferimos usar MaskedTextProvider para obtener con fiabilidad
            // los caracteres que el usuario ha introducido junto con las literales (guion).
            string rawDui = string.Empty;
            var provider = txtDUI.MaskedTextProvider;
            var prevPrompt = txtDUI.PromptChar;
            var prevFormat = txtDUI.TextMaskFormat;
            try
            {
                if (provider != null)
                {
                    // includePrompt = false (omit prompts), includeLiterals = true (mantener '-').
                    rawDui = provider.ToString(false, true) ?? string.Empty;
                }
                else
                {
                    // Forzar lectura sin prompts ni literales
                    txtDUI.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
                    rawDui = txtDUI.Text ?? string.Empty;
                }
            }
            catch
            {
                try
                {
                    txtDUI.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
                    rawDui = txtDUI.Text ?? string.Empty;
                }
                catch
                {
                    rawDui = txtDUI.Text ?? string.Empty;
                }
            }
            finally
            {
                txtDUI.TextMaskFormat = prevFormat;
                txtDUI.PromptChar = prevPrompt;
            }
            string digits = new string(rawDui.Where(char.IsDigit).ToArray());
            string formattedDui = string.Empty;
            if (digits.Length >= 9)
            {
                formattedDui = digits.Substring(0, 8) + "-" + digits[8];
            }
            else if (digits.Length == 8)
            {
                // si faltara el dígito verificador, dejar los 8 dígitos
                formattedDui = digits;
            }
            else
            {
                formattedDui = rawDui.Trim();
            }

            // DEBUG: mostrar valores intermedios para diagnosticar pérdida de ceros
            MessageBox.Show($"rawDui:'{rawDui}'\n digits:'{digits}'\n formattedDui:'{formattedDui}'", "DEBUG DUI", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var empleado = new lab_1_POE_U20200218.Models.Empleado
            {
                Nombre = nombre,
                Apellido = apellido,
                DUI = formattedDui,
                Cargo = txtCargo.Text?.Trim(),
                Edad = int.TryParse(txtEdad.Text, out var edad) ? edad : 0,
                Genero = rbiFemenino.Checked ? "Femenino" : (rbiMasculino.Checked ? "Masculino" : string.Empty),
                FechaIngreso = dtpFechaIngreso.Value
            };

            // Si es edición conservar Id
            if (_isEditMode && Empleado != null)
            {
                empleado.Id = Empleado.Id;
            }

            Empleado = empleado;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadEmpleado(lab_1_POE_U20200218.Models.Empleado empleado)
        {
            Empleado = empleado;
            txtNombre.Text = empleado.Nombre;
            txtApellido.Text = empleado.Apellido;
            // Mostrar DUI normalizado
            if (!string.IsNullOrWhiteSpace(empleado.DUI))
            {
                var digits = new string((empleado.DUI ?? string.Empty).Where(char.IsDigit).ToArray());
                if (digits.Length >= 9)
                    txtDUI.Text = digits.Substring(0, 8) + "-" + digits[8];
                else
                    txtDUI.Text = empleado.DUI;
                try { txtDUI.ForeColor = Color.Black; } catch { }
                try { if (lblDuiPlaceholder != null) lblDuiPlaceholder.Visible = false; } catch { }
            }
            else
            {
                txtDUI.Text = string.Empty;
            }
            txtEdad.Text = empleado.Edad.ToString();
            txtCargo.Text = empleado.Cargo;
            dtpFechaIngreso.Value = empleado.FechaIngreso == default ? DateTime.Now : empleado.FechaIngreso;
            if (empleado.Genero != null)
            {
                if (empleado.Genero.Equals("Femenino", StringComparison.OrdinalIgnoreCase)) rbiFemenino.Checked = true;
                else if (empleado.Genero.Equals("Masculino", StringComparison.OrdinalIgnoreCase)) rbiMasculino.Checked = true;
            }
        }
    }
}
