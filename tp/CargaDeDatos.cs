namespace Name
{
    public class Carga
    {

        public Cadeteria? Cadeteria(string archivo)
        {
            try
            {
                string contenido = Archivo.LeerArchivo(archivo);
                string[] lineas = contenido.Split('\n');

                if (lineas.Length >= 1)
                {
                    string[] splits = lineas[0].Split(';');
                    if (splits.Length == 2)
                    {
                        return new Cadeteria(splits[0], splits[1]);
                    }
                }
                else
                {
                    Console.WriteLine("El archivo está vacío o no contiene datos de cadetería.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar la cadetería: {ex.Message}");
            }

            return null;
        }
        public bool Cadetes(string archivo, Cadeteria cadeteria)
        {
            try
            {
                string cadetesContenido = Archivo.LeerArchivo(archivo);
                string[] cadetes = cadetesContenido.Split('\n');

                if (cadetes.Length >= 1)
                {
                    foreach (string line in cadetes)
                    {
                        string[] splits = line.Split(';');
                        if (splits.Length >= 3)
                        {
                            cadeteria.AgregarCadete(splits[0], splits[1], splits[2]);
                        }
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("El archivo de cadetes está vacío o no contiene datos válidos.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar cadetes desde el archivo: {ex.Message}");
                return false;
            }
        }
    }
}