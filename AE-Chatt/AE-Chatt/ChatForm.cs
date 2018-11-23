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
        private TextBox currentSendTextBox;
        private TextBox currentReadTextBox;

        public ChatForm()
        {
            InitializeComponent();
        }
        
        private void listView1_ItemChecked_1(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                TabPage tabPage = new TabPage(e.Item.Text);
                TextBox textBoxRead = new TextBox() { Location = new Point(3,3), Multiline = true, Size = new Size(635,314), ReadOnly = true, TabStop = false};
                tabPage.Controls.Add(textBoxRead);
                
                TextBox textBoxSend = new TextBox() { Location = new Point(0, 320), Multiline = true, Size = new Size(641, 80), TabStop = false, MaxLength = 800 };
                textBoxSend.KeyUp += (s, e2) => { if (!string.IsNullOrEmpty(currentSendTextBox.Text) && e2.KeyData == Keys.Enter) { currentReadTextBox.AppendText(currentSendTextBox.Text + "\n"); currentSendTextBox.Clear(); currentSendTextBox.Select(0, 0); } };
                tabPage.Controls.Add(textBoxSend);

                tabControlConversations.TabPages.Add(tabPage);
                tabControlConversations.SelectedTab = tabControlConversations.TabPages[tabControlConversations.TabCount - 1];

                tabPage.BringToFront();

                currentReadTextBox = textBoxRead;
                currentSendTextBox = textBoxSend;
            }
            else
            {
                //Ta bort taben
                for (int i = 0; i < tabControlConversations.TabPages.Count; i++)
                {
                    if (tabControlConversations.TabPages[i].Text == e.Item.Text)
                    {
                        tabControlConversations.TabPages.RemoveAt(i-1);
                        break;
                    }
                }
            }
        }

        private void tabControlConversations_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == null)
                return;
            else
                e.TabPage.BringToFront();
        }
    }
}
