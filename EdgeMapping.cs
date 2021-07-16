using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class EdgeMapping : Mapping<LEdge> {

            //Empty starting mapping
            public EdgeMapping() {
                M = new Dictionary<LEdge, LEdge>();
            }

            public EdgeMapping(Dictionary<LEdge, LEdge> m) {
                M = new Dictionary<LEdge, LEdge>(m);
            }

    }
}