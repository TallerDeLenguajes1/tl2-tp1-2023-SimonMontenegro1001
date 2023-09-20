
namespace Name
{
    public enum EstadoPedido
    {
        Pendiente,
        Entregado,
        Cancelado,
        NoAsignado
    }
    public class Pedido
    {
        public int Nro { get; set; }
        public Cliente Cliente { get; set; }
        public EstadoPedido Estado { get; private set; }
        public Cadete? Cadete { get; private set; }
        public Pedido(Cliente cliente, int nro)
        {
            // Agregar control control para datos vacios y nulos
            Cliente = cliente;
            Nro = nro;
            Estado = EstadoPedido.NoAsignado;
        }
        public void AsignarCadete(Cadete cadete)
        {
            if (cadete != null) Cadete = cadete;
            Estado = EstadoPedido.Pendiente;
        }
        public void Cancelar()
        {
            Estado = EstadoPedido.Cancelado;
        }
        public void Entregar()
        {
            Estado = EstadoPedido.Entregado;
            if (Cadete != null) Cadete.CantidadDePedidosEntregados++;
        }
        public string DatosCliente()
        {
            return $"Nombre: {Cliente.Nombre} \nTeléfono: {Cliente.Telefono}";
        }
        public string DireccionCliente()
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