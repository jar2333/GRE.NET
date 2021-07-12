using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine 
{
    public interface Mapper {

        Mapping Find(UndirectedGraph<Node, LEdge> G1, UndirectedGraph<Node, LEdge> G2);

        IList<Mapping> Enumerate(UndirectedGraph<Node, LEdge> G1, UndirectedGraph<Node, LEdge> G2);

        bool Exists(UndirectedGraph<Node, LEdge> G1, UndirectedGraph<Node, LEdge> G2);

    }
}