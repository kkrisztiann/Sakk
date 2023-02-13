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

        public Form1()
        {
            InitializeComponent();
            TablaGen();
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
                }
            }

            tabla[1, 0].Kijelolt = true;
            tabla[6, 0].Lepheto = true;
            tabla[4, 0].Lepheto = true;
        }

        private void Klikkeles(object sender, MouseEventArgs e)
        {
            Mezo klikkelt = sender as Mezo;

            if (klikkelt.Babu.Tipus == "üres") { return; };
          
            if (klikkelt.Babu.Szin == kijon) {


                JatekosCsere();
            };
        }

        private void JatekosCsere()
        {
            if (kijon == "fehér") { kijon = "fekete"; }
            else { kijon = "fehér"; }

            MessageBox.Show(kijon);
        }
    }
}
