﻿namespace AE_Chatt
{
    using System.Drawing;
    using System.Windows.Forms;
    using System.Linq;
    using System.Xml.Linq;
    using System;
    using System.Xml;
    using System.IO;
    using System.Timers;

    public partial class ChatForm : Form
    {
        public LoginForm LoginForm { get; set; }
        private TextBox currentSendTextBox;
        private TextBox currentReadTextBox;
        public System.Windows.Forms.Timer Timer { get; set; }
        public string Username { get; set; }

        public ChatForm()
        {
            InitializeComponent();
            Timer = new System.Windows.Forms.Timer();
            Timer.Interval = 1000;
            Timer.Enabled = false;
            Timer.Tick += Tick;
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
                string timestamp = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                LoadChatLog(e.Item.Text, timestamp, false);
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
                    string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                    AppendPendingMessage(Username, tabControlConversations.SelectedTab.Text, timestamp, currentSendTextBox.Text);
                    AppendChatLog(Username, tabControlConversations.SelectedTab.Text, timestamp, currentSendTextBox.Text);
                    currentReadTextBox.AppendText(Username + "> " + currentSendTextBox.Text + "\n");
                    string msg = currentSendTextBox.Text;
                    currentSendTextBox.Clear();
                    currentSendTextBox.Select(0, 0);
                    if (!ServerCommunicator.Communicating)
                    {
                        ServerCommunicator.Communicating = true;
                        if (await ServerCommunicator.SendMessage(Username, tabControlConversations.SelectedTab.Text, timestamp, msg))
                        {
                            //Ta bort från pending_messages.xml
                            RemovePendingMessage();
                        }
                        ServerCommunicator.Communicating = false;
                    }
                }
            }
        }

        private void AppendChatLog(string sender, string target, string timestamp, string message)
        {
            File.SetAttributes(Configurator.ChatLogPath, FileAttributes.Normal);
            XmlDocument doc = new XmlDocument();
            doc.Load(Configurator.ChatLogPath);

            XmlElement xmlElement = doc.CreateElement("message");
            XmlAttribute xmlAttribute = doc.CreateAttribute("sender");
            xmlAttribute.Value = sender;
            xmlElement.Attributes.Append(xmlAttribute);
            xmlAttribute = doc.CreateAttribute("timestamp");
            xmlAttribute.Value = timestamp;
            xmlElement.Attributes.Append(xmlAttribute);
            xmlElement.InnerText = message;
            
            var node = doc.SelectSingleNode("/chat_log/" + target);
            if(node == null)
            {
                doc.SelectSingleNode("/chat_log").AppendChild(doc.CreateElement(target));
                node = doc.SelectSingleNode("/chat_log/" + target);
            }
            node.AppendChild(xmlElement);
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
            File.SetAttributes(Configurator.PendingMessagesPath, FileAttributes.Normal);
            XmlDocument doc = new XmlDocument();
            doc.Load(Configurator.PendingMessagesPath);
            XmlNode messages = doc.SelectSingleNode("/pending_messages");
            XmlNode childMessage = messages.FirstChild;
            if(childMessage != null)
            {
                messages.RemoveChild(childMessage);
            }
            doc.Save(Configurator.PendingMessagesPath);
            File.SetAttributes(Configurator.PendingMessagesPath, FileAttributes.Hidden | FileAttributes.ReadOnly);
        }

        private async void Tick(object s, EventArgs e)
        {
            Timer.Stop();
            if (await ServerCommunicator.CheckConnection(Username))
            {
                textBoxServerStatus.BackColor = Color.DarkOliveGreen;
                textBoxServerStatus.Text = "Connected";
            }
            else
            {
                textBoxServerStatus.BackColor = Color.DarkRed;
                textBoxServerStatus.Text = "Disconnected";
            }
            LoadUsers();
            if(tabControlConversations.SelectedTab != null)
            {
                //Hämta timestamp
                string timestamp = LoadTimestamp(tabControlConversations.SelectedTab.Text);
                if (string.IsNullOrWhiteSpace(timestamp))
                    timestamp = DateTime.UtcNow.AddSeconds(-30).ToString("yyyy-MM-dd HH:mm:ss");

                LoadChatLog(tabControlConversations.SelectedTab.Text, timestamp);
            }
            //Ta bort pending_messages
            if (!ServerCommunicator.Communicating)
            {
                ServerCommunicator.Communicating = true;
                File.SetAttributes(Configurator.PendingMessagesPath, FileAttributes.Normal);
                XmlDocument doc = new XmlDocument();
                doc.Load(Configurator.PendingMessagesPath);
                XmlNode messages = doc.SelectSingleNode("/pending_messages");
                XmlNode childMessage = messages.FirstChild;
                doc = null;

                if (childMessage != null && await ServerCommunicator.SendMessage(Username, childMessage.Attributes["receiver"].Value, childMessage.Attributes["timestamp"].Value, childMessage.InnerText))
                {
                    RemovePendingMessage();
                }
                ServerCommunicator.Communicating = false;
                File.SetAttributes(Configurator.PendingMessagesPath, FileAttributes.Hidden | FileAttributes.ReadOnly);
            }
            Timer.Start();
        }

        private string LoadTimestamp(string target)
        {
            File.SetAttributes(Configurator.ChatLogPath, FileAttributes.Normal);
            XmlDocument doc = new XmlDocument();
            doc.Load(Configurator.ChatLogPath);
            XmlNode targetNode = doc.SelectSingleNode("/chat_log/" + target);
            if (targetNode == null)
                return string.Empty;
            XmlNode latestMessage = targetNode.LastChild;
            if (latestMessage == null)
                return string.Empty;
            string timestamp = latestMessage.Attributes["timestamp"].Value;
            File.SetAttributes(Configurator.ChatLogPath, FileAttributes.Hidden | FileAttributes.ReadOnly);
            return timestamp;
        }

        private async void LoadUsers()
        {
            XmlDocument doc = await ServerCommunicator.GetUsers(Username);
            if(doc != null)
            {
                XmlNodeList list = doc.SelectNodes("/users/user");
                if (list == null)
                    return;
                foreach(XmlNode node in list)
                {
                    bool alreadyThere = false;
                    foreach(ListViewItem listViewItem in listViewOthers.Items)
                    {
                        if(listViewItem.Text == node.InnerText)
                        {
                            alreadyThere = true;
                            break;
                        }
                    }

                    if (!alreadyThere)
                        listViewOthers.Items.Add(node.InnerText);
                    
                }
                for(int i = 0; i < listViewOthers.Items.Count; i++)
                {
                    bool exists = false;
                    foreach(XmlNode node in list)
                    {
                        if(listViewOthers.Items[i].Text == node.InnerText)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        listViewOthers.Items.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
        
        private async void LoadChatLog(string target, string sinceTime, bool remote = true)
        {
            if (remote)
            {
                XmlDocument doc = await ServerCommunicator.GetChatLog(Username, target, sinceTime);
                if (doc != null)
                {
                    XmlNodeList list = doc.SelectNodes("/chatlog/message");
                    //var querySpecificMessages = from XmlNode node in list where DateTime.Parse(node.Attributes["timestamp"].Value) > DateTime.Parse(sinceTime) select node;

                    foreach (XmlNode node in list)
                    {
                        if (node.Attributes["sender"].Value == Username)
                            currentReadTextBox.AppendText(Username + "> " + node.InnerText + "\n");
                        else
                            currentReadTextBox.AppendText(target + "> " + node.InnerText + "\n");

                        AppendChatLog(node.Attributes["sender"].Value, target, node.Attributes["timestamp"].Value, node.InnerText);
                        /*
                        XmlDocument logDoc = new XmlDocument();
                        logDoc.Load(Configurator.ChatLogPath);

                        XmlNode copiedNode = logDoc.CreateElement(node.Name);
                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            XmlAttribute copiedAttribute = logDoc.CreateAttribute(attribute.Name);
                            copiedAttribute.Value = attribute.Value;
                            copiedNode.Attributes.Append(copiedAttribute);
                        }
                        copiedNode.InnerText = node.InnerText;

                        XmlNode targetNode = logDoc.SelectSingleNode("chat_log/" + target);
                        targetNode.AppendChild(copiedNode);

                        logDoc.Save(Configurator.ChatLogPath);
                        */
                    }
                }
            }
            else
            {
                File.SetAttributes(Configurator.ChatLogPath, FileAttributes.Normal);
                XmlDocument doc = new XmlDocument();
                doc.Load(Configurator.ChatLogPath);
                XmlNodeList nodeList = doc.SelectNodes("/chat_log/" + target + "/message");
                var querySpecificMessages = from XmlNode node in nodeList
                                            where DateTime.Parse(node.Attributes["timestamp"].Value) > DateTime.Parse(sinceTime) && 
                                            (node.Attributes["sender"].Value == Username || node.Attributes["sender"].Value == target) select node;
                foreach(XmlNode node in querySpecificMessages)
                {
                    if (node.Attributes["sender"].Value == Username)
                        currentReadTextBox.AppendText(Username + "> " + node.InnerText + "\n");
                    else
                        currentReadTextBox.AppendText(target + "> " + node.InnerText + "\n");
                }
                File.SetAttributes(Configurator.ChatLogPath, FileAttributes.Hidden | FileAttributes.ReadOnly);
            }
        }
        
        private async void ButtonLogOut_Click(object sender, EventArgs e)
        {
            Timer.Enabled = false;
            Timer.Stop();
            await ServerCommunicator.Logout(Username);
            for(int i = 0; i < listViewOthers.Items.Count; i++)
            {
                listViewOthers.Items.RemoveAt(i);
                i--;
            }
            for(int i = 0; i < tabControlConversations.TabPages.Count; i++)
            {
                tabControlConversations.TabPages.RemoveAt(i);
                i--;
            }
            Hide();
            LoginForm.Show();
        }

        private async void ChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Timer.Enabled = false;
            Timer.Stop();
            await ServerCommunicator.Logout(Username);
            File.SetAttributes(Configurator.ChatLogPath, FileAttributes.Hidden | FileAttributes.ReadOnly);
            File.SetAttributes(Configurator.PendingMessagesPath, FileAttributes.Hidden | FileAttributes.ReadOnly);
        }
    }
}
