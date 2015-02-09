﻿using System;
using System.Drawing;

namespace GXPEngine
{
	public class Ship : Canvas
	{
		public readonly int radius;
		private Vec2 _position;
		private Vec2 _velocity;

		private Color _ballColor;
		private int _playNum;

		public Ship (int pRadius, int playNum, Vec2 pPosition = null, Vec2 pVelocity = null, Color? pColor = null):base (pRadius*2, pRadius*2)
		{
			radius = pRadius;
			position = pPosition;
			velocity = pVelocity;
			_ballColor = pColor ?? Color.Blue;
			_playNum = playNum;
			this.rotation = this.rotation + 180;

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

		public int PlayNum {
			get { return this._playNum; }
		}

		public void Step () {
			_position.Add (_velocity);
			x = (float)_position.x;
			y = (float)_position.y;
		}
	}
}

