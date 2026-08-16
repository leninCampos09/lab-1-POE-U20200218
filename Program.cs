namespace lab_1_POE_U20200218
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // Crear la base de datos y tablas si no existen
            try
            {
                using (var db = new lab_1_POE_U20200218.Data.AppDbContext())
                {
                    db.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo crear la base de datos: {ex.Message}", "Error DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Run(new FrmAdminEmpleados());
        }
    }
}