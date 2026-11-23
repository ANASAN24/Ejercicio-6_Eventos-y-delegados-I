using System;

namespace ejercicio3
{
    internal class Program
    {
        class BackupEventArgs : EventArgs
        {
            public string NombreArchivo { get; set; }
            public DateTime Fecha { get; set; }
        }

        class GestorBackups
        {
            public event EventHandler<BackupEventArgs> BackupCompletado;

            public void CrearBackup(string nombreArchivo)
            {
                BackupEventArgs datosBackup = new BackupEventArgs();

                datosBackup.NombreArchivo = nombreArchivo;
                datosBackup.Fecha = DateTime.Now;

                Console.WriteLine("Creando copia de seguridad...");

                if (BackupCompletado != null)
                {
                    BackupCompletado(this, datosBackup);
                }
            }
        }

        class ServicioNotificacion
        {
            public void EnviarNotificacion(object sender, BackupEventArgs e)
            {
                Console.WriteLine("Notificación: La copia de seguridad se ha completado.");
                Console.WriteLine("Archivo respaldado: " + e.NombreArchivo);
            }
        }

        class ServicioLog
        {
            public void RegistrarBackup(object sender, BackupEventArgs e)
            {
                Console.WriteLine("Operación registrada en el log:");
                Console.WriteLine("Archivo: " + e.NombreArchivo);
                Console.WriteLine("Fecha: " + e.Fecha);
            }
        }

        static void Main(string[] args)
        {
            string nombreArchivo;

            Console.Write("Introduce el nombre del archivo para realizar la copia de seguridad: ");
            nombreArchivo = Console.ReadLine();

            GestorBackups gestorBackups = new GestorBackups();

            ServicioNotificacion servicioNotificacion = new ServicioNotificacion();
            ServicioLog servicioLog = new ServicioLog();

            gestorBackups.BackupCompletado += servicioNotificacion.EnviarNotificacion;
            gestorBackups.BackupCompletado += servicioLog.RegistrarBackup;

            gestorBackups.CrearBackup(nombreArchivo);
        }
    }
}