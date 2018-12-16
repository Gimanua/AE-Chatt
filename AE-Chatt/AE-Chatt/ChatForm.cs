namespace AE_Chatt
{
    using System.Drawing;
    using System.Windows.Forms;
    using System.Linq;

    public partial class ChatForm : Form
    {
        private TextBox currentSendTextBox;
        private TextBox currentReadTextBox;
        public string Username { get; set; }

        public ChatForm()
        {
            InitializeComponent();
        }
        
        private void ListViewOthers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

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
                    //Write the messages to file
                    currentReadTextBox.ForeColor = Color.Red;
                    currentReadTextBox.AppendText(currentSendTextBox.Text + "\n");
                    currentSendTextBox.Clear();
                    currentSendTextBox.Select(0, 0);
                    await ServerCommunicator.SendMessage(currentSendTextBox.Text, Username, tabControlConversations.SelectedTab.Text);
                }
            }
        }
    }
}
