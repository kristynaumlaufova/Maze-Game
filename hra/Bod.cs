using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace hra
{
    class Bod
    {
        public static Image obrazekBod;
        public Policko poziceBod;

        public Bod(Policko PoziceBod) 
        {
            obrazekBod = Image.FromFile("../../obrazky/mince.png");
            poziceBod = PoziceBod;
        }

        public static void ZiskBodu(Policko aktivniPolicko, Mapa mapa) 
        {
            //pokud hrac dojde na policko s minci, dojde k jejimu sebrani (zvyseni hodnoty promenne StavovyRadek.pocetBodu)
            List<Bod> pomBody = new List<Bod>();
            foreach (Bod bod in mapa.body)//projde vsechny body na mape
            {
                if (bod.poziceBod == aktivniPolicko)
                {
                    /*pokud se dana mince (bod) nachazi na policku, na kterem se prave nachazi hrac, 
                      dojde ke zvyseni hodnoty promenne StavovyRadek.pocetBodu a zmene stavu policka na Volne*/
                    StavovyRadek.pocetBodu++;
                    aktivniPolicko.stavPolicka = StavPolicka.Volne;
                }
                else
                {
                    pomBody.Add(bod);
                }
            }
            //udaje v listu mapa.body jsou nahrazeny aktualnimi udaji
            mapa.body.Clear();
            mapa.body = pomBody;
        }

    }
}
