using System;
using System.Net.Http;
using System.Windows.Forms;

namespace AE_Chatt
{
    public partial class LoginForm : Form
    {
        private ChatForm chatForm;

        public LoginForm()
        {
            InitializeComponent();
            Configurator.Initialize();
            chatForm = new ChatForm();
            chatForm.FormClosed += (s, e) => { Close(); };
        }
        
        private async void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (!ValidInput() || ServerCommunicator.Communicating)
                return;

            ServerCommunicator.Communicating = true;
            bool error = false;
            do
            {
                try
                {
                    await ServerCommunicator.Register(textBoxUserName.Text, textBoxPassWord.Text);
                    break;
                }
                catch(HttpRequestException ex)
                {
                    error = true;
                    DialogResult result = MessageBox.Show(ex.Message, "Fel", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result != DialogResult.Retry)
                        break;
                }
                catch(ArgumentNullException ex)
                {
                    MessageBox.Show(ex.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            } while (error);
            ServerCommunicator.Communicating = false;
        }

        private async void ButtonLogIn_Click(object sender, EventArgs e)
        {
            if(!ValidInput() || ServerCommunicator.Communicating)
                return;

            ServerCommunicator.Communicating = true;
            bool error = false;
            do
            {
                try
                {
                    if (await ServerCommunicator.Login(textBoxUserName.Text, textBoxPassWord.Text))
                    {
                        chatForm.Show();
                        Hide();
                    }
                    break;
                }
                catch(HttpRequestException ex)
                {
                    error = true;
                    DialogResult result = MessageBox.Show(ex.Message, "Fel", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result != DialogResult.Retry)
                        break;
                }
                catch(ArgumentNullException ex)
                {
                    MessageBox.Show(ex.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            } while (error);
            ServerCommunicator.Communicating = false;
        }
        
        private bool ValidInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxUserName.Text) || string.IsNullOrWhiteSpace(textBoxPassWord.Text))
            {
                MessageBox.Show("Ogiltigt användarnamn och/eller lösenord. Dessa fält får inte vara tomma.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }
    }
}
