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
        public Babu Babu = null;

        public Mezo(Point koordinatak, Babu babu)
        {
            Koordinatak = koordinatak;
            Babu = babu;
        }
    }
}
