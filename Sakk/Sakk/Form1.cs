using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

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
        static string babu_tipus = FileIO.Beolvas();
        static bool matt = false;
        static int masikszamlalo = 0;
        static List<PictureBox> Tipusok = new List<PictureBox>();
        static Point SakkbanLevoKiralyPoz = new Point();
        static PromociKivalasztas valasztas = null;


        public Form1()
        {
            InitializeComponent();
            TablaGen();
            SzerkesztoMod();
            TipusValaszto();
            //this.ActiveControl = panel1;
            this.ActiveControl = null;
        }

        private void TipusValaszto()
        {
            List<string> tipusok = System.IO.Directory.GetDirectories(Environment.CurrentDirectory + "/piece").ToList();
            for (int i = 0; i < tipusok.Count; i++)
            {
                int b = Convert.ToInt32($"{i}");
                int gap = (TipusPanel.Size.Width- 23 - (50 * 3)) / 3;
                PictureBox Pbox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(50, 50),
                    Location = new Point(gap + ((i % 3)) * (gap + 50), (i / 3) * (gap + 50)),
                    BackColor = i % 2 == 0 ? Color.Brown : Color.Tan,
                    Image = Image.FromFile($"piece\\{tipusok[b].Split('\\')[tipusok[b].Split('\\').Length - 1]}\\wN.png"),
                };
                Tipusok.Add(Pbox);
                ToolTip tooltip1 = new ToolTip();
                tooltip1.SetToolTip(Pbox, $"{tipusok[b].Split('\\')[tipusok[b].Split('\\').Length - 1]}");
                Pbox.Click += delegate (object sender, EventArgs e) { TipusValasztas(tipusok[b], Pbox); };
                TipusPanel.Controls.Add(Pbox);
            }
        }

        private void TipusValasztas(string tipus, PictureBox kep)
        {
            tipus = tipus.Split('\\')[tipus.Split('\\').Length - 1];
            if (tipus != babu_tipus)
            {
                babu_tipus = tipus;
                for (int i = 0; i < Tipusok.Count; i++)
                {
                    Tipusok[i].BorderStyle = BorderStyle.None;
                }
                kep.BorderStyle = BorderStyle.Fixed3D;
                for (int i = 0; i < tablameret; i++)
                {
                    for (int j = 0; j < tablameret; j++)
                    {
                        tabla[i, j].babu_tipus = $"{tipus}";
                    }
                }
                SzerkesztoMod();
            }
        }

        private void TablaGen()
        {
            panel1.Size = new Size(833, 840);
            bool vilagos = true;
            for (int sor = 0; sor < tablameret; sor++)
            {
                for (int oszlop = 0; oszlop < tablameret; oszlop++)
                {
                    tabla[sor, oszlop] = new Mezo(new Point(sor, oszlop), babu_tipus, false);
                    tabla[sor, oszlop].Location = new Point(kezdoPont.X + oszlop * tabla[sor, oszlop].Size.Width, kezdoPont.Y + sor * tabla[sor, oszlop].Size.Height);
                    panel1.Controls.Add(tabla[sor, oszlop]);

                    tabla[sor, oszlop].BackColor = vilagos ? Color.Tan : Color.Brown;
                    if (oszlop != tablameret-1)
                    {
                        vilagos = !vilagos;
                    }

                    BabuGen(sor, oszlop);
                    tabla[sor, oszlop].MouseClick += new MouseEventHandler(Klikkeles);
                    int a = Convert.ToInt32($"{sor}");
                    int b = Convert.ToInt32($"{oszlop}");
                    tabla[sor, oszlop].MouseEnter += delegate (object sender, EventArgs e) { MouseEnter(tabla[a, b]); };
                    tabla[sor, oszlop].MouseLeave += delegate (object sender, EventArgs e) { MouseLeave(tabla[a, b]); };
                }
            }

        }

        private void MouseLeave(Mezo mezo)
        {
            if (mezo.Lepheto)
            {
                if (mezo.Babu == null)
                {
                    mezo.BackgroundImage = Image.FromFile("piece/kijeloltmezo.png");
                }
                else
                {
                    mezo.BackgroundImage = Image.FromFile("piece/kijeloltbabu.png");
                }
            }
        }

        private void MouseEnter(Mezo mezo)
        {
            if (mezo.Lepheto)
            {
                mezo.BackgroundImage = Image.FromFile("piece/kijelolt.png");
            }
        }

        private void BabuGen(int sor, int oszlop)
        {
            if (sor == 0 || sor == tablameret - 1)
            {
                tabla[sor, oszlop].Babu = new Babu(hatsosor[oszlop], sor == 0 ? "fekete" : "fehér");
            }
            if (sor == 1 || sor == tablameret - 2)
            {
                tabla[sor, oszlop].Babu = new Babu("paraszt", sor == 1 ? "fekete" : "fehér");
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
                    LepesPerUtes(klikkelt);
                    //kijelölések törlése
                    KijelolesekTorlese();
                    PromocioEllenorzes(klikkelt);
                    SakkVane();
                    JatekosCsere();
                    MattEllenorzes();

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
                        LephetoMezokKijelolese(klikkelt, false);
                    }
                }
            }
            
            //if (klikkelt.Babu.Tipus == "üres") { return; };
            /*if (klikkelt.Babu.Szin == kijon) {


                JatekosCsere();
            };*/
        }

        private void MattEllenorzes()
        {
            masikszamlalo = 0;
            for (int i = 0; i < tablameret; i++)
            {
                for (int j = 0; j < tablameret; j++)
                {
                    if (tabla[i, j].Babu != null && tabla[i, j].Babu.Szin == kijon)
                    {
                        masikszamlalo++;

                    }
                }
            }
            for (int i = 0; i < tablameret; i++)
            {
                for (int j = 0; j < tablameret; j++)
                {
                    if (tabla[i,j].Babu != null && tabla[i, j].Babu.Szin == kijon)
                    {
                        LephetoMezokKijelolese(tabla[i, j], true);

                    }
                }
            }
            if (masikszamlalo==0)
            {

                if (tabla[SakkbanLevoKiralyPoz.X, SakkbanLevoKiralyPoz.Y].Sakkban == true)
                {
                    JatekosCsere();
                    DialogResult dialogResult1 = MessageBox.Show($"Sakk, Matt.\nA {kijon} nyerte a játszmát!\nSzeretne új játékot kezdeni?", "Győzelem!", MessageBoxButtons.YesNo);
                    if (dialogResult1 == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                    else if (dialogResult1 == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    DialogResult dialogResult1 = MessageBox.Show($"Patt.\nA végeredmény döntetlen!\nSzeretne új játékot kezdeni?", "Döntetlen!", MessageBoxButtons.YesNo);
                    if (dialogResult1 == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                    else if (dialogResult1 == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
            }
            
        }

        private void PromocioEllenorzes(Mezo klikkelt)
        {
            if (klikkelt.Babu.Tipus=="paraszt")
            {
                if (klikkelt.Babu.Szin=="fehér" && klikkelt.Koordinatak.X==0 || klikkelt.Babu.Szin == "fekete" && klikkelt.Koordinatak.X == tablameret - 1)
                {
                    Promocio(klikkelt);
                }
            }
        }

        private void Promocio(Mezo klikkelt)
        {
            valasztas = new PromociKivalasztas(klikkelt, babu_tipus);
            valasztas.Location = klikkelt.Babu.Szin == "fehér" ? klikkelt.Location : new Point(klikkelt.Location.X, klikkelt.Location.Y+ klikkelt.Size.Height - valasztas.Size.Height);
            this.Controls.Add(valasztas);
            valasztas.BringToFront();
            valasztas.Dontes += delegate (object sender, EventArgs e) { PromocioKivalasztas(klikkelt); };
            panel1.Enabled = false;
        }

        private void PromocioKivalasztas(Mezo klikkelt)
        {
            string szin = klikkelt.Babu.Szin;
            switch (valasztas.BabuTipus)
            {
                case "Q":
                    klikkelt.Babu = new Babu("királynő", szin);
                    break;
                case "R":
                    klikkelt.Babu = new Babu("bástya", szin);
                    break;
                case "N":
                    klikkelt.Babu = new Babu("huszár", szin);
                    break;
                case "B":
                    klikkelt.Babu = new Babu("futó", szin);
                    break;
                default:
                    break;
            }
            this.Controls.Remove(valasztas);
            valasztas = null;
            panel1.Enabled = true;
            JatekosCsere();
            SakkVane();
            JatekosCsere();
            MattEllenorzes();
        }

        private void LepesPerUtes(Mezo klikkelt)
        {
            //klikkelt egyenlo lesz a kijeloltel
            kijelolt.Babu.Lepettemar = true;
            klikkelt.Babu = kijelolt.Babu;
            // sáncnál ez nem fog működni
            kijelolt.Babu = null;
        }

        private void SakkVane()
        {
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    tabla[i, j].Sakkban = false;
                }
            }
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    if (tabla[i,j].Babu!=null && tabla[i,j].Babu.Szin==kijon)
                    {
                        List<List<Point>> lista = tabla[i, j].LepesLehetosegek();
                        for (int k = 0; k < lista.Count; k++)
                        {
                            for (int l = 0; l < lista[k].Count; l++)
                            {
                                if (tabla[i, j].Babu.Tipus == "paraszt" && k == 1)
                                {
                                    break;
                                }
                                if (tabla[lista[k][l].X, lista[k][l].Y].Babu!=null && tabla[lista[k][l].X, lista[k][l].Y].Babu.Tipus == "király" && tabla[lista[k][l].X, lista[k][l].Y].Babu.Szin!=kijon)
                                {
                                    tabla[lista[k][l].X, lista[k][l].Y].Sakkban = true;
                                    SakkbanLevoKiralyPoz = new Point(lista[k][l].X, lista[k][l].Y);
                                    return;
                                }
                                else if (tabla[lista[k][l].X, lista[k][l].Y].Babu!=null)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void KijelolesekTorlese()
        {
            for (int i = 0; i < tablameret; i++)
            {
                for (int j = 0; j < tablameret; j++)
                {
                    tabla[i, j].Kijelolt = false;
                    tabla[i, j].Lepheto = false;
                    //tabla[i, j].Sakkban= false;

                }
            }
            kijelolt = null;
        }

        private void LephetoMezokKijelolese(Mezo klikkelt, bool ellenorzes)
        {
            List<List<Point>> lista = klikkelt.LepesLehetosegek();
            for (int i = 0; i < lista.Count; i++)
            {
                for (int j = 0; j < lista[i].Count; j++)
                {
                    if (tabla[lista[i][j].X, lista[i][j].Y].Babu==null)
                    {
                        //meg nemtudom mit csinal
                        //alitolag parasztot segiti
                        if (!(klikkelt.Babu.Tipus == "paraszt" && i % 2 == 0))
                        {
                            //Sánc lenne
                            /*
                            if (klikkelt.Babu.Tipus == "király" && !klikkelt.Babu.Lepettemar && !tabla[0, klikkelt.Koordinatak.Y].Babu.Lepettemar)
                            {
                                lista[4].Add(new Point(klikkelt.Koordinatak.X-2, klikkelt.Koordinatak.Y));
                            }
                            if (klikkelt.Babu.Tipus == "király" && !klikkelt.Babu.Lepettemar && !tabla[tablameret-1, klikkelt.Koordinatak.Y].Babu.Lepettemar)
                            {
                                lista[5].Add(new Point(klikkelt.Koordinatak.X + 2, klikkelt.Koordinatak.Y));
                            }
                            */
                            if (Sakkellenorzes(klikkelt, lista[i][j].X, lista[i][j].Y) /*sakkellenörzés oda lépés esetén*/)
                            {
                                if (!ellenorzes)
                                {
                                    tabla[lista[i][j].X, lista[i][j].Y].Lepheto = true;
                                }
                                else
                                {
                                    matt = false;
                                    return;
                                }
                            }
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
                            if (klikkelt.Babu.Tipus == "paraszt" && i % 2 == 0)
                            {
                                if (Sakkellenorzes(klikkelt, lista[i][j].X, lista[i][j].Y) /*sakkellenörzés oda lépés esetén*/)
                                {
                                    if (!ellenorzes)
                                    {
                                        tabla[lista[i][j].X, lista[i][j].Y].Lepheto = true;
                                    }
                                    else
                                    {
                                        matt = false;
                                        return;
                                    }
                                }
                            }
                            else if (klikkelt.Babu.Tipus != "paraszt")
                            {
                                if (Sakkellenorzes(klikkelt, lista[i][j].X, lista[i][j].Y) /*sakkellenörzés oda lépés esetén*/)
                                {
                                    if (!ellenorzes)
                                    {
                                        tabla[lista[i][j].X, lista[i][j].Y].Lepheto = true;
                                    }
                                    else
                                    {
                                        matt = false;
                                        return;
                                    }
                                }
                            }

                            break;
                        }
                    }

                }
            }
            if (ellenorzes)
            {
                masikszamlalo--;
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
                                    if (segedtabla[k,l].Babu.Tipus == "paraszt" && m == 1)
                                    {
                                        break;
                                    }
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
                    segedtabla[i, j] = new Mezo(new Point(i, j),babu_tipus, true);
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

                //bástyánál még nincs lekezelve
                tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Babu.Lepettemar = true;
                
                
                if ((aktszerkesztesbabu == "paraszt" && klikkelt.Koordinatak.X == 6 && aktszerkesztesszin == "w") || (aktszerkesztesbabu == "paraszt" && klikkelt.Koordinatak.X == 1 && aktszerkesztesszin == "b"))
                {
                    tabla[klikkelt.Koordinatak.X, klikkelt.Koordinatak.Y].Babu.Lepettemar = false;
                }
            }
        }

        private void JatekosCsere()
        {
            kijon = new List<string>() { "fekete", "fehér" }.Find(x => x != kijon);

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
            this.ActiveControl = null;
        }

        private void feherBtn_Click(object sender, EventArgs e)
        {
            feketeBtn.Enabled = true;
            feherBtn.Enabled = false;
            aktszerkesztesszin = "w";
            feketeBtn.Enabled = !feketeBtn.Enabled;
            feketeBtn.Enabled = !feketeBtn.Enabled;
            SzerkesztoMod();
            this.ActiveControl = null;
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

        private void TablaTorolPBox_Click(object sender, EventArgs e)
        {
            KijelolesekTorlese();
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    tabla[i, j].Babu = null;
                    tabla[i, j].Sakkban = false;
                }
            }
            TablaFeltoltPBox.BorderStyle = BorderStyle.None;
            TablaTorolPBox.BorderStyle = BorderStyle.Fixed3D;
        }

        private void TablaFeltoltPBox_Click(object sender, EventArgs e)
        {
            kijon = "fehér";
            TablaTorolPBox_Click(sender,e);
            TablaTorolPBox.BorderStyle = BorderStyle.None;
            TablaFeltoltPBox.BorderStyle = BorderStyle.Fixed3D;
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    BabuGen(i, j);
                }
            }
        }


        private void DontetlenBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Elfogadja a döntetlent?", $"{kijon} döntetlent ajánl.", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DialogResult dialogResult1 = MessageBox.Show("Szeretne új játékot kezdeni?", "Döntetlen!", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult1 == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            this.ActiveControl = null;

        }

        private void FeladasBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Szeretne új játékot kezdeni?", $"{kijon} feladta a játékot, ön nyert!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Restart();
            }
            else if (dialogResult == DialogResult.No)
            {
                Application.Exit();
            }
            this.ActiveControl = null;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileIO.Kiiras(babu_tipus);
        }

    }
}
