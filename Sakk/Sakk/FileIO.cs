using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Sakk
{
    class FileIO
    {
        public static string Beolvas()
        {
            try
            {
                StreamReader r = new StreamReader(Environment.CurrentDirectory+"/piece/aktualisbabu.txt");
                string aktualisbabu = r.ReadLine();
                r.Close();
                return aktualisbabu;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return "horsey";

            }
        }
        public static void Kiiras(string babutipus)
        {
            try
            {
                StreamWriter w = new StreamWriter(Environment.CurrentDirectory + "/piece/aktualisbabu.txt");
                w.WriteLine(babutipus);
                w.Close();


            }
            catch (Exception)
            {

            }
        }
    }
}
