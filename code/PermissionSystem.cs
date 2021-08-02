using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPBox;
using Sandbox;
using RPBox.Pawns;
using RPBox;

namespace RPBox.Permissions
{
	public class Permission
	{
		public string Name { get; set; }
		public Permission(string name)
		{
			Name = name;
		}
	}
	public class PermissionSystem : Entity
	{
		private static PermissionSystem instance;
		public static PermissionSystem Instance { get => instance; set => instance = value; }

		public static List<Permission> permissions;

		public PermissionSystem()
		{
			instance = this;
			Transmit = TransmitType.Always;
			permissions = new List<Permission>();

			permissions.Add( new Permission( "admin" ) );
		}
		
		[ClientCmd("AddPlayerPermission", Help = "Adds a permission to a player", Name = "Permission_Add")]
		public static void SetPlayerPermission(string playerName, string permission)
		{
			SandboxPlayer player = RPBoxGame.GetPlayerByName( playerName );
			if(player == null)
			{
				Log.Info( "player not found" );
				return;
			}
			Log.Info( "Permission " + permission + " for player " + player.GetClientOwner().Name + " added." );
		}
	}
}

