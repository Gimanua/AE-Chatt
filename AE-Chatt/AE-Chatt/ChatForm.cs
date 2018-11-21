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
        public ChatForm()
        {
            InitializeComponent();
        }

        private void listView1_ItemActivate(object sender, EventArgs e) { }

        private void listView1_ItemChecked_1(object sender, ItemCheckedEventArgs e)
        {

        }

        private void listViewOthers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)//Fel metod
        {
            if (e.IsSelected)
            {
                tabControlConversations.TabPages.Add(e.Item.Text);
                tabControlConversations.SelectedTab = tabControlConversations.TabPages[tabControlConversations.TabCount - 1];
            }
            else
            {
                for(int i = 0; i < tabControlConversations.TabPages.Count; i++)
                {
                    if (tabControlConversations.TabPages[i].Text == e.Item.Text)
                    {
                        tabControlConversations.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}
