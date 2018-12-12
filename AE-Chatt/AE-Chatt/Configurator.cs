using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_Chatt
{
    static class Configurator
    {
        public static Uri ServerDomain { get; set; }

        public static bool Initialize()
        {
            if (!File.Exists("connection.config"))
            {
                using(StreamWriter sw = new StreamWriter("connection.config"))
                {
                    sw.Write("Server domain = [{0}]", "http://10.110.226.181/AEChatt/AE.php");
                    sw.Close();
                }
            }

            using(StreamReader sr = new StreamReader("connection.config"))
            {
                string row;
                while((row = sr.ReadLine()) != null)
                {
                    if(row.StartsWith("Server domain"))
                    {
                        int exStart = row.IndexOf("[");
                        int exEnd = row.IndexOf("]");
                        try
                        {
                            string domain = row.Substring(exStart + 1, exEnd - exStart - 1);
                            ServerDomain = new Uri(domain);
                        }
                        catch(ArgumentOutOfRangeException e)
                        {
                            DialogResult result = MessageBox.Show("Ogiltig server domän, kontrollera i \"connection.config\". Vill du ha mer info?", "Fel", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (result == DialogResult.Yes)
                                MessageBox.Show(e.Message, "Mer info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        catch(ArgumentNullException e)
                        {
                            DialogResult result = MessageBox.Show("Ogiltig server domän, kontrollera i \"connection.config\". Vill du ha mer info?", "Fel", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (result == DialogResult.Yes)
                                MessageBox.Show(e.Message, "Mer info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        catch(UriFormatException e)
                        {
                            DialogResult result = MessageBox.Show("Ogiltig server domän, kontrollera i \"connection.config\". Vill du ha mer info?", "Fel", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (result == DialogResult.Yes)
                                MessageBox.Show(e.Message, "Mer info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                }
                sr.Close();
            }

            return true;
        }
    }
}
