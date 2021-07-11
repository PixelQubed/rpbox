using Sandbox;
using System.Collections;

namespace RPGamemode.Jobs
{
    public class Base
    {
        public string Name { get { return "Unknown"; } }
        public int Income { get { return 0; } }
        public ArrayList Loadout { get; }
    }
}
