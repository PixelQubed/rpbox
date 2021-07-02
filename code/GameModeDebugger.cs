using System;
using System.Reflection;
using Sandbox;

namespace SBoxGamemodeTest.GMD
{	
	
    public class Debugger
    {
		public const bool debugEnabled = true;

		public static void Invoke( object GroundEntity )
		{
			if ( Debugger.debugEnabled == true ) { Debugger.Debug( GroundEntity ); }
		}

		public static void Debug( object GroundEntity )
		{
			bool[] movementPropsBools = new bool[6];

			if ( GroundEntity == null ) {movementPropsBools[0] = true;}
			DebugOverlay.ScreenText(0, $"InAir = {movementPropsBools[0]}", 0f );

			if ( Input.Down( InputButton.Jump ) ) {movementPropsBools[1] = true;}
			DebugOverlay.ScreenText(1, $"Jump = {movementPropsBools[1]}", 0f );

			if ( Input.Down( InputButton.Forward ) ) {movementPropsBools[2] = true;}
			DebugOverlay.ScreenText(2, $"Forward = {movementPropsBools[2]}", 0f );

			if ( Input.Down( InputButton.Back ) ) {movementPropsBools[3] = true;}
			DebugOverlay.ScreenText(3, $"Back = {movementPropsBools[3]}", 0f );

			if ( Input.Down( InputButton.Left ) ) {movementPropsBools[4] = true;}
			DebugOverlay.ScreenText(4, $"Left = {movementPropsBools[4]}", 0f );

			if ( Input.Down( InputButton.Right ) ) {movementPropsBools[5] = true;}
			DebugOverlay.ScreenText(5, $"Right = {movementPropsBools[5]}", 0f );

		}

    }
}
