using System;
using System.Collections.Generic;

namespace MyGame
{
	public static class PlayingField
	{
		//static List<Bricks> _Bricks = new List<Bricks>();
		//static List<Wall> _Walls = new List<Wall>();
		static Player myPlayer = new Player ();
		public static Ball myBall = new Ball ();

		public static void DrawField ()
		{
			myBall.Draw ();
			myPlayer.Draw ();
		}

		public static void ProcessMovement ()
		{
			myBall.Move ();
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
