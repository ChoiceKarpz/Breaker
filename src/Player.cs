using System;
using SwinGameSDK;
namespace MyGame
{
	public class Player : MovableObject
	{
		private int _points;
		private int _width;
		private int _height;

		public Player (float x, float y, float xspeed, float yspeed, int width, int height) : base (x, y, xspeed, yspeed, Color.Red)
		{
			_width = width;
			_height = height;
		}

		public Player () : this (350, 580, 5, 0, 100, 10)
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

		public void AddToPoints (int i)
		{
			_points += i;
		}

		public override void Reset ()
		{
			throw new NotImplementedException ();
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
		}

		public void MoveRight ()
		{
			XLocation += XSpeed;
		}

		public void MoveLeft ()
		{
			XLocation -= XSpeed;
		}
	}
}
