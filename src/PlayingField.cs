using System;
using SwinGameSDK;
using System.Collections.Generic;

namespace MyGame
{
	public static class PlayingField
	{
		//static List<Bricks> _Bricks = new List<Bricks>();
		static List<Wall> _Walls = new List<Wall>();
		public static Player myPlayer = new Player ();
		public static Ball myBall = new Ball ();

		public static List<Wall> Walls {
			get {
				return _Walls;
			}
		}


		public static void DrawField ()
		{
			myBall.Draw ();
			myPlayer.Draw ();

			foreach (Wall w in _Walls) {
				w.Draw ();
			}
		}

		public static void ProcessMovement ()
		{
			myBall.Move ();
			myBall.CheckCollision ();
			myPlayer.CheckCollision ();
		}

		public static void ProcessInput ()
		{
			if (SwinGame.KeyDown(KeyCode.vk_RIGHT))
				myPlayer.MoveRight ();

			if (SwinGame.KeyDown (KeyCode.vk_LEFT))
				myPlayer.MoveLeft ();
		}

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

		public static void GenerateWalls ()
		{
			//generate walls accross the top row
			for (int i = 0; i <= GameMain.ScreenWidth - Wall.SideLength; i = i + 50) {
				_Walls.Add (new Wall (i, 0));
			}

			//generate walls down left column
			for (int i = Wall.SideLength; i <= GameMain.ScreenHeight - Wall.SideLength; i = i + 50) {
				_Walls.Add (new Wall (0, i));
			}
		}

		//public static void DeleteBrick (Brick b)
		//{
		//	Bricks.Remove (b);
		//}

		public static void DeleteWall (Wall w)
		{
			_Walls.Remove (w);
		}


	}
}
