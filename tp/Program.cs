using System.Security.Cryptography;
using Name;

class Program
{
    static void Main()
    {
        Console.Clear();
        var carga = new Carga();
        Cadeteria? c1 = carga.Cadeteria("cadeteria.csv");
        if (c1 != null)
        {
            if (carga.Cadetes("cadete.csv", c1))
            {
                var ListaPedidos = new List<Pedido>();
                do
                {
                    Console.WriteLine("\n\n\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                    //Console.WriteLine("\n\n1-Dar alta Pedido \n2-Asignar Pedidos \n3-Cambiar Pedido de estado \n4-Reasignar Pedido\n5-Lista de pedidos\n6-Lista de Cadetes\n7-Generar Informe");
                    Console.WriteLine("\n\n1-Dar alta Pedido \n2-Asignar Pedidos \n3-Cambiar Pedido de estado \n4-Reasignar Pedido\n5-Generar Informe");
                    if (int.TryParse(Console.ReadLine(), out var op))
                    {
                        switch (op)
                        {
                            case 1:
                                Console.WriteLine("Ingrese los datos del cliente:");
                                var nombreCliente = SolicitarEntrada<string>("Nombre:");
                                var telefonoCliente = SolicitarEntrada<string>("Teléfono: ");
                                var direccionCliente = SolicitarEntrada<string>("Dirección: ");
                                var observacionesDireccion = SolicitarEntrada<string>("Observaciones (opcional): ", true);
                                var pedido = c1.AltaPedido(nombreCliente, direccionCliente, telefonoCliente, observacionesDireccion);
                                Console.WriteLine($"Pedido #{pedido.Nro} Agregado a nombre de '{pedido.Cliente.Nombre}'.");
                                ListaPedidos.Add(pedido);
                                break;
                            case 2:
                                var pedidoNro = SolicitarEntrada<int>("Número del pedido: ");
                                var cadeteNombre = SolicitarEntrada<int>("Id del cadete: ");
                                Pedido? p = ListaPedidos.FirstOrDefault(pedido => pedido.Nro == pedidoNro);
                                if (p != null)
                                {
                                    string mensaje = c1.AsignarPedido(cadeteNombre, p) ? "Pedido Asignado." : "ERROR: El pedido ya está asignado.";
                                    Console.WriteLine(mensaje);
                                }
                                else
                                {
                                    Console.WriteLine("No existe el pedido.");
                                }
                                break;
                            case 3:
                                var cadeteId = SolicitarEntrada<int>("ID del cadete: ");
                                pedidoNro = SolicitarEntrada<int>("Número del pedido: ");
                                Console.WriteLine("¿Qué acción desea realizar?\n1. Entregar pedido\n2. Cancelar pedido");
                                var opcion = SolicitarEntrada<int>("");
                                switch (opcion)
                                {
                                    case 1:
                                        var mensaje = c1.EntregarPedido(cadeteId, pedidoNro) ? "Pedido Entregado." : "ERROR: El pedido no esta asignado a este cadete.";
                                        Console.WriteLine(mensaje);
                                        break;
                                    case 2:
                                        mensaje = c1.CancelarPedido(cadeteId, pedidoNro) ? "Pedido Cancelado." : "ERROR: El pedido no esta asignado a este cadete.";
                                        Console.WriteLine(mensaje);
                                        break;
                                    default:
                                        Console.WriteLine("Opción no válida.");
                                        break;
                                }
                                break;
                            case 4:
                                int cadeteOrigenId = SolicitarEntrada<int>("ID del cadete origen: ");
                                int cadeteDestinoId = SolicitarEntrada<int>("ID del cadete destino: ");
                                pedidoNro = SolicitarEntrada<int>("Número del pedido: ");
                                p = ListaPedidos.FirstOrDefault(pedido => pedido.Nro == pedidoNro);
                                if (p != null && (c1.ObtenerCadetePorId(cadeteDestinoId) != null) && (c1.ObtenerCadetePorId(cadeteOrigenId) != null))
                                {
                                    string mensaje = c1.ReasignarPedido(cadeteOrigenId, cadeteDestinoId, p) ? "Pedido Reasignado." : $"ERROR: El pedido no pertenece a al cadete.";
                                    Console.WriteLine(mensaje);
                                }
                                else
                                {
                                    Console.WriteLine("El pedido o los cadetes no existen.");
                                }
                                break;
                            case 5:
                                c1.GenerarInforme();
                                break;
                            // case 6:
                            //     Console.WriteLine($"\nlista de Pedidos");
                            //     foreach (var a in ListaPedidos)
                            //     {
                            //         Console.WriteLine("-----------------------------");
                            //         Console.WriteLine($"Número de Pedido: {a.Nro}");
                            //         Console.WriteLine($"Estado: {a.Estado}");
                            //         Console.WriteLine("\nCliente:\n");
                            //         Console.WriteLine(a.VerDatosCliente());
                            //         Console.WriteLine(a.VerDireccionCliente());
                            //     }
                            //     Console.WriteLine("-----------------------------");
                            //     break;
                            // case 7:
                            //     Console.WriteLine($"\nlista de Cadetes");
                            //     foreach (var a in c1.listadoCadetes)
                            //     {
                            //         Console.WriteLine("-----------------------------");
                            //         Console.WriteLine($"Número de Cadete: {a.Id}");
                            //         Console.WriteLine($"Nombre: {a.Nombre}");
                            //         Console.WriteLine($"Dirección: {a.Direccion}");
                            //         Console.WriteLine($"Telefono: {a.Telefono}\n");
                            //         a.VerPedidos();
                            //     }
                            //     Console.WriteLine("-----------------------------");
                            //     break;
                            default:
                                Console.WriteLine("Opción inválida");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada no válida. Debe ingresar un número.");
                    }
                } while (true);
            }
            else
            {
                Console.WriteLine("El archivo está vacío o no contiene datos de los cadetes.");
            }
        }
    }
    public static T SolicitarEntrada<T>(string mensaje, bool permitirValoresVacios = false)
    {
        T? entrada = default;

        bool esValido = false;
        do
        {
            Console.WriteLine(mensaje);
            string? entradaTexto = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entradaTexto))
            {
                if (permitirValoresVacios)
                {
                    if (typeof(T) == typeof(string))
                    {
                        entrada = (T)(object)entradaTexto;
                        esValido = true;
                    }
                    else
                    {
                        Console.WriteLine("Por favor, ingrese un valor válido.");
                    }
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un valor válido.");
                }
            }
            else if (typeof(T) == typeof(string))
            {
                entrada = (T)(object)entradaTexto;
                esValido = true;
            }
            else if (typeof(T) == typeof(int))
            {
                if (int.TryParse(entradaTexto, out int entradaEntero))
                {
                    entrada = (T)(object)entradaEntero;
                    esValido = true;
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                }
            }
            else
            {
                Console.WriteLine("Tipo de entrada no admitido.");
            }
        } while (!esValido);

        return entrada;
    }
}