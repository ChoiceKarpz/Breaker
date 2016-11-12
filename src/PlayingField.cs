using System;
using System.IO;
using SwinGameSDK;
using System.Collections.Generic;
using Newtonsoft.Json;

//this static class contains all the different methods that are needed for managing flow within the game, and also contains all of the different objects that the player interacts with

namespace MyGame
{
	public static class PlayingField
	{
		//create bricks, walls and other elements that are needed for the game
		public static List<Brick> _Bricks = new List<Brick>();
		public static List<Wall> _Walls = new List<Wall>();
		public static List<PowerUp> _ActivePowerUps = new List<PowerUp> ();
		public static Player myPlayer = new Player ();

		//create a random number which is used for setting the ball's horizontal speed
		public static Random random = new Random ();
		private static float randomXDirection = random.Next (-2, 3);
		public static Ball myBall = new Ball (randomXDirection);

		//define the default brick colours which are used if the JSON is invalid
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

		//attempt to load the colours from the JSON file
		public static void LoadColors ()
		{
			if (File.Exists ("colors.json")) {
				try {
					List<string> ColorsForBricks = new List<string> ();
					ColorsForBricks = JsonConvert.DeserializeObject<List<string>> (File.ReadAllText ("colors.json"));
					BrickColor = SwinGame.RGBColor(Convert.ToByte(ColorsForBricks[0]), Convert.ToByte(ColorsForBricks [1]), Convert.ToByte(ColorsForBricks[2]));
					BrickOutlineColor = SwinGame.RGBColor (Convert.ToByte(ColorsForBricks [3]), Convert.ToByte(ColorsForBricks [4]), Convert.ToByte(ColorsForBricks [5]));
				} catch { SetColors (); }
			} else {
				SetColors ();
			}
		}

		//if the colour values cannot be loaded from the JSON file, save the default colours to the JSON file
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

		//display a screen when creating a new set of bricks
		public static void DisplayResetBricksScreen ()
		{
			SwinGame.ClearScreen (Color.Black);
			SwinGame.DrawTextOnScreen ("Loading New Level....", Color.White, 300, 300);
			SwinGame.RefreshScreen ();
		}

		//draw everything to the field
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

		//move all moveable objects, and check any collisions
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

		//process input to move the paddle, and adjust the paddle location as required
		public static void ProcessInput ()
		{
			if (SwinGame.KeyDown(KeyCode.vk_RIGHT))
				myPlayer.MoveRight ();

			if (SwinGame.KeyDown (KeyCode.vk_LEFT))
				myPlayer.MoveLeft ();
		}

		//check each element on the field to ensure it has enough health to remain alive
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

		//generate the bricks
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



		//generate the walls
		public static void GenerateWalls ()
		{
			//top
			for (int i = 0; i <= GameMain.ScreenWidth - Wall.SideLength; i = i + Wall.SideLength) {
				_Walls.Add (new Wall (i, 0));
			}

			//left
			for (int i = Wall.SideLength; i <= GameMain.ScreenHeight - Wall.SideLength; i = i + Wall.SideLength) {
				_Walls.Add (new Wall (0, i));
			}

			//right
			for (int i = Wall.SideLength; i <= GameMain.ScreenHeight - Wall.SideLength; i = i + Wall.SideLength) {
				_Walls.Add (new Wall (GameMain.ScreenWidth - Wall.SideLength, i));
			}
		}

		//delete a brick from the brick list
		public static void DeleteBrick (Brick b)
		{
			List<Brick> NewBricks = new List<Brick> ();
			foreach (Brick brick in Bricks) {
				if (brick != b)
					NewBricks.Add (brick);
			}
			_Bricks = NewBricks;
		}

		//delete a wall from the wall list
		public static void DeleteWall (Wall w)
		{
			List<Wall> NewWalls = new List<Wall> ();
			foreach (Wall wall in Walls) {
				if (wall != w)
					NewWalls.Add (wall);
			}
			_Walls = NewWalls;

		}

		//check whether the ball has gone off of the screen, which means the game is over
		public static bool CheckGameOver() {
			return myBall.OffScreen;
		}

		//display a game over screen that displays the score
		public static void DisplayGameOver ()
		{
			SwinGame.ClearScreen (Color.Black);
			SwinGame.DrawTextOnScreen ("Game Over! Points scored: " + myPlayer.Points, Color.White, 300, 300);
			SwinGame.RefreshScreen ();
		}

		//generate a new powerup and add it to the active powerups list
		public static void CreatePowerUp (float x, float y)
		{
			//there is a 1 in 3 chance of each powerup type being created 
			int i = random.Next (1, 301);
			Console.WriteLine (i);
			if (i >= 1 && i <= 100)
				_ActivePowerUps.Add (new EnlargenPowerUp (x, y));
			else if (i >= 101 && i <= 200)
				_ActivePowerUps.Add (new SpeedUpPowerUp (x, y));
			else
				_ActivePowerUps.Add (new SlowBallPowerUp (x, y));
		}

		//delete a powerup from the active powerups list
		public static void DeletePowerUp (PowerUp powerUp)
		{
			List<PowerUp> NewPowerUps = new List<PowerUp> ();
			foreach (PowerUp p in _ActivePowerUps) {
				if (p != powerUp)
					NewPowerUps.Add (p);
			}
			_ActivePowerUps = NewPowerUps;
		}

		//delete all current bricks and create a new set within the list
		public static void ResetBricks ()
		{
			_Bricks.Clear ();
			GenerateBricks ();
			DrawField ();

		}

	}
}
