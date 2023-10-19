using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hra
{
    enum Obtiznost { Nizka, Stredni, Vysoka };
    
    public partial class okno : Form
    {       
        Mapa mapa;
        StavovyRadek stavovyRadek;       
        Postavicka postavicka = new Postavicka(Obtiznost.Nizka);
        Obtiznost obtiznost;
        Rychlost rychlost;
        Policko aktivniPolicko; //policko, na kterem se prave nachazi hrac

        bool pauza = false;
        bool novaHra = false;

        int sirkaPolicka = 30;
        string nulaSekundy, nulaMinuty;
        int sekunda, sekundy, minuty;
        string hodiny;
        

        public okno()
        {
            InitializeComponent();

            //pozadi - uvod
            this.BackgroundImageLayout = ImageLayout.Stretch;            
            this.BackgroundImage = Image.FromFile("../../mapa/pozadi.png");
                
        }
        private void tlacitkoNovaHra_Click(object sender, EventArgs e)
        {
            //zobrazuje obrazovku s napovedou
            this.BackgroundImage = Image.FromFile("../../mapa/napoveda.png");
            tlacitkoPokracovat.Visible = true;
            tlacitkoNovaHra.Visible = false;
            
        }
        private void tlacitkoPokracovat_Click(object sender, EventArgs e)
        {
            //zobrazuje vyber obtiznosti
            this.BackgroundImage = Image.FromFile("../../mapa/obtiznost.png");
            tlacitkoPokracovat.Visible = false;
            tlacitkoObtiznostL.Visible = tlacitkoObtiznostS.Visible = tlacitkoObtiznostT.Visible = true;
        }

        private void tlacitkaObtiznost_Click(object sender, EventArgs e)
        {
            //vyber obtiznosti
            Button stisknuteTlacitko = (Button)sender;
            switch (stisknuteTlacitko.Name)
            {
                case "tlacitkoObtiznostL":
                    obtiznost = Obtiznost.Nizka;
                    break;
                case "tlacitkoObtiznostS":
                    obtiznost = Obtiznost.Stredni;
                    break;
                case "tlacitkoObtiznostT":
                    obtiznost = Obtiznost.Vysoka;
                    break;
            }

            //zacatek hry
            mapa = new Mapa("../../mapa/mapa1_data.csv", sirkaPolicka, 1);
            this.BackgroundImage = mapa.mapaObrazek;
            Spusteni(obtiznost);
            tlacitkoObtiznostL.Visible = tlacitkoObtiznostS.Visible = tlacitkoObtiznostT.Visible = false;
        }
        private void Spusteni(Obtiznost obtiznost) 
        {
            //nastavuje promenne na pocatecni hodnoty
            sekunda = 0;
            sekundy = 0;
            minuty = 0;
            hodiny = "00:00";

            novaHra = true;
            casovac.Enabled = true;
            stavovyRadek = new StavovyRadek(obtiznost);

            aktivniPolicko = Postavicka.postavickaStart(mapa, aktivniPolicko);
        }

        private void okno_Paint(object sender, PaintEventArgs e)
        {
            Graphics kp = e.Graphics;

            if (novaHra == true)
            {
                //vykresleni postavicky na pozici start          
                kp.DrawImage(postavicka.obrazekPostavicka, aktivniPolicko.x, aktivniPolicko.y, sirkaPolicka, sirkaPolicka);

                //vykresleni predmetu/nepratel na mape
                foreach (Policko policko in mapa.policka)
                {
                    switch (policko.stavPolicka)
                    {
                        case StavPolicka.PoziceOstny:
                            foreach (Ostny ostny in mapa.ostny)
                            {
                                kp.DrawImage(ostny.obrazekOstny, ostny.poziceOstny.x, ostny.poziceOstny.y, sirkaPolicka, sirkaPolicka);
                            }
                            break;
                        case StavPolicka.Zamceno:
                            foreach (Zamek zamek in mapa.zamky)
                            {
                                if (zamek.poziceZamku == policko)
                                {
                                    kp.DrawImage(zamek.obrazekZamku, zamek.poziceZamku.x, zamek.poziceZamku.y, sirkaPolicka, sirkaPolicka);
                                }
                            }
                            break;
                        case StavPolicka.Klic:
                            foreach (Klic klic in mapa.klice)
                            {
                                kp.DrawImage(klic.obrazekKlice, klic.poziceKlice.x, klic.poziceKlice.y, sirkaPolicka, sirkaPolicka);
                            }
                            break;
                        case StavPolicka.PoziceNepritele:
                            foreach (Nepritel nepritel in mapa.nepratele)
                            {
                                kp.DrawImage(nepritel.obrazekNepritele, nepritel.poziceNepritele.x, nepritel.poziceNepritele.y, sirkaPolicka, sirkaPolicka);
                            }
                            break;
                        case StavPolicka.Bod:
                                kp.DrawImage(Bod.obrazekBod, policko.x, policko.y, sirkaPolicka, sirkaPolicka);                            
                            break;
                        case StavPolicka.Cil:
                            kp.DrawImage(Image.FromFile("../../obrazky/poklad.png"), policko.x, policko.y, sirkaPolicka, sirkaPolicka);
                            break;
                    }
                }

                //vykresleni stavoveho radku
                int souradniceRadek = sirkaPolicka;
                //zivoty
                int zivoty = StavovyRadek.pocetZivotu;
                for (int i = 0; i < zivoty; i++)
                {
                    kp.DrawImage(stavovyRadek.obrazekZivot, souradniceRadek, sirkaPolicka / 2, sirkaPolicka, sirkaPolicka);
                    souradniceRadek += sirkaPolicka;
                }
                //klice  
                souradniceRadek = 6 * sirkaPolicka;
                foreach (Klic klic in StavovyRadek.vlastneneKlice)
                {
                    kp.DrawImage(klic.obrazekKlice, souradniceRadek, sirkaPolicka / 2, sirkaPolicka, sirkaPolicka);
                    souradniceRadek += sirkaPolicka;
                }
                //body
                souradniceRadek = 10 * sirkaPolicka;
                SolidBrush barvaTextu = new SolidBrush(Color.Black);
                FontFamily fontFamily = new FontFamily("Arial");
                Font font = new Font(fontFamily, 26, FontStyle.Regular, GraphicsUnit.Pixel);
                string bodyText = String.Format("Body: {0}", StavovyRadek.pocetBodu);
                kp.DrawString(bodyText, font, barvaTextu, souradniceRadek, sirkaPolicka / 2);
                //hodiny
                souradniceRadek = 22 * sirkaPolicka;
                kp.DrawString(hodiny, font, barvaTextu, souradniceRadek, sirkaPolicka / 2);
             
            }
            
        }

        private void okno_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //ovladani panacka
                case Keys.Up:
                case Keys.W:
                    if (pauza == false)
                    {
                        aktivniPolicko = postavicka.PohybPostavicky(mapa, aktivniPolicko, SmerPohybu.Nahoru);
                        //pokud, panacek stoupne na policko pod ostny, dojde oznaceni daneho ostnu jako "pohybujici se osten" (resp. pohybOstnu = true)
                        mapa = Ostny.ZacatekPohybuOstnu(mapa, aktivniPolicko);
                    }                
                    break;
                case Keys.Down:
                case Keys.S:
                    if (pauza == false)
                    {
                        aktivniPolicko = postavicka.PohybPostavicky(mapa, aktivniPolicko, SmerPohybu.Dolu);
                        //pokud, panacek stoupne na policko pod ostny, dojde oznaceni daneho ostnu jako "pohybujici se osten" (resp. pohybOstnu = true)
                        mapa = Ostny.ZacatekPohybuOstnu(mapa, aktivniPolicko);
                    }
                    break;
                case Keys.Right:
                case Keys.D:
                    if (pauza == false)
                    {
                        aktivniPolicko = postavicka.PohybPostavicky(mapa, aktivniPolicko, SmerPohybu.Doprava);
                        //pokud, panacek stoupne na policko pod ostny, dojde oznaceni daneho ostnu jako "pohybujici se osten" (resp. pohybOstnu = true)
                        mapa = Ostny.ZacatekPohybuOstnu(mapa, aktivniPolicko);

                        //umoznuje prechod na mapu 2
                        if (aktivniPolicko.stavPolicka == StavPolicka.Prechod)
                        {
                            mapa = new Mapa("../../mapa/mapa2_data.csv", sirkaPolicka, 2);
                            this.BackgroundImage = mapa.mapaObrazek;
                            aktivniPolicko = Postavicka.postavickaStart(mapa, aktivniPolicko);
                        }
                    }
                    break;
                case Keys.Left:
                case Keys.A:
                    if (pauza == false)
                    {
                        aktivniPolicko = postavicka.PohybPostavicky(mapa, aktivniPolicko, SmerPohybu.Doleva);
                        //pokud, panacek stoupne na policko pod ostny, dojde oznaceni daneho ostnu jako "pohybujici se osten" (resp. pohybOstnu = true)
                        mapa = Ostny.ZacatekPohybuOstnu(mapa, aktivniPolicko);
                    }
                    break;
                //pauza
                case Keys.P:
                    if (pauza == false)
                    {
                        casovac.Enabled = false;
                        pauza = true;
                    }
                    else
                    {
                        pauza = false;
                        casovac.Enabled = true;
                    }                                  
                    break;
                //restart hry
                case Keys.R:
                    mapa = new Mapa("../../mapa/mapa1_data.csv", sirkaPolicka, 1);
                    Spusteni(obtiznost);
                    break;
                //navrat do hlavni nabidky
                case Keys.Escape:
                    Application.Restart();
                    break;
                default:
                    break;
            }
            Refresh();
        }

        private void okno_Load(object sender, EventArgs e)
        {

        }

        private void casovac_Tick_1(object sender, EventArgs e)
        {
            mapa = Ostny.PohybOstnu(mapa, aktivniPolicko); //k pohybu ostnu dochazi kazdy tick (resp. 250ms)
            mapa = Nepritel.PohybNepritel(mapa, aktivniPolicko, rychlost); //k pohybu nepritele dochazi kazdy 2. nebo 4. tick (zalezi na rychlosti)
            NastaveniHodin();
            Refresh();
        }
        private void NastaveniHodin() 
        {
            //umoznuje zobrazit ubehly cas
            sekunda += casovac.Interval;
            NastaveniRychlosti(sekunda);
            if (sekunda == 1000)
            {
                sekunda = 0;
                sekundy++;
                if (sekundy == 60)
                {
                    minuty++;
                    sekundy = 0;
                }
                if (sekundy < 10)
                {
                    nulaSekundy = "0";
                }
                else
                {
                    nulaSekundy = "";
                }
                if (minuty < 10)
                {
                    nulaMinuty = "0";
                }
                else
                {
                    nulaMinuty = "";
                }
                hodiny = String.Format("{0}{1}:{2}{3}", nulaMinuty, minuty, nulaSekundy, sekundy);
            }
        }

        private void NastaveniRychlosti(int sekunda) 
        {
            //nastavuje rychlost po 500ms se pohybuje rychli nepratele a po 1s pomali
            switch (sekunda / 250)
            {
                case 2:
                    rychlost = Rychlost.Rychly;
                    break;
                case 4:
                    rychlost = Rychlost.Pomaly;
                    break;
            }
        }
    }
}
