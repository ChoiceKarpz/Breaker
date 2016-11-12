using System;
using SwinGameSDK;

//this class outlines all of the basics that each stationary object within the game has in addition to those defined by the gameobject class

namespace MyGame
{
	public abstract class StationaryObject : GameObject
	{
		private int _width;
		private int _height;
		private int _health;
		private Color _outlineColor;

		public StationaryObject (float x, float y, int width, int height, int health, Color inside, Color outline) : base (x, y, inside)
		{
			_width = width;
			_height = height;
			_health = health;
			_outlineColor = outline;
		}

		public int Width {
			get {
				return _width;
			}
		}

		public int Height {
			get {
				return _height;
			}
		}

		public Color OutlineColor {
			get {
				return _outlineColor;
			}
		}

		public int Health {
			get {
				return _health;
			}
		}

		//the checkhealth method is abstract because the two stationary objects, walls and bricks, each delete themselves differently when their health is gone
		public abstract void CheckHealth ();

		public void DecreaseHealth ()
		{
			_health--;
			DecreaseBrightness ();
		}

		//this lowers the transperency of the object, making it look as though it is fading away 
		public void DecreaseBrightness ()
		{
			byte ColorTransperency = (byte)(SwinGame.TransparencyOf (Color) * (0.61));
			Color = SwinGame.RGBAColor (SwinGame.RedOf (Color), SwinGame.GreenOf (Color), SwinGame.BlueOf(Color), ColorTransperency);
		}
	}
}
