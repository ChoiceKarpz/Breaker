using System;
using SwinGameSDK;
namespace MyGame
{
	public class EnlargenPowerUp : PowerUp
	{

		public EnlargenPowerUp (float x, float y) : base (x, y, 0, 3, Color.Yellow)
		{
		}

		//when this powerup is activated, increase the width of the paddle
		public override void Activate ()
		{
			PlayingField.myPlayer.IncreaseWidth ();
		}

	}
}
