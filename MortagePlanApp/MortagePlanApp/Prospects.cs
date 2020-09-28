using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortagePlanApp
{
    public class Prospects
    {
        public string Name { get; set; }
        public double Total { get; set; }
        public double Interest { get; set; }
        public int Years { get; set; }

        public Prospects(string Name, double Total, double Interest, int Years)
        {
            this.Name = Name;
            this.Total = Total;
            this.Interest = Interest;
            this.Years = Years;

        }
    }
}
