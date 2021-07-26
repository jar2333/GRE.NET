using System;
using System.Collections.Generic;

namespace GraphRewriteEngine
{
    public interface IChooser {

        int Choose(Rule[] rules); //returns int of chosen rule, input is array with null and Rule entries (indexing!)
        
    }
}