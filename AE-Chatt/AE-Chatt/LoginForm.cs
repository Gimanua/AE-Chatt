namespace AE_Chatt
{
    using System;
    using System.Net.Http;
    using System.Windows.Forms;
    using System.Xml;

    public partial class LoginForm : Form
    {
        private ChatForm chatForm = new ChatForm();

        public LoginForm()
        {
            InitializeComponent();
            try
            {
                Configurator.Initialize();
            }
            catch(XmlException e)
            {
                //config filen är korrupt
                DialogResult result = MessageBox.Show("\"configuration.config\" har ett ogiltigt format, vill du ha mer information?", "Fel", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                    MessageBox.Show(e.Message, "Ytterligare information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(UriFormatException e)
            {
                //Ogiltig domän
                DialogResult result = MessageBox.Show("Ogiltig domän, kontrollera domänen i \"configuration.config\". Vill du ha mer information?", "Fel", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                    MessageBox.Show(e.Message, "Ytterligare information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
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
                    if (result == DialogResult.Cancel)
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
                    if (result == DialogResult.Cancel)
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
