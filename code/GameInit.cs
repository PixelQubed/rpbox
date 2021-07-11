using Sandbox;
using System.Collections.Generic;
using System.Text.Json;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace RPGamemode
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
	public partial class RPGame : Game
	{
		public List<Jobs.Base> Jobs;
		public static RPGame Instance;
		public RPGame()
		{
			Instance = this;
			if ( IsServer )
			{
				Log.Info( "My Gamemode Has Created Serverside!" );

				Jobs = FileSystem.Data.ReadJson<List<Jobs.Base>>("jobs.json");
				SetJobs(JsonSerializer.Serialize(Jobs));
				// Create Hud
				new UI.TestHud();
			}

			if ( IsClient )
			{
				//Jobs = new List<Jobs.Base>();
				GetJobs();
				Log.Info( "My Gamemode Has Created Clientside!" );
			}
		}



		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new Pawns.SelectJob();
			client.Pawn = player;

			player.Respawn();
		}

		[ServerCmd("change_job")]
		public static void ChangeJob(string jobName)
		{
			var owner = ConsoleSystem.Caller;

			if (owner == null)
				return;

			owner.Pawn.Delete();

			var player = new Pawns.GamePlayer();
			owner.Pawn = player;
			
			player.Respawn();
		}

		[ServerCmd]
		public static void GetJobs()
		{
			var json = JsonSerializer.Serialize(RPGame.Instance.Jobs);
			Log.Info("GETTING JOBS: " + json);
			RPGame.Instance.SetJobs(json);
		}

		[ClientRpc]
		public void SetJobs(string jobs)
		{
			Log.Info("SETTING JOBS: " + jobs);
			Jobs = JsonSerializer.Deserialize<List<Jobs.Base>>(jobs);
		}
	}

}
