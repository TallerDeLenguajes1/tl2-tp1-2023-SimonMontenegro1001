// using System.IO.Compression;
// using Name;

// class Program
// {
//     static void Main()
//     {
//         Cadeteria? c1 = CrearCadeteriaDesdeArchivo("cadeteria.csv");
//         if (CargarCadetesDesdeArchivo("cadete.csv", c1))
//         {
//             c1.Info();
//             foreach (var a in c1.listadoCadetes)
//             {
//                 System.Console.WriteLine($"id: {a.Id}");
//                 a.MostrarPedidos();
//             }
//         }
//         else
//         {
//             System.Console.WriteLine("ja");
//         }

//     }
//     static Cadeteria? CrearCadeteriaDesdeArchivo(string archivo)
//     {
//         try
//         {
//             string contenido = Archivo.LeerArchivo(archivo);
//             string[] lineas = contenido.Split('\n');

//             if (lineas.Length >= 1)
//             {
//                 string[] splits = lineas[0].Split(';');
//                 if (splits.Length == 2)
//                 {
//                     return new Cadeteria(splits[0], splits[1]);
//                 }
//             }
//             else
//             {
//                 Console.WriteLine("El archivo está vacío o no contiene datos de cadetería.");
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Error al cargar la cadetería: {ex.Message}");
//         }

//         return null;
//     }
//     static bool CargarCadetesDesdeArchivo(string archivo, Cadeteria cadeteria)
//     {
//         try
//         {
//             string cadetesContenido = Archivo.LeerArchivo(archivo);
//             string[] cadetes = cadetesContenido.Split('\n');

//             if (cadetes.Length >= 1)
//             {
//                 foreach (string line in cadetes)
//                 {
//                     string[] splits = line.Split(';');
//                     if (splits.Length >= 3)
//                     {
//                         var cadete = cadeteria.CrearCadete(splits[0], splits[1], splits[2]);
//                         cadeteria.AgregarCadete(cadete);
//                     }
//                 }
//                 return true;
//             }
//             else
//             {
//                 Console.WriteLine("El archivo de cadetes está vacío o no contiene datos válidos.");
//                 return false;
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Error al cargar cadetes desde el archivo: {ex.Message}");
//             return false;
//         }
//     }


// }

