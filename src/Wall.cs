using System;
using SwinGameSDK;

namespace MyGame
{
	public class Wall : StationaryObject
	{
		private const int _sideLength = 50;
		private const int _health = 10;

		public Wall (int x, int y) : base (x, y, _sideLength, _sideLength, _health, Color.Blue)
		{
		}

		public override void Draw ()
		{
			//Draw outline so that each is clearly defined
			SwinGame.DrawRectangle (Color.White, XLocation, YLocation, _sideLength, _sideLength);
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
