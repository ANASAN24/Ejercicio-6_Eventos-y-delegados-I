using System;

namespace ejercicio5
{
    internal class Program
    {
        class EnergiaEventArgs : EventArgs
        {
            public double ConsumoActual { get; set; }
            public double Umbral { get; set; }
        }

        class MonitorEnergia
        {
            public event EventHandler<EnergiaEventArgs> ConsumoExcesivoDetectado;

            public void ComprobarConsumo(double consumo, double umbral)
            {
                if (consumo > umbral)
                {
                    EnergiaEventArgs datosEnergia = new EnergiaEventArgs();

                    datosEnergia.ConsumoActual = consumo;
                    datosEnergia.Umbral = umbral;

                    if (ConsumoExcesivoDetectado != null)
                    {
                        ConsumoExcesivoDetectado(this, datosEnergia);
                    }
                }
                else
                {
                    Console.WriteLine("El consumo de energía es correcto.");
                    Console.WriteLine("Consumo actual: " + consumo + " kWh");
                }
            }
        }

        class ServicioNotificacion
        {
            public void EnviarAdvertencia(object sender, EnergiaEventArgs e)
            {
                Console.WriteLine("ADVERTENCIA: Se ha superado el consumo de energía permitido.");
                Console.WriteLine("Consumo actual: " + e.ConsumoActual + " kWh");
                Console.WriteLine("Umbral establecido: " + e.Umbral + " kWh");
            }
        }

        class ServicioAjusteAutomatizado
        {
            public void AjustarDispositivos(object sender, EnergiaEventArgs e)
            {
                Console.WriteLine("Ajuste automatizado activado.");
                Console.WriteLine("Se están ajustando los dispositivos para reducir el consumo.");
            }
        }

        static void Main(string[] args)
        {
            double consumo;
            double umbral = 100;

            Console.Write("Introduce el consumo actual de energía en kWh: ");
            consumo = Convert.ToDouble(Console.ReadLine());

            MonitorEnergia monitorEnergia = new MonitorEnergia();

            ServicioNotificacion servicioNotificacion = new ServicioNotificacion();
            ServicioAjusteAutomatizado servicioAjuste = new ServicioAjusteAutomatizado();

            monitorEnergia.ConsumoExcesivoDetectado += servicioNotificacion.EnviarAdvertencia;
            monitorEnergia.ConsumoExcesivoDetectado += servicioAjuste.AjustarDispositivos;

            monitorEnergia.ComprobarConsumo(consumo, umbral);
        }
    }
}
