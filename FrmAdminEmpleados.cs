using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using lab_1_POE_U20200218.Data;
using lab_1_POE_U20200218.Models;

namespace lab_1_POE_U20200218
{
    public partial class FrmAdminEmpleados : Form
    {
        private readonly EmpleadoService _service = new EmpleadoService();
        private System.Drawing.Image _imgEdit;
        private System.Drawing.Image _imgInfo;
        private System.Drawing.Image _imgDelete;

        public FrmAdminEmpleados()
        {
            InitializeComponent();
            // Estilos personalizados para los botones "Nuevo" y "Refrescar"
            try
            {
                btnNuevo.FlatStyle = FlatStyle.Flat;
                btnNuevo.BackColor = Color.SeaGreen;
                btnNuevo.ForeColor = Color.White;
                btnNuevo.FlatAppearance.BorderSize = 0;

                btnRefrescar.FlatStyle = FlatStyle.Flat;
                btnRefrescar.BackColor = Color.DarkOrange;
                btnRefrescar.ForeColor = Color.White;
                btnRefrescar.FlatAppearance.BorderSize = 0;
            }
            catch { }

            // Asegurar que después del enlace de datos no quede ninguna fila seleccionada
            try
            {
                dgvEmpleados.DataBindingComplete += DgvEmpleados_DataBindingComplete;
                this.Shown += FrmAdminEmpleados_Shown;
            }
            catch { }

            CreateActionIcons();
            LoadData();

            // Ajustar comportamiento del layout según el estado de la ventana
            this.Resize += FrmAdminEmpleados_Resize;
            // Aplicar una vez para el estado inicial
            UpdateLayoutBasedOnWindowState();
        }

        private void FrmAdminEmpleados_Resize(object? sender, EventArgs e)
        {
            UpdateLayoutBasedOnWindowState();
        }

        private void UpdateLayoutBasedOnWindowState()
        {
            try
            {
                // Cuando la ventana está maximizada queremos que el DataGridView llene todo el espacio
                if (this.WindowState == FormWindowState.Maximized)
                {
                    dgvEmpleados.Dock = DockStyle.Fill;
                    dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    // Restaurar comportamiento original (no Dock, usar Anchors y tamaño original)
                    dgvEmpleados.Dock = DockStyle.None;
                    dgvEmpleados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    dgvEmpleados.Location = new Point(12, 12);
                    dgvEmpleados.Size = new Size(760, 380);
                    // En modo no-maximizado se puede usar ajuste automático por contenido
                    dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                }
            }
            catch { }
        }

        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var grid = sender as DataGridView;
            if (grid == null) return;

            var colName = grid.Columns[e.ColumnIndex].Name;
            if (!int.TryParse(grid.Rows[e.RowIndex].Cells["Id"].Value?.ToString(), out var id)) return;

            if (colName == "colEdit")
            {
                // Editar
                var empleado = _service.GetById(id);
                if (empleado == null)
                {
                    MessageBox.Show("Empleado no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadData();
                    return;
                }
                using var dlg = new FrmFichaEmpleado(empleado);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        _service.Update(dlg.Empleado);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"No se pudo actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (colName == "colInfo")
            {
                var empleado = _service.GetById(id);
                if (empleado == null)
                {
                    MessageBox.Show("Empleado no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show($"Nombre: {empleado.Nombre}\nApellido: {empleado.Apellido}\nDUI: {empleado.DUI}\nCargo: {empleado.Cargo}", "Detalle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (colName == "colDelete")
            {
                if (MessageBox.Show("¿Eliminar empleado seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                try
                {
                    _service.Delete(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CreateActionIcons()
        {
            _imgEdit = CreateIconFromText("✎", System.Drawing.Color.SeaGreen); // pencil
            _imgInfo = CreateIconFromText("ℹ", System.Drawing.Color.DodgerBlue); // information
            _imgDelete = CreateIconFromText("\U0001F5D1", System.Drawing.Color.Crimson); // trash (emoji)
        }

        private void FrmAdminEmpleados_Shown(object? sender, EventArgs e)
        {
            try
            {
                dgvEmpleados.ClearSelection();
                dgvEmpleados.CurrentCell = null;
                this.ActiveControl = null;
            }
            catch { }
        }

        private void DgvEmpleados_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                dgvEmpleados.ClearSelection();
                dgvEmpleados.CurrentCell = null;
                foreach (DataGridViewRow r in dgvEmpleados.Rows)
                    r.Selected = false;
            }
            catch { }
        }

        private System.Drawing.Image CreateIconFromText(string text, System.Drawing.Color color)
        {
            int size = 20;
            var bmp = new System.Drawing.Bitmap(size, size);
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Transparent);
                using (var f = new System.Drawing.Font("Segoe UI Symbol", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel))
                using (var b = new System.Drawing.SolidBrush(color))
                {
                    var sf = new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Center };
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    g.DrawString(text, f, b, new System.Drawing.RectangleF(0, 0, size, size), sf);
                }
            }
            return bmp;
        }

        private void LoadData()
        {
            try
            {
                var list = _service.GetAll();
                dgvEmpleados.DataSource = list.Select(e => new
                {
                    e.Id,
                    e.Nombre,
                    e.Apellido,
                    DUI = FormatDui(e.DUI),
                    e.Edad,
                    e.Genero,
                    FechaIngreso = e.FechaIngreso.ToShortDateString(),
                    e.Cargo
                }).ToList();
                dgvEmpleados.AutoResizeColumns();

                // Eliminar columnas de acción si existen
                if (dgvEmpleados.Columns.Contains("colEdit")) dgvEmpleados.Columns.Remove("colEdit");
                if (dgvEmpleados.Columns.Contains("colInfo")) dgvEmpleados.Columns.Remove("colInfo");
                if (dgvEmpleados.Columns.Contains("colDelete")) dgvEmpleados.Columns.Remove("colDelete");

                // Agregar columnas de acción al final
                var colEdit = new DataGridViewImageColumn()
                {
                    Name = "colEdit",
                    HeaderText = "",
                    Image = _imgEdit,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 36,
                    ReadOnly = true
                };
                var colInfo = new DataGridViewImageColumn()
                {
                    Name = "colInfo",
                    HeaderText = "",
                    Image = _imgInfo,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 36,
                    ReadOnly = true
                };
                var colDelete = new DataGridViewImageColumn()
                {
                    Name = "colDelete",
                    HeaderText = "",
                    Image = _imgDelete,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 36,
                    ReadOnly = true
                };

                dgvEmpleados.Columns.Add(colEdit);
                dgvEmpleados.Columns.Add(colInfo);
                dgvEmpleados.Columns.Add(colDelete);

                // Ajustar última columna para que no quede cortada
                dgvEmpleados.Columns.Cast<DataGridViewColumn>().ToList().ForEach(c => c.SortMode = DataGridViewColumnSortMode.NotSortable);

                // Evitar que al iniciar la aplicación quede una fila seleccionada (sombra celeste)
                try
                {
                    // Limpiar selección explícitamente
                    dgvEmpleados.ClearSelection();
                    dgvEmpleados.CurrentCell = null;

                    // Neutralizar color de selección para que no destaque visualmente
                    var rowBack = dgvEmpleados.RowsDefaultCellStyle.BackColor;
                    var rowFore = dgvEmpleados.RowsDefaultCellStyle.ForeColor;
                    // Aplicar a DefaultCellStyle y RowTemplate por si se usan estilos distintos
                    dgvEmpleados.DefaultCellStyle.SelectionBackColor = rowBack;
                    dgvEmpleados.DefaultCellStyle.SelectionForeColor = rowFore;
                    dgvEmpleados.RowsDefaultCellStyle.SelectionBackColor = rowBack;
                    dgvEmpleados.RowsDefaultCellStyle.SelectionForeColor = rowFore;
                    dgvEmpleados.RowTemplate.DefaultCellStyle.SelectionBackColor = rowBack;
                    dgvEmpleados.RowTemplate.DefaultCellStyle.SelectionForeColor = rowFore;

                    // Asegurar que ninguna fila se marque como Selected
                    foreach (DataGridViewRow r in dgvEmpleados.Rows)
                        r.Selected = false;
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando empleados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatDui(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return string.Empty;
            var digits = new string((raw ?? string.Empty).Where(char.IsDigit).ToArray());
            if (digits.Length >= 9) return digits.Substring(0, 8) + "-" + digits[8];
            if (digits.Length > 0) return digits; // mostrar lo que haya
            return raw.Trim();
        }

        private int? GetSelectedId()
        {
            if (dgvEmpleados.SelectedRows.Count == 0) return null;
            var row = dgvEmpleados.SelectedRows[0];
            if (row.Cells[0].Value == null) return null;
            if (int.TryParse(row.Cells[0].Value.ToString(), out var id)) return id;
            return null;
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using var dlg = new FrmFichaEmpleado();
            var res = dlg.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                try
                {
                    _service.Add(dlg.Empleado);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo agregar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var id = GetSelectedId();
            if (!id.HasValue)
            {
                MessageBox.Show("Seleccione un empleado para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var empleado = _service.GetById(id.Value);
            if (empleado == null)
            {
                MessageBox.Show("Empleado no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
                return;
            }

            using var dlg = new FrmFichaEmpleado(empleado);
            var res = dlg.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                try
                {
                    _service.Update(dlg.Empleado);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var id = GetSelectedId();
            if (!id.HasValue)
            {
                MessageBox.Show("Seleccione un empleado para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("¿Eliminar empleado seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _service.Delete(id.Value);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
