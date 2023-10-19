using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace hra
{
    class Ostny
    {
        public Image obrazekOstny;
        public Policko poziceOstny;
        public bool pohybOstnu;

        public Ostny(Policko PoziceOstny) 
        {
            poziceOstny = PoziceOstny;
            obrazekOstny = Image.FromFile("../../obrazky/ostny.png");
            pohybOstnu = false;
        }

        public Ostny(Policko PoziceOstny, bool PohybOstnu) 
        {
            //alternativni konstruktor; umoznuje zachovani hodnoty pohybOstnu;
            poziceOstny = PoziceOstny;
            obrazekOstny = Image.FromFile("../../obrazky/ostny.png");
            pohybOstnu = PohybOstnu;
        }

        public static Mapa PohybOstnu(Mapa mapa, Policko aktivniPolicko)
        {
            //zajistuje pohyb ostnu smerem dolu dokud nenarazi na prekazku
            List<Ostny> pomListOstny = new List<Ostny>();
            Ostny pomOstny;
            Policko pomPozice;
            foreach (Ostny ostny in mapa.ostny)
            {
                pomOstny = ostny;
                if (ostny.pohybOstnu) //pohyb probiha pouze u "pohybujicich se" ostnu (jejich pohyb musel byt spusten aktivitou hrace)
                {
                    mapa.policka[pomOstny.poziceOstny.indexX, pomOstny.poziceOstny.indexY].stavPolicka = StavPolicka.Volne; //stavajici pozice ostnu je oznacena jako volne policko
                 
                    pomPozice = mapa.policka[ostny.poziceOstny.indexX, ostny.poziceOstny.indexY + 1]; //policko, na ktere se maji ostny presunout
                    if (pomPozice.stavPolicka == StavPolicka.Volne)
                    {
                        //pokud je pomPozice volna muze dojit k presunu ostnu
                        pomOstny = new Ostny(pomPozice, true);
                        pomListOstny.Add(pomOstny);

                        //pokud se ostny dotknou (maji shodne souradnice) postavicky dojde ke ztrate zivota 
                        if ((aktivniPolicko.indexX == pomOstny.poziceOstny.indexX) && (aktivniPolicko.indexY == pomOstny.poziceOstny.indexY))
                        {
                            StavovyRadek.OdpocetZivotu();
                        }

                        //pozice, na kterou se ostny presunuly, je oznacena jako pozice, kde se nachazi ostny
                        mapa.policka[pomOstny.poziceOstny.indexX, pomOstny.poziceOstny.indexY].stavPolicka = StavPolicka.PoziceOstny;
                    }
                    else
                    {
                        //ostny narazily na prekazku; policko, na kterem se doposud nachazely ostny, je oznaceno jako volne a dane ostny jsou vymazany
                        mapa.policka[pomOstny.poziceOstny.indexX, pomOstny.poziceOstny.indexY].stavPolicka = StavPolicka.Volne;
                    }                                                  
                }
                else
                {
                    pomListOstny.Add(pomOstny);
                }                
            }
            //udaje v listu mapa.ostny jsou nahrazeny aktualnimi udaji
            mapa.ostny = pomListOstny;

            return mapa;
        }

        public static Mapa ZacatekPohybuOstnu(Mapa mapa, Policko aktivniPolicko) 
        {
            //pokud hrac dojde na policko pod ostny (policko se stavem SpusteniOstnu), dojde k jejich oznaceni na "pohybujici se" (resp. pohybOstnu = true)
            List<Ostny> pomListOstny = new List<Ostny>();
            Ostny pomOstny;
            foreach (Ostny ostny in mapa.ostny)//projde vsechny ostny na mape
            {
                pomOstny = ostny;
                if ((aktivniPolicko.stavPolicka == StavPolicka.SpusteniOstnu) && (ostny.poziceOstny.indexX  == aktivniPolicko.indexX))
                {
                    /*dane ostny jsou identifikovany podle x-ove souradnice (staci, aby se rovnala x-ova souradnice ostnu a aktivnihoPolicka;
                      neni nutna rovnost y-ovych souradnice)*/
                    pomOstny.pohybOstnu = true;
                    mapa.policka[aktivniPolicko.indexX, aktivniPolicko.indexY].stavPolicka = StavPolicka.Volne;
                }
                pomListOstny.Add(pomOstny);
            }
            //udaje v listu mapa.ostny jsou nahrazeny aktualnimi udaji
            mapa.ostny = pomListOstny;
            return mapa;
        }

    }
}
