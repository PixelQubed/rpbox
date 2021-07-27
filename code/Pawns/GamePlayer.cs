using Sandbox;
using System;
using System.Collections.Generic;

namespace RPGamemode.Pawns
{
	public partial class GamePlayer : Player
	{
		private DamageInfo damageInfo;
		public ModelEntity cubemapModel;
		Vector3[] offsets = new Vector3[4];
		public bool shouldUpdate = false;
		[ConVar.ClientData( "debug_cubemap" )]
		public static bool shouldRender { get; set; } = false;

		[Net, OnChangedCallback]
		public Job Job { get; set; }

		public GamePlayer()
		public GamePlayer()
		{
			
		}

		public override void Spawn()
		{
			base.Spawn();
			cubemapModel = new ModelEntity();
			cubemapModel.PhysicsEnabled = false;
			cubemapModel.SetModel( "models/cubemap_test.vmdl" );
			cubemapModel.EnableHideInFirstPerson = false;
			cubemapModel.EnableDrawing = true;
			cubemapModel.Spawn();

			shouldUpdate = true;


		}

		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );
			//
			// Use WalkController for movement (you can make your own PlayerController for 100% control)
			//
			Controller = new WalkController();

			//
			// Use StandardPlayerAnimator  (you can make your own PlayerAnimator for 100% control)
			//
			Animator = new StandardPlayerAnimator();

			//
			// Use ThirdPersonCamera (you can make your own Camera for 100% control)
			//
			Camera = new FirstPersonCamera();

			EnableAllCollisions = true;
			EnableDrawing = true;
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

			Rotation offsetRot;
			if ( shouldUpdate )
			{
				cubemapModel.Position = cl.Pawn.EyePos + (cl.Pawn.EyeRot.Forward * 30f);
				cubemapModel.Rotation = Rotation.From(0, cl.Pawn.EyeRot.Yaw() + 90f, cl.Pawn.EyeRot.Pitch());
				cubemapModel.EnableDrawing = shouldRender; 
			}

			
			SimulateActiveChild( cl, ActiveChild );

			if (IsServer) {
				if (Velocity.z < -600 && GroundEntity == null && !Controller.HasTag("noclip")) {
					damageInfo.Damage = Math.Abs(Velocity.y / 15);
					damageInfo.Body = PhysicsBody;
				} else if (Velocity.z > 0 || Controller.HasTag("noclip")) {
					damageInfo.Damage = 0;
				}

				if (damageInfo.Damage > 0 && GroundEntity != null) {
					TakeDamage(damageInfo);
					damageInfo.Damage = 0;
				}
			}
		}


		public override void OnKilled()
		{
			base.OnKilled();

			EnableDrawing = false;
		}

		private void OnJobChanged()
		{
			if (Job != null)
				UI.Job.Instance.UpdateJobText(Job.Name);
		}
	}
}
