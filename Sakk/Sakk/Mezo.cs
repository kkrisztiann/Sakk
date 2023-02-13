using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Sakk
{
    public partial class Mezo: PictureBox
    {
        public Point Koordinatak;
        public string babu_tipus
        {
            get
            {
                return seged_tipus;
            }
            set
            {
                seged_tipus = value;
                Image = KepValasztas(Babu);
            }
        }
        public Babu Babu { 
            get
            {
                return seged_babu;
            }
            set
            {
                seged_babu = value;
                Image = KepValasztas(Babu);
            }
        }

        private Babu seged_babu;
        private string seged_tipus;



        public Mezo(Point koordinatak)
        {
            Babu = null;    
            Koordinatak = koordinatak;
            babu_tipus = "cburnett";

            SizeMode = PictureBoxSizeMode.Zoom;
            Size = new Size(80, 80);
        }



        private Image KepValasztas(Babu babu)
        {
            if (babu == null)
            {
                return null;
            }
            if (babu.Szin == "fekete")
            {
                return BabuTipusValasztas("b");
            }
            else if (babu.Szin == "fehér")
            {
                return BabuTipusValasztas("w");
            }
            else
            {
                return null;
            }
        }

        private Image BabuTipusValasztas(string szin)
        {
            switch (Babu.Tipus)
            {
                case "bástya":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}R.png");
                case "huszár":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}N.png");
                case "királynő":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}Q.png");
                case "futó":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}B.png");
                case "paraszt":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}P.png");
                case "király":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}K.png");
                default:
                    return null;
            }
        }
    }
}
