using System;

namespace ejercicio4
{
    internal class Program
    {
        class IntrusoEventArgs : EventArgs
        {
            public string NombreSensor { get; set; }
            public DateTime HoraDeteccion { get; set; }
        }

        class SensorMonitoreo
        {
            public event EventHandler<IntrusoEventArgs> AlertaIntruso;

            public void ComprobarSensor(string nombreSensor, DateTime hora)
            {
                if (hora.Hour < 8 || hora.Hour >= 22)
                {
                    IntrusoEventArgs datosIntruso = new IntrusoEventArgs();

                    datosIntruso.NombreSensor = nombreSensor;
                    datosIntruso.HoraDeteccion = hora;

                    if (AlertaIntruso != null)
                    {
                        AlertaIntruso(this, datosIntruso);
                    }
                }
                else
                {
                    Console.WriteLine("El sensor está dentro del horario permitido.");
                }
            }
        }

        class ServicioAlarma
        {
            public void ActivarAlarma(object sender, IntrusoEventArgs e)
            {
                Console.WriteLine("¡ALERTA! Se ha detectado una posible intrusión.");
                Console.WriteLine("Alarma activada en: " + e.NombreSensor);
            }
        }

        class ServicioRegistroIncidencias
        {
            public void RegistrarIncidencia(object sender, IntrusoEventArgs e)
            {
                Console.WriteLine("Incidencia registrada:");
                Console.WriteLine("Sensor: " + e.NombreSensor);
                Console.WriteLine("Hora de detección: " + e.HoraDeteccion);
            }
        }

        static void Main(string[] args)
        {
            string nombreSensor;
            DateTime hora;

            Console.Write("Introduce el nombre de la puerta o ventana: ");
            nombreSensor = Console.ReadLine();

            Console.Write("Introduce la hora de detección (HH:mm): ");
            hora = Convert.ToDateTime(Console.ReadLine());

            SensorMonitoreo sensorMonitoreo = new SensorMonitoreo();

            ServicioAlarma servicioAlarma = new ServicioAlarma();
            ServicioRegistroIncidencias servicioRegistro = new ServicioRegistroIncidencias();

            sensorMonitoreo.AlertaIntruso += servicioAlarma.ActivarAlarma;
            sensorMonitoreo.AlertaIntruso += servicioRegistro.RegistrarIncidencia;

            sensorMonitoreo.ComprobarSensor(nombreSensor, hora);
        }
    }
}
