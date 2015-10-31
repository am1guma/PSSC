using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSC
{
    class FisaCazare
    {
        public FisaCazare(string nume, string prenume, string facultate, int an, double medie)
        {
            this.Nume = nume;
            this.Prenume = prenume;
            this.Facultate = facultate;
            this.An = an;
            this.Medie = medie;
        }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Facultate { get; set; }
        public int An { get; set; }
        public double Medie { get; set; }
    }
}
