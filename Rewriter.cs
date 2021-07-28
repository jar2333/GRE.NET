using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Rewriter{ //make an abstract class?

        //grammar, axiom, controlled derivation step by step or until termination condition
        //Assumes DPO approach, split class for SPO later?
        //A "Picker" interface for classes that choose the next applicable grammar rule to use (can be stochastic!)
        public Grammar grammar;

        public IMatcher mapper; //the mapper should determine for itself how the morphism returned by "Find" is determined (stochastically, lazily, etc)

        public IChooser chooser;

        public UndirectedGraph<Node, LEdge> axiom;

        public UndirectedGraph<Node, LEdge> generated;

        public Rewriter(Grammar G, IMatcher m, IChooser c, UndirectedGraph<Node, LEdge> a) {
            this.grammar = G;
            this.axiom = a;
            this.mapper = m;
            this.chooser = c;
            Reset();
        }

        public void Reset() {
            this.generated = this.axiom.Clone();
        }

        public UndirectedGraph<Node, LEdge> GetGraph() {
            return this.generated;
        }

        public bool IsApplicable(Rule r, Morphism match) { //DPO conditions for existence of context
            //Condition 1
            foreach (Node v in r.LHS.Vertices) {
                IEnumerable<LEdge> adjacent = this.generated.AdjacentEdges(match.Vm.M[v]);
                adjacent = adjacent.Where(e => !match.Em.Values().Contains(e));
                if (adjacent.Any() && !r.IsInterface(v)) { //should be impossible if applicable
                    return false;
                }
            }
            //Condition 2 (only applies when match is not injective, so ignored for now)
            return true; 
        }

        public int Step(int ruleIndex = -1) { //a derivation step, returns index of applied rule (perhaps tuple with index AND previous graph?)
            //iterate over each Grammar rule and store applicable ones:
            var applicableRules = new Dictionary<int, Rule>();
            var applicableMatches = new Dictionary<int, Morphism>();
            for (int i = 0; i < grammar.Size; i++) {
                Rule r = grammar.GetRule(i);
                Morphism match = mapper.Find(r.LHS, this.generated);
                if (match != null && IsApplicable(r, match)) {
                    applicableRules[i] = r;
                    applicableMatches[i] = match;
                }
            }
            
            //cases:
            int index;
            if (!applicableRules.Any()) {
                return -3; //no applicable rules
            } 
            else if (!applicableMatches.Any()) {
                return -2; //no matches
            }
            else if (ruleIndex >= grammar.Size || (ruleIndex > 0 && (!applicableRules.ContainsKey(ruleIndex) || !applicableMatches.ContainsKey(ruleIndex)))) {
                return -1; //invalid ruleIndex
            }
            else if (ruleIndex < 0) { //using Chooser, pick a rule. Default
                index = chooser.Choose(applicableRules); //this is so ugly but whatever
            }
            else {
                index = ruleIndex; //valid ruleIndex
            }
            Rule appliedRule = applicableRules[index];
            Morphism appliedMatch = applicableMatches[index];

            //Console.WriteLine(appliedRule.Debug());
            //Console.WriteLine($"applied match {appliedMatch}");

            //Actually rewrite the graph (kinda hacky, excuse my implementation)
            //Remove m(e), m(v) for each obsolete e, v. Store indeces in stack!
            int maxIndex = this.generated.VertexCount - 1; //indexing [0, ..., |V| - 1]
            var removedIndeces = new Stack<int>();
            foreach (LEdge e in appliedRule.LHS.Edges) {
                if (!appliedRule.IsInterface(e)) {
                    //Console.WriteLine($"Removed {appliedMatch.Em.M[e]}");
                    generated.RemoveEdge(appliedMatch.Em.M[e]);
                }
            }
            foreach (Node v in appliedRule.LHS.Vertices) {
                if (!appliedRule.IsInterface(v)) {
                    removedIndeces.Push(v.Index);
                    //Console.WriteLine($"Removed {appliedMatch.Vm.M[v]}");
                    generated.RemoveVertex(appliedMatch.Vm.M[v]);
                }
            }
            //Search RHS:
            //instantiate a morphism for fresh nodes/edges -> new nodes/edges to be inserted to host
            //for each fresh edge: 
            //check source/target: 
                //if "interface" node, use match morphism.
                //if "fresh" 
                    //if not in fresh morphism, add to it, using new indeces (either from stack, or new and increase max)
                    //use fresh morphism to add define new Source/Target
                //add edge with new (Source, Target)
            //for each fresh vertex (this whole process might be unnecessary for connected graphs)
                //if not already added to fresh morphism, add to it and then host
            NodeMapping freshMapping = new NodeMapping(); //Make a node mapping
            foreach(LEdge e in appliedRule.RHS.Edges) {
                if (appliedRule.IsInterface(e)) { //if an edge is interface, so must its nodes be. (interface graph cannot have dangling edges)
                    Node s = appliedRule.R.Vm.M.FirstOrDefault(x => x.Value.Equals(e.Source)).Key;
                    s = appliedRule.L.Vm.M[s];
                    s = appliedMatch.Vm.M[s]; //should find a way to seal this in a method

                    Node t = appliedRule.R.Vm.M.FirstOrDefault(x => x.Value.Equals(e.Target)).Key;
                    t = appliedRule.L.Vm.M[t];
                    t = appliedMatch.Vm.M[t];

                    LEdge toAdd = new LEdge(s, t, e.Tag);
                    if (!this.generated.ContainsEdge(toAdd)) { //necessary?
                        this.generated.AddEdge(toAdd);
                    }
                }
                else { //optimize this later (interface or not)
                    //check Source
                    Node newSource;
                    if(appliedRule.IsInterface(e.Source)) { //optimize this later (interface or not)
                        Node q = appliedRule.R.Vm.M.FirstOrDefault(x => x.Value.Equals(e.Source)).Key; //optimize later! (reverse direction?)
                        q = appliedRule.L.Vm.M[q];
                        q = appliedMatch.Vm.M[q];
                        newSource = q; //unnecessary clone?
                        //Console.WriteLine(newSource.ToString());
                    }
                    else {
                        //Console.WriteLine($"Considered not interface: {e.Source}");
                        if (!freshMapping.M.ContainsKey(e.Source)) {
                            int newIndex;
                            if (removedIndeces.Count > 0) {
                                newIndex = removedIndeces.Pop();
                            }
                            else {
                                maxIndex++;
                                newIndex = maxIndex;
                            }
                            Node toAdd = new Node(newIndex, e.Source.Tag);
                            freshMapping.M[e.Source] = toAdd; //Ah, deal with labels!
                            this.generated.AddVertex(toAdd);
                        }
                        newSource = freshMapping.M[e.Source];
                    }
                    //check Target (consider making LEdge indexable...)
                    Node newTarget;
                    if(appliedRule.IsInterface(e.Target)) { //optimize this later (interface or not)
                        Node q = appliedRule.R.Vm.M.FirstOrDefault(x => x.Value.Equals(e.Target)).Key; //optimize later! 
                        q = appliedRule.L.Vm.M[q];              //(reverse direction possible due to injectivity assumption)
                        q = appliedMatch.Vm.M[q];
                        newTarget = q; //if IsInterface, mapping already exists
                        //Console.WriteLine(newTarget.ToString());
                    }
                    else {
                        //Console.WriteLine($"Considered not interface: {e.Target}");
                        if (!freshMapping.M.ContainsKey(e.Target)) {
                            int newIndex;
                            if (removedIndeces.Count > 0) {
                                newIndex = removedIndeces.Pop();
                            }
                            else {
                                maxIndex++;
                                newIndex = maxIndex;
                            }
                            Node toAdd = new Node(newIndex, e.Target.Tag);
                            freshMapping.M[e.Target] = toAdd; //Ah, deal with labels!
                            this.generated.AddVertex(toAdd);
                        }
                        newTarget = freshMapping.M[e.Target];
                    }

                    //add edge constructed from new (Source, Target)
                    LEdge newEdge = new LEdge(newSource, newTarget, e.Tag); //add label
                    if (!this.generated.ContainsEdge(newEdge)) {  //contains check unnecessary?
                        //Console.WriteLine($"adding: {newEdge}");
                        this.generated.AddEdge(newEdge); 
                    }
                }
            }
            //The final vertex pass to add fresh verteces
            
            foreach (Node v in appliedRule.RHS.Vertices) {
                if (!appliedRule.IsInterface(v)) { //add connectivity check
                    Node newNode;
                    if (!freshMapping.M.ContainsKey(v)) {
                        int newIndex;
                        if (removedIndeces.Count > 0) {
                            newIndex = removedIndeces.Pop();
                        }
                        else {
                            maxIndex++;
                            newIndex = maxIndex;
                        }
                        freshMapping.M[v] = new Node(newIndex, v.Tag); //Ah, deal with labels!
                    }
                    newNode = freshMapping.M[v];

                    //add node
                    if (!this.generated.ContainsVertex(newNode)) { //contains check unnecessary?
                        this.generated.AddVertex(newNode); 
                    }
                }
            }
            
            return index;
        }

        public bool Recreate(IEnumerable<int> steps) {
            //skips picker procedure, recreates specific derivation (catch invalid sequences)
            foreach (int i in steps) {
                int index = this.Step(i);
                if (index < 0) {
                    Reset();
                    return false; //recreation failed
                }
            }
            return true;
        }


    }
}