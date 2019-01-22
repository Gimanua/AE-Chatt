namespace AE_Chatt
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Linq;

    static class ServerCommunicator
    {
        private static readonly HttpClient client = new HttpClient();
        public static bool Communicating { get; set; } = false;
        
        public static async Task<bool> Login(string username, string password)
        {
            Dictionary<string,string> postData = new Dictionary<string, string>
            {
                { "intent", "login"},
                { "username", username },
                { "password", password }
            };

            string response = await ServerRequest(postData);
            if (response.Contains("SUCCESS"))
                return true;
            else
                MessageBox.Show(response, "Server respons", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return false;
        }

        public static async Task<bool> Register(string username, string password)
        {
            Dictionary<string,string> postData = new Dictionary<string, string>
            {
                { "intent", "register"},
                { "username", username },
                { "password", password }
            };

            string response = await ServerRequest(postData);
            MessageBox.Show(response, "Server respons", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (response.Contains("SUCCESS"))
                return true;

            return false;
        }

        public static async Task<bool> SendMessage(string username, string receiver, string timestamp, string message)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>
            {
                { "intent", "sendMessage"},
                { "username", username },
                { "receiver", receiver },
                { "timestamp", timestamp},
                { "message", message }
            };

            string response = await ServerRequest(postData);
            if (response.Contains("SUCCESS"))
                return true;

            return false;
        }

        public static async Task<XmlDocument> GetUsers(string username)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>
            {
                { "intent", "getUsers"},
                { "username", username }
            };

            try
            {
                string response = await ServerRequest(postData);
                if (!string.IsNullOrWhiteSpace(response))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(response);
                    return doc;
                }
            }
            catch(XmlException e)
            {
                MessageBox.Show(e.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        } 

        public static async Task<XmlDocument> GetChatLog(string username, string target, string sinceTime)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>
            {
                { "intent", "getLog"},
                { "username", username },
                { "target", target},
                { "sinceTime", sinceTime}
            };
            
            try
            {
                string response = await ServerRequest(postData);
                if (!string.IsNullOrWhiteSpace(response))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(response);
                    return doc;
                }
            }
            catch(XmlException e)
            {
                MessageBox.Show(e.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private static async Task<string> ServerRequest(Dictionary<string,string> postData)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
            try
            {
                HttpResponseMessage response = await client.PostAsync(Configurator.Address, content);
                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch(HttpRequestException e)
            {
                return string.Empty;
            }
        }
    }
}
