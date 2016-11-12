using System;
using SwinGameSDK;

//this object outlines all the basics that every object (paddle/player, brick, ball etc.) within the game have

namespace MyGame
{
	public abstract class GameObject
	{
		private float _xLocation;
		private float _yLocation;
		private Color _color;

		public GameObject (float x, float y, Color c)
		{
			_xLocation = x;
			_yLocation = y;
			_color = c;
		}

		//the draw method is abstract because each game object draws itself differently 
		public abstract void Draw ();

		public float XLocation {
			get {
				return _xLocation;
			}

			set {
				_xLocation = value;
			}
		}

		public float YLocation {
			get {
				return _yLocation;
			}

			set {
				_yLocation = value;
			}
		}

		public Color Color {
			get {
				return _color;
			}
			set {
				_color = value;
			}
		}
	}
}
