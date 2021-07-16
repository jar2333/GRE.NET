using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Mapping<T> {

            public Dictionary<T, T> M; //make private later

            public IEnumerable<T> Keys() {
                return M.Keys;
            }

            public IEnumerable<T> Values() {
                return M.Values;
            }

            //Empty starting mapping
            public Mapping() {
                M = new Dictionary<T, T>();
            }

            public Mapping(Dictionary<T, T> m) {
                M = new Dictionary<T, T>(m);
            }

            public bool IsSubset(IEnumerable<T> A, IEnumerable<T> B) {
                return !A.Except(B).Any();
            }

            public Mapping<T> Compose(Mapping<T> m) {
                if (IsSubset(this.Values(), m.Keys())) {
                    var composition = new Dictionary<T, T>();
                    foreach (T key in this.Keys()) {
                        composition[key] = m.M[this.M[key]];
                    }
                    return new Mapping<T>(composition);
                }
                throw new NotSupportedException();
            }

            public override string ToString() {
                string output = "";
                foreach (KeyValuePair<T, T> entry in M) {
                    output += $"{entry.Key.ToString()} -> {entry.Value.ToString()}\n";
                }
                return output;
            }

    }
}