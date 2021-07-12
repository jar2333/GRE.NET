using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class VF2Ind : VF2Procedure {

        public VF2Ind() : base() {}

        //Cons(m) iff m satisfies reqs of PT by considering
        //G(D(m)) ⊆ pattern and G(R(m)) ⊆ host (induced subgraphs)
        public override bool Cons(Mapping m, Node[] p)
        {
            //L(u) == L(v) [quick discard if unequal]
            if (!p[0].IsEquivalent(p[1])) {
                return false;
            }

            //G(D(m))
            ICollection<Node> D = m.M.Keys;
            IEnumerable<LEdge> E1 = pattern.Edges.Where(e => D.Contains(e.Source) && D.Contains(e.Target));
            //G(R(m))
            ICollection<Node> R = m.M.Values;
            IEnumerable<LEdge> E2 = host.Edges.Where(e => R.Contains(e.Source) && R.Contains(e.Target));

            //Cons(m)
            foreach (Node u in D) {
                if (!u.IsEquivalent(m.M[u])) { //1st check
                    return false;
                }
                foreach (Node v in D) { //2rd check
                    bool A = E1.Contains(new LEdge(u, v)); //Assuming the Equals works
                    bool B = E2.Contains(new LEdge(m.M[u], m.M[v]));
                    if ((A && !B) || (!A && B)) {
                        return false;
                    }
                }
            }

            foreach (Node u in pattern.AdjacentVertices(p[0]).Intersect(D)) { //Not every mapping fails here
                if (!host.Edges.Contains(new LEdge(p[1], m.M[u]))) {
                    return false; 
                }
            }
            //the induced subgraphs are only useful for Cons(m)
            //For the following checks, we use the global graphs (global vertex/edge sets, as delineated in the paper)
            foreach (Node v in host.AdjacentVertices(p[1]).Intersect(R)) { 
                if (!pattern.Edges.Contains(new LEdge(p[0], m.M.FirstOrDefault(x => x.Value.Equals(v)).Key))) { //OH RIGHT, FirstOrDefault is checking PAIRS
                    return false; 
                }
            }

            return true;
        }

        public override bool Cut(Mapping m, Node[] p)
        {
            //D and R
            ICollection<Node> D = m.M.Keys;
            ICollection<Node> R = m.M.Values;

            //The Ts
            IEnumerable<Node> uV1 = pattern.Vertices.Except(D);
            IEnumerable<Node> uV2 = host.Vertices.Except(R);
            IEnumerable<Node> T1 = uV1.Where(node => ExistsUncoveredNeighbor(node, D, pattern));
            IEnumerable<Node> T2 = uV2.Where(node => ExistsUncoveredNeighbor(node, R, host));

            //The T hats
            IEnumerable<Node> TH1 = uV1.Except(T1);
            IEnumerable<Node> TH2 = uV2.Except(T2);

            bool A = host.AdjacentVertices(p[1]).Intersect(T2).Count() < pattern.AdjacentVertices(p[0]).Intersect(T1).Count();
            bool B = host.AdjacentVertices(p[1]).Intersect(TH2).Count() < pattern.AdjacentVertices(p[0]).Intersect(TH1).Count();

            return A || B;
        }

        public override Mapping Find(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host)
        {
            throw new NotImplementedException();
        }

        public override IList<Mapping> Enumerate(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host)
        {
            this.pattern = pattern;
            this.host = host;
            VF2(new Mapping());
            return this.mappings;
        }

        public override bool Exists(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host)
        {
            throw new NotImplementedException();
        }

    }

}