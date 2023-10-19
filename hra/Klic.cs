using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace hra
{
    class Klic
    {
        public BarvaZamku barvaKlice;
        public Image obrazekKlice;
        public Policko poziceKlice;

        public Klic(Policko PoziceKlice, string barva)
        {
            poziceKlice = PoziceKlice;

            //na zaklade dat z .csv souboru urcuje barvu klice a prislusny obrazek
            switch (barva)
            {
                case "m":
                    barvaKlice = BarvaZamku.Modra;
                    obrazekKlice = Image.FromFile("../../obrazky/klic_modra.png");
                    break;
                case "z":
                    barvaKlice = BarvaZamku.Zelena;
                    obrazekKlice = Image.FromFile("../../obrazky/klic_zelena.png");
                    break;
                case "f":
                    barvaKlice = BarvaZamku.Fialova;
                    obrazekKlice = Image.FromFile("../../obrazky/klic_fialova.png");
                    break;
                case "ž":
                    barvaKlice = BarvaZamku.Zluta;
                    obrazekKlice = Image.FromFile("../../obrazky/klic_zluta.png");
                    break;
            }
        }
        public static void ZiskaniKlice(Policko aktivniPolicko, Mapa mapa) 
        {
            //pokud hrac dojde na policko s klicem, dojde k jeho sebrani (pridani do listu vlastneneKlice)
            List<Klic> pomKlice = new List<Klic>();
            foreach (Klic klic in mapa.klice) //projde list vsech klicu na mape
            {
                if (klic.poziceKlice == aktivniPolicko) 
                {
                    /*pokud se dany klic nachazi na policku, na kterem se prave nachazi hrac, 
                      dojde k pridani klice do listu vlastneneKlice a zmene stavu policka na Volne*/
                    StavovyRadek.vlastneneKlice.Add(klic);
                    aktivniPolicko.stavPolicka = StavPolicka.Volne;
                }
                else
                {
                    pomKlice.Add(klic);
                }
            }
            //udaje v listu mapa.klice jsou nahrazeny aktualnimi udaji
            mapa.klice.Clear();
            mapa.klice = pomKlice;
            
        }
    }
}
