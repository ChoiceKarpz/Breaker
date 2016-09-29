using System;
using SwinGameSDK;
using System.Collections.Generic;

namespace MyGame
{
	public static class PlayingField
	{
		//static List<Bricks> _Bricks = new List<Bricks>();
		//static List<Wall> _Walls = new List<Wall>();
		public static Player myPlayer = new Player ();
		public static Ball myBall = new Ball ();


		public static void DrawField ()
		{
			myBall.Draw ();
			myPlayer.Draw ();
		}

		public static void ProcessMovement ()
		{
			myBall.Move ();
			myBall.CheckCollision ();
		}

		public static void ProcessInput ()
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
