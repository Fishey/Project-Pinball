using System;

namespace GXPEngine
{
	public class HudShell : AnimSprite
	{
		private int _firstFrame;
		private int _lastFrame;
		private float _frame; 

		public HudShell () : base ("Images/ShellSheet.png", 5, 1)
		{
			_firstFrame = 0;
			_lastFrame = 0;
		}

		public void Update()
		{
			UpdateAnimation ();
		}

		public void UpdateAnimation() // Continuously loop through the frames based on the maximum and
		{
			_frame = _frame + 0.1f;

			if (_frame >= _lastFrame + 1)
				_frame = _firstFrame;
			if (_frame < _firstFrame)
				_frame = _firstFrame;
			this.SetFrame ((int)_frame);

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

