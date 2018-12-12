using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace AE_Chatt
{
    static class Configurator
    {
        public static Uri ServerDomain { get; set; }

        public static bool Initialize()
        {
            if (!File.Exists("connection.config"))
            {
                using(XmlWriter writer = XmlWriter.Create("connection.config"))
                {
                    writer.WriteStartElement("configuration");
                    writer.WriteElementString("server_domain", "http://10.110.226.181/AEChatt/AE.php");
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }

            using(XmlReader reader = XmlReader.Create("connection.config"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "server_domain":
                                reader.Read();
                                ServerDomain = new Uri(reader.Value);
                                break;
                        }
                    }
                }
            }

            return true;
        }
    }
}
