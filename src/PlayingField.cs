using System;
using System.IO;
using SwinGameSDK;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyGame
{
	public static class PlayingField
	{
		public static List<Brick> _Bricks = new List<Brick>();
		public static List<Wall> _Walls = new List<Wall>();
		public static Player myPlayer = new Player ();
		public static Ball myBall = new Ball ();
		public static Color BrickColor = Color.Red;
		public static Color BrickOutlineColor = Color.DarkRed;

		public static List<Wall> Walls {
			get {
				return _Walls;
			}
		}

		public static List<Brick> Bricks {
			get {
				return _Bricks;
			}
		}

		public static void LoadColors ()
		{
			if (File.Exists ("colors.json")) {
				try {
					List<float> ColorsForBricks = new List<float> ();
					ColorsForBricks = JsonConvert.DeserializeObject<List<float>> (File.ReadAllText ("colors.json"));
					BrickColor = SwinGame.RGBFloatColor(ColorsForBricks [0], ColorsForBricks [1], ColorsForBricks[2]);
					BrickOutlineColor = SwinGame.RGBFloatColor (ColorsForBricks [3], ColorsForBricks [4], ColorsForBricks [5]);
				} catch { SetColors (); }
			} else {
				SetColors ();
			}
		}

		public static void SetColors ()
		{
			List<float> ColorsToSave = new List<float> ();
			ColorsToSave.Add (BrickColor.R);
			ColorsToSave.Add (BrickColor.G);
			ColorsToSave.Add (BrickColor.B);
			ColorsToSave.Add (BrickOutlineColor.R);
			ColorsToSave.Add (BrickOutlineColor.G);
			ColorsToSave.Add (BrickOutlineColor.B);

			string ColorsToSaveText = JsonConvert.SerializeObject (ColorsToSave);
			File.WriteAllText ("colors.json", ColorsToSaveText);
		}


		public static void DrawField ()
		{
			myBall.Draw ();
			myPlayer.Draw ();

			foreach (Wall w in _Walls) {
				w.Draw ();
			}

			foreach (Brick b in _Bricks) {
				b.Draw ();
			}

			SwinGame.DrawTextOnScreen ("Score: " + myPlayer.Points, Color.White, GameMain.ScreenWidth - 150, GameMain.ScreenHeight - 20);
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

		public static void CheckHealthOfField ()
		{
			foreach (Wall w in Walls) {
				w.CheckHealth ();
			}

			foreach (Brick b in Bricks) {
				b.CheckHealth ();
			}
		}

		//public static int NumberOfBricks {
		//	get {
		//		return Bricks.Length();
		//	}
		//}

		public static void GenerateBricks ()
		{
			//attempt to read brick locations from file 
			//if (File.Exists ("level.json")) {
			//	List<float []> BricksToAdd = new List<float []> ();

			//	BricksToAdd = JsonConvert.DeserializeObject<List<float []>> (File.ReadAllText ("level.json"));


			//	foreach (float [] data in BricksToAdd) 
			//		_Bricks.Add (new Brick (data [0], data [1]));

			//	Console.WriteLine ("File was read");
			//}
			////create if they don't exist
			//else {

				//1st row
				for (int i = Wall.SideLength * 2; i <= GameMain.ScreenWidth - Wall.SideLength * 2 - Brick.BrickWidth; i = i + Brick.BrickWidth) {
					_Bricks.Add (new Brick (i, Wall.SideLength * 2));
				}
				//2nd row
				for (int i = Wall.SideLength * 2; i <= GameMain.ScreenWidth - Wall.SideLength * 2 - Brick.BrickWidth; i = i + Brick.BrickWidth) {
					_Bricks.Add (new Brick (i, Wall.SideLength * 2 + Brick.BrickHeight));
				}
				//3rd row
				for (int i = Wall.SideLength * 2; i <= GameMain.ScreenWidth - Wall.SideLength * 2 - Brick.BrickWidth; i = i + Brick.BrickWidth) {
					_Bricks.Add (new Brick (i, Wall.SideLength * 2 + Brick.BrickHeight * 2));
				}
				//4th row
				for (int i = Wall.SideLength * 2; i <= GameMain.ScreenWidth - Wall.SideLength * 2 - Brick.BrickWidth; i = i + Brick.BrickWidth) {
					_Bricks.Add (new Brick (i, Wall.SideLength * 2 + Brick.BrickHeight * 3));
				}

				List<float []> Positions = new List<float []> ();
				foreach (Brick b in Bricks) {
					Positions.Add (new float [] { b.XLocation, b.YLocation });
				}

				//string BricksToSave = JsonConvert.SerializeObject (Positions);
				//File.WriteAllText ("level.json", BricksToSave);
			}
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
			for (int i = 0; i <= GameMain.ScreenWidth - Wall.SideLength; i = i + Wall.SideLength) {
				_Walls.Add (new Wall (i, 0));
			}

			//generate walls down left column
			for (int i = Wall.SideLength; i <= GameMain.ScreenHeight - Wall.SideLength; i = i + Wall.SideLength) {
				_Walls.Add (new Wall (0, i));
			}

			//generate walls down right column
			for (int i = Wall.SideLength; i <= GameMain.ScreenHeight - Wall.SideLength; i = i + Wall.SideLength) {
				_Walls.Add (new Wall (GameMain.ScreenWidth - Wall.SideLength, i));
			}
		}


		public static void DeleteBrick (Brick b)
		{
			List<Brick> NewBricks = new List<Brick> ();
			foreach (Brick brick in Bricks) {
				if (brick != b)
					NewBricks.Add (brick);
			}
			_Bricks = NewBricks;
		}

		public static void DeleteWall (Wall w)
		{
			List<Wall> NewWalls = new List<Wall> ();
			foreach (Wall wall in Walls) {
				if (wall != w)
					NewWalls.Add (wall);
			}
			_Walls = NewWalls;

		}

		public static bool CheckGameOver() {
			return myBall.OffScreen;
		}

		public static void DisplayGameOver ()
		{
			SwinGame.ClearScreen (Color.Black);
			SwinGame.DrawTextOnScreen ("Game Over! Points scored: " + myPlayer.Points, Color.White, 300, 300);
			SwinGame.RefreshScreen ();
		}


	}
}
