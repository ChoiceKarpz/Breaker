﻿using System;
using SwinGameSDK;

//this class defines the basics of each type of powerup within the game

namespace MyGame
{
	public abstract class PowerUp : MovableObject
	{
		public const int _radius = 20;
		public PowerUp (float x, float y, float xspeed, float yspeed, Color c) : base (x, y, xspeed, yspeed, c)
		{
		}

		//the activate method is abstract because each type of powerup does something different upon activation
		public abstract void Activate ();

		public override void Draw ()
		{
			SwinGame.FillCircle (Color, XLocation, YLocation, _radius);
		}

		public override void CheckCollision ()
		{
			if (SwinGame.PointInRect (XLocation, YLocation + _radius, PlayingField.myPlayer.XLocation, PlayingField.myPlayer.YLocation, PlayingField.myPlayer.Width, PlayingField.myPlayer.Height)) {
				Activate ();
				PlayingField.DeletePowerUp (this);
			}
		}
	}
}	