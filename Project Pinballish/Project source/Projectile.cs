using System;
using System.Drawing;

namespace GXPEngine
{
	public class Projectile : Canvas
	{
		public readonly int radius;
		private Vec2 _position;
		private Vec2 _velocity;

		private MyGame _MG;
		private Level _level;

		private Color _ballColor;
		private int _timer;

		public Projectile (int pRadius, MyGame MG, Level level, Vec2 pPosition = null, Vec2 pVelocity = null, Color? pColor = null):base (pRadius*2, pRadius*2)
		{
			radius = pRadius;
			position = pPosition;
			velocity = pVelocity;
			_timer = 200;
			_MG = MG;
			_level = level;
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

		public bool Step () {
			_position.Add (_velocity);
			_timer--;
			x = (float)_position.x;
			y = (float)_position.y;
			Trail trail = new Trail (_level);
			trail.SetXY (this.x, this.y);
			_level.AddChild (trail);
			if (_timer == 0 || this.position.x < 0 || this.position.x > _MG.width || this.position.y < 0 || this.position.y > _MG.height) {
				_level.RemoveChild (this);
				_level.Projectiles.Remove (this);
				this.Destroy ();
				this.velocity = Vec2.zero;
				this.position = Vec2.zero;
				return true;
			}
			return false;
		}
	}
}

