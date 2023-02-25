using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sakk
{
    public partial class PromociKivalasztas : UserControl
    {
        public string BabuTipus { get; set; }

        static List<PictureBox> boxok;
        static List<string> babuk;
        public PromociKivalasztas(Mezo klikkelt, string babu_tipus)
        {
            InitializeComponent();
            boxok = new List<PictureBox>() { Pbox_0, Pbox_1, Pbox_2, Pbox_3 };
            babuk = new List<string>() { "Q", "N", "R", "B" };
            if (klikkelt.Koordinatak.X == 0)
            {
                KepFeltoltes(babu_tipus, klikkelt.Babu.Szin, klikkelt.BackColor);
            }
            else
            {
                KepFeltoltes(babu_tipus, klikkelt.Babu.Szin, klikkelt.BackColor);
            }

        }

        public event EventHandler Dontes;

        private void KepFeltoltes(string babu_tipus, string szin, Color hatterszin)
        {
            List<Color> szinek = new List<Color>() { Color.Tan, Color.Brown, Color.Tan, Color.Brown, Color.Tan };
            for (int i = szin == "fehér" ? 0 : 3; szin == "fehér" ? i <= 3 : i >= 0; i+= szin == "fehér" ? 1 : -1)
            {
                int b = Convert.ToInt32($"{i}");
                boxok[b].SizeMode = PictureBoxSizeMode.Zoom;
                string rovid = szin == "fehér" ? "w" : "b";
                string babu = szin == "fehér" ? babuk[b] : babuk[3 - b];
                boxok[b].Image = Image.FromFile($"piece\\{babu_tipus}\\{rovid}{babu}.png");
                boxok[b].Click += delegate (object sender, EventArgs e) { Kivalaszt(babu, sender, e); };
                (szin == "fehér" ? boxok[b] : boxok[3 - b]).BackColor =  hatterszin == Color.Tan ? szinek[b] : szinek[b + 1];
                boxok[b].BackgroundImageLayout = ImageLayout.Zoom;
                boxok[b].MouseEnter += delegate (object sender, EventArgs e) { MouseEnter(boxok[b]); };
                boxok[b].MouseLeave += delegate (object sender, EventArgs e) { MouseLeave(boxok[b]); };
            }
        }

        private void MouseEnter(PictureBox kep)
        {
            kep.BackgroundImage = Image.FromFile("piece/kijeloltbabu.png");
        }
        private void MouseLeave(PictureBox kep)
        {
            kep.BackgroundImage = null;
        }

        private void Kivalaszt(string babu, object sender, EventArgs e)
        {
            BabuTipus = babu;
            Dontes(sender, e);
        }
    }
}
