using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Rule {
        public BidirectionalGraph<Node, LEdge> LHS;
        public BidirectionalGraph<Node, LEdge> I;
        public BidirectionalGraph<Node, LEdge> RHS;

        public Morphism L;
        public Morphism R;




    }
}
