using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMB.Logic.Models
{
    internal class LoadCase
    {
        public string? Name { get; set; }
        public int LoadStatesQuantity { get; set; }
        public double TimeShare { get; set; }
    }
}
