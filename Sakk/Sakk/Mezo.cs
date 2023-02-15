﻿using System;
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
        private int tablameret = 8;
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
            babu_tipus = "cburnett";

            SizeMode = PictureBoxSizeMode.Zoom;
            BackgroundImageLayout = ImageLayout.Zoom;
            Size = new Size(60, 60);
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

        public List<List<Point>> LepesLehetosegek()
        {
            if (Babu != null)
            {
                switch (Babu.Tipus)
                {
                    case "bástya":
                        return Bastya();
                    case "huszár":
                        return Huszar();
                    case "királynő":
                        return Kiralyno();
                    case "futó":
                        return Futo();
                    case "paraszt":
                        return Paraszt();
                    case "király":
                        return Kiraly();
                    default:
                        return null;
                }
            }
            else
            {
                return new List<List<Point>>() { new List<Point>() };
            }
        }

        private List<List<Point>> Kiraly()
        {
            List<List<Point>> lista = new List<List<Point>>();

            for (int i = -1; i <= 1; i+=2)
            {
                for (int j = -1; j <= 1; j+=2)
                {
                    lista.Add(new List<Point>() { new Point(Koordinatak.X + i, Koordinatak.Y + j) });
                }
                List<Point> irany1 = new List<Point>();
                List<Point> irany2 = new List<Point>();
                for (int sor_oszlop = 1; sor_oszlop < 2; sor_oszlop++)
                {
                    irany1.Add(new Point(Koordinatak.X + i * sor_oszlop, Koordinatak.Y));
                    irany2.Add(new Point(Koordinatak.X, Koordinatak.Y + i * sor_oszlop));
                }
                lista.Add(irany1);
                lista.Add(irany2);
            }

            return lista;
        }

        private List<List<Point>> Paraszt()
        {
            List<List<Point>> lista = new List<List<Point>>();

            int irany = Babu.Szin == "fekete" ? 1 : -1;
            for (int i = -1; i <= 1; i++)
            {
                lista.Add(new List<Point>() { new Point(Koordinatak.X + i, Koordinatak.Y + irany) });
            }
            return lista;
        }

        private List<List<Point>> Futo()
        {
            List<List<Point>> lista = new List<List<Point>>();

            for (int i = -1; i <= 1; i+=2)
            {
                for (int j = -1; j <= 1; j+=2)
                {
                    List<Point> irany = new List<Point>();
                    for (int k = 1; k < tablameret; k++)
                    {
                        irany.Add(new Point(Koordinatak.X + i * k, Koordinatak.Y + j * k));
                    }
                    lista.Add(irany);
                }
            }
            return lista;
        }

        private List<List<Point>> Kiralyno()
        {
            return Bastya().Concat(Futo()).ToList();
        }

        private List<List<Point>> Huszar()
        {
            List<List<Point>> lista = new List<List<Point>>();

            foreach (List<int> item in new List<List<int>>() { new List<int>() { 2, 1}, new List<int>() { 1, 2 } })
            {
                for (int i = -1; i <= 1; i+=2)
                {
                    for (int j = -1; j <= 1; j+=2)
                    {
                        lista.Add(new List<Point>() { new Point(Koordinatak.X + i * item[0], Koordinatak.Y + j * item[1]) });
                    }
                }
            }
            return lista;
        }

        private List<List<Point>> Bastya()
        {
            List<List<Point>> lista = new List<List<Point>>();
            for (int i = -1; i <= 1; i+=2)
            {
                List<Point> irany1 = new List<Point>();
                List<Point> irany2 = new List<Point>();
                for (int sor_oszlop = 1; sor_oszlop < tablameret; sor_oszlop++)
                {
                    irany1.Add(new Point(Koordinatak.X + i*sor_oszlop, Koordinatak.Y));
                    irany2.Add(new Point(Koordinatak.X, Koordinatak.Y + i * sor_oszlop));
                }
                lista.Add(irany1);
                lista.Add(irany2);
            }
            return lista;
        }
    }
}
