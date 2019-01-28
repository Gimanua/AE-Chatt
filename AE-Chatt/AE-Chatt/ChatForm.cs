namespace AE_Chatt
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
        public string Username { get; set; } = "Tölpen"; //tillfällig

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
                

                //LoadChatLog(e.Item.Text, sinceTime);
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
                    currentReadTextBox.AppendText(currentSendTextBox.Text + "\n");
                    string msg = currentSendTextBox.Text;
                    currentSendTextBox.Clear();
                    currentSendTextBox.Select(0, 0);
                    if (await ServerCommunicator.SendMessage(Username, tabControlConversations.SelectedTab.Text, timestamp, msg))
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
            LoadUsers();
            if(tabControlConversations.SelectedTab != null)
            {
                string timestamp = DateTime.UtcNow.AddSeconds(-5).ToString("yyyy-MM-dd HH:mm:ss");
                LoadChatLog(tabControlConversations.SelectedTab.Text, timestamp);
            }
            //Ta bort pending_messages
            File.SetAttributes(Configurator.PendingMessagesPath, FileAttributes.Normal);
            XmlDocument doc = new XmlDocument();
            doc.Load("pending_messages.xml");
            XmlNode messages = doc.SelectSingleNode("/pending_messages");
            XmlNode childMessage = messages.FirstChild;
            doc = null;
            if (childMessage != null && await ServerCommunicator.SendMessage(Username, childMessage.Attributes["receiver"].Value, childMessage.Attributes["timestamp"].Value, childMessage.InnerText))
            {
                RemovePendingMessage();
            }
            Timer.Start();
        }

        private async void LoadUsers()
        {
            XmlDocument doc = await ServerCommunicator.GetUsers(Username);
            if(doc != null)
            {
                XmlNodeList list = doc.SelectNodes("/users/user");
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

        private async void LoadChatLog(string target, string sinceTime)
        {
            XmlDocument doc = await ServerCommunicator.GetChatLog(Username, target, sinceTime);
            if(doc != null)
            {
                XmlNodeList list = doc.SelectNodes("/chatlog/message");
                foreach (XmlNode node in list)
                {
                    currentReadTextBox.AppendText(node.InnerText + "\n");
                }
            }
        }
        
        private void ButtonLogOut_Click(object sender, EventArgs e)
        {
            Timer.Enabled = false;
            Timer.Stop();
            ServerCommunicator.Logout(Username);
            Hide();
            LoginForm.Show();
        }

        private void ChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Timer.Enabled = false;
            Timer.Stop();
            ServerCommunicator.Logout(Username);
        }
    }
}
