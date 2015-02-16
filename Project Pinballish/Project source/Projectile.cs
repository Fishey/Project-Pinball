using System;
using System.Drawing;

namespace GXPEngine
{
	public class Projectile : Sprite
	{
		public readonly int radius;
		private Vec2 _position;
		private Vec2 _velocity;

		private MyGame _MG;
		private Level _level;

		private int _playerNum;
		private int _catchTimer;
		private int _hitTimer;
		private AnimSprite graphic;

		public Projectile (int pRadius, MyGame MG, Level level, int playerNum, Vec2 pPosition = null, Vec2 pVelocity = null, Color? pColor = null):base ("Images/LaserHitbox.png")
		{
			_catchTimer = 50;
			this.SetOrigin (this.width / 2, this.height / 2+20);
			if (playerNum == 1)
				graphic = new AnimSprite ("Images/Blue Laser.png", 2, 1);
			else if (playerNum == 2)
				graphic = new AnimSprite ("Images/Red Laser.png", 1, 1);
			this.AddChild (graphic);
			graphic.SetXY (-this.width/2-20, -this.height/2-18);
			graphic.SetScaleXY (0.5, 0.5);
			this.SetScaleXY (0.5, 0.5);
			radius = pRadius;
			position = pPosition;
			velocity = pVelocity;
			_MG = MG;
			_level = level;
			_playerNum = playerNum;

			x = (float)position.x;
			y = (float)position.y;

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

		public int PlayerNum {
			get { return this._playerNum; }
		}

		public int CatchTimer {
			get { return this._catchTimer; }
			set { this._catchTimer = value; }
		}

		public int HitTimer {
			get { return this._hitTimer; }
			set { this._hitTimer = value; }
		}

		public bool Step () {
			_position.Add (_velocity);
			x = (float)_position.x;
			y = (float)_position.y;
			Trail trail = new Trail (_level, PlayerNum);
			trail.SetXY (this.x, this.y);
			trail.rotation = this.rotation;
			_level.AddChild (trail);
			if (_catchTimer > 0)
				_catchTimer--;
			if (_hitTimer > 0)
				_hitTimer--;
			if ( this.position.x < 0 || this.position.x > _MG.width || this.position.y < 0 || this.position.y > _MG.height) {
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

