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