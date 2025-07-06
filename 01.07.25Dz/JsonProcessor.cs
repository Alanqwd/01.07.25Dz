using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dz
{ 
    public class JsonProcessor : IDataProcessor<ModernArtifact>
    {



        public List<ModernArtifact> LoadData(string filePath)


        {
            try
            {
                string json = File.ReadAllText(filePath);
                return DeserializeModernArtifacts(json);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке JSON файла: {ex.Message}");
                return new List<ModernArtifact>();
            }

        }

        public void SaveData(List<ModernArtifact> data, string filePath)
        {
            try
            {
                string json = SerializeModernArtifacts(data);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении JSON файла: {ex.Message}");
            }
        }

        private List<ModernArtifact> DeserializeModernArtifacts(string json)
        {
            List<ModernArtifact> artifacts = new List<ModernArtifact>();

         
            json = json.Trim().TrimStart('(').TrimEnd(')');

     
            string[] jsonObjects = json.Split(new string[] { "},{" }, StringSplitOptions.None);

            foreach (string jsonObject in jsonObjects)
            {
     
                string[] keyValuePairs = jsonObject.Split(',');

                ModernArtifact artifact = new ModernArtifact();
                foreach (string keyValuePair in keyValuePairs)
                {
                    string[] parts = keyValuePair.Split(':');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim().Trim('"');
                        string value = parts[1].Trim().Trim('"');

                        switch (key)
                        {
                            case "Id":
                                artifact.Id = int.Parse(value);
                                break;
                            case "Name":
                                artifact.Name = value;
                                break;
                            case "PowerLevel":
                                artifact.PowerLevel = int.Parse(value);
                                break;
                            case "Rarity":
                                if (Enum.TryParse<Rarity>(value, out Rarity rarity))
                                {
                                    artifact.Rarity = rarity;
                                }
                                else
                                {
                                    Console.WriteLine($"Не удалось разобрать Rarity: {value}");
                                }
                                break;
                            case "TechLevel":
                                artifact.TechLevel = double.Parse(value);
                                break;
                            case "Manufacturer":
                                artifact.Manufacturer = value;
                                break;
                        }
                    }
                }
                artifacts.Add(artifact);
            }
            return artifacts;
        }

        private string SerializeModernArtifacts(List<ModernArtifact> artifacts)
        {


            string json = "(";
            foreach (ModernArtifact artifact in artifacts)
            {



                json += "{";
                json += $"\"Id\": \"{artifact.Id}\",";
                json += $"\"Name\": \"{artifact.Name}\",";
                json += $"\"PowerLevel\": \"{artifact.PowerLevel}\",";
                json += $"\"Rarity\": \"{artifact.Rarity}\",";
                json += $"\"TechLevel\": \"{artifact.TechLevel}\",";
                json += $"\"Manufacturer\": \"{artifact.Manufacturer}\"";
                json += "},";


            }


            if (artifacts.Any())
            {
                json = json.TrimEnd(',');
            }
            json += ")";
            return json;
        }
    }
}
