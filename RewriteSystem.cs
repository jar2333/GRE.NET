using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class RewriteSystem{ //grammar, axiom, controlled derivation step by step or until termination condition

        //A "Picker" interface for classes that choose the next applicable grammar rule to use (can be stochastic!)

        private Grammar grammar;

        private UndirectedGraph<Node, LEdge> axiom;

        public UndirectedGraph<Node, LEdge> generated;

        public RewriteSystem(Grammar G, UndirectedGraph<Node, LEdge> a) {
            this.grammar = G;
            this.axiom = a;
        }

        public int Step() { //a derivation step, returns index of applied rule
            throw new NotImplementedException();
        }

    }
}