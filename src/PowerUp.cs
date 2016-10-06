using System;
using SwinGameSDK;

namespace MyGame
{
	public abstract class PowerUp : MovableObject
	{
		public PowerUp (float x, float y, float xspeed, float yspeed, Color c) : base (x, y, xspeed, yspeed, c)
		{
		}

		public abstract void Activate ();
	}
}
