using System;
using System.Reflection;
using Sandbox;

namespace RPGamemode.Debug
{	
	
    public class MovementDebugger
    {
		public const bool debugEnabled = true;
		private static bool inAir = false;
		private static bool isJumping = false;
		private static bool isForward = false;
		private static bool isBack = false;
		private static bool isLeft = false;
		private static bool isRight = false;

		public static void Invoke( Entity GroundEntity )
		{
			if ( MovementDebugger.debugEnabled == true )
			{
				inAir = GroundEntity == null;
				isJumping = Input.Down( InputButton.Jump );
				isForward = Input.Down( InputButton.Forward );
				isBack = Input.Down( InputButton.Back );
				isLeft = Input.Down( InputButton.Left );
				isRight = Input.Down( InputButton.Right );
				MovementDebugger.Debug();
			}
		}

		public static void Debug()
		{
			// Dont need the array and its slow to use new as it needs to ask for the memory and then wait to recieve it.
			// bool[] movementPropBools = new bool[6]

			DebugOverlay.ScreenText(0, $"InAir = {inAir}", 0f);
			DebugOverlay.ScreenText(1, $"Jump = {isJumping}", 0f);
			DebugOverlay.ScreenText(2, $"Forward = {isForward}", 0f);
			DebugOverlay.ScreenText(3, $"Back = {isBack}", 0f);
			DebugOverlay.ScreenText(4, $"Left = {isLeft}", 0f);
			DebugOverlay.ScreenText(5, $"Right = {isRight}", 0f);
		}

    }
}
