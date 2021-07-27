using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Rule { //This whole representation might be entirely unnecesary, we'll see!
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

            AddRuleTags();
        }

        private void AddRuleTags() {
            //Tag as obsolete
            foreach (Node v in LHS.Vertices) {
                if (!L.Vm.Values().Contains(v)) {
                    v.Tag = "o";
                }
                else {
                    v.Tag = "i";
                }
            }
            foreach (LEdge e in LHS.Edges) {
                if (!L.Em.Values().Contains(e)) {
                    e.Tag = "o";
                }
                else {
                    e.Tag = "i";
                }
            }
            //Tag as fresh
            foreach (Node v in RHS.Vertices) {
                if (!L.Vm.Values().Contains(v)) {
                    v.Tag = "f";
                }
                else {
                    v.Tag = "i";
                }
            }
            foreach (LEdge e in RHS.Edges) {
                if (!L.Em.Values().Contains(e)) {
                    e.Tag = "f";
                }
                else {
                    e.Tag = "i";
                }
            }
        } 




    }
}
