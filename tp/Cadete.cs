namespace Name
{
    public class Cadete
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int CantidadDePedidosEntregados { get; set; }
        public Cadete(int id, string nombre, string direccion, string telefono)
        {
            // Agregar control control para datos vacios y nulos
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            CantidadDePedidosEntregados = 0;
        }
    }
}