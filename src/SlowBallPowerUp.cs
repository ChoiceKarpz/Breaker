﻿using System;
using SwinGameSDK;
namespace MyGame
{
	public class SlowBallPowerUp : PowerUp
	{
		public SlowBallPowerUp (float x, float y) : base (x, y, 0, 3, Color.Pink)
		{
		}

		//when this powerup is activated, decrease the vertical speed of the ball
		public override void Activate ()
		{
			PlayingField.myBall.DecreaseSpeed ();
		}
	}
}
