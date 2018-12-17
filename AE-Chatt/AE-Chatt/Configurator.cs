namespace AE_Chatt
{
    using System;
    using System.IO;
    using System.Xml;

    static class Configurator
    {
        public static Uri ServerDomain { get; set; }

        public static void Initialize()
        {
            if (!File.Exists("configuration.config"))
            {
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, IndentChars = "\t"};
                using (XmlWriter writer = XmlWriter.Create("configuration.config", settings))
                {
                    writer.WriteStartElement("configuration");
                    writer.WriteElementString("server_domain", "http://10.110.226.181/AEChatt/AE.php");
                    writer.WriteEndElement();
                }
            }
            if (!File.Exists("pending_messages.xml"))
            {
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, IndentChars = "\t" };
                using (XmlWriter writer = XmlWriter.Create("pending_messages.xml", settings))
                {
                    writer.WriteStartElement("pending_messages");
                    writer.WriteEndElement();
                }
            }

            using(XmlReader reader = XmlReader.Create("configuration.config"))
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
        }
    }
}
