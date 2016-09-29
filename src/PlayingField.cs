using System;
using System.Collections.Generic;

namespace MyGame
{
	public static class PlayingField
	{
		List<Bricks> _Bricks = new List<Bricks>();
		List<Wall> _Walls = new List<Wall>();
		Player mainPlayer = new Player ();
		Ball myBall = new Ball ();

		public Brick[] Bricks {
			get {
				return _Bricks;
			}
		}

		public Wall [] Walls {
			get {
				return _Walls;
			}
		}

		public static int NumberOfBricks {
			get {
				return Bricks.Length();
			}
		}

		public static void GenerateBricks ()
		{
		}

		public static void ShuffleBricks ()
		{
		}

		public static void SetBrickLocations ()
		{
		}

		public static void DeleteBrick (Brick b)
		{
			Bricks.Remove (b);
		}

		public static void DeleteWall (Wall w)
		{
			Walls.Remove (w);
		}


	}
}
