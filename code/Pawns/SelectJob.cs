using Sandbox;
using System;

namespace RPGamemode.Pawns
{
	public partial class SelectJob : Player
	{
		public override void Respawn()
		{
			SetModel("");

			EnableAllCollisions = false;
			EnableDrawing = false;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			Debug.MovementDebugger.Invoke( GroundEntity );

			base.Simulate( cl );
			//
			// If you have active children (like a weapon etc) you should call this to
			// simulate those too.
			//
			SimulateActiveChild( cl, ActiveChild );
		}

		public override void OnKilled()
		{
			base.OnKilled();

			EnableDrawing = false;
		}
	}
}
