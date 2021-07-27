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

        public Dictionary<Node, bool> interfaceNodes;
        public Dictionary<LEdge, bool> interfaceEdges;

        public Rule(UndirectedGraph<Node, LEdge> LHS, UndirectedGraph<Node, LEdge> RHS, 
                    UndirectedGraph<Node, LEdge> I, Morphism L, Morphism R) {
            this.LHS = LHS;
            this.RHS = RHS;
            this.I = I;
            this.L = L;
            this.R = R;

            interfaceNodes = new Dictionary<Node, bool>();
            interfaceEdges = new Dictionary<LEdge, bool>();

            CacheInterface();
        }

        public bool IsInterface(Node v) {
            return interfaceNodes[v];
        }

        public bool IsInterface(LEdge e) {
            return interfaceEdges[e];
        }

        private void CacheInterface() {
            //Tag as obsolete
            foreach (Node v in LHS.Vertices) {
                if (L.Vm.Values().Contains(v)) {
                    interfaceNodes[v] = true;
                }
                else {
                    interfaceNodes[v] = false;
                }
            }
            foreach (LEdge e in LHS.Edges) {
                if (L.Em.Values().Contains(e)) {
                    interfaceEdges[e] = true;
                }
                else {
                    interfaceEdges[e] = false;
                }
            }
            //Tag as fresh
            foreach (Node v in RHS.Vertices) {
                if (L.Vm.Values().Contains(v)) {
                    interfaceNodes[v] = true;
                }
                else {
                    interfaceNodes[v] = false;
                }
            }
            foreach (LEdge e in RHS.Edges) {
                if (!L.Em.Values().Contains(e)) {
                    interfaceEdges[e] = true;
                }
                else {
                    interfaceEdges[e] = false;
                }
            }
                
        }
    }
}
