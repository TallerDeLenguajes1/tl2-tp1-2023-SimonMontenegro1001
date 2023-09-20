namespace Name
{
    public class Cadeteria
    {
        private int contadorPedidos = 0;
        private int contadorCadetes = 0;
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public List<Pedido> ListadoPedidos { get; set; }
        public List<Cadete> listadoCadetes;
        public Cadeteria(string nombre, string telefono)
        {
            // Agregar control control para datos vacios y nulos
            Nombre = nombre;
            Telefono = telefono;
            ListadoPedidos = new();
            listadoCadetes = new();
        }
        public int JornalACobrar(int cadeteId)
        {
            int jornal = 0;
            foreach (var pedido in ListadoPedidos)
            {
                if (pedido.Cadete?.Id == cadeteId && pedido.Estado == EstadoPedido.Entregado)
                {
                    jornal += 500;
                }
            }
            return jornal;
        }
        public void EntregarPedido(int pedidoNro)
        {
            var pedido = ObtenerPedidoPorId(pedidoNro);
            pedido?.Entregar();
        }
        public void CancelarPedido(int pedidoNro)
        {
            var pedido = ObtenerPedidoPorId(pedidoNro);
            pedido?.Cancelar();
        }
        public void AsignarCadeteAPedido(int cadeteId, int pedidoNro)
        {
            var pedido = ObtenerPedidoPorId(pedidoNro);
            var cadete = ObtenerCadetePorId(cadeteId);
            if (pedido != null && cadete != null)
            {
                pedido.AsignarCadete(cadete);
            }
        }
        public void AltaCadete(string nombre, string direccion, string telefono)
        {
            contadorCadetes++;
            var nuevoCadete = new Cadete(contadorCadetes, nombre, direccion, telefono);
            listadoCadetes.Add(nuevoCadete);
        }
        public void AltaPedido(string Nombre, string Direccion, string Telefono, string DatosReferenciaDireccion)
        {
            var nuevoCliente = new Cliente(Nombre, Direccion, Telefono, DatosReferenciaDireccion);
            contadorPedidos++;
            Pedido nuevoPedido = new(nuevoCliente, contadorPedidos);
            ListadoPedidos.Add(nuevoPedido);
        }
        public Cadete? ObtenerCadetePorId(int cadeteId)
        {
            return listadoCadetes.FirstOrDefault(c => c.Id == cadeteId);
        }
        public Pedido? ObtenerPedidoPorId(int pedidoId)
        {
            return ListadoPedidos.FirstOrDefault(c => c.Nro == pedidoId);
        }
        public void GenerarInforme()
        {
            double montoTotal = 0;
            int totalEnvios = 0;

            Console.WriteLine("Informe de Pedidos:");

            foreach (Cadete cadete in listadoCadetes)
            {
                int enviosCadete = cadete.CantidadDePedidosEntregados;
                double montoCadete = JornalACobrar(cadete.Id);

                Console.WriteLine($"Cadete: {cadete.Nombre}");
                Console.WriteLine($"Cantidad de Envíos: {enviosCadete}");
                Console.WriteLine($"Monto Ganado: ${montoCadete}\n");

                totalEnvios += enviosCadete;
                montoTotal += montoCadete;
            }

            double promedioEnviosPorCadete = (double)totalEnvios / listadoCadetes.Count;

            Console.WriteLine("Resumen General:");
            Console.WriteLine($"Total de Envíos: {totalEnvios}");
            Console.WriteLine($"Monto Total Ganado: ${montoTotal}");
            Console.WriteLine($"Cantidad Promedio de Envíos por Cadete: {promedioEnviosPorCadete:F2}");
        }
        public void VerInformacion()
        {
            Console.WriteLine($"nombre: {Nombre}, telefono {Telefono}");
        }
        public void VerPedidos()
        {
            Console.WriteLine($"\nlista de Pedidos");
            foreach (var a in ListadoPedidos)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"Número de Pedido: {a.Nro}");
                string mensaje = a.Cadete?.Id != null ? a.Cadete.Id.ToString() : "Sin asignar";
                Console.WriteLine($"Cadete: {mensaje}");
                Console.WriteLine($"Estado: {a.Estado}");
                Console.WriteLine("\nCliente:\n");
                Console.WriteLine(a.DatosCliente());
                Console.WriteLine(a.DireccionCliente());
            }
            Console.WriteLine("-----------------------------");
        }
        public void VerCadetes()
        {
            Console.WriteLine($"\nlista de Cadetes");
            foreach (var a in listadoCadetes)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"Número de Cadete: {a.Id}");
                Console.WriteLine($"Nombre: {a.Nombre}");
                Console.WriteLine($"Dirección: {a.Direccion}");
                Console.WriteLine($"Telefono: {a.Telefono}\n");
            }
            Console.WriteLine("-----------------------------");
        }
    }
}