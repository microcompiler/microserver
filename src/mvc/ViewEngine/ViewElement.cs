﻿using System;
using System.Collections;

namespace Bytewizer.TinyCLR.Http.Mvc.ViewEngine
{
    [Serializable]
    public struct ViewElement
    {
        public string Name;
        public string Path;
        public string Htm;
        public IDictionary Vars; //public Dictionary<string, string> Vars;
    }
}
