using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_Chatt
{
    public partial class ChatForm : Form
    {
        private List<TextBox> textBoxesRead = new List<TextBox>();
        private List<TextBox> textBoxesSend = new List<TextBox>();
        private TextBox currentSendTextBox;
        private TextBox currentReadTextBox;

        public ChatForm()
        {
            TextBox textBoxStandard = new TextBox() { Location = new Point(139, 364), Multiline = true, Size = new Size(649, 74), TabStop = false, ReadOnly = true, Text = "Här skriver du meddelanden" };
            Controls.Add(textBoxStandard);
            InitializeComponent();
        }
        
        private void listView1_ItemChecked_1(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                TabPage tabPage = new TabPage(e.Item.Text) { Name = e.Item.Text};
                TextBox textBoxTab = new TextBox() { Location = new Point(3,3), Multiline = true, Size = new Size(635,314), ReadOnly = true, TabStop = false};
                currentReadTextBox = textBoxTab;
                textBoxesRead.Add(textBoxTab);
                tabPage.Controls.Add(textBoxTab);
                tabControlConversations.TabPages.Add(tabPage);
                tabControlConversations.SelectedTab = tabControlConversations.TabPages[tabControlConversations.TabCount - 1];
                TextBox textBoxSend = new TextBox() { Location = new Point(139, 364), Multiline = true, Size = new Size(649, 74), TabStop = false, MaxLength = 800, Name = e.Item.Text };
                textBoxSend.KeyUp += (s, e2) => { if (!string.IsNullOrEmpty(currentSendTextBox.Text) && e2.KeyData == Keys.Enter) { currentReadTextBox.AppendText(currentSendTextBox.Text + "\n"); currentSendTextBox.Clear(); currentSendTextBox.Select(0, 0); } };
                textBoxesSend.Add(textBoxSend);
                Controls.Add(textBoxSend);
                textBoxSend.BringToFront();
                currentSendTextBox = textBoxSend;
            }
            else
            {
                //Ta bort taben
                for (int i = 0; i < tabControlConversations.TabPages.Count; i++)
                {
                    if (tabControlConversations.TabPages[i].Text == e.Item.Text)
                    {
                        tabControlConversations.TabPages.RemoveAt(i);
                        break;
                    }
                }
                //Ta bort textboxen
                for(int i = 0; i < Controls.Count; i++)
                {
                    if(Controls[i].Name == e.Item.Text)
                    {
                        Controls.RemoveAt(i);
                        break;
                    }
                }
                for(int i = 0; i < textBoxesSend.Count; i++)
                {
                    if(textBoxesSend[i].Name == e.Item.Text)
                    {
                        textBoxesSend.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        

        private void AdjustTextBox(TextBox textBox)
        {
            textBox.Location = new Point(139, 364);
            textBox.Multiline = true;
            textBox.Size = new Size(649, 74);
            textBox.TabStop = false;
            textBox.MaxLength = 800;
        }

        private void tabControlConversations_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == null)
                return;

            foreach(TextBox textBox in textBoxesRead)
            {
                if(textBox.Name == e.TabPage.Name)
                {
                    textBox.BringToFront();
                    currentReadTextBox = textBox;
                    break;
                }
            }

            foreach (TextBox textBox in textBoxesSend)
            {
                if (textBox.Name == e.TabPage.Name)
                {
                    textBox.BringToFront();
                    textBox.Select(0,0);
                    currentSendTextBox = textBox;
                    break;
                }
            }
        }
    }
}
