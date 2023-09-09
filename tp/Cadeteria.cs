namespace Name
{
    public class Cadeteria
    {
        private int contadorPedidos = 0;
        private int contadorCadetes = 0;
        private string Nombre { get; set; }
        private string Telefono { get; set; }
        public List<Cadete> listadoCadetes;

        public Cadeteria(string nombre, string telefono)
        {
            // Agregar control control para datos vacios y nulos
            Nombre = nombre;
            Telefono = telefono;
            listadoCadetes = new List<Cadete>();
        }
        public bool EntregarPedido(int cadeteId, int pedidoNro)
        {
            Cadete? cadete = ObtenerCadetePorId(cadeteId);
            if (cadete != null)
            {
                return cadete.EntregarPedido(pedidoNro);
            }
            return false;
        }
        public bool CancelarPedido(int cadeteId, int pedidoNro)
        {
            Cadete? cadete = ObtenerCadetePorId(cadeteId);
            if (cadete != null)
            {
                return cadete.CancelarPedido(pedidoNro);
            }
            return false;
        }
        public bool AsignarPedido(int cadeteId, Pedido pedido)
        {
            Cadete? cadete = ObtenerCadetePorId(cadeteId);

            if (cadete != null && !PedidoAsignado(pedido))
            {
                return cadete.AgregarPedido(pedido);
            }
            return false;
        }
        public bool ReasignarPedido(int viejoCadeteId, int nuevoCadeteId, Pedido pedido)
        {
            Cadete? viejoCadete = ObtenerCadetePorId(viejoCadeteId);
            Cadete? nuevoCadete = ObtenerCadetePorId(nuevoCadeteId);

            if (viejoCadete != null && nuevoCadete != null)
            {
                if (viejoCadete.BorrarPedido(pedido))
                {
                    return nuevoCadete.AgregarPedido(pedido);
                }
            }
            return false;
        }
        public void AgregarCadete(string nombre, string direccion, string telefono)
        {
            contadorCadetes++;
            var nuevoCadete = new Cadete(contadorCadetes, nombre, direccion, telefono);
            listadoCadetes.Add(nuevoCadete);
        }
        public Pedido AltaPedido(string Nombre, string Direccion, string Telefono, string DatosReferenciaDireccion)
        {
            var nuevoCliente = new Cliente(Nombre, Direccion, Telefono, DatosReferenciaDireccion);
            contadorPedidos++;
            Pedido nuevoPedido = new(nuevoCliente, contadorPedidos);
            return nuevoPedido;
        }
        public Cadete? ObtenerCadetePorId(int cadeteId)
        {
            return listadoCadetes.FirstOrDefault(c => c.Id == cadeteId);
        }
        public void GenerarInforme()
        {
            // Inicializar variables para el informe
            double montoTotal = 0;
            int totalEnvios = 0;

            Console.WriteLine("Informe de Pedidos:");

            // Iterar a través de los cadetes
            foreach (Cadete cadete in listadoCadetes)
            {
                int enviosCadete = cadete.CantidadPedidos();
                double montoCadete = cadete.Jornal();

                // Mostrar información del cadete
                Console.WriteLine($"Cadete: {cadete.Nombre}");
                Console.WriteLine($"Cantidad de Envíos: {enviosCadete}");
                Console.WriteLine($"Monto Ganado: ${montoCadete}\n");

                // Actualizar variables totales
                totalEnvios += enviosCadete;
                montoTotal += montoCadete;
            }

            // Calcular la cantidad promedio de envíos por cadete
            double promedioEnviosPorCadete = (double)totalEnvios / listadoCadetes.Count;

            // Mostrar información total
            Console.WriteLine("Resumen General:");
            Console.WriteLine($"Total de Envíos: {totalEnvios}");
            Console.WriteLine($"Monto Total Ganado: ${montoTotal}");
            Console.WriteLine($"Cantidad Promedio de Envíos por Cadete: {promedioEnviosPorCadete:F2}");
        }
        public void VerInformacion()
        {
            Console.WriteLine($"nombre: {Nombre}, telefono {Telefono}");
        }
        private bool PedidoAsignado(Pedido pedido)
        {
            return listadoCadetes.Any(cadete => cadete.ListadoPedidos.Contains(pedido));
        }
    }
}