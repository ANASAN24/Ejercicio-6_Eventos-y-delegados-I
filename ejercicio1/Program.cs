using System;

namespace ejercicio1
{
    internal class Program
    {
        class VentaEventArgs : EventArgs
        {
            public string Producto { get; set; }
            public double Precio { get; set; }
        }

        class RegistroVentas
        {
            public event EventHandler<VentaEventArgs> VentaRealizada;

            public void ProcesarVenta(string producto, double precio)
            {
                VentaEventArgs datosVenta = new VentaEventArgs();

                datosVenta.Producto = producto;
                datosVenta.Precio = precio;

                if (VentaRealizada != null)
                {
                    VentaRealizada(this, datosVenta);
                }
            }
        }

        class ServicioRegistro
        {
            public void RegistrarVenta(object sender, VentaEventArgs e)
            {
                Console.WriteLine("Venta registrada:");
                Console.WriteLine("Producto: " + e.Producto);
                Console.WriteLine("Precio: " + e.Precio + " euros");
            }
        }

        class ServicioNotificacion
        {
            public void EnviarNotificacionVenta(object sender, VentaEventArgs e)
            {
                Console.WriteLine("Notificación: Se ha realizado una venta de " + e.Producto);
            }
        }

        static void Main(string[] args)
        {
            string producto;
            double precio;

            Console.Write("Introduce el nombre del producto vendido: ");
            producto = Console.ReadLine();

            Console.Write("Introduce el precio del producto: ");
            precio = Convert.ToDouble(Console.ReadLine());

            RegistroVentas registroVentas = new RegistroVentas();

            ServicioRegistro servicioRegistro = new ServicioRegistro();
            ServicioNotificacion servicioNotificacion = new ServicioNotificacion();

            registroVentas.VentaRealizada += servicioRegistro.RegistrarVenta;
            registroVentas.VentaRealizada += servicioNotificacion.EnviarNotificacionVenta;

            registroVentas.ProcesarVenta(producto, precio);
        }
    }
}