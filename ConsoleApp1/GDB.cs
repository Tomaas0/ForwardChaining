using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Projekcija
    {
        public List<char> Reikalavimai { get; set; }
        public char Rezultatas { get; set; }
        public int Flag { get; set; }
        public Projekcija()
        {
            Reikalavimai = new List<char>();
            Flag = 0;
        }
    }
    class GDB
    {
        public List<Projekcija> Projekcijos { get; set; }
        public List<char> InitFaktai { get; set; }
        public List<char> Faktai { get; set; }
        public List<char> VisiFaktai { get { List<char> x = new List<char>();
                x.AddRange(InitFaktai);
                x.AddRange(Faktai);
                return x;
            } }
        public char Tikslas { get; set; }

        public List<Projekcija> Kelias { get; set; }
        public GDB()
        {
            Projekcijos = new List<Projekcija>();
            InitFaktai = new List<char>();
            Faktai = new List<char>();
            Kelias = new List<Projekcija>();
        }
        public GDB(bool x)
        {
            Projekcijos = new List<Projekcija>();

            Projekcija p = new Projekcija();
            p.Reikalavimai.Add('F');
            p.Reikalavimai.Add('B');
            p.Rezultatas = 'Z';
            Projekcijos.Add(p);

            p = new Projekcija();
            p.Reikalavimai.Add('C');
            p.Reikalavimai.Add('D');
            p.Rezultatas = 'F';
            Projekcijos.Add(p);

            p = new Projekcija();
            p.Reikalavimai.Add('A');
            p.Rezultatas = 'D';
            Projekcijos.Add(p);

            InitFaktai = new List<char>();
            InitFaktai.Add('A');
            InitFaktai.Add('B');
            InitFaktai.Add('C');

            Faktai = new List<char>();
            Tikslas = 'Z';
            Kelias = new List<Projekcija>();
        }
    }
}
