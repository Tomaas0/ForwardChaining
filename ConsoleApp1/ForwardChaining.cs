using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    public static class ForwardChaining
    {
        public static void Run(GDB db)
        {
            #region Protokolas 1 dalis
            string output;
            StreamWriter file = new StreamWriter(String.Format("{0} protokolas.txt", db.TestName), false);
            file.WriteLine("1 DALIS. Duomenys");
            file.WriteLine("");
            file.WriteLine("  1) Taisyklės");
            foreach (Projekcija p in db.Projekcijos)
            {
                file.WriteLine(String.Format("    {0}", p));
            }
            file.WriteLine("");
            file.WriteLine("  2) Faktai");
            output = String.Format("    {0}", db.InitFaktai.ElementAt(0));
            for (int i = 1; i < db.InitFaktai.Count; i++)
            {
                output += String.Format(", {0}", db.InitFaktai.ElementAt(i));
            }
            file.WriteLine(output);
            file.WriteLine("");
            file.WriteLine("  2) Tikslas");
            file.WriteLine(String.Format("    {0}", db.Tikslas));
            file.WriteLine("");
            #endregion

            #region Vykdymas
            if (db.InitFaktai.Contains(db.Tikslas))
            {
                file.WriteLine("3 DALIS. Rezultatai");
                file.WriteLine(String.Format("  Tikslas {0} tarp faktų. Kelias tuščias.", db.Tikslas));
                file.Close();
                return;
            }
            file.WriteLine("2 DALIS. Vykdymas");
            file.WriteLine("");
            int iCount = 0; //Iteraciju skaitliukas
            while (!db.VisiFaktai.Contains(db.Tikslas))
            {
                iCount++;
                file.WriteLine(String.Format("  {0} ITERACIJA", iCount.ToString()));

                bool done = false;
                foreach (Projekcija p in db.Projekcijos)
                {
                    if (p.Flag == 0 && !done)
                    {
                        if (db.VisiFaktai.Contains(p.Rezultatas))
                        {
                            p.Flag = 2;
                            file.WriteLine(String.Format("    {0} netaikome, nes konsekventas faktuose. Pakeliame flag2", p));
                        }
                        else
                        {
                            bool tinka = true;
                            String trukstamas = "";
                            foreach (char reikalavimas in p.Reikalavimai)
                            {
                                if (!db.VisiFaktai.Contains(reikalavimas))
                                {
                                    tinka = false;
                                    if (trukstamas == "")
                                    {
                                        trukstamas += reikalavimas;
                                    }
                                    else
                                    {
                                        trukstamas += ", " + reikalavimas;
                                    }
                                }
                            }
                            if (tinka)
                            {
                                db.Faktai.Add(p.Rezultatas);
                                p.Flag = 1;
                                db.Kelias.Add(p);
                                done = true;
                                file.WriteLine(String.Format("    {0} taikome. Pakeliame flag1. Faktai {1}.", p, db.FaktaiToString));
                            }
                            else
                            {
                                file.WriteLine(String.Format("    {0} netaikome, nes trūksta {1}.", p, trukstamas));
                            }
                        }
                    }
                    else if (p.Flag != 0 && !done)
                    {
                        file.WriteLine(String.Format("    {0} praleidžiame, nes pakelta flag{1}.", p, p.Flag.ToString()));
                    }
                }
                if(db.VisiFaktai.Contains(db.Tikslas)) file.WriteLine("    Tikslas gautas.");
                if (!done)
                {
                    file.WriteLine("    Tikslas nerastas.");
                    file.WriteLine("");
                    file.WriteLine("3 DALIS. Rezultatai");
                    file.WriteLine(String.Format("  Tikslas {0} nerastas.", db.Tikslas));
                    file.Close();
                    return;
                }
                file.WriteLine("");
            }
            #endregion

            #region Rezultatai
            file.WriteLine("3 DALIS. Rezultatai");
            file.WriteLine(String.Format("  Tikslas {0} išvestas.", db.Tikslas));
            output = "R" + db.Kelias.ElementAt(0).Index.ToString();
            for (int i = 1; i < db.Kelias.Count; i++)
            {
                output += String.Format(", R{0}", db.Kelias.ElementAt(i).Index.ToString());
            }
            file.WriteLine(String.Format("  Kelias: {0}.", output));
            file.Close();
            #endregion
        }
    }
}
