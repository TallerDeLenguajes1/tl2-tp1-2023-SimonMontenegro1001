namespace Name
{
    public class Cadete
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public List<Pedido> ListadoPedidos { get; private set; }

        public Cadete(int id, string nombre, string direccion, string telefono)
        {
            // Agregar control control para datos vacios y nulos
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            ListadoPedidos = new List<Pedido>();
        }
        public bool AgregarPedido(Pedido pedido)
        {

            if (pedido != null && !ListadoPedidos.Contains(pedido))
            {
                ListadoPedidos.Add(pedido);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CancelarPedido(int nroPedido)
        {
            Pedido? pedido = ListadoPedidos.FirstOrDefault(pedido => pedido.Nro == nroPedido);

            if (pedido != null)
            {
                pedido.Cancelar();
                BorrarPedido(pedido);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool BorrarPedido(Pedido pedido)
        {
            if (pedido != null)
            {
                ListadoPedidos.Remove(pedido);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EntregarPedido(int nroPedido)
        {
            Pedido? pedido = ListadoPedidos.FirstOrDefault(pedido => pedido.Nro == nroPedido);

            if (pedido != null)
            {
                pedido.Entregar();
                return true;
            }
            else
            {
                return false;
            }
        }
        public int Jornal()
        {
            int jornal = ListadoPedidos.Count(pedido => pedido.Estado == EstadoPedido.Entregado) * 500;
            return jornal;
        }
        public int CantidadPedidos()
        {
            return ListadoPedidos.Count(pedido => pedido.Estado == EstadoPedido.Entregado);
        }
        public void VerPedidos()
        {
            Console.WriteLine($"Pedidos de {Nombre}:");

            foreach (var pedido in ListadoPedidos)
            {
                Console.WriteLine($"Pedido #{pedido.Nro} - Estado: {pedido.Estado}");
            }
        }

    }
}