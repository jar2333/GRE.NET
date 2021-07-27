using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine {

    public class EdgeMapper: IMatcher { //Class for when pattern is a K2 graph

        public List<Morphism> morphisms; //should this be a thing? Should mappers just compute and not store?

        public EdgeMapper() {
            this.morphisms = new List<Morphism>();
        }

        public void EdgeSearch(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host, bool searchAll) {
            LEdge A = pattern.Edges.First();
            if (!searchAll) {
                LEdge e = host.Edges.FirstOrDefault(x => x.IsEquivalent(A));
                if (e != default(LEdge)) {
                    NodeMapping vm = new NodeMapping(new Dictionary<Node, Node>() {{A.Source, e.Source}, {A.Target, e.Target}});
                    EdgeMapping em = new EdgeMapping(new Dictionary<LEdge, LEdge>() {{A, e}});
                    morphisms.Add(new Morphism(vm, em));
                }
                return;
            }
            foreach(var e in host.Edges) {
                if (e.IsEquivalent(A)) {
                    NodeMapping vm = new NodeMapping(new Dictionary<Node, Node>() {{A.Source, e.Source}, {A.Target, e.Target}});
                    EdgeMapping em = new EdgeMapping(new Dictionary<LEdge, LEdge>() {{A, e}});
                    morphisms.Add(new Morphism(vm, em));
                } 
            }
        }

        public Morphism Find(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host) {
            EdgeSearch(pattern, host, false);
            return morphisms.FirstOrDefault();
        }

        public IList<Morphism> Enumerate(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host, int iter = 0) {
            EdgeSearch(pattern, host, true);
            return morphisms;
        }

        public bool Exists(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host) {
            EdgeSearch(pattern, host, false);
            return morphisms.Count > 0;
        }


    }

}