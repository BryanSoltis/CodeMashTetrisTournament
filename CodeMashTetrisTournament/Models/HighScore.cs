using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMashTetrisTournament.Models
{
    public class HighScore
    {
        public string Id { get; set; }
        public string Rank { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }
    }
}
