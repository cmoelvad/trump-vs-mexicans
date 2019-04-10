using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class ScoreObject : IComparable<ScoreObject>
    {
        public int score { get; set; }
        public string name { get; set; }

        public int CompareTo(ScoreObject other)
        {
            return this.score - other.score;
        }
    }
}
