using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class ProbabilisticGrammar : Grammar {

        public double[] weights;

        //add a Random generator object, with seeding options. Check out MathNet.Numerics

        public ProbabilisticGrammar(List<Rule> r) : base(r) {
            this.weights = new double[this.rules.Length];
            InitializeUniform();
        }

        public void InitializeUniform() {
            double uniform = 1 / this.rules.Length;
            for (int i = 0; i < this.rules.Length; i++) {
                weights[i] = uniform;
            }
        }

    }
}