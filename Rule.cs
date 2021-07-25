using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Rule {
        public UndirectedGraph<Node, LEdge> LHS;
        public UndirectedGraph<Node, LEdge> RHS;
        public UndirectedGraph<Node, LEdge> I;

        public Morphism L;
        public Morphism R;

        public Rule(UndirectedGraph<Node, LEdge> LHS, UndirectedGraph<Node, LEdge> RHS, 
                    UndirectedGraph<Node, LEdge> I, Morphism L, Morphism R) {
            this.LHS = LHS;
            this.RHS = RHS;
            this.I = I;
            this.L = L;
            this.R = R;

            
        }




    }
}
