using System;
using SwinGameSDK;
namespace MyGame
{
	public class SpeedUpPowerUp : PowerUp
	{
		public SpeedUpPowerUp (float x, float y) : base (x, y, 0, 3, Color.Orange)
		{
		}

		//when this powerup is activated, increase the speed of the paddle
		public override void Activate ()
		{
			PlayingField.myPlayer.IncreaseSpeed ();
		}
	}
}
