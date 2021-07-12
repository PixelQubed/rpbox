using Sandbox;
using System;
using System.Collections;

namespace RPGamemode.Jobs
{
    public class Base
    {
		public string GUID { get; set; }
        public string Name { get; set; }
        public int Income { get; set; }
        public ArrayList Loadout { get; set; }

		public Base()
		{
			if (RPGame.Instance.IsServer)
				GUID = Guid.NewGuid().ToString();
		}
    }
}
