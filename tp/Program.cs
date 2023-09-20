using Name;

class Program
{
    static void Main()
    {
        Console.Clear();
        var fileReadingMethod = SolicitarEntrada<string>("Ingrese la extensión del archivo que desea leer (1 -> json, 2 -> csv): ");
        Cadeteria? c1 = null;
        switch (fileReadingMethod)
        {
            case "1":
                var accesoJson = new AccesoJSON();
                c1 = accesoJson.GetCadeteria("data/cadeteria.json");
                if (c1 != null) accesoJson.AddCadetes(c1, "data/cadete.json");
                Console.WriteLine("Los datos se cargaron.");
                break;
            case "2":
                var accesoCsv = new AccesoCSV();
                c1 = accesoCsv.GetCadeteria("data/cadeteria.csv");
                if (c1 != null) accesoCsv.AddCadetes(c1, "data/cadete.csv");
                Console.WriteLine("Los datos se cargaron.");
                break;
            default:
                Console.WriteLine("No se cargaron datos.");
                break;
        }
        c1?.VerInformacion();
        Console.WriteLine("\n\n\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
        if (c1 != null)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n1-Dar alta Pedido \n2-Asignar Pedidos \n3-Cambiar Pedido de estado \n4-Reasignar Pedido\n5-Generar Informe\n6-Lista de pedidos\n7-Lista de Cadetes");
                //Console.WriteLine("\n\n1-Dar alta Pedido \n2-Asignar Cadete a pedido \n3-Cambiar Pedido de estado \n4-Reasignar Pedido\n5-Generar Informe");
                string? op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        Console.WriteLine("Ingrese los datos del cliente:");
                        var nombreCliente = SolicitarEntrada<string>("Nombre:");
                        var telefonoCliente = SolicitarEntrada<string>("Teléfono: ");
                        var direccionCliente = SolicitarEntrada<string>("Dirección: ");
                        var observacionesDireccion = SolicitarEntrada<string>("Observaciones (opcional): ", true);
                        c1.AltaPedido(nombreCliente, direccionCliente, telefonoCliente, observacionesDireccion);
                        Console.WriteLine("\n\n\nPresiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case "2":
                        var pedidoNro = SolicitarEntrada<int>("Número del pedido: ");
                        var cadeteId = SolicitarEntrada<int>("Id del cadete: ");
                        c1.AsignarCadeteAPedido(cadeteId, pedidoNro);
                        Console.WriteLine("\n\n\nPresiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case "3":
                        pedidoNro = SolicitarEntrada<int>("Número del pedido: ");
                        var opcion = SolicitarEntrada<string>("¿Qué acción desea realizar?\n1. Entregar pedido\n2. Cancelar pedido");
                        switch (opcion)
                        {
                            case "1":
                                c1.EntregarPedido(pedidoNro);
                                break;
                            case "2":
                                c1.CancelarPedido(pedidoNro);
                                break;
                            default:
                                Console.WriteLine("Opción no válida.");
                                break;
                        }
                        break;
                    case "4":
                        int cadeteDestinoId = SolicitarEntrada<int>("ID del cadete destino: ");
                        pedidoNro = SolicitarEntrada<int>("Número del pedido: ");
                        c1.AsignarCadeteAPedido(cadeteDestinoId, pedidoNro);
                        Console.WriteLine("\n\n\nPresiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case "5":
                        c1.GenerarInforme();
                        Console.WriteLine("\n\n\nPresiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case "6":
                        c1.VerPedidos();
                        Console.WriteLine("\n\n\nPresiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case "7":
                        c1.VerCadetes();
                        Console.WriteLine("\n\n\nPresiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }

            } while (true);
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