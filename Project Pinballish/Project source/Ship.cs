using System;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	public class Ship : Sprite
	{

		public float speed = 0.0f;

		public static readonly Dictionary<ShipType, int[]> SHIP_DICT
		= new Dictionary<ShipType, int[]>
		{
			{ShipType.REDSHARK ,  		new int[2]{0,1}},
			{ShipType.BLUESHARK,  		new int[2]{2,3}},
			{ShipType.REDSHIP,  		new int[2]{4,5}},
			{ShipType.BLUESHIP, 	 	new int[2]{7,8}},

		};

		private Vec2 _position;
		private Vec2 _velocity;

		private MyGame _MG;
		private Level _level;

		private int _playNum;
		private int _laserTimer;
		private int _stunTimer;
		private int _energy;
		private float _speed;
		private int _multiplier;
		private int _firstFrame;
		private int _lastFrame;
		private float _frame; 

		public int _score;
		private int _shieldTimer;
		private bool _soundPlayed;

		private AnimSprite _graphic;
		private Shield _shield;
		private AnimSprite _laser;
		private ShipType _shipType;
		private List<PowerUp> _powerUps;
		private SoundChannel sound;

		public Ship (ShipType imagepath, int playNum, MyGame MG, Level level, int score = 0, Vec2 pPosition = null, Vec2 pVelocity = null) : base ("Images/Hitboxshark.png")
		{
			_shipType = imagepath;
			_energy = 10;
			_speed = 1;
			_powerUps = new List<PowerUp> ();
			_multiplier = 1;
			_firstFrame = SHIP_DICT [imagepath][0];
			_lastFrame = SHIP_DICT [imagepath] [1];
			_graphic = new AnimSprite ("Images/SpriteSheet.png", 16, 16);	
			_graphic.SetXY (-140, -80);
			_graphic.SetScaleXY (0.8, 0.8);
			this._graphic.SetFrame (_firstFrame);
			this.AddChild (_graphic);
			this._laser = new AnimSprite ("Images/SpriteSheet.png", 16, 16);

			_laser.MirrorX = true;
			position = pPosition;
			velocity = pVelocity;
			_playNum = playNum;
			_MG = MG;
			_level = level;

			if (PlayerNum == 1) {
				this._laser.SetFrame (11);
				_graphic.AddChild (_laser);
			} else if (PlayerNum == 2) {
				this._laser.SetFrame (10);
				_graphic.AddChild (_laser);
			}
			//this._laser.SetOrigin (_laser.width / 2, -100);
			//this._laser.SetXY (-_laser.width / 2+70, -180);
			this.SetOrigin (this.width, this.height / 2);

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
		public void addscore (int score)

		{
			_score += score;
		}

		public void Fire()

		{
			if (LaserTimer == 0 & StunTimer == 0 && Energy > 0) {
				_level.Hud.removeEnergy (this);
				_energy--;
				_laserTimer = 100;
				Projectile bullet = new Projectile (10, _MG, _level, this.PlayerNum);
				bullet.position = this.position.Clone ();
				bullet.velocity = new Vec2 (10, 0);
				bullet.velocity.SetAngleDegrees (this.rotation);
				bullet.rotation = this.rotation;
				_level.Projectiles.Add (bullet);
				_level.AddChild (bullet);
					if (PlayerNum == 1)
						SoundManager.PlaySound (SoundFile.PEW1);
					else if (PlayerNum == 2)
						SoundManager.PlaySound (SoundFile.PEW2);
			} else {
				if (!_soundPlayed || _soundPlayed & sound.Volume == 0) {
					sound = SoundManager.PlaySound (SoundFile.ENERGYLOW);
					_soundPlayed = true;
				}

			}
		}

		public void Shield()
		{
			if (ShieldTimer == 0 && Energy > 0)
			{
				_level.Hud.removeEnergy (this);
				_energy--;
				_shieldTimer = 200;
				_shield = new Shield ();
				AddChild (_shield);

				if (PlayerNum == 1)
					SoundManager.PlaySound (SoundFile.SHIELD1);
				else if (PlayerNum == 2)
					SoundManager.PlaySound (SoundFile.SHIELD2);
				 
			}
		}
		public void AddPowerUp(PowerUp powerUp)
		{
			powerUp.Timer = 120;
			switch (powerUp.PowerUpType) {
			case PowerUpType.ENERGYUP:
				if (Energy < 9) {
					this.Energy ++;
					_MG.Hud.addEnergy (this);
				} else if (Energy == 9) {
					Energy++;
					_MG.Hud.addEnergy (this);
				} else {
					//Console.WriteLine ("already full");
				}
				break;
			case PowerUpType.MULTIPLIER:
				this.Multiplier = 2;
				//_MG.Hud.addPowerup (this, powerUp);
				break;
			case PowerUpType.SPEEDDOWN:
				this.Speed = .5f;
				_MG.Hud.addPowerup (this, powerUp);
				break;
			case PowerUpType.SPEEDUP:
				this.Speed = 1.5f;
				_MG.Hud.addPowerup (this, powerUp);
				break;
			default:
				break;
			}

			_powerUps.Add (powerUp);
		}

		public void RemovePowerUp(PowerUp powerUp)
		{
			_powerUps.Remove (powerUp);

			switch (powerUp.PowerUpType) {
			case PowerUpType.ENERGYUP:
				break;
			case PowerUpType.MULTIPLIER:
				//this.Multiplier = 1;
				break;
			case PowerUpType.SPEEDDOWN:
				this.Speed = 1;
				break;
			case PowerUpType.SPEEDUP:
				this.Speed = 1;
				break;
			default:
				break;
			}


		}

		public void Flip(bool horizontal = false, bool vertical = false)
		{
			_graphic.Mirror (horizontal, vertical);
			_laser.Mirror (_laser.MirrorX, vertical);
		}

		public int PlayerNum {
			get { return this._playNum; }
		}

		public int LaserTimer {
			get {return this._laserTimer;}
			set { this._laserTimer = value; }
		}

		public int StunTimer {
			get { return this._stunTimer; }
			set { this._stunTimer = value; }
		}

		public int ShieldTimer {
			get { return this._shieldTimer; }
			set { this._shieldTimer = value; }

			}

		public int Energy {
			get { return this._energy;}
			set { this._energy = value; }
		}

		public float Speed {
			get { return this.speed*_speed; }
			set { this._speed = value; }

		}

		public int Multiplier {
			get { return this._multiplier; }
			set { this._multiplier = value; }
		}

		public ShipType Type{
			get { return this._shipType; }
		}

		public AnimSprite Laser{
			get { return this._laser; }
			set { this._laser = value; }
		}

		public int Score{
			get { return this._score; }
			set { this._score = value; }
		}

		public void UpdateAnimation() // Continuously loop through the frames based on the maximum and
		{
			_frame = _frame + 0.1f;

			if (_frame >= _lastFrame + 1)
				_frame = _firstFrame;
			if (_frame < _firstFrame)
				_frame = _firstFrame;
			_graphic.SetFrame ((int)_frame);

		}

		public void Idle()
		{
			if (this._shipType == ShipType.REDSHIP || this._shipType == ShipType.BLUESHIP)
				_graphic.SetFrame (_lastFrame + 1);
		}

		public void Step () {
			if (LaserTimer > 0)
				_laserTimer--;
			if (StunTimer > 0)
				_stunTimer--;
			if (ShieldTimer > 0)
				_shieldTimer--;
			if (_shieldTimer < 100 && this.HasChild(_shield))
				this.RemoveChild (_shield);
				
			for (int i = _powerUps.Count - 1; i >= 0; i--)
			{
				_powerUps [i].Step ();
				if (_powerUps [i].Timer == 0) {
					PowerUp powerup = new PowerUp (_powerUps [i].PowerUpType, null, null, this);
					_MG.Hud.removePowerup (this, powerup);
					this.RemovePowerUp (_powerUps [i]);
				}
			}

			_position.Add (_velocity);
				x = (float)_position.x;
				y = (float)_position.y;
		}
	}
}

