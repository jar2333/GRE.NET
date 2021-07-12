using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class VF2Ind : VF2Procedure {

        public VF2Ind() : base() {}

        //Cons(m) iff m satisfies reqs of PT by considering
        //G(D(m)) ⊆ G1 and G(R(m)) ⊆ G2 (induced subgraphs)
        public override bool Cons(Mapping m, Node[] p)
        {
            ICollection<Node> D = m.M.Keys;
            ICollection<Node> R = m.M.Values;
            //The induced subgraphs
            var G1 = GraphExtensions.ToUndirectedGraph<Node, LEdge>(pattern.Edges.Where(e => D.Contains(e.Source) || D.Contains(e.Target)));
            var G2 = GraphExtensions.ToUndirectedGraph<Node, LEdge>(host.Edges.Where(e => R.Contains(e.Source) || R.Contains(e.Target)));

            //iterate each node
            foreach (Node u in G1.Vertices) {
                if (!u.IsEquivalent(m.M[u])) {
                    return false;
                }
            }

            //iterate each edge but get them through the vertex

            //Calculate the rest of the other properties
            bool equivalent = p[0].IsEquivalent(p[1]);
            throw new NotImplementedException();
        }

        public override bool Cut(Mapping m, Node[] p)
        {
            throw new NotImplementedException();
        }

        public override Node[] Find(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host)
        {
            throw new NotImplementedException();
        }

        public override IList<Node[]> Enumerate(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host)
        {
            throw new NotImplementedException();
        }

    }

}