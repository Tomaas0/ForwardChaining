using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace ConsoleApp1
{
    class Projekcija
    {
        public int Index { get; set; }
        public List<char> Reikalavimai { get; set; }
        public char Rezultatas { get; set; }
        public int Flag { get; set; }
        public Projekcija()
        {
            Reikalavimai = new List<char>();
            Flag = 0;
        }
        public override string ToString()
        {
            string output = "R";
            output += this.Index.ToString();
            output += String.Format(":{0}", this.Reikalavimai.ElementAt(0));
            for (int i = 1; i < this.Reikalavimai.Count; i++)
            {
                output += String.Format(",{0}", this.Reikalavimai.ElementAt(i));
            }
            output += "->";
            output += this.Rezultatas;
            return output;
        }
    }
    class GDB
    {
        public String TestName { get; set; }
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
        public String FaktaiToString { get { return String.Format("{0} ir {1}", CharListToString(InitFaktai), CharListToString(Faktai)); } }
        public GDB()
        {
            Projekcijos = new List<Projekcija>();
            InitFaktai = new List<char>();
            Faktai = new List<char>();
            Kelias = new List<Projekcija>();
            StreamReader file = new StreamReader(ConfigurationManager.AppSettings["InputFileName"]);
            TestName = file.ReadLine();
            file.ReadLine();
            string line = file.ReadLine();
            int projekcijuCount = 0;
            while (line != "")
            {
                projekcijuCount++;
                Projekcija p = new Projekcija();
                p.Index = projekcijuCount;
                line = line.Split('\t')[0];
                p.Rezultatas = line.ElementAt(0);
                for (int i = 1; i < line.Length; i++)
                {
                    p.Reikalavimai.Add(line.ElementAt(i));
                }
                Projekcijos.Add(p);
                line = file.ReadLine();
            }
            line = file.ReadLine();
            line = file.ReadLine();
            foreach (Char c in line)
            {
                InitFaktai.Add(c);
            }
            line = file.ReadLine();
            line = file.ReadLine();
            line = file.ReadLine();
            Tikslas = line.ElementAt(0);
            file.Close();
        }
        public GDB(bool x)
        {
            TestName = "test";

            Projekcijos = new List<Projekcija>();

            Projekcija p = new Projekcija();
            p.Index = 1;
            p.Reikalavimai.Add('F');
            p.Reikalavimai.Add('B');
            p.Rezultatas = 'Z';
            Projekcijos.Add(p);

            p = new Projekcija();
            p.Index = 2;
            p.Reikalavimai.Add('C');
            p.Reikalavimai.Add('D');
            p.Rezultatas = 'F';
            Projekcijos.Add(p);

            p = new Projekcija();
            p.Index = 3;
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

        private String CharListToString(List<char> list)
        {
            string output;
            if (list.Count > 0)
            {
                output = list.ElementAt(0).ToString();
                for (int i = 1; i < list.Count; i++)
                {
                    output += String.Format(", {0}", list.ElementAt(i));
                }
            }
            else output = "";
            return output;
        }
    }
}
