namespace AE_Chatt
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Forms;

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

        public static async Task<bool> SendMessage(string message, string sender, string receiver)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>
            {
                { "intent", "sendMessage"},
                { "message", message },
                { "sender", sender },
                { "receiver", receiver }
            };

            string response = await ServerRequest(postData);
            if (response.Contains("SUCCESS"))
                return true;

            return false;
        }

        private static async Task<string> ServerRequest(Dictionary<string,string> postData)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
            HttpResponseMessage response = await client.PostAsync(Configurator.ServerDomain, content);
            string responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}
