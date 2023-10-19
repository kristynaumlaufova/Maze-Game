using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hra
{
    class Mapa
    {
        /*Nacita data z .csv souboru, na jejichz zaklade 
          vytvari vicerozmerne pole predstavujici tabulku poliček.
          Kazda polozka pole obsahuje zaznam o souradnicich policka
          a stav policka (volne, prekazka, start, cil ...).
          Dale obsahuje listy, ve kterych jsou ulozeny klice, zamky,
          body, ostny a nepratele.*/

        public Policko[,] policka = new Policko[26,23];
        public List<Zamek> zamky = new List<Zamek>();
        public List<Klic> klice = new List<Klic>();
        public List<Bod> body = new List<Bod>();
        public List<Ostny> ostny = new List<Ostny>();
        public List<Nepritel> nepratele = new List<Nepritel>();
        public Image mapaObrazek;


        public Mapa(string zdrojovySoubor, int sirkaPolicka, int cisloMapy) 
        {
            //list policka
            string data;
            string[] stavy;
            int souradniceX = 0;
            int souradniceY = 0;
            int indexX = 0;
            int indexY = 0;

            //obrazekMapy
            if (cisloMapy == 1)
            {
                mapaObrazek = Image.FromFile("../../mapa/mapa1_obrazek.png");
            }
            else
            {
                mapaObrazek = Image.FromFile("../../mapa/mapa2_obrazek.png");
            }

            //nacteni dat ze souboru
            StreamReader soubor = new StreamReader(zdrojovySoubor, Encoding.Default);

            while ((data = soubor.ReadLine()) != null) 
            {
                stavy = data.Split(';');
                //vytvoreni vicerozmerneho pole policka
                foreach (string stav in stavy)
                {
                    //vytvoreni instanci jednotlivych policek
                    Policko pomPolicko = new Policko(souradniceX, souradniceY, indexX, indexY, stav);

                    //vytvoreni instanci objektu Ostny, Zamek, Nepritel, Klic, a Bod a jejich pridani do listu (na zaklade informace o stavu policka)
                    switch (pomPolicko.stavPolicka)
                    {
                        case StavPolicka.PoziceOstny:
                            ostny.Add(new Ostny(pomPolicko));
                            break;
                        case StavPolicka.SpusteniOstnu:
                            break;
                        case StavPolicka.Zamceno:
                            zamky.Add(new Zamek(pomPolicko, stav));
                            break;
                        case StavPolicka.PoziceNepritele:
                            nepratele.Add(new Nepritel(pomPolicko, stav));
                            break;
                        case StavPolicka.Klic:
                            klice.Add(new Klic(pomPolicko, stav));
                            break;
                        case StavPolicka.Bod:
                            body.Add(new Bod(pomPolicko));
                            break;
                    }
                    policka[indexX, indexY] = pomPolicko;
                    souradniceX += sirkaPolicka;
                    indexX++;
                }               
                souradniceX = indexX = 0;
                souradniceY += sirkaPolicka;
                indexY++;
            }
    
            soubor.Close();
        }
    }
}
