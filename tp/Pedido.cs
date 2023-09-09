namespace Name
{
    public enum EstadoPedido
    {
        Pendiente,
        Entregado,
        Cancelado
    }
    public class Pedido
    {
        public int Nro { get; set; }
        public Cliente Cliente { get; set; }
        public EstadoPedido Estado { get; private set; }
        public Pedido(Cliente cliente, int nro)
        {
            // Agregar control control para datos vacios y nulos
            Cliente = cliente;
            Nro = nro;
            Estado = EstadoPedido.Pendiente;
        }
        public void Cancelar()
        {
            Estado = EstadoPedido.Cancelado;
        }
        public void Entregar()
        {
            Estado = EstadoPedido.Entregado;
        }
        public string VerDatosCliente()
        {
            return $"Nombre: {Cliente.Nombre} \nTeléfono: {Cliente.Telefono}";
        }
        public string VerDireccionCliente()
        {
            if (string.IsNullOrWhiteSpace(Cliente.DatosReferenciaDireccion))
            {
                return $"Direccion: {Cliente.Direccion}";
            }
            else
            {
                return $"Direccion: {Cliente.Direccion} [{Cliente.DatosReferenciaDireccion}]";
            }
        }
    }
}