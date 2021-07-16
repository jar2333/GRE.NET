using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Morphism {

        public NodeMapping Vm;
        public EdgeMapping Em;

        public Morphism() {
            Vm = new NodeMapping();
            Em = new EdgeMapping();
        }

        public Morphism(NodeMapping vm, EdgeMapping em) {
            Vm = vm;
            Em = em;
        }

        public Morphism Compose(Morphism f) {
            return new Morphism(this.Vm.Compose(f.Vm) as NodeMapping, this.Em.Compose(f.Em) as EdgeMapping);
        } 

    }
}