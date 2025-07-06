
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dz
{
    public class XmlProcessor : IDataProcessor<AntiqueArtifact>
    {

        public List<AntiqueArtifact> LoadData(string filePath)
        {

            try
            {

                XmlSerializer serializer = new XmlSerializer(typeof(List<AntiqueArtifact>));  
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    return (List<AntiqueArtifact>)serializer.Deserialize(fileStream);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ошибка при загрузке XML файла: {ex.Message}");
                return new List<AntiqueArtifact>();

            }
        }

        public void SaveData(List<AntiqueArtifact> data, string filePath)
        {
            try

            {

                XmlSerializer serializer = new XmlSerializer(typeof(List<AntiqueArtifact>));
                using (TextWriter writer = new StreamWriter(filePath))
                {

                    serializer.Serialize(writer, data);

                }
            }

            catch (Exception ex)
            {

                Console.WriteLine($"Ошибка при сохранении XML файла: {ex.Message}");

            }
        }
    }
}
