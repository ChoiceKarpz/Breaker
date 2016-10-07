using System;
using SwinGameSDK;
namespace MyGame
{
	public class EnlargenPowerUp : PowerUp
	{

		public EnlargenPowerUp (float x, float y) : base (x, y, 0, 3, Color.Yellow)
		{
		}

		public override void Activate ()
		{
			PlayingField.myPlayer.IncreaseWidth ();
		}

	}
}
