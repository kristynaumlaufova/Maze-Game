using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace hra
{
    enum BarvaZamku { Modra, Fialova, Zelena, Zluta};
    class Zamek
    {
        public BarvaZamku barvaZamku;
        public Image obrazekZamku;
        public Policko poziceZamku;

        public Zamek(Policko PoziceZamku, string barva) 
        {
            poziceZamku = PoziceZamku;

            //na zaklade dat z .csv souboru urcuje barvu zamku a prislusny obrazek
            switch (barva)
            {
                case "M":
                    barvaZamku = BarvaZamku.Modra;
                    obrazekZamku = Image.FromFile("../../obrazky/zamek_modra.png");
                    break;
                case "Z":
                    barvaZamku = BarvaZamku.Zelena;
                    obrazekZamku = Image.FromFile("../../obrazky/zamek_zelena.png");
                    break;
                case "F":
                    barvaZamku = BarvaZamku.Fialova;
                    obrazekZamku = Image.FromFile("../../obrazky/zamek_fialova.png");
                    break;
                case "Ž":
                    barvaZamku = BarvaZamku.Zluta;
                    obrazekZamku = Image.FromFile("../../obrazky/zamek_zluta.png");
                    break;
            }
        }

        public static void Odemknuti(Policko pomPolicko, Mapa mapa) 
        {
            //pokud jiz hrac sebral klic stejne barvy, dojde k "odemknuti" zamku (zmenu stavu policka ze Zamceno na Volne)
            if (pomPolicko.stavPolicka == StavPolicka.Zamceno)
            {
                foreach (Zamek zamek in mapa.zamky) //projde vsechny zamky
                {
                    if (pomPolicko == zamek.poziceZamku) //najde hledany zamek
                    {
                        foreach (Klic klic in StavovyRadek.vlastneneKlice) //projde vsechny vlastnene klice
                        {
                            if (zamek.barvaZamku == klic.barvaKlice) //pokud je mezi vlastnenymi klici klic spravne barvy, dojde k "odemknuti" zamku
                            {
                                pomPolicko.stavPolicka = StavPolicka.Volne;
                            }
                        }
                    }
                }
            }
        }
    }
}
