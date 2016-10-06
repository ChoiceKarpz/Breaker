using System;
using SwinGameSDK;

namespace MyGame
{

	public class Ball : MovableObject
	{
		private int _radius;
		private bool _offScreen = false;
		private const float _constXSpeed = 3;

		public Ball (float x, float y, float xspeed, float yspeed, int radius) : base (x, y, xspeed, yspeed, Color.White)
		{
			_radius = radius;
		}

		public Ball () : this (400, 300, 3, 5, 10)
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
			//check if ball is off screen
			if (XLocation + _radius > GameMain.ScreenWidth || XLocation - _radius < 0 || YLocation + _radius > GameMain.ScreenHeight || YLocation - _radius < 0)
				_offScreen = true;

			//check if the ball makes contact with any walls
			foreach (Wall w in PlayingField.Walls) {


				//check collision with left side of ball
				if (SwinGame.PointInRect (XLocation - _radius, YLocation, w.XLocation, w.YLocation, w.Width, w.Height)) 
					
				{
					XLocation = _radius + w.XLocation + w.Width;
					XSpeed = Reflect (XSpeed);
					w.DecreaseHealth ();
				}
				//check collision with right side of ball
				else if (SwinGame.PointInRect (XLocation + _radius, YLocation, w.XLocation, w.YLocation, w.Width, w.Height))
					
				{
					XLocation = w.XLocation - _radius;
					XSpeed = Reflect (XSpeed);
					w.DecreaseHealth ();
				}
				//check collision with top side of ball
				else if (SwinGame.PointInRect (XLocation, YLocation - _radius, w.XLocation, w.YLocation, w.Width, w.Height))
					
				{
					YLocation = w.YLocation + w.Height + _radius;
					YSpeed = Reflect (YSpeed);
					w.DecreaseHealth ();
				}
				//check collision with bottom side of ball
				else if (SwinGame.PointInRect (XLocation, YLocation + _radius, w.XLocation, w.YLocation, w.Width, w.Height))
					
				{
					YLocation = w.YLocation - _radius;
					YSpeed = Reflect (YSpeed);
					w.DecreaseHealth ();
				}
			}

			foreach (Brick b in PlayingField.Bricks) {
				if (SwinGame.PointInRect (XLocation - _radius, YLocation, b.XLocation, b.YLocation, b.Width, b.Height)) {
					XLocation = _radius + b.XLocation + b.Width;
					XSpeed = Reflect (XSpeed);
					b.DecreaseHealth ();
				}
				//check collision with right side of ball
				else if (SwinGame.PointInRect (XLocation + _radius, YLocation, b.XLocation, b.YLocation, b.Width, b.Height)) {
					XLocation = b.XLocation - _radius;
					XSpeed = Reflect (XSpeed);
					b.DecreaseHealth ();
				}
				//check collision with top side of ball
				else if (SwinGame.PointInRect (XLocation, YLocation - _radius, b.XLocation, b.YLocation, b.Width, b.Height)) {
					YLocation = b.YLocation + b.Height + _radius;
					YSpeed = Reflect (YSpeed);
					b.DecreaseHealth ();
				}
				//check collision with bottom side of ball
				else if (SwinGame.PointInRect (XLocation, YLocation + _radius, b.XLocation, b.YLocation, b.Width, b.Height)) {
					YLocation = b.YLocation - _radius;
					YSpeed = Reflect (YSpeed);
					b.DecreaseHealth ();
				}
			}


			//calculates if the ball makes contact with the paddle 
			if (SwinGame.PointInRect (XLocation, YLocation + _radius, PlayingField.myPlayer.XLocation, PlayingField.myPlayer.YLocation, PlayingField.myPlayer.Width, PlayingField.myPlayer.Height)) {
				YSpeed = Reflect (YSpeed);
				//XSpeed = _constXSpeed * ((PlayingField.myBall.XLocation - (PlayingField.myPlayer.XLocation + PlayingField.myPlayer.Width / 2)) * (1 / 100) + (float) 0.5);
				if (SwinGame.KeyDown (KeyCode.vk_RIGHT))
					XSpeed++;
				else if (SwinGame.KeyDown (KeyCode.vk_LEFT))
					XSpeed--;
				    
			}
	                                                                                                   	
			                                                                                                            
		
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
