using System;
using SwinGameSDK;
namespace MyGame
{
	public class SpeedUpPowerUp : PowerUp
	{
		public SpeedUpPowerUp (float x, float y) : base (x, y, 0, 3, Color.Orange)
		{
		}

		public override void Draw ()
		{
			throw new NotImplementedException ();
		}

		public override void Activate ()
		{
			throw new NotImplementedException ();
		}

		public override void CheckCollision ()
		{
			throw new NotImplementedException ();
		}
	}
}
