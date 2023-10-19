using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    enum StavPolicka { Volne, Prekazka, PoziceOstny, Prechod, SpusteniOstnu, Start, Cil, Zamceno, StavovyRadek, PoziceNepritele, Klic, Bod};
    enum SmerPohybu { Nahoru, Dolu, Doleva, Doprava};
    class Policko
    {
        /*Definuje objekt Policko. Uklada informaci o souradnicich
          policka a stavu policka (co je na danem policku umisteno).*/

        public int x;
        public int y;
        public int indexX;
        public int indexY;
        public StavPolicka stavPolicka;

        public Policko(int X, int Y, int IndexX, int IndexY, string Stav)
        {
            x = X;
            y = Y;
            indexX = IndexX;
            indexY = IndexY;

            //podle dat nactenych z .csv souboru je urcen stav policka
            switch (Stav)
            {
                //volne policko
                case "0":
                    stavPolicka = StavPolicka.Volne;
                    break;
                //prekazka
                case "1":
                    stavPolicka = StavPolicka.Prekazka;
                    break;
                //pozice pro ostny
                case "2":
                    stavPolicka = StavPolicka.PoziceOstny;
                    break;
                //stavovy radek
                case "3":
                    stavPolicka = StavPolicka.StavovyRadek;
                    break;
                //startovni pozice nepratel (jednotliva pismena oznacuji ruzne typy nepratel)
                case "v":
                case "h":
                case "r":
                    stavPolicka = StavPolicka.PoziceNepritele;
                    break;
                //cil
                case "C":
                    stavPolicka = StavPolicka.Cil;
                    break;
                //start
                case "S":
                    stavPolicka = StavPolicka.Start;
                    break;
                //klic (jednotliva pismena oznacuji ruzne barvy)
                case "m":
                case "z":
                case "f":
                case "ž":
                    stavPolicka = StavPolicka.Klic;
                    break;
                //zamek (jednotliva pismena oznacuji ruzne barvy)
                case "M":
                case "F":
                case "Z":
                case "Ž":
                    stavPolicka = StavPolicka.Zamceno;
                    break;
                //body
                case "b":
                    stavPolicka = StavPolicka.Bod;
                    break;
                //spusteni ostnu
                case "o":
                    stavPolicka = StavPolicka.SpusteniOstnu;
                    break;
                //prechod na dalsi mapu
                case "P":
                    stavPolicka = StavPolicka.Prechod;
                    break;
            }
        }

        public Policko(int X, int Y, int IndexX, int IndexY, StavPolicka Stav)
        {
            //alternativni konstruktor, nevyzaduje data z .csv souboru
            x = X;
            y = Y;
            indexX = IndexX;
            indexY = IndexY;
            stavPolicka = Stav;
        }
    }
}
