﻿namespace AE_Chatt
{
    using System.Drawing;
    using System.Windows.Forms;
    using System.Linq;
    using System.Xml.Linq;
    using System;
    using System.Xml;
    using System.IO;

    public partial class ChatForm : Form
    {
        private TextBox currentSendTextBox;
        private TextBox currentReadTextBox;
        public string Username { get; set; } = "Tölpen"; //tillfällig

        public ChatForm()
        {
            InitializeComponent();
        }
        
        private void ListViewOthers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                TabPage tabPage = new TabPage(e.Item.Text);

                TextBox textBoxRead = new TextBox() { Location = new Point(3, 3), Multiline = true, Size = new Size(635, 314), ReadOnly = true, TabStop = false, Name = "read", ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill };
                tabPage.Controls.Add(textBoxRead);

                TextBox textBoxSend = new TextBox() { Location = new Point(3, 320), Multiline = true, Size = new Size(635, 80), TabStop = false, MaxLength = 800, Name = "send", Dock = DockStyle.Bottom };
                textBoxSend.KeyDown += TextBoxSend_KeyDown;
                tabPage.Controls.Add(textBoxSend);

                tabControlConversations.TabPages.Add(tabPage);
                tabControlConversations.SelectedTab = tabPage;

                currentReadTextBox = textBoxRead;
                currentSendTextBox = textBoxSend;

                //Från en dag tillbaks
                TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                string sinceTime = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-ddTHH:mm:ss" + ((utcOffset < TimeSpan.Zero) ? "-" : "+") + utcOffset.ToString("hh") + ":" + utcOffset.ToString("mm"));

                LoadChatLog(e.Item.Text, sinceTime);
            }
            else
            {
                //Ta bort taben
                foreach (TabPage tabPage in tabControlConversations.TabPages)
                {
                    if (tabPage.Text == e.Item.Text)
                    {
                        tabControlConversations.TabPages.Remove(tabPage);
                        break;
                    }
                }
            }
        }

        private void TabControlConversations_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage != null)
            {
                e.TabPage.BringToFront();
                foreach (TextBox textBox in e.TabPage.Controls.OfType<TextBox>())
                {
                    if (textBox.Name == "send")
                        currentSendTextBox = textBox;
                    else if (textBox.Name == "read")
                        currentReadTextBox = textBox;
                }
            }
        }

        private async void TextBoxSend_KeyDown(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!string.IsNullOrWhiteSpace(currentSendTextBox.Text))
                {
                    TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                    string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss" + ((utcOffset < TimeSpan.Zero) ? "-" : "+") + utcOffset.ToString("hh") + ":" + utcOffset.ToString("mm"));
                    AppendPendingMessage(Username, tabControlConversations.SelectedTab.Text, timestamp, currentSendTextBox.Text);
                    AppendChatLog(Username, tabControlConversations.SelectedTab.Text, timestamp, currentSendTextBox.Text);
                    currentReadTextBox.AppendText(currentSendTextBox.Text + "\n");
                    currentSendTextBox.Clear();
                    currentSendTextBox.Select(0, 0);
                    if(await ServerCommunicator.SendMessage(Username, tabControlConversations.SelectedTab.Text, timestamp, currentSendTextBox.Text))
                    {
                        //Ta bort från pending_messages.xml
                        RemovePendingMessage();
                    }
                }
            }
        }

        private void AppendChatLog(string sender, string receiver, string timestamp, string message)
        {
            File.SetAttributes(Configurator.ChatLogPath, FileAttributes.Normal);
            XmlDocument doc = new XmlDocument();
            doc.Load(Configurator.ChatLogPath);

            XmlElement xmlElement = doc.CreateElement("message");
            XmlAttribute xmlAttribute = doc.CreateAttribute("sender");
            xmlAttribute.Value = sender;
            xmlElement.Attributes.Append(xmlAttribute);
            xmlAttribute = doc.CreateAttribute("receiver");
            xmlAttribute.Value = receiver;
            xmlElement.Attributes.Append(xmlAttribute);
            xmlAttribute = doc.CreateAttribute("timestamp");
            xmlAttribute.Value = timestamp;
            xmlElement.Attributes.Append(xmlAttribute);
            xmlElement.InnerText = message;

            if (sender == Username)
            {
                if(doc.SelectSingleNode("/chat_log/" + receiver) == null)
                {
                    XmlElement tmp = doc.CreateElement(receiver);
                    doc.SelectSingleNode("/chat_log").AppendChild(tmp);
                }
                doc.SelectSingleNode("/chat_log/" + receiver).AppendChild(xmlElement);
            }
            else
            {
                if(doc.SelectSingleNode("/chat_log/" + sender) == null)
                {
                    XmlElement tmp = doc.CreateElement(sender);
                    doc.SelectSingleNode("/chat_log").AppendChild(tmp);
                }
                doc.SelectSingleNode("/chat_log/" + sender).AppendChild(xmlElement);
            }
            doc.Save(Configurator.ChatLogPath);
            File.SetAttributes(Configurator.ChatLogPath, FileAttributes.Hidden | FileAttributes.ReadOnly);
        }

        private void AppendPendingMessage(string sender, string receiver, string timestamp, string message)
        {
            File.SetAttributes(Configurator.PendingMessagesPath, FileAttributes.Normal);
            XDocument doc = XDocument.Load("pending_messages.xml");
            XElement msg = new XElement("pending_message");
            msg.Add(new XAttribute("sender", sender));
            msg.Add(new XAttribute("receiver", receiver));
            msg.Add(new XAttribute("timestamp", timestamp));
            msg.Value = message;
            doc.Element("pending_messages").Add(msg);
            doc.Save("pending_messages.xml");
            File.SetAttributes(Configurator.PendingMessagesPath, FileAttributes.Hidden | FileAttributes.ReadOnly);
        }

        private void RemovePendingMessage()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("pending_messages.xml");
            XmlNode messages = doc.SelectSingleNode("/pending_messages");
            XmlNode childMessage = messages.FirstChild;
            messages.RemoveChild(childMessage);
            doc.Save("pending_messages.xml");
        }

        private async void LoadUsers()
        {

        }

        private async void LoadChatLog(string target, string sinceTime)
        {
            XmlDocument doc = await ServerCommunicator.GetChatLog(Username, target, sinceTime);
            if(doc != null)
            {
                XmlNodeList list = doc.SelectNodes("/chatlog/message");
                foreach (XmlNode node in list)
                {
                    currentReadTextBox.AppendText(node.InnerText);
                }
            }
        }
    }
}
