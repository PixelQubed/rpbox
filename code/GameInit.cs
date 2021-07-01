using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Threading.Tasks;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace SBoxGamemodeTest
{
	[Library( "game" )]
	public class DebugView
	{
		private bool DebugEnabled;



	}

	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// 
	/// Your game needs to be registered (using [Library] here) with the same name 
	/// as your game addon. If it isn't then we won't be able to find it.
	/// </summary>
	public partial class SBoxGamemodeTest : Sandbox.Game
	{
		public SBoxGamemodeTest()
		{
			if ( IsServer )
			{
				Log.Info( "My Gamemode Has Created Serverside!" );
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

			var player = new GamePlayer();
			client.Pawn = player;

			player.Respawn();
		}
	}

}
