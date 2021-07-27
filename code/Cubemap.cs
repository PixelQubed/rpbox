using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace RPGamemode.Utils
{
	public class Cubemap : ModelEntity
	{

		public Cubemap()
		{
			SetModel( "models/editor/env_cubemap.vmdl" );
			PhysicsEnabled = false;
			Scale = 2.0f;
			EnableHideInFirstPerson = false;
			EnableDrawing = true;
			Spawn();
		}

		public override void FrameSimulate( Client cl )
		{
			base.FrameSimulate( cl );

			Position = cl.Pawn.EyePos + (cl.Pawn.EyeRot.Forward * 50f);
		}
	}
}
