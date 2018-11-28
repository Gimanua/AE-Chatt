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
    public partial class LoginForm : Form
    {
        private ChatForm chatForm = new ChatForm();
        

        public LoginForm()
        {
            InitializeComponent();
            chatForm.FormClosed += HandleChatClose;
        }

        private void labelPassWord_Click(object sender, EventArgs e)
        {

        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void HandleChatClose(object sender, EventArgs e)
        {
            Close();
        }
        

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            chatForm.Show();
            Hide();
        }

        private void labelUserName_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPassWord_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
