﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_Chatt
{
    public partial class LoginForm : Form
    {
        private ChatForm chatForm = new ChatForm();
        private string serverAddress = "http://10.110.226.181/AEChatt/AE.php";
        private bool error = false;

        public LoginForm()
        {
            InitializeComponent();
            chatForm.FormClosed += HandleChatClose;
        }

        private void HandleChatClose(object sender, EventArgs e)
        {
            Close();
        }
        
        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (!ValidInput())
                return;

            string connectionType = "register";

            using (WebClient client = new WebClient())
            {
                NameValueCollection postData = new NameValueCollection()
                {
                    { "username", textBoxUserName.Text },  //order: {"parameter name", "parameter value"}
                    { "password", textBoxPassWord.Text },
                    { "connectionType", connectionType }
                };

                // client.UploadValues returns page's source as byte array (byte[])
                // so it must be transformed into a string
                do
                {
                    try
                    {
                        string pagesource = Encoding.UTF8.GetString(client.UploadValues(serverAddress, postData));
                        MessageBox.Show(pagesource, "Server response", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        error = true;
                        DialogResult result = MessageBox.Show(ex.Message, "Server status", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        if (result == DialogResult.Retry)
                            continue;
                        else
                            break;
                    }
                }
                while(error);
                
            }
        }

        private void ButtonLogIn_Click(object sender, EventArgs e)
        {
            if(!ValidInput())
                return;

            string connectionType = "login";

            using (WebClient client = new WebClient())
            {
                NameValueCollection postData = new NameValueCollection()
                {
                    { "username", textBoxUserName.Text },  //order: {"parameter name", "parameter value"}
                    { "password", textBoxPassWord.Text },
                    { "intent", connectionType }
                };

                // client.UploadValues returns page's source as byte array (byte[])
                // so it must be transformed into a string
                do
                {
                    try
                    {
                        string pagesource = Encoding.UTF8.GetString(client.UploadValues(serverAddress, postData));
                        MessageBox.Show(pagesource, "Server response", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        error = true;
                        DialogResult result = MessageBox.Show(ex.Message, "Server status", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        if (result == DialogResult.Retry)
                            continue;
                        else
                            break;
                    }
                } while (error);

            }

            chatForm.Show();
            Hide();
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
