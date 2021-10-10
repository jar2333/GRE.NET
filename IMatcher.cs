using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine 
{
    public interface IMatcher {

        Morphism Find(BidirectionalGraph<Node, LEdge> G1, BidirectionalGraph<Node, LEdge> G2);

        IList<Morphism> Enumerate(BidirectionalGraph<Node, LEdge> G1, BidirectionalGraph<Node, LEdge> G2, int iter = 0);

        bool Exists(BidirectionalGraph<Node, LEdge> G1, BidirectionalGraph<Node, LEdge> G2);

    }
}