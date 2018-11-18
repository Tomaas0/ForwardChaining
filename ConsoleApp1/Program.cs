using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GDB db = new GDB(true);

            #region Vykdymas
            if (db.InitFaktai.Contains(db.Tikslas))
            {

            }
            while (!db.VisiFaktai.Contains(db.Tikslas))
            {
                bool done = false;
                foreach(Projekcija p in db.Projekcijos)
                {
                    if(p.Flag == 0 && !done)
                    {
                        if (db.VisiFaktai.Contains(p.Rezultatas))
                        {
                            p.Flag = 2;
                        }
                        bool tinka = true;
                        foreach (char reikalavimas in p.Reikalavimai)
                        {
                            if (!db.VisiFaktai.Contains(reikalavimas))
                            {
                                tinka = false;
                            }
                        }
                        if (tinka)
                        {
                            db.Faktai.Add(p.Rezultatas);
                            p.Flag = 1;
                            db.Kelias.Add(p);
                            done = true;
                        }
                    }
                }
            }
            #endregion
        }
    }
}
