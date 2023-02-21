﻿using System;
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
        static Mezo kijelolt = null;
        static string babu_tipus = "cburnett";

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

        }

        private void Klikkeles(object sender, MouseEventArgs e)
        {
            Mezo klikkelt = sender as Mezo;
            if (szerekesztomod)
            {
                Szerkesztes(klikkelt, e);
                return;
            }
            //van kijelölt
            if (kijelolt!=null)
            {
                //lepheto mezore kattintott?
                if (klikkelt.Lepheto)
                {
                    //megtörténik a lépés/ütés
                    //kijelölések törlése
                }
                else
                {
                    //kijelölések törlése
                    KijelolesekTorlese();
                }
            }
            else
            {
                //bábura klikk
                if (klikkelt.Babu != null)
                {
                    //saját bábura klikk
                    if (klikkelt.Babu.Szin==kijon)
                    {
                        kijelolt = klikkelt;
                        kijelolt.Kijelolt = true;
                        LephetoMezokKijelolese(klikkelt);
                    }
                }
            }
            
            //if (klikkelt.Babu.Tipus == "üres") { return; };
            /*if (klikkelt.Babu.Szin == kijon) {


                JatekosCsere();
            };*/
        }

        private void KijelolesekTorlese()
        {
            for (int i = 0; i < tablameret; i++)
            {
                for (int j = 0; j < tablameret; j++)
                {
                    tabla[i, j].Kijelolt = false;
                    tabla[i, j].Lepheto = false;
                }
            }
            kijelolt = null;
        }

        private void LephetoMezokKijelolese(Mezo klikkelt)
        {
            List<List<Point>> lista = klikkelt.LepesLehetosegek();
            for (int i = 0; i < lista.Count; i++)
            {
                for (int j = 0; j < lista[i].Count; j++)
                {
                    if (tabla[lista[i][j].X, lista[i][j].Y].Babu==null)
                    {
                        if (Sakkellenorzes(klikkelt, lista[i][j].X, lista[i][j].Y) /*sakkellenörzés oda lépés esetén*/)
                        {
                            tabla[lista[i][j].X, lista[i][j].Y].Lepheto = true;
                        }
                    }
                    //vanrajtababu
                    else
                    {
                        //saját bábu-e
                        if (tabla[lista[i][j].X, lista[i][j].Y].Babu.Szin==kijon)
                        {
                            break;
                        }
                        //nem saját
                        else
                        {
                            tabla[lista[i][j].X, lista[i][j].Y].Lepheto = true;
                            break;
                        }
                    }

                }
            }
        }

        private bool Sakkellenorzes(Mezo klikkelt, int i, int j)
        {
            Mezo[,] segedtabla = TablaMasolo();
            //segedtablan majd megnezzuk hogy lenne e sakk
            segedtabla[i, j].Babu = new Babu(klikkelt.Babu.Tipus, klikkelt.Babu.Szin);
            segedtabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Babu = null;
            for (int k = 0; k < tablameret; k++)
            {
                for (int l = 0; l < tablameret; l++)
                {
                    if (segedtabla[k, l].Babu != null && segedtabla[k,l].Babu.Szin != kijon)
                    {
                        List<List<Point>> lista = segedtabla[k, l].LepesLehetosegek();
                        for (int m = 0; m < lista.Count; m++)
                        {
                            for (int n = 0; n < lista[m].Count; n++)
                            {
                                if (segedtabla[lista[m][n].X, lista[m][n].Y].Babu != null)
                                {
                                    if (segedtabla[lista[m][n].X, lista[m][n].Y].Babu.Tipus == "király" && segedtabla[lista[m][n].X, lista[m][n].Y].Babu.Szin == kijon)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        private Mezo[,] TablaMasolo()
        {
            Mezo[,] segedtabla = new Mezo[tablameret, tablameret];

            for (int i = 0; i < tablameret; i++)
            {
                for (int j = 0; j < tablameret; j++)
                {
                    segedtabla[i, j] = new Mezo(new Point(i, j));
                    segedtabla[i, j].Babu = tabla[i, j].Babu;
                }
            }
            return segedtabla;
        }

        private void Szerkesztes(Mezo klikkelt, MouseEventArgs e)
        {
            KijelolesekTorlese();
            if (e.Button == MouseButtons.Right)
            {
                tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Babu = null;
                tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Lepheto = false;
                tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Kijelolt = false;
            }
            else if (e.Button == MouseButtons.Left && aktszerkesztesbabu != "")
            {
                tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Babu = new Babu(aktszerkesztesbabu, aktszerkesztesszin == "w" ? "fehér" : "fekete");
                tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Lepheto = false;
                tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Kijelolt = false;
            }
        }

        private void JatekosCsere()
        {
            if (kijon == "fehér") { kijon = "fekete"; }
            else { kijon = "fehér"; }

            //MessageBox.Show(kijon);
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
            parasztPBox.Image = Image.FromFile($"piece/{babu_tipus}/{aktszerkesztesszin}P.png");
            futoPBox.Image = Image.FromFile($"piece/{babu_tipus}/{aktszerkesztesszin}B.png");
            loPBox.Image = Image.FromFile($"piece/{babu_tipus}/{aktszerkesztesszin}N.png");
            bastyaPBox.Image = Image.FromFile($"piece/{babu_tipus}/{aktszerkesztesszin}R.png");
            kiralynoPBox.Image = Image.FromFile($"piece/{babu_tipus}/{aktszerkesztesszin}Q.png");
            KiralyPBox.Image = Image.FromFile($"piece/{babu_tipus}/{aktszerkesztesszin}K.png");
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
                
                if (this.Size.Width==1075) this.Size = new Size(833, this.Height);
                else this.Size = new Size(1075, this.Height);
            }
        }

        private void TablaTorolBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    tabla[i, j].Babu = null;
                }
            }
            TablaTorolBtn.Enabled = false;
            TablaTorolBtn.Enabled = true;
            this.Focus();
        }

        private void TablaFeltoltBtn_Click(object sender, EventArgs e)
        {
            TablaFeltoltBtn.Enabled = false;
            TablaFeltoltBtn.Enabled = true;
            this.Focus();
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    if (i == 0 || i == tablameret - 1)
                    {
                        tabla[i, j].Babu = new Babu(hatsosor[j], i == 0 ? "fekete" : "fehér");
                    }
                    if (i == 1 || i == tablameret - 2)
                    {
                        tabla[i, j].Babu = new Babu("paraszt", i == 1 ? "fekete" : "fehér");
                    }
                }
            }
        }
    }
}
