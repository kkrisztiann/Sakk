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
        static string[] hatsosor = new string[] { "bástya", "ló", "futó", "királynő", "király", "futó", "ló", "bástya"};

        public Form1()
        {
            InitializeComponent();
            BabuLepesVisszaAdas("bástya" );
            TablaGen();
        }

        private void TablaGen()
        {
            bool vilagosmezo = true;
            for (int i = 0; i < tablameret; i++)
            {
                for (int j = 0; j < tablameret; j++)
                {
                    if (i == 0)
                    {
                        tabla[i, j] = new Mezo(new Point(i, j), new Babu(hatsosor[j], "fekete"));
                    }
                    else if (i == 1)
                    {
                        tabla[i, j] = new Mezo(new Point(i, j), new Babu("paraszt", "fekete"));
                    }
                    else if (i == 7)
                    {
                        tabla[i, j] = new Mezo(new Point(i, j), new Babu(hatsosor[j], "fehér"));
                    }
                    else if (i == 6)
                    {
                        tabla[i, j] = new Mezo(new Point(i, j), new Babu("paraszt", "fehér"));
                    }
                    else
                    {
                        tabla[i, j] = new Mezo(new Point(i, j), new Babu("üres", "üres"));
                    }
                    if (vilagosmezo)
                    {
                        tabla[i, j].BackColor = Color.Tan;
                    }
                    else
                    {
                        tabla[i, j].BackColor = Color.Brown;
                    }
                    tabla[i, j].Size = new Size(80, 80);
                    tabla[i, j].Location = new Point(50 + i * 80, 50 + j * 80);
                    tabla[i, j].SizeMode=PictureBoxSizeMode.StretchImage;
                    tabla[i, j].Koordinatak= new Point(i,j);
                    tabla[i, j].MouseClick += new MouseEventHandler(Klikkeles);
                    this.Controls.Add(tabla[i, j]);
                    if (j!=7)
                    {
                        vilagosmezo = !vilagosmezo;
                    }

                }
            }
        }

        private void Klikkeles(object sender, MouseEventArgs e)
        {
            Mezo klikkelt = sender as Mezo;
        }

        private void BabuLepesVisszaAdas(string babu)
        {
            switch (babu)
            {
                case "bástya":

                    break;
                case "ló":

                    break;
                case "futó":

                    break;
                case "királynő":

                    break;
                case "király":

                    break;
                case "paraszt":

                    break;

                default:
                    break;
            }
        }
    }
}
