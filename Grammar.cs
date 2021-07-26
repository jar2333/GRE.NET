using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Grammar {

        public Rule[] rules;
        public int Size;
        
        public Grammar(List<Rule> r) {
            this.rules = r.ToArray();
            this.Size = this.rules.Length;
        }

        public Rule GetRule(int i) {
            if (0 <= i && i <= this.rules.Length) {
                return rules[i];
            }
            return null;
        }

    }
}