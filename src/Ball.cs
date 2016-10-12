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
