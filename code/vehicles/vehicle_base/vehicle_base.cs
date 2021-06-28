using System;
using System.Collections;
using System.Collections.Generic;
using Sandbox;



namespace VehicleController
{
	[Library( "ent_vehicle", Title = "Vehicle Test", Spawnable = true )]
	public partial class Controller : Prop
	{

		private const string AXIS_X = "Axis X";
		private const string AXIS_Y = "Axis Y";
		private const string AXIS_Z = "Axis Z";

		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/citizen_props/chair02.vmdl" );
			SetupPhysicsFromModel( PhysicsMotionType.Dynamic, true );
		}

			public class VehicleController : BasePlayerController
			{

				public override void Simulate()
				{
					// Face whichever way the player is aiming
					Rotation = Input.Rotation;

					// Create a direction vector from the input from the client
					var direction = new Vector3( Input.Forward, Input.Left, 0 );

					// Rotate the vector so forward is the way we're facing
					direction *= Rotation;

					// Normalize it and multiply by speed
					direction = direction.Normal * 1000;

					// Apply the move
					Position += direction * Time.Delta;

				}

			}
	}
}




