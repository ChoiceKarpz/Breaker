using System;
using SwinGameSDK;
namespace MyGame
{
	public abstract class MovableObject : GameObject
	{
		private float _xSpeed;
		private float _ySpeed;

		public MovableObject (float x, float y, float xspeed, float yspeed, Color c) : base(x, y, c)
		{
			_xSpeed = xspeed;
			_ySpeed = yspeed;
		}

		public float XSpeed {
			get {
				return _xSpeed;
			}
			set {
				_xSpeed = value;
			}
		}

		public float YSpeed {
			get {
				return _ySpeed;
			}
			set {
				_ySpeed = value;
			}
		}

		public void Move ()
		{
			XLocation += XSpeed;
			YLocation += YSpeed;
		}

		public abstract void Reset ();

		public abstract void CheckCollision ();

	}
}
