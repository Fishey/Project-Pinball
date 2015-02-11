using System;

namespace GXPEngine
{
	public class Trail : AnimSprite
	{
		private int _timer;
		private Level _level;

		public Trail (Level level) : base ("trail.png",1,1)
		{
			_timer = 100;
			_level = level;
			SetScaleXY (0.2, 0.2);
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

