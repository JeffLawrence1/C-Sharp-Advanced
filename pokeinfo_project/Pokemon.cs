using System.Collections.Generic;

namespace pokeinfo_project
{
    public class Pokemon
    {
        public string Name { get; set; }

        public List<string> Types { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }
    }
}