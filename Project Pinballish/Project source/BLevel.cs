using System;

namespace GXPEngine
{
	public class BLevel : AnimSprite
	{
		MyGame _mygame;
		int _levelNum;
		private int _firstFrame = 0;
		private int _lastFrame = 3;
		private float _frame;
		private int Timer;

		public BLevel (MyGame myGame, int levelNum) : base ("CountDown.png",4,1)
		{
			Timer = 180;

			_levelNum = levelNum;
		
			_mygame =  myGame;

			_mygame.Add (this);

		}

		void Update ()
		{
			Timer--;

		
			if (Timer == 0) {
				//something something darkside
				//something something complete
				_mygame.SetState ("level" + _levelNum);
			}
			UpdateAnimation ();

		}

		void UpdateAnimation ()
		{
			_frame = _frame + 0.022f;

			if (_frame >= _lastFrame + 1)
				_frame = _firstFrame;
			if (_frame < _firstFrame)
				_frame = _firstFrame;
			this.SetFrame ((int)_frame);

		}
	}
}

