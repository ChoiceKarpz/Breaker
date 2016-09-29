using System;
using SwinGameSDK;

namespace MyGame
{

	public class Ball : MovableObject
	{
		private int _radius;

		public Ball (float x, float y, float xspeed, float yspeed, int radius) : base (x, y, xspeed, yspeed, Color.White)
		{
			_radius = radius;
		}

		public Ball () : this (400, 300, 5, 5, 10)
		{
		}

		public override void Draw ()
		{
			SwinGame.FillCircle (Color, XLocation, YLocation, _radius);
		}

		//ball should check if it collides with any of the walls or paddles, and reflects appropriately 
		public override void CheckCollision ()
		{
			if ((XLocation + _radius >= GameMain.ScreenWidth) || (XLocation - _radius <= 0))
				XSpeed = Reflect (XSpeed);
			if ((YLocation + _radius >= GameMain.ScreenHeight) || (YLocation - _radius <= 0))
				YSpeed = Reflect (YSpeed);

			if (SwinGame.CircleRectCollision (SwinGame.CreateCircle (XLocation, YLocation, _radius), SwinGame.CreateRectangle (PlayingField.myPlayer.XLocation, PlayingField.myPlayer.YLocation, PlayingField.myPlayer.Width, PlayingField.myPlayer.Height)))
				YSpeed = Reflect (YSpeed);                                                                                                   	
			                                                                                                            
		
		}

		public override void Reset ()
		{
			XLocation = 400;
			YLocation = 300;
			XSpeed = 20;
			YSpeed = 20;
			Color = Color.White;

		}

		public float Reflect (float n)
		{
			return n * -1;
		}
	}
}
