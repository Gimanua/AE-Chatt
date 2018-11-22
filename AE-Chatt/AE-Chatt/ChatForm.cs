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
        private List<TextBox> textBoxes = new List<TextBox>();

        public ChatForm()
        {
            TextBox textBoxStandard = new TextBox();
            AdjustTextBox(textBoxStandard);
            textBoxStandard.ReadOnly = true;
            textBoxStandard.Name = "standardIndex";
            textBoxStandard.Text = "Här skriver du meddelanden";
            textBoxes.Add(textBoxStandard);
            Controls.Add(textBoxStandard);
            InitializeComponent();
        }
        

        private void listView1_ItemChecked_1(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                TabPage tabPage = new TabPage(e.Item.Text);
                tabPage.Name = e.Item.Text;
                TextBox textBoxTab = new TextBox() { Location = new Point(3,3), Size = new Size(635,314), ReadOnly = true};
                tabControlConversations.TabPages.Add(tabPage);
                tabControlConversations.SelectedTab = tabControlConversations.TabPages[tabControlConversations.TabCount - 1];
                TextBox textBox = new TextBox();
                textBox.Name = e.Item.Text;
                AdjustTextBox(textBox);
                textBoxes.Add(textBox);
                Controls.Add(textBox);
                textBox.BringToFront();
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
                for(int i = 0; i < textBoxes.Count; i++)
                {
                    if(textBoxes[i].Name == e.Item.Text)
                    {
                        textBoxes.RemoveAt(i);
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
            
        }

        private void tabControlConversations_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == null)
                return;

            foreach (TextBox textBox in textBoxes)
            {
                if (textBox.Name == e.TabPage.Name)
                    textBox.BringToFront();
            }
        }
    }
}
