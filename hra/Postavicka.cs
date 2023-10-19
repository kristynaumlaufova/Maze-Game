using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace hra
{
    class Postavicka
    {
        public Image obrazekPostavicka;

        public Postavicka(Obtiznost obtiznost) 
        { 
            obrazekPostavicka = Image.FromFile("../../obrazky/postavicka.png");
        }

        public Policko PohybPostavicky(Mapa mapa, Policko aktivniPolicko, SmerPohybu smer) 
        {
            //umoznuje presun postavicky na dalsi policko na zaklde vstupu z klavesnice
            Policko pomPolicko;

            switch (smer)
            {
                case SmerPohybu.Nahoru:
                    pomPolicko = mapa.policka[aktivniPolicko.indexX, aktivniPolicko.indexY - 1]; //policko, na ktere ma byt postavicka presunuta
                    Zamek.Odemknuti(pomPolicko, mapa);//zkusi "odemknout" dane policko
                    if ((pomPolicko.stavPolicka != StavPolicka.Prekazka) && ((pomPolicko.stavPolicka != StavPolicka.Zamceno)))
                    {
                        //pokud policko neni zamek nebo prekazka, muze dojit k presunu postavicky na toto policko
                        aktivniPolicko = pomPolicko;                                                             
                    }
                    break;
                case SmerPohybu.Dolu:
                    pomPolicko = mapa.policka[aktivniPolicko.indexX, aktivniPolicko.indexY + 1];//policko, na ktere ma byt postavicka presunuta
                    Zamek.Odemknuti(pomPolicko, mapa);//zkusi "odemknout" dane policko
                    if ((pomPolicko.stavPolicka != StavPolicka.Prekazka) && (pomPolicko.stavPolicka != StavPolicka.Zamceno))
                    {
                        //pokud policko neni zamek nebo prekazka, muze dojit k presunu postavicky na toto policko
                        aktivniPolicko = pomPolicko;
                    }
                    break;
                case SmerPohybu.Doprava:
                    pomPolicko = mapa.policka[aktivniPolicko.indexX + 1, aktivniPolicko.indexY];//policko, na ktere ma byt postavicka presunuta
                    Zamek.Odemknuti(pomPolicko, mapa);//zkusi "odemknout" dane policko
                    if ((pomPolicko.stavPolicka != StavPolicka.Prekazka) && (pomPolicko.stavPolicka != StavPolicka.Zamceno) && (aktivniPolicko.stavPolicka != StavPolicka.Prechod))
                    {
                        //pokud policko neni zamek nebo prekazka, muze dojit k presunu postavicky na toto policko
                        aktivniPolicko = pomPolicko;
                    }                                         
                    break;
                case SmerPohybu.Doleva:
                    pomPolicko = mapa.policka[aktivniPolicko.indexX - 1, aktivniPolicko.indexY];//policko, na ktere ma byt postavicka presunuta
                    Zamek.Odemknuti(pomPolicko, mapa);//zkusi "odemknout" dane policko
                    if ((pomPolicko.stavPolicka != StavPolicka.Prekazka) && (pomPolicko.stavPolicka != StavPolicka.Zamceno))
                    {
                        //pokud policko neni zamek nebo prekazka, muze dojit k presunu postavicky na toto policko
                        aktivniPolicko = pomPolicko;
                    }
                    break;
            }
            //umoznuje interakci s predmety na mape na zaklade stavu policka
            switch (aktivniPolicko.stavPolicka)
            {
                case StavPolicka.PoziceOstny:
                case StavPolicka.PoziceNepritele:
                    StavovyRadek.OdpocetZivotu();
                    break;
                case StavPolicka.Cil:
                    MessageBox.Show("Výhra!");
                    Application.Restart();
                    break;
                case StavPolicka.Klic:
                    Klic.ZiskaniKlice(aktivniPolicko, mapa);
                    break;
                case StavPolicka.Bod:
                    Bod.ZiskBodu(aktivniPolicko, mapa);
                    break;
            }
            return aktivniPolicko;  
        }

        public static Policko postavickaStart(Mapa mapa, Policko aktivniPolicko) 
        {
            //vykresleni postavicku na pozici start
            foreach (Policko policko in mapa.policka)
            {
                if (policko.stavPolicka == StavPolicka.Start)
                {
                    aktivniPolicko = policko;
                    policko.stavPolicka = StavPolicka.Volne;
                }
            }
            return aktivniPolicko;
        }
    }
}
