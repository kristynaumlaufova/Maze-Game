using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace hra
{
    class StavovyRadek
    {
        // zobrazuje stav zakladnich udaju o dane hre (zivoty, body, vlastnene klice)
        
        public static int pocetBodu = 0;
        public static int pocetZivotu;
        public Image obrazekZivot;
        public static List<Klic> vlastneneKlice = new List<Klic>();

        public StavovyRadek(Obtiznost obtiznost) 
        {
            obrazekZivot = Image.FromFile("../../obrazky/zivot.png");
            pocetBodu = 0;
            vlastneneKlice.Clear();

            switch (obtiznost)
            {
                case Obtiznost.Nizka:
                    pocetZivotu = 5;
                    break;
                case Obtiznost.Stredni:
                    pocetZivotu = 4;
                    break;
                case Obtiznost.Vysoka:
                    pocetZivotu = 3;
                    break;
            }

        }

        public static void OdpocetZivotu() 
        {
            //odecita zivoty pri stretnuti hrace s nepritelem a ukoncuje hru v pripde, ze jsou vsechny zivoty vycerpany
            pocetZivotu--;
            if (pocetZivotu == 0)
            {
                MessageBox.Show("Konec hry");
                Application.Restart();
            }
        }
               
    }
}
