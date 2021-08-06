using Sandbox;
using System.Collections.Generic;
using System.Text.Json;
using RPBox.Pawns;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace RPBox
{
	// <summary>
	// This is your game class. This is an entity that is created serverside when
	// the game starts, and is replicated to the client.
	//
	// You can use this to create things like HUD's and declare which player class
	// to use for spawned players.
	//
	// Your game needs to be registered (using [Library] here) with the same name
	// as your game addon. If it isn't then we won't be able to find it.
	// </summary>
	[Library( "game" )]
	public partial class RPBoxGame : Game
	{
		public JobManager JobManager;
		private static RPBoxGame instance;
		public static RPBoxGame Instance { get => instance; set => instance = value; }

		public RPBoxGame()
		{
			Instance = this;

			if ( IsServer )
			{
				Log.Info( "My Gamemode Has Created Serverside!" );

				// Create Hud
				_ = new UI.TestHud();
				JobManager = new JobManager();
			}

			if ( IsClient )
			{
				Log.Info( "My Gamemode Has Created Clientside!" );
			}
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			Log.Info( "Player " + client.Name + " has joined the game." );
			var player = new Pawns.SelectJob();
			client.Pawn = player;

			player.Respawn();
		}

		public override void ClientDisconnect( Client client, NetworkDisconnectionReason reason )
		{
			base.ClientDisconnect( client, reason );
		}

		[ServerCmd("change_job")]
		public static void ChangeJob(int id)
		{
			var job = RPBoxGame.Instance.JobManager.GetJob(id);
			if (job is null)
			{
				Sandbox.Log.Error($"job {id} does not exist!");
				return;
			}

			var owner = ConsoleSystem.Caller;

			if (owner == null)
				return;

			owner.Pawn.Delete();

			SandboxPlayer player = new SandboxPlayer();

			
			player.Job = job;
			owner.Pawn = player;

			player.Respawn();

			Log.Info(player.GetClientOwner().Name + " is now playing as " + player.Job.Name );
			EquipLoadoutFromJob( player );
		}

		public static void EquipLoadoutFromJob( SandboxPlayer player )
		{
			int i = 0;
			foreach ( string weapon in player.Job.Loadout )
			{
				Log.Info( "Weapon " + i + " is " + weapon );
				i++;
				Entity loadoutWeapon = Library.Create<Entity>( weapon, true );
				player.Inventory.Add( loadoutWeapon, true );
			}
			i = 0;
		}

		[ServerCmd( "spawn" )]
		public static void Spawn( string modelname )
		{
			var owner = ConsoleSystem.Caller?.Pawn;

			if ( ConsoleSystem.Caller == null )
				return;

			var tr = Trace.Ray( owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 500 )
				.UseHitboxes()
				.Ignore( owner )
				.Size( 2 )
				.Run();

			var ent = new Prop();
			ent.Position = tr.EndPos;
			ent.Rotation = Rotation.From( new Angles( 0, owner.EyeRot.Angles().yaw, 0 ) ) * Rotation.FromAxis( Vector3.Up, 180 );
			ent.SetModel( modelname );

			// Drop to floor
			if ( ent.PhysicsBody != null && ent.PhysicsGroup.BodyCount == 1 )
			{
				var p = ent.PhysicsBody.FindClosestPoint( tr.EndPos );

				var delta = p - tr.EndPos;
				ent.PhysicsBody.Position -= delta;
				DebugOverlay.Line( p, tr.EndPos, 10, false );
			}

		}
		[ServerCmd( "spawn_unowned" )]
		public static void SpawnUnowned( string modelname )
		{
			var owner = ConsoleSystem.Caller?.Pawn;

			if ( ConsoleSystem.Caller == null )
				return;

			var tr = Trace.Ray( owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 500 )
				.UseHitboxes()
				.Ignore( owner )
				.Size( 2 )
				.Run();

			var ent = new Prop();
			ent.Position = tr.EndPos;
			ent.Rotation = Rotation.From( new Angles( 0, owner.EyeRot.Angles().yaw, 0 ) ) * Rotation.FromAxis( Vector3.Up, 180 );
			ent.SetModel( modelname );

			// Drop to floor
			if ( ent.PhysicsBody != null && ent.PhysicsGroup.BodyCount == 1 )
			{
				var p = ent.PhysicsBody.FindClosestPoint( tr.EndPos );

				var delta = p - tr.EndPos;
				ent.PhysicsBody.Position -= delta;
				DebugOverlay.Line( p, tr.EndPos, 10, false );
			}

		}

		[ServerCmd( "spawn_entity" )]
		public static void SpawnEntity( string entName )
		{
			var owner = ConsoleSystem.Caller.Pawn;

			if ( owner == null )
				return;

			var attribute = Library.GetAttribute( entName );

			if ( attribute == null || !attribute.Spawnable )
				return;

			var tr = Trace.Ray( owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 200 )
				.UseHitboxes()
				.Ignore( owner )
				.Size( 2 )
				.Run();

			var ent = Library.Create<Entity>( entName );
			if ( ent is BaseCarriable && owner.Inventory != null )
			{
				if ( owner.Inventory.Add( ent, true ) )
					return;
			}

			ent.Position = tr.EndPos;
			ent.Rotation = Rotation.From( new Angles( 0, owner.EyeRot.Angles().yaw, 0 ) );

			//Log.Info( $"ent: {ent}" );
		}

	}
}
