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



        public Mezo(Point koordinatak, Babu babu)
        {
            Koordinatak = koordinatak;
            Babu = babu;
            babu_tipus = "cburnett";
            SizeMode = PictureBoxSizeMode.Zoom;
        }



        private Image KepValasztas(Babu seged_babu)
        {
            if (seged_babu.Szin == "fekete")
            {
                return BabuTipusValasztas("b");
            }
            else if (seged_babu.Szin == "fehér")
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
            switch (seged_babu.Tipus)
            {
                case "bástya":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}R");
                case "huszár":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}N");
                case "királynő":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}Q");
                case "futó":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}B");
                case "paraszt":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}P");
                case "király":
                    return Image.FromFile($"piece/{babu_tipus}/{szin}K");
                default:
                    return null;
            }
        }
    }
}
