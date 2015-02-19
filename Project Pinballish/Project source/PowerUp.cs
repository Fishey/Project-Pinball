using System;

namespace GXPEngine
{
	public class PowerUp : AnimSprite
	{
		PowerUpType _type;
		Vec2 _position, _velocity;
		int _timer;
		Ship _ship;

		public PowerUp (PowerUpType type, Vec2 position = null, Vec2 velocity = null, Ship ship = null) : base ("Images/SpriteSheet.png", 16, 16)
		{
			_ship = ship;
			_timer = 120;
			_type = type;
			_position = position;
			this.SetScaleXY (0.5, 0.5);
			if (velocity == null)
				velocity = new Vec2 (3, 0);
			velocity.SetAngleDegrees (Utils.Random (0.0f, 360.0f));
			_velocity = velocity;
			switch (type) {

			case PowerUpType.ENERGYUP:
				SetFrame (24);
				break;
			case PowerUpType.MULTIPLIER:
				SetFrame (25);
				break;
			case PowerUpType.SPEEDUP:
				SetFrame (26);
				break;
			case PowerUpType.SPEEDDOWN:
				SetFrame (27);				
				break;
			default:

				break;
			}

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

		public int PlayerNum
		{
			get { return this._ship.PlayerNum; }
		}

		public PowerUpType PowerUpType
		{
			get { return this._type; }
		}

		public int Timer{
			get { return this._timer; }
			set { this._timer = value; }
		}

		public void Step () {
			if (Timer > 0)
				Timer--;
			_position.Add (_velocity);
			x = (float)_position.x;
			y = (float)_position.y;
		}
	}
}

