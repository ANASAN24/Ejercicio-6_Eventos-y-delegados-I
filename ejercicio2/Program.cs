using System;

namespace ejercicio2
{
    internal class Program
    {
        class TemperaturaEventArgs : EventArgs
        {
            public double TemperaturaActual { get; set; }
            public double Umbral { get; set; }
        }

        class ControlTemperatura
        {
            public event EventHandler<TemperaturaEventArgs> TemperaturaAlta;

            public void ComprobarTemperatura(double temperatura, double umbral)
            {
                if (temperatura > umbral)
                {
                    TemperaturaEventArgs datosTemperatura = new TemperaturaEventArgs();

                    datosTemperatura.TemperaturaActual = temperatura;
                    datosTemperatura.Umbral = umbral;

                    if (TemperaturaAlta != null)
                    {
                        TemperaturaAlta(this, datosTemperatura);
                    }
                }
                else
                {
                    Console.WriteLine("La temperatura del invernadero es correcta.");
                    Console.WriteLine("Temperatura actual: " + temperatura + " grados");
                }
            }
        }

        class ServicioAlerta
        {
            public void EnviarAlerta(object sender, TemperaturaEventArgs e)
            {
                Console.WriteLine("ALERTA: La temperatura del invernadero es demasiado alta.");
                Console.WriteLine("Temperatura actual: " + e.TemperaturaActual + " grados");
                Console.WriteLine("Umbral establecido: " + e.Umbral + " grados");
            }
        }

        class ServicioRegistroTemperatura
        {
            public void RegistrarTemperatura(object sender, TemperaturaEventArgs e)
            {
                Console.WriteLine("Temperatura registrada: " + e.TemperaturaActual + " grados");
            }
        }

        static void Main(string[] args)
        {
            double temperatura;
            double umbral = 30;

            Console.Write("Introduce la temperatura actual del invernadero: ");
            temperatura = Convert.ToDouble(Console.ReadLine());

            ControlTemperatura controlTemperatura = new ControlTemperatura();

            ServicioAlerta servicioAlerta = new ServicioAlerta();
            ServicioRegistroTemperatura servicioRegistro = new ServicioRegistroTemperatura();

            controlTemperatura.TemperaturaAlta += servicioAlerta.EnviarAlerta;
            controlTemperatura.TemperaturaAlta += servicioRegistro.RegistrarTemperatura;

            controlTemperatura.ComprobarTemperatura(temperatura, umbral);
        }
    }
}
