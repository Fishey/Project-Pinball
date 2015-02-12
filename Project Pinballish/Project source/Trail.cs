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
				graphic = new AnimSprite ("Images/BlueLaser.png", 2, 1);
			else if (player == 2)
				graphic = new AnimSprite ("Images/RedLaser.png", 2, 1);
			graphic.SetFrame (1);
			this.AddChild (graphic);
			_timer = 100;
			_level = level;
			graphic.SetScaleXY (0.5, 0.5);
			graphic.SetXY (-25, -20);
			graphic.rotation += 90;
			SetOrigin (this.width / 2, 38);
		}

		public void Update()
		{
			if (_timer > 0) {
				this.alpha = (float)_timer / 100;
				_timer--;
			} else {
				_level.RemoveChild (this);
				this.Destroy ();
			}
		}
	}
}

