using System;
using System.Collections.Generic;

namespace Contract
{
    public class Element
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class System
    {
        public List<Element> Elements { get; set; }
    }
}