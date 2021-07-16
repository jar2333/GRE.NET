using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine 
{
    public interface IMapper {

        Morphism Find(UndirectedGraph<Node, LEdge> G1, UndirectedGraph<Node, LEdge> G2);

        IList<Morphism> Enumerate(UndirectedGraph<Node, LEdge> G1, UndirectedGraph<Node, LEdge> G2);

        bool Exists(UndirectedGraph<Node, LEdge> G1, UndirectedGraph<Node, LEdge> G2);

    }
}