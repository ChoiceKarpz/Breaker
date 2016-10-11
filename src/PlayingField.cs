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
		public static List<PowerUp> _ActivePowerUps = new List<PowerUp> ();
		public static Player myPlayer = new Player ();

		public static Random myRandom = new Random ();
		private static float randomXDirection = myRandom.Next (-3, 4);
		public static Ball myBall = new Ball (randomXDirection);


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
					List<string> ColorsForBricks = new List<string> ();
					ColorsForBricks = JsonConvert.DeserializeObject<List<string>> (File.ReadAllText ("colors.json"));
					BrickColor = SwinGame.RGBColor(Convert.ToByte(ColorsForBricks[0]), Convert.ToByte(ColorsForBricks [1]), Convert.ToByte(ColorsForBricks[2]));
					Console.WriteLine (Convert.ToString (BrickColor.R));
					BrickOutlineColor = SwinGame.RGBColor (Convert.ToByte(ColorsForBricks [3]), Convert.ToByte(ColorsForBricks [4]), Convert.ToByte(ColorsForBricks [5]));
				} catch { SetColors (); }
			} else {
				SetColors ();
			}
		}

		public static void SetColors ()
		{
			Console.WriteLine ("Setting Colors");
			List<string> ColorsToSave = new List<string> ();
			ColorsToSave.Add (Convert.ToString(BrickColor.R));
			ColorsToSave.Add (Convert.ToString(BrickColor.G));
			ColorsToSave.Add (Convert.ToString(BrickColor.B));
			ColorsToSave.Add (Convert.ToString (BrickOutlineColor.R));
			ColorsToSave.Add (Convert.ToString (BrickOutlineColor.G));
			ColorsToSave.Add (Convert.ToString (BrickOutlineColor.B));

			string ColorsToSaveText = JsonConvert.SerializeObject (ColorsToSave);
			File.WriteAllText ("colors.json", ColorsToSaveText);
		}


		public static void DrawField ()
		{
			SwinGame.ClearScreen (Color.Black);

			myBall.Draw ();
			myPlayer.Draw ();

			foreach (Wall w in _Walls) {
				w.Draw ();
			}

			foreach (Brick b in _Bricks) {
				b.Draw ();
			}

			foreach (PowerUp p in _ActivePowerUps) {
				p.Draw ();
			}

			SwinGame.DrawTextOnScreen ("Score: " + myPlayer.Points, Color.White, GameMain.ScreenWidth - 150, GameMain.ScreenHeight - 20);
			SwinGame.RefreshScreen (60);
		}

		public static void ProcessMovement ()
		{
			myBall.Move ();
			myBall.CheckCollision ();
			myPlayer.CheckCollision ();

			foreach (PowerUp p in _ActivePowerUps) {
				p.Move ();
				p.CheckCollision ();
			}
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

		public static int NumberOfBricks {
			get {
				return Bricks.Count;
			}
		}

		public static void GenerateBricks ()
		{
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
			}




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

		public static void CreatePowerUp (float x, float y)
		{
			//produce a powerup, with a 1/3 chance of creating each type of powerup 
			Random random = new Random ();
			int i = random.Next (1, 301);
			if (i >= 1 && i <= 100)
				_ActivePowerUps.Add (new EnlargenPowerUp (x, y));
			else if (i >= 101 && i <= 200)
				_ActivePowerUps.Add (new SpeedUpPowerUp (x, y));
			else
				_ActivePowerUps.Add (new SlowBallPowerUp (x, y));
		}

		public static void DeletePowerUp (PowerUp powerUp)
		{
			List<PowerUp> NewPowerUps = new List<PowerUp> ();
			foreach (PowerUp p in _ActivePowerUps) {
				if (p != powerUp)
					NewPowerUps.Add (p);
			}
			_ActivePowerUps = NewPowerUps;
		}

		public static void ResetBricks ()
		{
			_Bricks.Clear ();
			GenerateBricks ();
			myBall.ResetLocation ();
			DrawField ();

		}

	}
}
