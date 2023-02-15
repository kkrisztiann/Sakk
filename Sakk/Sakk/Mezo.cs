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
            get { return seged_tipus; }
            set
            {
                seged_tipus = value;
                Image = KepValasztas(Babu);
            }
        }
        public Babu Babu {
            get { return seged_babu; }
            set
            {
                seged_babu = value;
                Image = KepValasztas(Babu);
            }
        }
        public bool Lepheto
        {
            get { return seged_lepheto; }
            set
            {
                seged_lepheto = value;
                BackgroundImage = BgcImageChooseLepheto(Lepheto);
            }
        }
        public bool Kijelolt
        {
            get { return seged_kijelolt; }
            set
            {
                seged_kijelolt = value;
                BackgroundImage = BgcImageChooseKijelolt(Kijelolt);
            }
        }

        private bool seged_kijelolt;
        private bool seged_lepheto;
        private Babu seged_babu;
        private string seged_tipus;


        //KONSTRUKTOR----------------------------------------------------------------------------
        public Mezo(Point koordinatak)
        {
            Babu = null;
            Lepheto = false;
            Kijelolt = false;
            Koordinatak = koordinatak;
            babu_tipus = "horsey";

            SizeMode = PictureBoxSizeMode.Zoom;
            BackgroundImageLayout = ImageLayout.Zoom;
            Size = new Size(90, 90);
        }

        private Image BgcImageChooseKijelolt(bool kijelolt)
        {
            return kijelolt ? Image.FromFile("piece/kijelolt.png") : null;
        }

        private Image BgcImageChooseLepheto(bool lepheto)
        {
            if (lepheto)
            {
                return Babu == null ? Image.FromFile("piece/kijeloltmezo.png") : Image.FromFile("piece/kijeloltbabu.png");
            }
            else
            {
                return null;
            }
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
