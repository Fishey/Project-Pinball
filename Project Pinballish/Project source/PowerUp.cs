using System;

namespace GXPEngine
{
	public class PowerUp : AnimSprite
	{
		PowerUpType _type;
		Vec2 _position, _velocity;
		Level _level;
		int _timer;

		public PowerUp (PowerUpType type, Level level, Vec2 position = null, Vec2 velocity = null) : base ("Images/EnergyUpSheet.png", 4, 1)
		{
			_timer = 100;
			_type = type;
			_position = position;
			if (velocity == null)
				velocity = new Vec2 (3, 0);
			velocity.SetAngleDegrees (Utils.Random (0.0f, 360.0f));
			_velocity = velocity;
			switch (type) {
			case PowerUpType.ENERGYUP:
				SetFrame (0);
				break;
			case PowerUpType.MULTIPLIER:
				SetFrame (1);
				break;
			case PowerUpType.SPEEDUP:
				SetFrame (2);
				break;
			case PowerUpType.SPEEDDOWN:
				SetFrame (3);
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

