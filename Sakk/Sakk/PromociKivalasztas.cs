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
        public static string babukep { get; set; }
        public static string szin { get; set; }
        public string kivalasztott = "";
        public PromociKivalasztas(string Bkep, string Szin, string melyikszin, int xkoordinata)
        {
            InitializeComponent();
            if (melyikszin=="fehér")
            {
                this.Location = new Point(50+xkoordinata*90, 50);
            }
            else
            {
                //fekete
                kiralynoPbox.Location = new Point(0, 270);
                bastyaPbox.Location = new Point(0, 180);
                futoPbox.Location = new Point(0, 90);
                loPbox.Location = new Point(0, 0);
                this.Location = new Point(50 + xkoordinata * 90, 50+360);
            }
            if (xkoordinata % 2 == 0)
            {
                kiralynoPbox.BackColor = Color.Tan;
                bastyaPbox.BackColor = Color.Brown;
                futoPbox.BackColor = Color.Tan;
                loPbox.BackColor = Color.Brown;
            }
            

            babukep = Bkep;
            szin = Szin;
            if (szin == "fehér")
            {
                szin = "w";
            }
            else
            {
                szin = "b";
            }
            
            KépFeltoltés();
        }

        private void KépFeltoltés()
        {
            kiralynoPbox.Image = Image.FromFile($"piece/{babukep}/{szin}Q.png");
            bastyaPbox.Image = Image.FromFile($"piece/{babukep}/{szin}R.png");
            futoPbox.Image = Image.FromFile($"piece/{babukep}/{szin}B.png");
            loPbox.Image = Image.FromFile($"piece/{babukep}/{szin}N.png");

            kiralynoPbox.SizeMode = PictureBoxSizeMode.Zoom;
            kiralynoPbox.Size = new Size(90, 90);
            bastyaPbox.SizeMode = PictureBoxSizeMode.Zoom;
            bastyaPbox.Size = new Size(90, 90);
            futoPbox.SizeMode = PictureBoxSizeMode.Zoom;
            futoPbox.Size = new Size(90, 90);
            loPbox.SizeMode = PictureBoxSizeMode.Zoom;
            loPbox.Size = new Size(90, 90);
        }
        private void kiralynoPbox_Click_1(object sender, EventArgs e)
        {
            kivalasztott = "Q";
        }

        private void bastyaPbox_Click(object sender, EventArgs e)
        {
            kivalasztott = "R";
        }

        private void futoPbox_Click(object sender, EventArgs e)
        {
            kivalasztott = "B";
        }

        private void loPbox_Click(object sender, EventArgs e)
        {
            kivalasztott = "N";
        }
    }
}
