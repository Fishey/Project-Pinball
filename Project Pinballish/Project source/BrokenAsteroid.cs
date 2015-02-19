using System;

namespace GXPEngine
{
	public class BrokenAsteroid : AnimSprite
	{
		private int _firstFrame;
		private int _lastFrame;
		private float _frame = 0.01f ;
		private Level _level;

		public BrokenAsteroid (int radius, Level level, Vec2 position) : base ("Images/Spritesheet.png", 16, 16)
		{
			this.SetOrigin (this.width / 2, this.height / 2);
			this.x = position.x;
			this.y = position.y;
			this.SetScaleXY (0.5*0.8, 0.5*0.8);
			_level = level;
			if (radius == 80) {
				_firstFrame = 32;
				_lastFrame = 34;
				this.SetXY (this.x+20, this.y+15);
			}
			else if (radius == 60) {
				_firstFrame = 36;
				_lastFrame = 38;
				this.SetXY (this.x+20, this.y+15);

			} else if (radius == 40) {
				_firstFrame = 39;
				_lastFrame = 39;
				this.SetXY (this.x+20, this.y+15);

			}
			SetFrame (_firstFrame);
		}

		public void Update()
		{
			UpdateAnimation ();
		}

		public void UpdateAnimation() // Continuously loop through the frames based on the maximum and
		{			
			if (_frame < _firstFrame)
			_frame = _firstFrame;

			_frame = _frame + 0.04f;
			if ((int)_frame == _lastFrame + 1) {
				this.Destroy ();
				_level.RemoveChild (this);
			}
			this.SetFrame ((int)_frame);
			this.alpha = this.alpha - (0.04f / (_lastFrame-_firstFrame+1));


		}

		public int FirstFrame
		{
			get { return this._firstFrame; }
			set { this._firstFrame = value; }
		}

		public int LastFrame
		{
			get { return this._lastFrame; }
			set { this._lastFrame = value; }

		}
	}
}

