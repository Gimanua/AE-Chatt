using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_Chatt
{
    class ServerCommunicator
    {
        private const string ServerAddress = "http://10.110.226.181/AEChatt/AE.php";
        private bool error = false;
        
        public void Login(string username, string password)
        {
            NameValueCollection postData = new NameValueCollection()
            {
                { "intent", "login"},
                { "username", username },
                { "password", password }
            };
            
            do
            {
                //string response = ServerRequest(postData);
                
            } while (error);
        }

        public static void Register()
        {

        }
        /*
        private string ServerRequest(NameValueCollection postData)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string response = Encoding.UTF8.GetString(client.UploadValues(ServerAddress, postData));
                    error = false;
                    return response;
                }
                catch (Exception ex)
                {
                    error = true;
                    DialogResult result = MessageBox.Show(ex.Message, "Server message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }
        */
    }
}
