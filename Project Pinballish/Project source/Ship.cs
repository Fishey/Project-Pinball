﻿using System;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	public class Ship : Sprite
	{

		public static readonly Dictionary<ShipSprites, String> SHIP_DICT
		= new Dictionary<ShipSprites, String>
		{
			{ShipSprites.REDSHARK ,  		"Images/RedShark.png"},
			{ShipSprites.BLUESHARK,  		"Images/BlueShark.png"},
			{ShipSprites.REDSHIP,  			"Images/RedShip.png"},
			{ShipSprites.BLUESHIP, 	 		"Images/BlueShip.png"},

		};

		private Vec2 _position;
		private Vec2 _velocity;

		private MyGame _MG;
		private Level _level;

		private int _playNum;
		private int _laserTimer;
		private int _stunTimer;

		private Sprite _graphic;

		public Ship (ShipSprites imagepath, int playNum, MyGame MG, Level level, Vec2 pPosition = null, Vec2 pVelocity = null) : base ("Hitboxshark.png")
		{
			_graphic = new Sprite (SHIP_DICT [imagepath]);
			_graphic.SetScaleXY (0.1, 0.1);
			_graphic.SetXY (-165, -47);
			this.AddChild (_graphic);

			position = pPosition;
			velocity = pVelocity;
			_playNum = playNum;
			_MG = MG;
			_level = level;
			this.SetOrigin (this.width, this.height / 2);
			if (PlayerNum == 2)
				this.Mirror (true, false);
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

		public void Fire()
		{
			if (LaserTimer == 0 & StunTimer == 0) {
				_laserTimer = 100;
				Projectile bullet = new Projectile (10, _MG, _level, this.PlayerNum);
				bullet.position = this.position.Clone ();
				bullet.velocity = new Vec2 (5, 0);
				bullet.velocity.SetAngleDegrees (this.rotation);
				bullet.rotation = this.rotation;
				_level.Projectiles.Add (bullet);
				_level.AddChild (bullet);
				if (PlayerNum == 1)
					SoundManager.PlaySound (SoundFile.PEW1);
				else if (PlayerNum == 2)
					SoundManager.PlaySound (SoundFile.PEW2);
			}
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

		public void Step () {
			if (LaserTimer > 0)
				_laserTimer--;
			if (StunTimer > 0)
				_stunTimer--;
			_position.Add (_velocity);
			x = (float)_position.x;
			y = (float)_position.y;
		}
	}
}

