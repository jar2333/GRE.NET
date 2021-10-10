using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Rule { //This whole representation might be entirely unnecesary, we'll see!

        //Change to fit the following scheme (as per that one paper...):
        //LHS Array: Application conditions evaluation.
            // ∃ a match of LHS[0] and
            //!∃ a match of LHS[1] and
            //!∃ a match of LHS[2] and
            //...
            //!∃ a match of LHS[LHS.Length-1]
        //The ith condition is valid if the (i+1)th condition is true.
        //The match of LHS[i] shares interface nodes/edges with the matches of LHS[j] for all j > i.

        //RHS Array: Different replacement choices
        //Questions: 
            //Should probabilities be given at the level of rule or grammar?
                //That is, ProbabilisticRule subclass vs ProbabilisticGrammar subclass
                    //As defined by Mosbah, each different RHS for a given LHS is assinged a probability.
                    //This is easier to keep track of by encapsulating the probabilities in parallel to the RHSs
            //At what level does the IChooser operate?
                //Choose between rules vs choose between RHS _in_ a rule
                //Proposed implementation:
                    //IChoosers work on both layers, by using a pair of indeces:
                        //Choose among rules (assumed to be applicable)
                            //Self-contained choosing procedure
                            //for example, discrete uniform random sampling the index i
                        //Once rule is chosen, choose among RHS 
                            //Self-contained choosing procedure
                            //for example, production weight discrete distribution sampling the index j
                        //Return (i, j)
                            //This is used at the Rewriter layer.

        //Add a new methods and properties: 
            //bool IsApplicable(Graph host, IMatcher m)
                //Checks if application conditions clear
            //Graph GetProduction(int i)
            //For ProbabilisticRule class:
            //double[] weights = new double[RHS.Length];
            //double[] GetWeights()
            //void InitializeUniform()
        public BidirectionalGraph<Node, LEdge> LHS;
        public BidirectionalGraph<Node, LEdge> RHS;
        public BidirectionalGraph<Node, LEdge> I;

        public Morphism L;
        public Morphism R;

        public Dictionary<Node, bool> interfaceNodes;
        public Dictionary<LEdge, bool> interfaceEdges;

        public Rule(BidirectionalGraph<Node, LEdge> LHS, BidirectionalGraph<Node, LEdge> RHS, 
                    BidirectionalGraph<Node, LEdge> I, Morphism L, Morphism R) {
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

        public string Debug() {
            return $"IEs: {interfaceEdges.Where(x => x.Value).Count()}, IVs: {interfaceNodes.Where(x => x.Value).Count()}";
        }

        private void CacheInterface() {
            //Cache as obsolete
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
            //Cache as fresh
            foreach (Node v in RHS.Vertices) {
                if (R.Vm.Values().Contains(v)) {
                    interfaceNodes[v] = true;
                }
                else {
                    interfaceNodes[v] = false;
                }
            }
            foreach (LEdge e in RHS.Edges) {
                if (R.Em.Values().Contains(e)) {
                    interfaceEdges[e] = true;
                }
                else {
                    interfaceEdges[e] = false;
                }
            }
                
        }
    }
}
