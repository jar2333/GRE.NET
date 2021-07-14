using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine {

    public class EdgeMapper: IMapper { //Class for when patten is a K2 graph

        public List<Mapping> mappings;

        public EdgeMapper() {
            this.mappings = new List<Mapping>();
        }

        public void EdgeSearch(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host, bool searchAll) {
            LEdge A = pattern.Edges.First();
            if (!searchAll) {
                LEdge e = host.Edges.FirstOrDefault(x => x.IsEquivalent(A));
                if (e != default(LEdge)) {
                    mappings.Add(new Mapping(new Dictionary<Node, Node>() {{A.Source, e.Source}, {A.Target, e.Target}}));
                }
                return;
            }
            foreach(var e in host.Edges) {
                if (e.IsEquivalent(A)) {
                    mappings.Add(new Mapping(new Dictionary<Node, Node>() {{A.Source, e.Source}, {A.Target, e.Target}}));
                } 
            }
        }

        public Mapping Find(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host) {
            EdgeSearch(pattern, host, false);
            return mappings.FirstOrDefault();
        }

        public IList<Mapping> Enumerate(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host) {
            EdgeSearch(pattern, host, true);
            return mappings;
        }

        public bool Exists(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host) {
            EdgeSearch(pattern, host, false);
            return mappings.Count > 0;
        }


    }

}