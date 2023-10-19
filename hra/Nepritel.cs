using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace hra
{
    enum TypNepritele { Vertikalni, Horizontalni, Rychly};
    enum Rychlost { Pomaly, Rychly };
    class Nepritel
    {
        public Policko poziceNepritele;
        public Image obrazekNepritele;
        public TypNepritele typNepritele;
        public SmerPohybu smerNepritele;

        public Nepritel(Policko PoziceNepritele, string typ) 
        {
            poziceNepritele = PoziceNepritele;

            //urceni typu nepritele (resp. smer, kterym se pohybuje)
            switch (typ)
            {
                case "v":
                    typNepritele = TypNepritele.Vertikalni;
                    smerNepritele = SmerPohybu.Dolu;
                    break;
                case "h":
                    typNepritele = TypNepritele.Horizontalni;
                    smerNepritele = SmerPohybu.Doleva;
                    break;
                case "r":
                    typNepritele = TypNepritele.Rychly;
                    break;
            }

            //vyber obrazku
            if (typNepritele == TypNepritele.Rychly)
            {
                obrazekNepritele = Image.FromFile("../../obrazky/nepritel1.png");
            }
            else
            {
                obrazekNepritele = Image.FromFile("../../obrazky/nepritel2.png");
            }

        }

        public Nepritel(Policko PoziceNepritele, TypNepritele typ, SmerPohybu smer)
        {
            //alternativni konstruktor, ktery umoznuje vytvorit instanti objektu Nepritel bez vstupnich dat z .csv souboru
            poziceNepritele = PoziceNepritele;
            typNepritele = typ;
            smerNepritele = smer;

            //vyber obrazku
            if (typNepritele == TypNepritele.Rychly)
            {
                obrazekNepritele = Image.FromFile("../../obrazky/nepritel1.png");
            }
            else
            {
                obrazekNepritele = Image.FromFile("../../obrazky/nepritel2.png");
            }

        }


        public static Mapa PohybNepritel(Mapa mapa, Policko aktivniPolicko, Rychlost rychlost) 
        {
            List<Nepritel> pomNepratele = new List<Nepritel>();
            Nepritel pomNepritel;
            foreach (Nepritel nepritel in mapa.nepratele)
            {
                pomNepritel = nepritel;
                //stavajici pozice nepritele je oznacena jako volne policko
                mapa.policka[nepritel.poziceNepritele.indexX, nepritel.poziceNepritele.indexY].stavPolicka = StavPolicka.Volne;
                //na zaklade typu nepritele rozhodne jakym zpusobem se bude pohybovat
                switch (nepritel.typNepritele)
                {
                    case TypNepritele.Vertikalni:
                        if (rychlost == Rychlost.Pomaly) //pomali nepratele se pohybuji pouze jednou za sekundu
                        {
                            //zajistuje vertikalni pohyb pomaleho nepritele
                            pomNepritel = PohybNepritelVertikalni(nepritel, mapa, aktivniPolicko);
                        }
                        break;
                    case TypNepritele.Rychly:
                        if ((rychlost == Rychlost.Rychly) || (rychlost == Rychlost.Pomaly)) //rychli nepratele se pohybuji dvakrat za sekundu
                        {
                            //zajistuje vertikalni pohyb rychleho nepritele
                            pomNepritel = PohybNepritelVertikalni(nepritel, mapa, aktivniPolicko);
                        }
                        break;
                    case TypNepritele.Horizontalni:
                        if (rychlost == Rychlost.Pomaly)//pomali nepratele se pohybuji pouze jednou za sekundu
                        {
                            //zajistuje horizontalni pohyb pomaleho nepritele
                            pomNepritel = PohybNepritelHorizontalni(nepritel, mapa, aktivniPolicko);
                        }                                        
                        break;
                }
                pomNepratele.Add(pomNepritel);
                //nova pozice nepritele je oznacena jako policko, na kterem je nepritel
                mapa.policka[pomNepritel.poziceNepritele.indexX, pomNepritel.poziceNepritele.indexY].stavPolicka = StavPolicka.PoziceNepritele;
            }
            //udaje v listu mapa.neprtale jsou nahrazeny aktualnimi udaji
            mapa.nepratele = pomNepratele;

            return mapa;
        }

        public static Nepritel PohybNepritelVertikalni(Nepritel nepritel, Mapa mapa, Policko aktivniPolicko) 
        {
            //zajistuje vertikalni pohyb nepritele
            Policko pomPozice;
            Nepritel pomNepritel;
            if (nepritel.smerNepritele == SmerPohybu.Dolu)
            {
                //nepritel se pohybuje smerem dolu
                pomPozice = mapa.policka[nepritel.poziceNepritele.indexX, nepritel.poziceNepritele.indexY + 1]; //policko, na ktere se ma nepritel presunout
                if (pomPozice.stavPolicka == StavPolicka.Volne)
                {
                    //pokud, je pomPozice volne policko, dojde k presunu nepritele na toto policko
                    pomNepritel = new Nepritel(pomPozice, nepritel.typNepritele, SmerPohybu.Dolu);
                }
                else
                {
                    //pokud, je pomPozice prekazka, dojde k presunu nepritel na policko v opacnem smeru a dojde ke zmene smeru pohybu nepritele
                    pomPozice = mapa.policka[nepritel.poziceNepritele.indexX, nepritel.poziceNepritele.indexY - 1];
                    pomNepritel = new Nepritel(pomPozice, nepritel.typNepritele, SmerPohybu.Nahoru);
                }
            }
            else
            {
                //nepritel se pohybuje smerem nahoru
                pomPozice = mapa.policka[nepritel.poziceNepritele.indexX, nepritel.poziceNepritele.indexY - 1];//policko, na ktere se ma nepritel presunout
                if (pomPozice.stavPolicka == StavPolicka.Volne)
                {
                    //pokud, je pomPozice volne policko, dojde k presunu nepritele na toto policko
                    pomNepritel = new Nepritel(pomPozice, nepritel.typNepritele, SmerPohybu.Nahoru);
                }
                else
                {
                    //pokud, je pomPozice prekazka, dojde k presunu nepritel na policko v opacnem smeru a dojde ke zmene smeru pohybu nepritele
                    pomPozice = mapa.policka[nepritel.poziceNepritele.indexX, nepritel.poziceNepritele.indexY + 1];
                    pomNepritel = new Nepritel(pomPozice, nepritel.typNepritele, SmerPohybu.Dolu);
                }
            }

            //pokud se ostny dotknou (maji shodne souradnice) postavicky dojde ke ztrate zivota
            if ((aktivniPolicko.indexX == pomNepritel.poziceNepritele.indexX) && (aktivniPolicko.indexY == pomNepritel.poziceNepritele.indexY))
            {
                StavovyRadek.OdpocetZivotu();
            }

            return pomNepritel;
        }

        public static Nepritel PohybNepritelHorizontalni(Nepritel nepritel, Mapa mapa, Policko aktivniPolicko)
        {
            //zajistuje horizontalni pohyb nepritele

            Policko pomPozice;
            Nepritel pomNepritel;
            if (nepritel.smerNepritele == SmerPohybu.Doleva)
            {
                //nepritel se pohybuje smerem doleva
                pomPozice = mapa.policka[nepritel.poziceNepritele.indexX - 1, nepritel.poziceNepritele.indexY];//policko, na ktere se ma nepritel presunout
                if (pomPozice.stavPolicka == StavPolicka.Volne)
                {
                    //pokud, je pomPozice volne policko, dojde k presunu nepritele na toto policko
                    pomNepritel = new Nepritel(pomPozice, nepritel.typNepritele, SmerPohybu.Doleva);
                }
                else
                {
                    //pokud, je pomPozice prekazka, dojde k presunu nepritel na policko v opacnem smeru a dojde ke zmene smeru pohybu nepritele
                    pomPozice = mapa.policka[nepritel.poziceNepritele.indexX + 1, nepritel.poziceNepritele.indexY];
                    pomNepritel = new Nepritel(pomPozice, nepritel.typNepritele, SmerPohybu.Doprava);
                }
            }
            else
            {
                //nepritel se pohybuje smerem doprava
                pomPozice = mapa.policka[nepritel.poziceNepritele.indexX + 1, nepritel.poziceNepritele.indexY];//policko, na ktere se ma nepritel presunout
                if (pomPozice.stavPolicka == StavPolicka.Volne)
                {
                    //pokud, je pomPozice volne policko, dojde k presunu nepritele na toto policko
                    pomNepritel = new Nepritel(pomPozice, nepritel.typNepritele, SmerPohybu.Doprava);
                }
                else
                {
                    //pokud, je pomPozice prekazka, dojde k presunu nepritel na policko v opacnem smeru a dojde ke zmene smeru pohybu nepritele
                    pomPozice = mapa.policka[nepritel.poziceNepritele.indexX - 1, nepritel.poziceNepritele.indexY];
                    pomNepritel = new Nepritel(pomPozice, nepritel.typNepritele, SmerPohybu.Doleva);
                }
            }

            //pokud se ostny dotknou (maji shodne souradnice) postavicky dojde ke ztrate zivota
            if ((aktivniPolicko.indexX == pomNepritel.poziceNepritele.indexX) && (aktivniPolicko.indexY == pomNepritel.poziceNepritele.indexY))
            {
                StavovyRadek.OdpocetZivotu();
            }

            return pomNepritel;
        }

    }
}
