using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Grammar {

        public Rule[] rules;
        
        public Grammar(List<Rule> r) {
            this.rules = r.ToArray();
        }

    }
}