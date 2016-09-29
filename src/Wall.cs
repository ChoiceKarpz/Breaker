using System;
using SwinGameSDK;

namespace MyGame
{
	public class Brick : StationaryObject
	{
		private const int _sideLength = 50;
		private const int _health = 5;

		public Brick (int x, int y) : base (x, y, _sideLength, _sideLength, _health, Color.Blue)
		{
		}

		public override void Draw ()
		{
			SwinGame.FillRectangle (Color, XLocation, YLocation, _sideLength, _sideLength);
		}


	}
}
