﻿namespace AE_Chatt
{
    using System.Drawing;
    using System.Windows.Forms;
    using System.Linq;
    using System.Xml.Linq;
    using System;
    using System.Xml;

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
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    int id = AppendPendingMessage(Username, tabControlConversations.SelectedTab.Text, timestamp, currentSendTextBox.Text);
                    currentReadTextBox.AppendText(currentSendTextBox.Text + "\n");
                    currentSendTextBox.Clear();
                    currentSendTextBox.Select(0, 0);
                    if(await ServerCommunicator.SendMessage(Username, tabControlConversations.SelectedTab.Text, timestamp, currentSendTextBox.Text))
                    {
                        //Ta bort från pending_messages.xml
                        RemovePendingMessage(id);
                    }
                }
            }
        }

        private int AppendPendingMessage(string sender, string receiver, string timestamp, string message)
        {
            XDocument doc = XDocument.Load("pending_messages.xml");
            int id = doc.Descendants("pending_message").Count() + 1;
            XElement msg = new XElement("pending_message");
            msg.Add(new XAttribute("id", id));
            msg.Add(new XAttribute("sender", sender));
            msg.Add(new XAttribute("receiver", receiver));
            msg.Add(new XAttribute("timestamp", timestamp));
            msg.Value = message;
            doc.Element("pending_messages").Add(msg);
            doc.Save("pending_messages.xml");
            return id;
        }

        private void RemovePendingMessage(int id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("pending_messages.xml");
            XmlNode node = doc.SelectSingleNode("/pending_messages/pending_message[@id=" + id + "]");
            node.ParentNode.RemoveChild(node);
            doc.Save("pending_messages.xml");
        }

        //Behöver fixas
        private void UpdatePendingMessageIds()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("pending_messages.xml");
            XmlNode messages = doc.SelectSingleNode("/pending_messages");
            int count = messages.ChildNodes.Count;
            
        }
    }
}
