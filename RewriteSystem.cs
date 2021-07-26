using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class RewriteSystem{ //make an abstract class?

        //grammar, axiom, controlled derivation step by step or until termination condition
        //Assumes DPO approach, split class for SPO later?
        //A "Picker" interface for classes that choose the next applicable grammar rule to use (can be stochastic!)
        public Grammar grammar;

        public IMapper mapper; //the mapper should determine for itself how the morphism returned by "Find" is determined (stochastically, lazily, etc)

        public IChooser chooser;

        public UndirectedGraph<Node, LEdge> axiom;

        public UndirectedGraph<Node, LEdge> generated;

        public RewriteSystem(Grammar G, IMapper m, IChooser c, UndirectedGraph<Node, LEdge> a) {
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
                IEnumerable<LEdge> adjacent = this.generated.AdjacentEdges(v);
                adjacent = adjacent.Where(e => !match.Em.Values().Contains(e));
                if (adjacent.Any() && v.Tag.Equals("o")) { //should be impossible if applicable
                    return false;
                }
            }
            //Condition 2 (only applies when match is not injective, so ignored for now)
            return true; 
        }

        public int Step() { //a derivation step, returns index of applied rule (perhaps tuple with index AND previous graph?)
            //iterate over each Grammar rule and store applicable ones:
            Rule[] applicableRules = new Rule[grammar.Size];
            Array.Clear(applicableRules, 0, grammar.Size);
            var applicableMatches = new Dictionary<int, Morphism>();
            for (int i = 0; i < grammar.Size; i++) {
                Rule r = grammar.GetRule(i);
                Morphism match = mapper.Find(r.LHS, this.generated);
                if (match != null && IsApplicable(r, match)) {
                    applicableRules[i] = r;
                    applicableMatches[i] = match;
                }
            }
            //using Chooser, pick a rule
            int index = chooser.Choose(applicableRules); //this is so ugly but whatever
            Rule appliedRule = applicableRules[index];
            Morphism appliedMatch = applicableMatches[index];

            //Actually rewrite the graph
            throw new NotImplementedException();
        }

        public void Recreate(IEnumerable<int> steps) {
            throw new NotImplementedException(); //skips picker procedure, recreates specific derivation (catch invalid sequences)
        }


    }
}