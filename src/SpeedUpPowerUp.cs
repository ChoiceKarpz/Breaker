using System;
using SwinGameSDK;
namespace MyGame
{
	public class SpeedUpPowerUp : PowerUp
	{
		public SpeedUpPowerUp (float x, float y) : base (x, y, 0, 3, Color.Orange)
		{
		}

		public override void Activate ()
		{
			PlayingField.myPlayer.IncreaseSpeed ();
		}
	}
}
