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

		public Player () : this (350, 580, 10, 0, 100, 10)
		{
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
			SwinGame.FillRectangle (Color, XLocation, YLocation, _width, _height);
		}

		public override void CheckCollision ()
		{
			throw new NotImplementedException ();
		}
	}
}
