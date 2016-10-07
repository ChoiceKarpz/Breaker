using System;
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
