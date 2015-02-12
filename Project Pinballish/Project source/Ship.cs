﻿using System;
using System.Drawing;

namespace GXPEngine
{
	public class Ship : Canvas
	{
		public readonly int radius;
		private Vec2 _position;
		private Vec2 _velocity;

		private MyGame _MG;
		private Level _level;

		private Color _ballColor;
		private int _playNum;
		private int _laserTimer;
		private int _stunTimer;

		public Ship (int pRadius, int playNum, MyGame MG, Level level, Vec2 pPosition = null, Vec2 pVelocity = null, Color? pColor = null):base (pRadius*2, pRadius*2)
		{
			radius = pRadius;
			position = pPosition;
			velocity = pVelocity;
			_ballColor = pColor ?? Color.Blue;
			_playNum = playNum;
			_MG = MG;
			_level = level;

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

		public void Fire()
		{
			if (LaserTimer == 0 & StunTimer == 0) {
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

