using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine {

    public abstract class VF2Procedure: Mapper {

        public UndirectedGraph<Node, LEdge> pattern;
        public UndirectedGraph<Node, LEdge> host;

        Mapping mapping;

        //Main procedure, avoid the recursion (fix later)
        public void VF2(Mapping m) {
            if (m.Covers(pattern.Vertices)) {
                this.mapping = m;
            }
            else {
                IEnumerable<Node[]> candidatePairs = Candidates(m);
                foreach (var p in candidatePairs) {
                    Mapping mExt = m.Extend(p[0], p[1]);
                    if (Cons(mExt) && !Cut(mExt)) {
                        VF2(mExt);
                    }
                }
            }
        }

        //purely helper method (perhaps optimize with Linq)
        public bool ExistsUncoveredNeighbor(Node v, IEnumerable<Node> V, UndirectedGraph<Node, LEdge> G) {
            IEnumerable<Node> neighbors = G.AdjacentVertices(v);
            foreach (var node in V) {
                if (neighbors.Contains(node)) {
                    return true;
                }
            }
            return false;
        }

        //Cartesian product helper, perhaps change Node[] to Tuple<Node>
        //IMPLEMENT!!!!! (abstract only for nowVv)
        public abstract IEnumerable<Node[]> CartesianProduct(IEnumerable<Node> A, IEnumerable<Node> B);

        //Yeah, make a mapping class, with IEnumerables for D(m) and R(m)
        public IEnumerable<Node[]> Candidates(Mapping m) {
            IEnumerable<Node> uV1 = pattern.Vertices.Except<Node>(m.M.Keys);
            IEnumerable<Node> uV2 = host.Vertices.Except<Node>(m.M.Values);

            IEnumerable<Node> T1 = uV1.Where(node => ExistsUncoveredNeighbor(node, m.M.Keys, pattern));
            IEnumerable<Node> T2 = uV1.Where(node => ExistsUncoveredNeighbor(node, m.M.Values, host));
            //Use IEnumerable.Any to check if empty
            if (T1.Any<Node>() && T2.Any<Node>()) {
                return CartesianProduct(T1, T2);
            }
            return CartesianProduct(uV1, uV2);
            
        }

        public abstract bool Cons(Mapping m);

        public abstract bool Cut(Mapping m);

        public abstract Node[] Find(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host);

        public abstract IList<Node[]> Enumerate(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host);

        public abstract bool Exists(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host);

    }

}