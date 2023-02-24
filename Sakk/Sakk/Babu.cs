using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Sakk
{
    public class Babu
    {
        public string Tipus;
        public string Szin;
        public bool Lepettemar = false;
        public bool Sakkban = false;

        public Babu(string tipus, string szin)
        {
            Tipus = tipus;
            Szin = szin;
        }


    }
}
