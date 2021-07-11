using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine 
{
    public interface Mapper {

        Node[] Find(UndirectedGraph<Node, LEdge> G1, UndirectedGraph<Node, LEdge> G2);

        IList<Node[]> Enumerate(UndirectedGraph<Node, LEdge> G1, UndirectedGraph<Node, LEdge> G2);

        bool Exists(UndirectedGraph<Node, LEdge> G1, UndirectedGraph<Node, LEdge> G2);

    }
}