using System;

namespace GXPEngine
{
	public class Trail : AnimSprite
	{
		private int _timer;
		private Level _level;

		private AnimSprite graphic;

		public Trail (Level level, int player) : base ("Images/LaserHitbox.png",1,1)
		{
			if (player == 1)
				graphic = new AnimSprite ("Images/Blue Laser.png", 2, 1);
			else if (player == 2)
				graphic = new AnimSprite ("Images/Red Laser.png", 1, 1);
			graphic.SetFrame (0);
			this.AddChild (graphic);
			_timer = 50;
			_level = level;
			graphic.SetScaleXY (0.25, 0.25);
			graphic.SetXY (0, -20);
			SetOrigin (this.width / 2, 38);
		}

		public void Update()
		{
			if (_timer > 0) {
				this.graphic.alpha = (float)_timer / 50;
				_timer--;
			} else {
				_level.RemoveChild (this);
				this.Destroy ();
			}
		}
	}
}

