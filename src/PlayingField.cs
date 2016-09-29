using System;
using SwinGameSDK;
using System.Collections.Generic;

namespace MyGame
{
	public class PlayingField
	{
		//static List<Bricks> _Bricks = new List<Bricks>();
		//static List<Wall> _Walls = new List<Wall>();
		public Player myPlayer = new Player ();
		public Ball myBall = new Ball ();

		public void DrawField ()
		{
			myBall.Draw ();
			myPlayer.Draw ();
		}

		public void ProcessMovement ()
		{
			myBall.Move ();
		}

		public void ProcessInput ()
		{
			if (SwinGame.KeyDown (KeyCode.vk_RIGHT))
				myPlayer.MoveRight ();

			if (SwinGame.KeyDown (KeyCode.vk_LEFT))
				myPlayer.MoveLeft ();
		}

		//public static Brick[] Bricks {
		//	get {
		//		return _Bricks;
		//	}
		//}

		//public static Wall [] Walls {
		//	get {
		//		return _Walls;
		//	}
		//}

		//public static int NumberOfBricks {
		//	get {
		//		return Bricks.Length();
		//	}
		//}

		//public static void GenerateBricks ()
		//{
		//}

		//public static void ShuffleBricks ()
		//{
		//}

		//public static void SetBrickLocations ()
		//{
		//}

		//public static void DeleteBrick (Brick b)
		//{
		//	Bricks.Remove (b);
		//}

		//public static void DeleteWall (Wall w)
		//{
		//	Walls.Remove (w);
		//}


	}
}
