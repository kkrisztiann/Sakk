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
    public partial class Form1 : Form
    {
        static int tablameret = 8;
        static Mezo[,] tabla = new Mezo[tablameret,tablameret];
        static Point kezdoPont = new Point(50, 50);
        static string[] hatsosor = new string[] { "bástya", "huszár", "futó", "királynő", "király", "futó", "huszár", "bástya"};
        static string kijon = "fehér";
        static string aktszerkesztesszin = "w";
        static string aktszerkesztesbabu = "";
        static bool szerekesztomod = false;

        public Form1()
        {
            InitializeComponent();
            TablaGen();
            SzerkesztoMod();
        }

        private void TablaGen()
        {
            bool vilagos = true;
            for (int sor = 0; sor < tablameret; sor++)
            {
                for (int oszlop = 0; oszlop < tablameret; oszlop++)
                {
                    tabla[sor, oszlop] = new Mezo(new Point(sor, oszlop));
                    tabla[sor, oszlop].Location = new Point(kezdoPont.X + oszlop * tabla[sor, oszlop].Size.Width, kezdoPont.Y + sor * tabla[sor, oszlop].Size.Height);
                    this.Controls.Add(tabla[sor, oszlop]);

                    tabla[sor, oszlop].BackColor = vilagos ? Color.Tan : Color.Brown;
                    if (oszlop != tablameret-1)
                    {
                        vilagos = !vilagos;
                    }

                    if (sor == 0 || sor == tablameret-1)
                    {
                        tabla[sor, oszlop].Babu = new Babu(hatsosor[oszlop] ,sor == 0 ? "fekete" : "fehér");
                    }
                    if (sor == 1 || sor == tablameret-2)
                    {
                        tabla[sor, oszlop].Babu = new Babu("paraszt", sor == 1 ? "fekete" : "fehér");
                    }
                    tabla[sor, oszlop].MouseClick += new MouseEventHandler(Klikkeles);
                }
            }

            tabla[1, 0].Kijelolt = true;
            tabla[6, 0].Lepheto = true;
            tabla[4, 0].Lepheto = true;
        }

        private void Klikkeles(object sender, MouseEventArgs e)
        {
            Mezo klikkelt = sender as Mezo;
            if (szerekesztomod)
            {
                if (e.Button==MouseButtons.Right)
                {
                    tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Babu = null;
                    tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Lepheto = false;
                    tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Kijelolt = false;
                }
                else if (e.Button == MouseButtons.Left || aktszerkesztesbabu!="")
                {
                    tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Babu = new Babu(aktszerkesztesbabu, aktszerkesztesszin == "w" ? "fehér" : "fekete");
                    tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Lepheto = false;
                    tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Kijelolt = false;
                }
            }          
            //if (klikkelt.Babu.Tipus == "üres") { return; };
            /*if (klikkelt.Babu.Szin == kijon) {


                JatekosCsere();
            };*/
        }

        private void JatekosCsere()
        {
            if (kijon == "fehér") { kijon = "fekete"; }
            else { kijon = "fehér"; }

            MessageBox.Show(kijon);
        }

        private void feketeBtn_Click(object sender, EventArgs e)
        {
            feherBtn.Enabled = true;
            feketeBtn.Enabled = false;
            aktszerkesztesszin = "b";
            SzerkesztoMod();
            feherBtn.Enabled = !feherBtn.Enabled;
            feherBtn.Enabled = !feherBtn.Enabled;
        }

        private void feherBtn_Click(object sender, EventArgs e)
        {
            feketeBtn.Enabled = true;
            feherBtn.Enabled = false;
            aktszerkesztesszin = "w";
            feketeBtn.Enabled = !feketeBtn.Enabled;
            feketeBtn.Enabled = !feketeBtn.Enabled;
            SzerkesztoMod();
        }

        private void SzerkesztoKijelolesLevetel()
        {
            parasztPBox.BorderStyle = BorderStyle.None;
            futoPBox.BorderStyle = BorderStyle.None;
            loPBox.BorderStyle = BorderStyle.None;
            bastyaPBox.BorderStyle = BorderStyle.None;
            kiralynoPBox.BorderStyle = BorderStyle.None;
            KiralyPBox.BorderStyle = BorderStyle.None;
        }

        private void SzerkesztoMod()
        {
            parasztPBox.Image = Image.FromFile($"piece/horsey/{aktszerkesztesszin}P.png");
            futoPBox.Image = Image.FromFile($"piece/horsey/{aktszerkesztesszin}B.png");
            loPBox.Image = Image.FromFile($"piece/horsey/{aktszerkesztesszin}N.png");
            bastyaPBox.Image = Image.FromFile($"piece/horsey/{aktszerkesztesszin}R.png");
            kiralynoPBox.Image = Image.FromFile($"piece/horsey/{aktszerkesztesszin}Q.png");
            KiralyPBox.Image = Image.FromFile($"piece/horsey/{aktszerkesztesszin}K.png");
        }

        private void parasztPBox_Click(object sender, EventArgs e)
        {
            SzerkesztoKijelolesLevetel();
            parasztPBox.BorderStyle = BorderStyle.Fixed3D;
            aktszerkesztesbabu = "paraszt";
        }

        private void futoPBox_Click(object sender, EventArgs e)
        {
            SzerkesztoKijelolesLevetel();
            futoPBox.BorderStyle = BorderStyle.Fixed3D;
            aktszerkesztesbabu = "futó";
        }

        private void loPBox_Click(object sender, EventArgs e)
        {
            SzerkesztoKijelolesLevetel();
            loPBox.BorderStyle = BorderStyle.Fixed3D;
            aktszerkesztesbabu = "huszár";
        }

        private void bastyaPBox_Click(object sender, EventArgs e)
        {
            SzerkesztoKijelolesLevetel();
            bastyaPBox.BorderStyle = BorderStyle.Fixed3D;
            aktszerkesztesbabu = "bástya";
        }

        private void kiralynoPBox_Click(object sender, EventArgs e)
        {
            SzerkesztoKijelolesLevetel();
            kiralynoPBox.BorderStyle = BorderStyle.Fixed3D;
            aktszerkesztesbabu = "királynő";
        }

        private void KiralyPBox_Click(object sender, EventArgs e)
        {
            SzerkesztoKijelolesLevetel();
            KiralyPBox.BorderStyle = BorderStyle.Fixed3D;
            aktszerkesztesbabu = "király";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.ControlKey)
            {
                groupBox1.Visible = !groupBox1.Visible;
                szerekesztomod = !szerekesztomod;
            }
        }
    }
}
