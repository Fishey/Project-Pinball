using System;
using System.Drawing;

namespace GXPEngine
{
	public class Projectile : Canvas
	{
		public readonly int radius;
		private Vec2 _position;
		private Vec2 _velocity;

		private Color _ballColor;
		private int _timer;

		public Projectile (int pRadius, Vec2 pPosition = null, Vec2 pVelocity = null, Color? pColor = null):base (pRadius*2, pRadius*2)
		{
			radius = pRadius;
			position = pPosition;
			velocity = pVelocity;
			_timer = 200;
			_ballColor = pColor ?? Color.SaddleBrown;

			draw ();
			x = (float)position.x;
			y = (float)position.y;
		}
			
		private void draw() {
			SetOrigin (radius, radius);

			graphics.FillPolygon (
				new SolidBrush (_ballColor),
				new PointF[] {
					new PointF (2*radius, radius),
					new PointF (0, 2*radius),
					new PointF (0, 0)
				}
			);
		}

		public Vec2 position {
			set {
				_position = value ?? Vec2.zero;
			}
			get {
				return _position;
			}
		}

		public Vec2 velocity {
			set {
				_velocity = value ?? Vec2.zero;
			}
			get {
				return _velocity;
			}
		}

		public void Step () {
			_position.Add (_velocity);
			_timer--;
			x = (float)_position.x;
			y = (float)_position.y;
			if (_timer == 0)
				this.Destroy ();
		}
	}
}

