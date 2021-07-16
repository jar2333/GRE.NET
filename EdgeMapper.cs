using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine {

    public class EdgeMapper: IMapper { //Class for when patten is a K2 graph

        public List<NodeMapping> mappings; //should this be a thing? Should mappers just compute and not store?

        public EdgeMapper() {
            this.mappings = new List<NodeMapping>();
        }

        public void EdgeSearch(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host, bool searchAll) {
            LEdge A = pattern.Edges.First();
            if (!searchAll) {
                LEdge e = host.Edges.FirstOrDefault(x => x.IsEquivalent(A));
                if (e != default(LEdge)) {
                    mappings.Add(new NodeMapping(new Dictionary<Node, Node>() {{A.Source, e.Source}, {A.Target, e.Target}}));
                }
                return;
            }
            foreach(var e in host.Edges) {
                if (e.IsEquivalent(A)) {
                    mappings.Add(new NodeMapping(new Dictionary<Node, Node>() {{A.Source, e.Source}, {A.Target, e.Target}}));
                } 
            }
        }

        public Morphism Find(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host) {
            //EdgeSearch(pattern, host, false);
            //return mappings.FirstOrDefault();
            throw new NotImplementedException();
        }

        public IList<Morphism> Enumerate(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host, int iter = 0) {
            //EdgeSearch(pattern, host, true);
            //return mappings;
            throw new NotImplementedException();
        }

        public bool Exists(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host) {
            //EdgeSearch(pattern, host, false);
            //return mappings.Count > 0;
            throw new NotImplementedException();
        }


    }

}