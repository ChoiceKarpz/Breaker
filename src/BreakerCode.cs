using System;
using SwinGameSDK;

namespace MyGame
{

	public class Ball : MovableObject
	{
		private const int _radius = 10;
		private bool _offScreen = false;

		public Ball (float xspeed) : base (400, 300, xspeed, 5, Color.White)
		{
		}


		public bool OffScreen {
			get {
				return _offScreen;
			}
		}

		public override void Draw ()
		{
			SwinGame.FillCircle (Color, XLocation, YLocation, _radius);
		}

		//ball should check if it collides with any of the walls or paddles, and reflects appropriately 
		public override void CheckCollision ()
		{
			//Circle ball = new Circle ();
			//Rectangle rect = new Rectangle ();
			//Point2D ballLocation = new Point2D ();
			//ballLocation.X = XLocation;
			//ballLocation.Y = YLocation;
			//ball.Center = ballLocation;
			//ball.Radius = _radius;

			//check if ball is off screen
			if (XLocation + _radius > GameMain.ScreenWidth || XLocation - _radius < 0 || YLocation + _radius > GameMain.ScreenHeight || YLocation - _radius < 0)
				_offScreen = true;

			//check if the ball makes contact with any walls
			foreach (Wall w in PlayingField.Walls) {
				

				//check collision with left side of ball
				if (SwinGame.PointInRect (XLocation - _radius, YLocation, w.XLocation, w.YLocation, w.Width, w.Height)) 
					
				{
					XLocation = _radius + w.XLocation + w.Width + 1;
					XSpeed = Reflect (XSpeed);
					w.DecreaseHealth ();
				}
				//check collision with right side of ball
				else if (SwinGame.PointInRect (XLocation + _radius, YLocation, w.XLocation, w.YLocation, w.Width, w.Height))
					
				{
					XLocation = w.XLocation - _radius - 1;
					XSpeed = Reflect (XSpeed);
					w.DecreaseHealth ();
				}
				//check collision with top side of ball
				else if (SwinGame.PointInRect (XLocation, YLocation - _radius, w.XLocation, w.YLocation, w.Width, w.Height))
					
				{
					YLocation = w.YLocation + w.Height + _radius + 1;
					YSpeed = Reflect (YSpeed);
					w.DecreaseHealth ();
				}
				//check collision with bottom side of ball
				else if (SwinGame.PointInRect (XLocation, YLocation + _radius, w.XLocation, w.YLocation, w.Width, w.Height))
					
				{
					YLocation = w.YLocation - _radius - 1;
					YSpeed = Reflect (YSpeed);
					w.DecreaseHealth ();
				}
			}

			foreach (Brick b in PlayingField.Bricks) {
				//check collision with left side of ball
				if (SwinGame.PointInRect (XLocation - _radius, YLocation, b.XLocation, b.YLocation, b.Width, b.Height)) {
					XLocation = _radius + b.XLocation + b.Width + 1;
					XSpeed = Reflect (XSpeed);
					b.DecreaseHealth ();
				}
				//check collision with right side of ball
				else if (SwinGame.PointInRect (XLocation + _radius, YLocation, b.XLocation, b.YLocation, b.Width, b.Height)) {
					XLocation = b.XLocation - _radius - 1;
					XSpeed = Reflect (XSpeed);
					b.DecreaseHealth ();
				}
				//check collision with top side of ball
				else if (SwinGame.PointInRect (XLocation, YLocation - _radius, b.XLocation, b.YLocation, b.Width, b.Height)) {
					YLocation = b.YLocation + b.Height + _radius + 1;
					YSpeed = Reflect (YSpeed);
					b.DecreaseHealth ();
				}
				//check collision with bottom side of ball
				else if (SwinGame.PointInRect (XLocation, YLocation + _radius, b.XLocation, b.YLocation, b.Width, b.Height)) {
					YLocation = b.YLocation - _radius - 1;
					YSpeed = Reflect (YSpeed);
					b.DecreaseHealth ();
				}
			}


			//calculates if the ball makes contact with the paddle 
			if (SwinGame.PointInRect (XLocation, YLocation + _radius, PlayingField.myPlayer.XLocation, PlayingField.myPlayer.YLocation, PlayingField.myPlayer.Width, PlayingField.myPlayer.Height)) {
				YLocation = PlayingField.myPlayer.YLocation - _radius - 1;
				YSpeed = Reflect (YSpeed);
				//XSpeed = _constXSpeed * ((PlayingField.myBall.XLocation - (PlayingField.myPlayer.XLocation + PlayingField.myPlayer.Width / 2)) * (1 / 100) + (float) 0.5);
				if (SwinGame.KeyDown (KeyCode.vk_RIGHT))
					XSpeed++;
				else if (SwinGame.KeyDown (KeyCode.vk_LEFT))
					XSpeed--;
				    
			}

	                                                                                                   	
		
		}

		public void ResetLocation ()
		{
			XLocation = 400;
			YLocation = 300;
			XSpeed = 0;
		}

		public float Reflect (float n)
		{
			return n * -1;
		}

		public void IncreaseSpeed ()
		{
			if (YSpeed > 0)
				YSpeed += 3;
			else if (YSpeed < 0)
				YSpeed -= 3;
		}

		public void DecreaseSpeed ()
		{
			float oldSpeed = YSpeed;

			if (YSpeed > 0)
				YSpeed -= 1;
			else if (YSpeed < 0)
				YSpeed += 1;

			if ((int) YSpeed  == 0) {
				if (oldSpeed < 0)
					YSpeed = -1;
				if (oldSpeed > 0)
					YSpeed = 1;
				}


		}

	}
}
﻿using System;
using SwinGameSDK;
namespace MyGame
{
	public class Brick : StationaryObject
	{
		private const int _Brickwidth = 100;
		private const int _Brickheight = 20;
		private const int _health = 2;
		private int _points = 200;

		public Brick (float x, float y) : base (x, y, _Brickwidth, _Brickheight, _health, PlayingField.BrickColor, PlayingField.BrickOutlineColor)
		{
		}

		public static int BrickWidth {
			get {
				return _Brickwidth;
			}
		}

		public static int BrickHeight {
			get {
				return _Brickheight;
			}
		}

		public override void Draw ()
		{
			//draw a dark green outline
			SwinGame.FillRectangle (Color, XLocation, YLocation, Width, Height);
			SwinGame.DrawRectangle (OutlineColor, XLocation, YLocation, Width, Height);
		}

		public override void CheckHealth ()
		{
			if (Health <= 0) {
				PlayingField.DeleteBrick (this);
				PlayingField.myPlayer.AddToPoints (_points);


				int i = PlayingField.random.Next (1, 101);

				//25% chance of creating a powerup upon death 
				if (i >= 0 && i <= 25)
					PlayingField.CreatePowerUp (XLocation + (Width / 2), YLocation + Height);
			}
		}
	}
}

﻿using System;
using SwinGameSDK;
namespace MyGame
{
	public class EnlargenPowerUp : PowerUp
	{

		public EnlargenPowerUp (float x, float y) : base (x, y, 0, 3, Color.Yellow)
		{
		}

		public override void Activate ()
		{
			PlayingField.myPlayer.IncreaseWidth ();
		}

	}
}
using System;
using System.Threading;
using SwinGameSDK;
using Newtonsoft.Json;
namespace MyGame
{
    public class GameMain
    {
		public static readonly int ScreenWidth = 800;
		public static readonly int ScreenHeight = 600;
        public static void Main()
        {
			//Open the game window
			SwinGame.OpenGraphicsWindow ("Breaker", ScreenWidth, ScreenHeight);
			//attempt to load colours from a JSON file
			PlayingField.LoadColors ();
			//make game objects
			PlayingField.GenerateWalls ();
			PlayingField.GenerateBricks ();
			PlayingField.DrawField ();
			Thread.Sleep (5000);




            //Run the game loop
			while(!SwinGame.WindowCloseRequested() && !SwinGame.KeyTyped(KeyCode.vk_ESCAPE))
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();

				//Clear the screen and draw the framerate

				PlayingField.DrawField ();
				PlayingField.ProcessInput ();
				PlayingField.ProcessMovement ();
				PlayingField.CheckHealthOfField ();

				if (PlayingField.CheckGameOver () == true)
					break;

				if (PlayingField.NumberOfBricks <= 0) {
					PlayingField.ResetBricks ();
					PlayingField.myBall.ResetLocation ();
					PlayingField.myBall.IncreaseSpeed ();
					PlayingField.DisplayResetBricksScreen ();
					Thread.Sleep (3000);
					PlayingField.DrawField ();
					Thread.Sleep (2000);
				}
            }

			PlayingField.DisplayGameOver ();
			Thread.Sleep (5000);
        }
    }
}﻿using System;
using SwinGameSDK;

namespace MyGame
{
	public abstract class GameObject
	{
		private float _xLocation;
		private float _yLocation;
		private Color _color;

		public GameObject (float x, float y, Color c)
		{
			_xLocation = x;
			_yLocation = y;
			_color = c;
		}

		public abstract void Draw ();

		public float XLocation {
			get {
				return _xLocation;
			}

			set {
				_xLocation = value;
			}
		}

		public float YLocation {
			get {
				return _yLocation;
			}

			set {
				_yLocation = value;
			}
		}

		public Color Color {
			get {
				return _color;
			}
			set {
				_color = value;
			}
		}
	}
}
﻿using System;
using SwinGameSDK;
namespace MyGame
{
	public abstract class MovableObject : GameObject
	{
		private float _xSpeed;
		private float _ySpeed;

		public MovableObject (float x, float y, float xspeed, float yspeed, Color c) : base(x, y, c)
		{
			_xSpeed = xspeed;
			_ySpeed = yspeed;
		}

		public float XSpeed {
			get {
				return _xSpeed;
			}
			set {
				_xSpeed = value;
			}
		}

		public float YSpeed {
			get {
				return _ySpeed;
			}
			set {
				_ySpeed = value;
			}
		}

		public void Move ()
		{
			XLocation += XSpeed;
			YLocation += YSpeed;
		}

		public abstract void CheckCollision ();

	}
}
﻿using System;
using SwinGameSDK;
namespace MyGame
{
	public class Player : MovableObject
	{
		private int _points = 0;
		private int _width;
		private int _height;

		public Player (float x, float y, float xspeed, float yspeed, int width, int height) : base (x, y, xspeed, yspeed, Color.LimeGreen)
		{
			_width = width;
			_height = height;
		}

		//standard values for the game
		public Player () : this (350, 580, 5, 0, 150, 10)
		{
		}

		public int Width {
			get {
				return _width;
			}
		}

		public int Height {
			get {
				return _height;
			}
		}

		public int Points {
			get {
				return _points;
			}
		}

		public void AddToPoints (int i)
		{
			_points += i;
		}

		public override void Draw ()
		{
			SwinGame.FillRectangle (Color, XLocation, YLocation, Width, Height);
		}

		public override void CheckCollision ()
		{
			if (XLocation + Width > GameMain.ScreenWidth)
				XLocation = GameMain.ScreenWidth - Width;

			if (XLocation < 0)
				XLocation = 0;

			foreach (Wall w in PlayingField.Walls) {
				//check if right side of paddle hits wall
				if (SwinGame.PointInRect (XLocation + Width, YLocation, w.XLocation, w.YLocation, w.Width, w.Height))
					XLocation = w.XLocation - Width;
				//check if left side of paddle hits wall
				if (SwinGame.PointInRect (XLocation, YLocation, w.XLocation, w.YLocation, w.Width, w.Height))
					XLocation = w.XLocation + w.Width;
			}
		}

		public void MoveRight ()
		{
			XLocation += XSpeed;
		}

		public void MoveLeft ()
		{
			XLocation -= XSpeed;
		}

		public void IncreaseWidth ()
		{
			_width += 5;
		}

		public void IncreaseSpeed ()
		{
			XSpeed += 3;
		}
	}
}
﻿using System;
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
		private static float randomXDirection = myRandom.Next (0);
		public static Ball myBall = new Ball (randomXDirection);


		public static Color BrickColor = Color.Red;
		public static Color BrickOutlineColor = Color.DarkRed;

		public static Random random = new Random ();

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

		public static void DisplayResetBricksScreen ()
		{
			SwinGame.ClearScreen (Color.Black);
			SwinGame.DrawTextOnScreen ("Loading New Level....", Color.White, 300, 300);
			SwinGame.RefreshScreen ();
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
			int i = random.Next (1, 301);
			Console.WriteLine (i);
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
			DrawField ();

		}

	}
}
﻿using System;
using SwinGameSDK;

namespace MyGame
{
	public abstract class PowerUp : MovableObject
	{
		public const int _radius = 20;
		public PowerUp (float x, float y, float xspeed, float yspeed, Color c) : base (x, y, xspeed, yspeed, c)
		{
		}

		public abstract void Activate ();

		public override void Draw ()
		{
			SwinGame.FillCircle (Color, XLocation, YLocation, _radius);
		}

		public override void CheckCollision ()
		{
			if (SwinGame.PointInRect (XLocation, YLocation + _radius, PlayingField.myPlayer.XLocation, PlayingField.myPlayer.YLocation, PlayingField.myPlayer.Width, PlayingField.myPlayer.Height)) {
				Activate ();
				PlayingField.DeletePowerUp (this);
			}
		}
	}
}	﻿using System;
using SwinGameSDK;
namespace MyGame
{
	public class SlowBallPowerUp : PowerUp
	{
		public SlowBallPowerUp (float x, float y) : base (x, y, 0, 3, Color.Pink)
		{
		}

		public override void Activate ()
		{
			PlayingField.myBall.DecreaseSpeed ();
		}
	}
}
﻿using System;
using SwinGameSDK;
namespace MyGame
{
	public class SpeedUpPowerUp : PowerUp
	{
		public SpeedUpPowerUp (float x, float y) : base (x, y, 0, 3, Color.Orange)
		{
		}

		public override void Activate ()
		{
			PlayingField.myPlayer.IncreaseSpeed ();
		}
	}
}
﻿using System;
using SwinGameSDK;
namespace MyGame
{
	public abstract class StationaryObject : GameObject
	{
		private int _width;
		private int _height;
		private int _health;
		private Color _outlineColor;

		public StationaryObject (float x, float y, int width, int height, int health, Color inside, Color outline) : base (x, y, inside)
		{
			_width = width;
			_height = height;
			_health = health;
			_outlineColor = outline;
		}

		public int Width {
			get {
				return _width;
			}
		}

		public int Height {
			get {
				return _height;
			}
		}

		public Color OutlineColor {
			get {
				return _outlineColor;
			}
		}

		public int Health {
			get {
				return _health;
			}
		}

		public abstract void CheckHealth ();

		public void DecreaseHealth ()
		{
			_health--;
			DecreaseBrightness ();
		}

		public void DecreaseBrightness ()
		{
			byte ColorTransperency = (byte)(SwinGame.TransparencyOf (Color) * (0.61));
			Color = SwinGame.RGBAColor (SwinGame.RedOf (Color), SwinGame.GreenOf (Color), SwinGame.BlueOf(Color), ColorTransperency);
		}
	}
}
﻿using System;
using SwinGameSDK;

namespace MyGame
{
	public class Wall : StationaryObject
	{
		private const int _sideLength = 50;
		private const int _health = 10;

		public Wall (int x, int y) : base (x, y, _sideLength, _sideLength, _health, Color.Blue, Color.White)
		{
		}

		public override void Draw ()
		{
			//Draw outline so that each is clearly defined
			SwinGame.DrawRectangle (OutlineColor, XLocation, YLocation, _sideLength, _sideLength);
			SwinGame.FillRectangle (Color, XLocation, YLocation, _sideLength - 1, _sideLength - 1);
		}

		public override void CheckHealth ()
		{
			if (Health <= 0) 
				PlayingField.DeleteWall (this);
				
		}

		public static int SideLength {
			get {
				return _sideLength;
			}
		}


	}
}
