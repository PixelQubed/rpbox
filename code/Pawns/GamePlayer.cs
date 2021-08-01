using Sandbox;
using System;

namespace RPBox.Pawns
{
	public partial class GamePlayer : Player
	{
		private DamageInfo damageInfo;

		[Net, OnChangedCallback]
		public Job Job { get; set; }

		public GamePlayer()
		{
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
