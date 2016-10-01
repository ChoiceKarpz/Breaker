using System;
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
			}
		}
	}
}

