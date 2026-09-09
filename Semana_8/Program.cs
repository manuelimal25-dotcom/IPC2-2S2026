namespace Semana_8
{
    static class Program
    {
        // STAThread es un atributo que indica que el modelo de subprocesos utilizado por la aplicación es de un solo subproceso (Single Thread Apartment), lo cual es necesario para aplicaciones de Windows Forms.
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Inicia con el formulario del menú
            Application.Run(new FormMenu());
        }
    }
}