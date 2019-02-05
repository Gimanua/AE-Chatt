namespace AE_Chatt
{
    using System;
    using System.IO;
    using System.Xml;

    static class Configurator
    {
        public static Uri Address { get; set; }
        public static string ConfigPath { get; } = "configuration.config";
        public static string PendingMessagesPath { get; } = "pending_messages.xml";
        public static string ChatLogPath { get; } = "chat_log.xml";

        public static void Initialize()
        {
            if (!File.Exists(ConfigPath))
            {
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, IndentChars = "\t" };
                using (XmlWriter writer = XmlWriter.Create(ConfigPath, settings))
                {
                    writer.WriteStartElement("configuration");
                    writer.WriteElementString("address", "https://10.20.20.110/AE.php");
                    writer.WriteEndElement();
                }
            }
            if (!File.Exists(PendingMessagesPath))
            {
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, IndentChars = "\t" };
                using (XmlWriter writer = XmlWriter.Create(PendingMessagesPath, settings))
                {
                    writer.WriteStartElement("pending_messages");
                    writer.WriteEndElement();
                }
                File.SetAttributes(PendingMessagesPath, FileAttributes.Hidden | FileAttributes.ReadOnly);
            }
            if (!File.Exists(ChatLogPath))
            {
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, IndentChars = "\t" };
                using (XmlWriter writer = XmlWriter.Create(ChatLogPath, settings))
                {
                    writer.WriteStartElement("chat_log");
                    writer.WriteEndElement();
                }
                File.SetAttributes(ChatLogPath, FileAttributes.Hidden | FileAttributes.ReadOnly);
            }

            using (XmlReader reader = XmlReader.Create(ConfigPath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "address":
                                reader.Read();
                                Address = new Uri(reader.Value);
                                break;
                        }
                    }
                }
            }
        }
    }
}
