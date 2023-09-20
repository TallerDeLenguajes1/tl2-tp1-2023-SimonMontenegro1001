using System.Text.Json;
namespace Name
{
    // Deberia llamarse de otra forma la clase o crear otra clase, por lo que se crean cadeterias y se  agregan cadetes
    public abstract class AccesoADatos
    {
        protected string ReadFile(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return text;
        }
        public abstract void AddCadetes(Cadeteria cadeteria, string cadetesFilePath);
        public abstract Cadeteria? GetCadeteria(string cadeteriaFilePath);
    }

    public class AccesoCSV : AccesoADatos
    {
        public override Cadeteria? GetCadeteria(string cadeteriaFilePath)
        {
            var text = ReadFile(cadeteriaFilePath);
            string[] lines = text.Split('\n');

            if (lines.Length >= 1)
            {
                string[] splits = lines[0].Split(';');
                if (splits.Length == 2)
                {
                    return new Cadeteria(splits[0], splits[1]);
                }
            }
            return null;
        }
        public override void AddCadetes(Cadeteria cadeteria, string cadetesFilePath)
        {
            var text = ReadFile(cadetesFilePath);
            string[] cadetes = text.Split('\n');

            if (cadetes.Length >= 1)
            {
                foreach (string line in cadetes)
                {
                    string[] splits = line.Split(';');
                    if (splits.Length >= 3)
                    {
                        cadeteria.AltaCadete(splits[0], splits[1], splits[2]);
                    }
                }
            }
        }
    }
    public class AccesoJSON : AccesoADatos
    {
        public override Cadeteria? GetCadeteria(string cadeteriaFilePath)
        {
            var text = ReadFile(cadeteriaFilePath);
            try
            {
                var cadeteria = JsonSerializer.Deserialize<Cadeteria>(text);
                return cadeteria;
            }
            catch (JsonException)
            {
                System.Console.WriteLine("ups");
                return null;
            }
        }
        public override void AddCadetes(Cadeteria cadeteria, string cadetesFilePath)
        {
            var text = ReadFile(cadetesFilePath);
            var cadetes = JsonSerializer.Deserialize<List<Cadete>>(text);
            if (cadetes != null)
            {
                foreach (var cadete in cadetes)
                {
                    cadeteria.AltaCadete(cadete.Nombre, cadete.Direccion, cadete.Telefono);
                }
            }
        }
    }
}
