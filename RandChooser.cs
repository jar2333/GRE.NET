using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphRewriteEngine
{
    public class RandChooser : IChooser {

        Random r;
        public Int32 Seed {get; set;}

        public RandChooser(Int32 seed) {
            this.r = new Random(seed);
            this.Seed = seed;
        }

        public int Choose(Dictionary<int, Rule> rules) {
            int[] indeces = rules.Keys.ToArray();
            int k = r.Next(0, indeces.Length);
            return indeces[k];
        }
        
    }
}