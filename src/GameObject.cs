using System;
using SwinGameSDK;

namespace MyGame
{
	public abstract class GameObject
	{
		private double _xLocation;
		private double _yLocation;
		private Color _color;

		public GameObject (double x, double y, Color c)
		{
			_xLocation = x;
			_yLocation = y;
			_color = c;
		}

		public abstract void Draw ();

		public double XLocation {
			get {
				return _xLocation;
			}
		}

		public double YLocation {
			get {
				return _yLocation;
			}
		}

		public Color Color {
			get {
				return _color;
			}
		}
	}
}
