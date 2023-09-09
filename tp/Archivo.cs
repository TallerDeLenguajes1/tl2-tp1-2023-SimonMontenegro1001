namespace Name
{
    class Archivo
    {
        public static string LeerArchivo(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using var reader = new StreamReader(path);
                    return reader.ReadToEnd();
                }
                else
                {
                    return "El archivo no existe.";
                }
            }
            catch (IOException e)
            {
                return "Error al leer el archivo: " + e.Message;
            }
        }

        public static void EscribirArchivo(string path, string contenido)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(path);
                writer.Write(contenido);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error al escribir en el archivo: " + e.Message);
            }
        }
    }
}
